using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Database.Repositories.ContainerRepository
{
    /// <summary>
    /// This Class represents a Container Repository that contains all the necessary methods for container management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public class ContainerRepository : IContainerRepository
    {
        /// <summary>
        /// A DbContext instance that represents a session with the database that can be used to query and save
        /// instances of your entities.
        /// </summary>
        private readonly DatabaseContext _dbContext;
        
        /// <summary>
        /// Constructor method for a Container Repository
        /// </summary>
        /// <param name="databaseContext"> Instance of the database</param>
        public ContainerRepository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        
        /// <inheritdoc/>
        public async Task<Container> addContainer(Container container, Guid idRecBin)
        {
            var targetRecBin = await _dbContext.RecycleBins.FirstOrDefaultAsync(bin => bin.id == idRecBin);
            if (targetRecBin == null)
            {
                throw new KeyNotFoundException($"The RecycleBin with the id \"{idRecBin}\" does not exist");
            }

            container.RecBin = targetRecBin;
            var containerToAdd = await _dbContext.Containers.AddAsync(container);
            await _dbContext.SaveChangesAsync();

            return containerToAdd.Entity;
        }

        /// <inheritdoc/>
        public async Task<Container> disableContainer(Guid id)
        {
            var result = await _dbContext.Containers.FirstOrDefaultAsync(fun => fun.id == id);

            if (result != null)
            {
                result.status = Status.INACTIVE;
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<Container> GetContainer(Guid id)
        {
            
            return await _dbContext.Containers.FirstOrDefaultAsync(container => container.id == id);
            
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Container>> GetContainers()
        {
            
            return await _dbContext.Containers.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Container> UpdateContainer(Container container)
        {
            var result = await _dbContext.Containers.FirstOrDefaultAsync(fun => fun.id == container.id);

            if (container != null)
            {
                result.height = container.height;
                result.width = container.width;
                result.depth = container.depth;
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
        
        public Task<List<Container>> GetContainersOnAlert()
        {
            var containersAsync = _dbContext.Containers.Where(container => container.currentPercOccupied / 100 >= Config.MAX_ALERT_CAPACITY).ToList();

            return Task.FromResult(containersAsync);
        }
    }
}