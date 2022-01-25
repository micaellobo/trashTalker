using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashTalker.Models;

namespace TrashTalker.Database.Repositories.MeasurementsRepository
{
    /// <summary>
    /// This interface represents a Measurement Repository that contains all the necessary methods for measurement management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public interface IMeasurementRepository
    {
        /// <summary>
        /// This method returns all the existing measurements
        /// </summary>
        /// <returns><see cref="IEnumerable{T}" /> of <see cref="Measurement" /> with all existing measurements</returns>
        Task<IEnumerable<Measurement>> getMeasurements();

        /// <summary>
        /// This method adds the last measurement values of the sensor that doesn't exists in the DB into the DB. 
        /// </summary>
        /// <returns></returns>
        Task addMeasurement();
    }
}
