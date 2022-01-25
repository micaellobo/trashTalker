package pt.ipp.estg.trashtalkerapp.room

import android.app.Application
import androidx.lifecycle.LiveData
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route

class RepositoryRoute(val application:Application) {
    val routeDao:RouteDao

    init{
        routeDao = RouteDb.getInstance(application).routeDao()
    }

    fun getAllPlannedRoutes(): LiveData<List<Route>>{
        return routeDao.getAllPlannedRoutes();
    }

    fun getOnGoingRoute(): LiveData<List<Route>>{
        return routeDao.getOnGoingRoute();
    }

    fun getFinishedRoutes(): LiveData<List<Route>>{
        return routeDao.getFinishedRoutes();
    }

    fun insertRoute(route: Route):Long{
        return routeDao.insertRoute(route);
    }

    fun updateRoute(id:String,state:String){
        routeDao.updateRoute(id,state)
    }

    fun deleteAll(){
        routeDao.deleteAll()
    }
}