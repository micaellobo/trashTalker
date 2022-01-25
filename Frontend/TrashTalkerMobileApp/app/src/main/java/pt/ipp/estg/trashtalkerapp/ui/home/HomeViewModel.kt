package pt.ipp.estg.trashtalkerapp.ui.home

import android.app.Application
import androidx.lifecycle.AndroidViewModel
import androidx.lifecycle.LiveData
import pt.ipp.estg.trashtalkerapp.room.RepositoryRoute
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route

class HomeViewModel(application: Application) : AndroidViewModel(application) {
    val routeRepo:RepositoryRoute
    val allPlannedRoutes: LiveData<List<Route>>
    val allFinishedRoutes: LiveData<List<Route>>
    val onGoingRoute: LiveData<List<Route>>

    init{
        routeRepo = RepositoryRoute(application)
        allPlannedRoutes =routeRepo.getAllPlannedRoutes()
        allFinishedRoutes =routeRepo.getFinishedRoutes()
        onGoingRoute =routeRepo.getOnGoingRoute()
    }

    fun getAllPlannedRoute():LiveData<List<Route>> = allPlannedRoutes
    fun getAllFinishedRoute():LiveData<List<Route>> = allFinishedRoutes
    fun getCurrentOnGoingRoute():LiveData<List<Route>> = onGoingRoute
}