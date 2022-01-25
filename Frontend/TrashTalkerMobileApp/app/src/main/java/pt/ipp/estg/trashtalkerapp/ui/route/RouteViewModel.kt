package pt.ipp.estg.trashtalkerapp.ui.route

import android.app.Application
import androidx.lifecycle.AndroidViewModel
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route

class RouteViewModel(application: Application) : AndroidViewModel(application) {
    var selectedRoute: MutableLiveData<Route> = MutableLiveData()

    fun setRoute(route: Route) {
        this.selectedRoute.value = route
    }

    fun getRoute(): LiveData<Route> {
        return this.selectedRoute
    }
}