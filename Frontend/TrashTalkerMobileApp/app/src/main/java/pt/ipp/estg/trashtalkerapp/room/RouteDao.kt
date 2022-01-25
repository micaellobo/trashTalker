package pt.ipp.estg.trashtalkerapp.room

import androidx.lifecycle.LiveData
import androidx.room.*
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route

@Dao
public interface RouteDao {
    @Query("SELECT * FROM Route")
    fun getAllRoutes():LiveData<List<Route>>

    @Query("SELECT * FROM Route WHERE Route.status= 'Planned'")
    fun getAllPlannedRoutes():LiveData<List<Route>>

    @Query("SELECT * FROM Route WHERE Route.status= 'Ongoing'")
    fun getOnGoingRoute():LiveData<List<Route>>

    @Query("SELECT * FROM Route WHERE Route.status= 'Finished'")
    fun getFinishedRoutes():LiveData<List<Route>>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun insertRoute(route: Route):Long

    @Query("UPDATE Route SET status = :stateOfRoute WHERE Route.id = :id")
    fun updateRoute(id:String,stateOfRoute:String)

    @Query("DELETE FROM Route")
    fun deleteAll()
}