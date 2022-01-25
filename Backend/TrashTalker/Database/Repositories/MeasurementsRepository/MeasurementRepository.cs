using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashTalker.Dto.Measurement;
using TrashTalker.Models;
using TrashTalker.Services;

namespace TrashTalker.Database.Repositories.MeasurementsRepository
{
    /// <summary>
    /// This Class represents a Measurement Repository that contains all the necessary methods for Measurement management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public class MeasurementRepository : IMeasurementRepository
    {
        /// <summary>
        /// A DbContext instance that represents a session with the database that can be used to query and save
        /// instances of your entities.
        /// </summary>
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Measurement services needed in this class.
        /// </summary>
        private readonly IMeasuramentsService _measuramentsService;

        /// <summary>
        /// Constructor method for a Measurement Repository
        /// </summary>
        /// <param name="databaseContext">Instance of the database</param>
        /// <param name="measuramentsService">Measurement services</param>
        public MeasurementRepository(DatabaseContext databaseContext, IMeasuramentsService measuramentsService)
        {
            _dbContext = databaseContext;
            _measuramentsService = measuramentsService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Measurement>> getMeasurements()
        {
            return await _dbContext.Measurements.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task addMeasurement()
        {
            // Gets of measurements from the sensors
            var measuramentResponse = await _measuramentsService.getDistancesFromArduinoCloud();

            if (measuramentResponse is null)
                return;

            // Iterates all the measurements
            foreach (var widget in measuramentResponse.widgets)
            {
                Guid.TryParse(widget.name, out var containerId);
                var date = widget.variables[0].last_value_updated_at;
                var distance = widget.variables[0].last_value;

                //Finds for the Container associated to the sensor
                var containerDb = await _dbContext.Containers.FirstOrDefaultAsync(db => db.id == containerId);

                //If the Container dont exists ignores the measurement 
                if (containerDb is null)
                    continue;

                // Verify if this measurement already exists in the database
                var measurementDb =
                    await _dbContext.Measurements.FirstOrDefaultAsync(mas => mas.container == containerDb && mas.date == date);

                // If the measurement doesn't exists its added into the database, otherwise is ignored
                if (measurementDb is null)
                    await _dbContext.Measurements.AddAsync(new Measurement
                    {
                        id = Guid.NewGuid(),
                        container = containerDb,
                        date = date,
                        distance = distance
                    });
            }

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();
        }
    }
}