using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;
using TrashTalker.Services;

namespace TrashTalker.Database.Repositories.RecycleBinRepository
{
    /// <summary>
    /// This Class represents a RecycleBin Repository that contains all the necessary methods for RecycleBin management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public class RecycleBinRepository : IRecycleBinRepository
    {
        /// <summary>
        /// A DbContext instance that represents a session with the database that can be used to query and save
        /// instances of your entities.
        /// </summary>
        private readonly DatabaseContext _dbContext;

        private readonly IQrCodeService _qrCodeService;

        /// <summary>
        /// Constructor method for a RecycleBin Repository
        /// </summary>
        /// <param name="databaseContext"> Instance of the database</param>
        /// <param name="qrCodeService"></param>
        public RecycleBinRepository(DatabaseContext databaseContext, IQrCodeService qrCodeService)
        {
            _dbContext = databaseContext;
            _qrCodeService = qrCodeService;
        }

        /// <inheritdoc/>
        public async Task<RecycleBin> addRecycleBin(RecycleBin bin)
        {
            var binToAdd = await _dbContext.RecycleBins.AddAsync(bin);
            
            await addContainer(TypeOfWaste.PAPER, bin);
            await addContainer(TypeOfWaste.GLASS, bin);
            await addContainer(TypeOfWaste.PLASTIC, bin);
            await addContainer(TypeOfWaste.UNDIFFERENTIATED, bin);

            await _dbContext.SaveChangesAsync();
            
            return binToAdd.Entity;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RecycleBin>> getAllRecycleBin()
        {
            var allRecycleBin = await _dbContext.RecycleBins.ToListAsync();
            return allRecycleBin;
        }

        /// <inheritdoc/>
        public async Task<RecycleBin> getRecycleBinById(Guid id)
        {
            var targetRecycleBin = await _dbContext.RecycleBins.FirstOrDefaultAsync(bin => bin.id == id);

            return targetRecycleBin ?? null;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RecycleBin>> getActiveRecycleBin()
        {
            var activeRecycleBins = await _dbContext.RecycleBins.Where(bin => bin.status == Status.ACTIVE).ToListAsync();
            return activeRecycleBins;
        }

        /// <inheritdoc/>
        public async Task<RecycleBin> disableRecycleBin(Guid id)
        {
            var targetRecycleBin = await getRecycleBinById(id);
            if (targetRecycleBin == null)
                return null;

            targetRecycleBin.status = Status.INACTIVE;
            await _dbContext.SaveChangesAsync();
            return targetRecycleBin;
        }

        /// <inheritdoc/>
        public async Task<RecycleBin> updateRecycleBin(RecycleBin bin)
        {
            var targetRecycleBin = await _dbContext.RecycleBins.FirstOrDefaultAsync(b => b.id == bin.id);
            if (targetRecycleBin == null)
                return null;

            targetRecycleBin.city = bin.city;
            targetRecycleBin.zipCode = bin.zipCode;
            targetRecycleBin.country = bin.country;
            targetRecycleBin.longit = bin.longit;
            targetRecycleBin.latit = bin.latit;
            targetRecycleBin.street = bin.street;

            await _dbContext.SaveChangesAsync();

            return targetRecycleBin;
        }

        private async Task addContainer(TypeOfWaste typeOfWaste, RecycleBin recycle)
        {
            var container = new Container
            {
                RecBin = recycle,
                id = Guid.NewGuid(),
                depth = Config.DEPTH,
                height = Config.HEIGHT,
                width = Config.WIDTH,
                currentPercOccupied = 0,
                avgGrowthOccupiedVolumePerDay = 0,
                typeOfWaste = typeOfWaste
            };
            await _dbContext.Containers.AddAsync(container);
            _qrCodeService.generateQRCode(container.id);
        }
    }
}