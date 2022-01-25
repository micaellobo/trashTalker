using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashTalker.Models;

namespace TrashTalker.Database.Repositories.RecycleBinRepository
{
    /// <summary>
    /// This interface represents a RecycleBin Repository that contains all the necessary methods for RecycleBin management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public interface IRecycleBinRepository
    {
        /// <summary>
        /// Adds a RecycleBin to the database.
        /// </summary>
        /// <param name="bin">The RecycleBin to be added</param>
        /// <returns>The added <see cref="RecycleBin" /></returns>
        Task<RecycleBin> addRecycleBin(RecycleBin bin);
        
        /// <summary>
        /// Returns all the existing RecycleBins
        /// </summary>
        /// <returns><see cref="IEnumerable{T}" /> of <see cref="RecycleBin" /> with all existing RecycleBins </returns>
        Task<IEnumerable<RecycleBin>> getAllRecycleBin();
        
        /// <summary>
        /// Returns a specific RecycleBin by its id.
        /// </summary>
        /// <param name="id">Id of the RecycleBin</param>
        /// <returns>The chosen <see cref="RecycleBin" /></returns>
        Task<RecycleBin> getRecycleBinById(Guid id);
        
        /// <summary>
        /// Returns all the active RecycleBins (STATUS == "ACTIVE").
        /// </summary>
        /// <returns><see cref="IEnumerable{T}" /> of <see cref="RecycleBin" /> with all active RecycleBins</returns>
        Task<IEnumerable<RecycleBin>> getActiveRecycleBin();
        
        /// <summary>
        /// Disables a RecycleBin by its id.
        /// </summary>
        /// <param name="id">Id of the RecycleBin to be disabled</param>
        /// <returns>The disabled <see cref="RecycleBin" /></returns>
        Task<RecycleBin> disableRecycleBin(Guid id);
        
        /// <summary>
        /// This method updates the data of a RecycleBin in the database.
        /// </summary>
        /// <param name="bin">New RecycleBin data</param>
        /// <returns>The updated <see cref="RecycleBin" /></returns>
        Task<RecycleBin> updateRecycleBin(RecycleBin bin);

    }
}
