using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashTalker.Models;

namespace TrashTalker.Database.Repositories.ContainerRepository
{
    /// <summary>
    /// This interface represents a Container Repository that contains all the necessary methods for container management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public interface IContainerRepository
    {
        /// <summary>
        /// This method gets all containers that exists in the database.
        /// </summary>
        /// <returns> <see cref="IEnumerable{T}" /> of <see cref="Container" /> with all existing containers </returns>
        Task<IEnumerable<Container>> GetContainers();

        Task<List<Container>> GetContainersOnAlert();

        /// <summary>
        /// This method gets a specific <see cref="Container"/> by its <see cref="Guid"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> of the desired container </param>
        /// <returns>A single <see cref="Container" /> </returns>
        Task<Container> GetContainer(Guid id);

        /// <summary>
        /// This method adds a <see cref="Container"/> to the database and associates it to a <see cref="RecycleBin"/>.
        /// </summary>
        /// <param name="container"><see cref="Container"/> object to be added</param>
        /// <param name="idRecBin">The <see cref="Guid"/> of the RecycleBin where the <see cref="Container"/> is placed</param>
        /// <returns>The added <see cref="Container" /></returns>
        Task<Container> addContainer(Container container, Guid idRecBin);

        /// <summary>
        /// This method updates the data of a <see cref="Container"/> in the database.
        /// </summary>
        /// <param name="container">New container data</param>
        /// <returns>The updated <see cref="Container" /></returns>
        Task<Container> UpdateContainer(Container container);

        /// <summary>
        /// This method disables a container.
        /// </summary>
        /// <param name="id"><see cref="Guid"/> of the container to be disabled</param>
        /// <returns>The disabled <see cref="Container" /></returns>
        Task<Container> disableContainer(Guid id);
    }
}