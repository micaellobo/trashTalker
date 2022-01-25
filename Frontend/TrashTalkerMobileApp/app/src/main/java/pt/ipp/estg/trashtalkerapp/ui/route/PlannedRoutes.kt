package pt.ipp.estg.trashtalkerapp.ui.route

import android.annotation.SuppressLint
import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import pt.ipp.estg.trashtalkerapp.R
import pt.ipp.estg.trashtalkerapp.retrofitService.TrashtalkerAPI
import pt.ipp.estg.trashtalkerapp.room.RepositoryRoute
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route
import pt.ipp.estg.trashtalkerapp.ui.home.HomeViewModel
import pt.ipp.estg.trashtalkerapp.ui.route.utils.routeAdapter
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.util.concurrent.Executors

class PlannedRoutes : Fragment() , Communication {
    private lateinit var listPlannedRoute:List<Route>
    private lateinit var routeViewModel: RouteViewModel
    private lateinit var homeViewModel: HomeViewModel
    private lateinit var myContext: Context
    private var isRouteActive = false

    override fun onAttach(context: Context) {
        super.onAttach(context)
        myContext = context
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        routeViewModel = ViewModelProvider(requireActivity()).get(RouteViewModel::class.java)
        homeViewModel = ViewModelProvider(requireActivity()).get(HomeViewModel::class.java)


    }

    @SuppressLint("WrongConstant")
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        // Inflate the layout for this fragment
        var view = inflater.inflate(R.layout.fragment_planned_routes, container, false)

        //RecyclerView configuration
        listPlannedRoute = ArrayList<Route>()
        var myAdapter = routeAdapter(listPlannedRoute, this)
        var recView = view.findViewById<RecyclerView>(R.id.recView)
        var estimatedDuration = view.findViewById<TextView>(R.id.estimatedDuration)
        var estimatedDistance = view.findViewById<TextView>(R.id.estimatedDistance)
        var routeName = view.findViewById<TextView>(R.id.routeNameText)
        var startRouteBt = view.findViewById<Button>(R.id.startRouteBt)


        val sh = myContext.getSharedPreferences(getString(R.string.sharedPreferences), Context.MODE_APPEND)
        val token:String? = sh.getString("token","")
        val retrofitClient = token?.let { TrashtalkerAPI.getApi(it) }

        startRouteBt.setOnClickListener {
            val id:String? = routeViewModel.selectedRoute.value?.id
            if (id != null) {
                if(isRouteActive){
                    Toast.makeText(myContext,"Já tem uma rota a decorrer!",Toast.LENGTH_LONG).show()
                }
                else{
                    var serverResponse = ""

                    Executors.newFixedThreadPool(1).execute {
                        //Update local Room db
                        RepositoryRoute(requireActivity().application).updateRoute(id,"Ongoing")

                            //Update Api
                            val callback = retrofitClient?.startRoute(id)

                            callback?.enqueue(object: Callback<Unit> {
                                override fun onResponse(call: Call<Unit>, response: Response<Unit>) {
                                    if(response.body()!=null){
                                        serverResponse = "Rota Iniciada com Sucesso!"
                                    }
                                }

                                override fun onFailure(call: Call<Unit>, t: Throwable) {
                                    serverResponse = "Não foi possivel iniciar a rota!"
                                }
                            })
                    }
                    Toast.makeText(myContext,serverResponse, Toast.LENGTH_LONG).show()
                }
            }
        }

        recView.apply {
            adapter = myAdapter
            layoutManager = LinearLayoutManager(requireContext(), LinearLayoutManager.HORIZONTAL, false);
        }

        homeViewModel.getAllPlannedRoute()
            .observe(viewLifecycleOwner, Observer {
                myAdapter.updateList(it)
                if (it.size >= 1) {
                    routeViewModel.setRoute(it[0])
                } else {
                    startRouteBt.isEnabled = false
                }
            })

        homeViewModel.getCurrentOnGoingRoute().observe(viewLifecycleOwner,Observer{
            isRouteActive = it.size > 0
        })

        routeViewModel.getRoute().observe(viewLifecycleOwner, Observer {
            estimatedDistance.text = it.distanceEstimatedKm.toString() + " Km"
            estimatedDuration.text = it.estimatedDuration + " H"
            routeName.text = it.name
        })

        return view
    }

    override fun updateSelectedRoute(route: Route) {
        routeViewModel.setRoute(route)
    }

}

interface Communication {
    fun updateSelectedRoute(route: Route)
}