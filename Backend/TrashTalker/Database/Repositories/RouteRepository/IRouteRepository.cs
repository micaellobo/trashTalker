using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrashTalker.Dto.Route;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Database.Repositories.RouteRepository
{
    /// <summary>
    /// This interface represents a Route Repository that contains all the necessary methods for Route management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public interface IRouteRepository
    {
        /// <summary>
        /// Returns all the existing routes.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}" /> of <see cref="Route" /> with all existing routes</returns>
        Task<IEnumerable<Route>> getRoutes();

        /// <summary>
        /// Returns all the existing routes for the current logged in Employee
        /// </summary>
        /// <returns><see cref="IEnumerable{T}" /> of <see cref="Route" /> with all existing routes</returns>
        Task<IEnumerable<Route>> getRoutesEmployeeLoggedIn(Guid employeeId);

        /// <summary>
        /// Returns a specific Route by its id associated to the logged in Employee.
        /// </summary>
        /// <param name="routeId"><see cref="Guid"/>of the Route </param>
        /// <param name="employeeId"><see cref="Guid"/> of the emploee</param>
        /// <returns></returns>
        Task<Route> getRouteByIdEmployeeLoggedIn(Guid routeId, Guid employeeId);

        /// <summary>
        /// Returns a specific Route by its id.
        /// </summary>
        /// <param name="id">Id of the Route</param>
        /// <returns>The chosen <see cref="Route" /></returns>
        Task<Route> getRoute(Guid id);

        /// <summary>
        /// Adds a route to the database.
        /// </summary>
        /// <param name="route">to be added</param>
        /// <param name="recycleBinsId">List with all ids of the recycle bins that belongs to the route</param>
        /// <returns>Added <see cref="Route"/></returns>
        Task<Route> addRoute(CreateRouteDto createRouteDto);

        /// <summary>
        /// Changes the state of the route to FINISH
        /// </summary>
        /// <returns>Finished <see cref="Route"/></returns>
        Task<Route> updateRoute(Route route);

        /// <summary>
        /// Confirmes the ROute creation
        /// </summary>
        /// <returns>Finished <see cref="Route"/></returns>
        Task<Route> confirmeRouteCreation(Route route);

        /// <summary>
        /// Deletes an <see cref="Route"/> with <see cref="StatusRoute"/> = <see cref="StatusRoute.PENDING"/>
        /// </summary>
        /// <param name="route">to be deleted</param>
        /// <returns>Finished <see cref="Route"/></returns>
        Task deletePendingRoute(Guid route);

        /// <summary>
        /// Gets the <see cref="Routes"/> with in a specific <see cref="StatusRoute"/> associated to an Employee
        /// </summary>
        /// <param name="status"> <see cref="StatusRoute"/> od the <see cref="Route"/></param>
        /// <param name="employeeId"><see cref="Guid"/> of the employee</param>
        /// <returns><see cref="IList{T}"/> of <see cref="Route"/></returns>
        Task<IList<Route>> getRoutesByStateEmployeeLoggedIn(StatusRoute status, Guid employeeId);

        /// <summary>
        /// Adds an <see cref="Route"/> that was created automatic
        /// </summary>
        /// <param name="createAutomaticRoute"><see cref="CreateAutomaticRoute"/> with the params need to create a Route</param>
        /// <returns><see cref="Route"/></returns>
        Task<Route> createPendingRoute(CreateAutomaticRoute createAutomaticRoute);
    }
}