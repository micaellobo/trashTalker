using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashTalker.Models;

namespace TrashTalker.Database.Repositories.PickingRepository
{
    /// <summary>
    /// This interface represents a Picking Repository that contains all the necessary methods for pickings management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public interface IPickingRepository
    {
        /// <summary>
        /// Adds a picking to the database.
        /// </summary>
        /// <param name="picking">Picking to be added</param>
        /// <param name="idContainer">Container that was collected</param>
        /// <returns>Added <see cref="Picking" /></returns>
        Task<Picking> addPicking(Picking picking, Guid idContainer);
        
        /// <summary>
        /// Gets all the existing Pickings.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}" /> of <see cref="Picking" /> with all the existing pickings</returns>
        Task<IEnumerable<Picking>> getAllPickings();
        
        /// <summary>
        /// Get a specific picking by its id.
        /// </summary>
        /// <param name="id">Id of the chosen picking</param>
        /// <returns>The chosen <see cref="Picking" /></returns>
        Task<Picking> getPickingId(Guid id);
    }
}
