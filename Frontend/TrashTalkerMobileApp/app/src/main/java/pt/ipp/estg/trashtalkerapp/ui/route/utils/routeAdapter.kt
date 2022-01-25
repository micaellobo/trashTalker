package pt.ipp.estg.trashtalkerapp.ui.route.utils

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import pt.ipp.estg.trashtalkerapp.R
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route
import pt.ipp.estg.trashtalkerapp.ui.route.Communication

class routeAdapter(var listPlannedRoute:List<Route>, var context: Communication): RecyclerView.Adapter<routeAdapter.myViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): myViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.route_recycler_view_item,parent,false)
        return myViewHolder(view)
    }

    override fun onBindViewHolder(holder: myViewHolder, position: Int) {
        holder.apply {
            txtDateBegin.setText(listPlannedRoute.get(position).dateBegin)
            txtName.setText(listPlannedRoute.get(position).name)
            txtTimeBegin.setText(listPlannedRoute.get(position).timeBegin)
        }

        holder.itemView.setOnClickListener {
            context.updateSelectedRoute(listPlannedRoute.get(position))
        }
    }

    override fun getItemCount(): Int {
        return listPlannedRoute.size
    }


    inner class myViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var txtName:TextView
        var txtDateBegin:TextView
        var txtTimeBegin:TextView

        init{
            txtName = itemView.findViewById(R.id.txtNamePlanned)
            txtDateBegin = itemView.findViewById(R.id.txtDateBeginPlanned)
            txtTimeBegin = itemView.findViewById(R.id.txtTimeBeginPlanned)
        }
    }

    fun updateList(updatedList:List<Route>){
        this.listPlannedRoute=updatedList;
        this.notifyDataSetChanged()
    }
}