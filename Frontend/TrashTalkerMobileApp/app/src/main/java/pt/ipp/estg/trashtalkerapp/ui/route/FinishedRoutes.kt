package pt.ipp.estg.trashtalkerapp.ui.route

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import pt.ipp.estg.trashtalkerapp.R
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route
import pt.ipp.estg.trashtalkerapp.ui.home.HomeViewModel
import pt.ipp.estg.trashtalkerapp.ui.route.utils.routeAdapterFinishedRoutes
import androidx.recyclerview.widget.DividerItemDecoration




class FinishedRoutes : Fragment() {
    private lateinit var listFinishedRoute:List<Route>

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {

        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_finished_routes, container, false)

        //RecyclerView configuration
        listFinishedRoute = ArrayList<Route>()
        var myAdapter = routeAdapterFinishedRoutes(listFinishedRoute)
        var recView = view.findViewById<RecyclerView>(R.id.recView2)

        recView.apply {
            adapter = myAdapter
            layoutManager = LinearLayoutManager(requireContext(), LinearLayoutManager.VERTICAL, false);
            addItemDecoration(DividerItemDecoration(requireContext(), DividerItemDecoration.VERTICAL))
        }

        ViewModelProvider(this).get(HomeViewModel::class.java).getAllFinishedRoute()
            .observe(viewLifecycleOwner, Observer {
                myAdapter.updateList(it)
            })
        return view
    }
}