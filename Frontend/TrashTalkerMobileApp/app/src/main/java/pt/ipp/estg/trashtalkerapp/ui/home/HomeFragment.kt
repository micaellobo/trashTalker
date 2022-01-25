package pt.ipp.estg.trashtalkerapp.ui.home

import android.annotation.SuppressLint
import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import pt.ipp.estg.trashtalkerapp.R
import pt.ipp.estg.trashtalkerapp.retrofitService.FinishRoute
import pt.ipp.estg.trashtalkerapp.retrofitService.TrashtalkerAPI
import pt.ipp.estg.trashtalkerapp.room.RepositoryRoute
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.text.SimpleDateFormat
import java.util.*
import java.util.concurrent.Executors

class HomeFragment : Fragment() {
    private lateinit var homeViewModel: HomeViewModel
    private lateinit var txtRouteName:TextView
    private lateinit var txtRouteDuration:TextView
    private lateinit var txtRouteDistance:TextView
    private lateinit var txtTitleRouteDuration:TextView
    private lateinit var txtTitleRouteDistance:TextView
    private lateinit var btnCloseRoute:Button
    private lateinit var myContext: Context

    override fun onAttach(context: Context) {
        super.onAttach(context)
        myContext = context
    }
    @SuppressLint("WrongConstant")
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_home, container, false)

        homeViewModel =
            ViewModelProvider(this).get(HomeViewModel::class.java)

        txtRouteName = view.findViewById(R.id.txtRouteName)
        txtRouteDuration = view.findViewById(R.id.txtDuration)
        txtRouteDistance = view.findViewById(R.id.txtDistance)
        txtTitleRouteDuration = view.findViewById(R.id.txtTitleDuration)
        txtTitleRouteDistance = view.findViewById(R.id.txtTitleDistance)
        btnCloseRoute = view.findViewById(R.id.btnCloseRoute)

        var txtUsername = view.findViewById<TextView>(R.id.txtUsername)
        var txtDateTime = view.findViewById<TextView>(R.id.txtDateTime)

        val df = SimpleDateFormat("EEE, d MMM yyyy")
        val date = df.format(Calendar.getInstance().time).toString()
        txtDateTime.setText(date)

        val sh = myContext.getSharedPreferences(getString(R.string.sharedPreferences), Context.MODE_APPEND)
        val username: String? = sh.getString("username","")
        val token:String? = sh.getString("token","")
        val retrofitClient = token?.let { TrashtalkerAPI.getApi(it) }

        txtUsername.setText(username)

        var routeOnGoing:Route? = null

        homeViewModel.getCurrentOnGoingRoute().observe(viewLifecycleOwner, Observer {
            if(it.isNotEmpty()){
                txtRouteName.setText(it[0].name)
                txtRouteDistance.setText((it[0].distanceEstimatedKm/1000).toString() + " Km")
                txtRouteDuration.setText(it[0].estimatedDuration + " h")
                setDisplay(View.VISIBLE)
                routeOnGoing = it[0]
            }
            else{
                txtRouteName.setText("Nenhuma Rota Iniciada.")
                setDisplay(View.GONE)
                routeOnGoing = null
            }
        })

        btnCloseRoute.setOnClickListener {view ->
            var serverResponse = ""
            routeOnGoing?.let {
                Executors.newFixedThreadPool(1).execute {
                    //Update local Room db
                    RepositoryRoute(requireActivity().application).updateRoute(it.id,"Finished")

                    //Update Api
                    val callback = retrofitClient?.finishRoute(it.id, FinishRoute(it.distanceEstimatedKm/1000))

                    callback?.enqueue(object: Callback<Unit> {
                        override fun onResponse(call: Call<Unit>, response: Response<Unit>) {
                            if(response.body()!=null){
                                serverResponse = "Rota Terminada com Sucesso!"
                            }
                        }

                        override fun onFailure(call: Call<Unit>, t: Throwable) {
                                serverResponse = "Não foi possivel terminar a rota!"
                            }
                    })
                }
            }
            Toast.makeText(myContext,serverResponse, Toast.LENGTH_LONG).show()
        }

        return view
    }

    private fun setDisplay(mode:Int){
        txtRouteDistance.visibility = mode
        txtRouteDuration.visibility = mode
        txtTitleRouteDistance.visibility = mode
        txtTitleRouteDuration.visibility = mode
        btnCloseRoute.visibility = mode
    }
}