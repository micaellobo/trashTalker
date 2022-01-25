using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashTalker.Dto.Alert;
using TrashTalker.Models;

namespace TrashTalker.Database.Repositories.AlertRepository
{
    /// <summary>
    /// This interface represents a Alert Repository that contains all the necessary methods for alert management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public interface IAlertRepository
    {
        /// <summary>
        /// Returns all alerts in the system
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Alert"/></returns>
        Task<IEnumerable<Alert>> getAlerts();

        /// <summary>
        /// Return a alert by its id
        /// </summary>
        /// <param name="id">id of the alert</param>
        /// <returns>The chosen <see cref="Alert"/></returns>
        Task<Alert> getAlert(Guid id);
        
        //Task<IEnumerable<Alert>> getAlertsByUser();
        
        /// <summary>
        /// Stores a alert in the system waiting for being solved
        /// </summary>
        /// <param name="createAlertDto">Alert to be stored in the DB</param>
        /// <param name="id">id of the responsible user</param>
        /// <returns>The stored <see cref="Alert"/></returns>
        Task<Alert> addAlert(CreateAlertDTO createAlertDto, Guid id);

        /// <summary>
        /// This method changes the status of an Alert to SOLVED
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Alert> resolveAlert(Guid id);
    }
}