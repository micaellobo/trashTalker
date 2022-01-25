using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrashTalker.Dto.Alert;
using TrashTalker.Helpers;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Database.Repositories.AlertRepository
{
    /// <summary>
    /// This Class represents a Alert Repository that contains all the necessary methods for Alert management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public class AlertRepository : IAlertRepository
    {
        /// <summary>
        /// A DbContext instance that represents a session with the database that can be used to query and save
        /// instances of your entities.
        /// </summary>
        private readonly DatabaseContext _dbContext;
        
        /// <summary>
        /// Constructor method for a Alert Repository
        /// </summary>
        /// <param name="databaseContext"> Instance of the database</param>
        public AlertRepository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<Alert>> getAlerts()
        {
            return await _dbContext.Alerts.ToListAsync();
        }
        
        /// <inheritdoc/>
        public async Task<Alert> getAlert(Guid id)
        {
            return await _dbContext.Alerts.FirstOrDefaultAsync(alert => alert.id == id);
        }

        /// <inheritdoc/>
        public async Task<Alert> addAlert(CreateAlertDTO alertDto, Guid id)
        {
            var alert = alertDto.asAlert();
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.id == id);
            alert.employee = user;
            
            var alertToAdd = await _dbContext.Alerts.AddAsync(alert);
            
            await _dbContext.SaveChangesAsync();
            
            return alertToAdd.Entity;
        }
        
        
        /// <inheritdoc/>
        public async Task<Alert> resolveAlert(Guid id)
        {
            var alert = await _dbContext.Alerts.FirstOrDefaultAsync(alert => alert.id == id);

            if (alert is null) return null;
            alert.alertStatus = AlertStatus.SOLVED;
            await _dbContext.SaveChangesAsync();
            return alert;
        }
    }
}