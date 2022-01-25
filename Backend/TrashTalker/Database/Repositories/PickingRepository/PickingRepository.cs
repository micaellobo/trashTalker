using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrashTalker.Models;

namespace TrashTalker.Database.Repositories.PickingRepository
{
    /// <summary>
    /// This Class represents a Picking Repository that contains all the necessary methods for pickings management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public class PickingRepository : IPickingRepository
    {
        /// <summary>
        /// A DbContext instance that represents a session with the database that can be used to query and save
        /// instances of your entities.
        /// </summary>
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Constructor method for a Picking Repository
        /// </summary>
        /// <param name="databaseContext"> Instance of the database</param>
        public PickingRepository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<Picking> addPicking(Picking picking, Guid idContainer)
        {
            var targetCont = await _dbContext.Containers.FirstOrDefaultAsync(cont => cont.id == idContainer);
            if(targetCont == null)
            {
                return null;
            }
            picking.container = targetCont;
            var pickingToAdd = await _dbContext.Pickings.AddAsync(picking);
            await _dbContext.SaveChangesAsync();

            return pickingToAdd.Entity;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Picking>> getAllPickings()
        {
            return await _dbContext.Pickings.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Picking> getPickingId(Guid id)
        {
            return await _dbContext.Pickings.FirstOrDefaultAsync(picking => picking.id == id);
        }
    }
}