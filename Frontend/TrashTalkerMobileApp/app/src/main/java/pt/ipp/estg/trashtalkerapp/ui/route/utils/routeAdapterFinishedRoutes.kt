package pt.ipp.estg.trashtalkerapp.ui.route.utils

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import pt.ipp.estg.trashtalkerapp.R
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route

class routeAdapterFinishedRoutes(var listFinishedRoute:List<Route>): RecyclerView.Adapter<routeAdapterFinishedRoutes.myViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): myViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.route_recycler_view_finished_item,parent,false)
        return myViewHolder(view)
    }

    override fun onBindViewHolder(holder: myViewHolder, position: Int) {
        holder.apply {
            txtDateBegin.setText(listFinishedRoute.get(position).dateBegin)
            txtName.setText(listFinishedRoute.get(position).name)
            txtTimeBegin.setText(listFinishedRoute.get(position).timeBegin)
        }
    }

    override fun getItemCount(): Int {
        return listFinishedRoute.size
    }

    inner class myViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var txtName:TextView
        var txtDateBegin:TextView
        var txtTimeBegin:TextView

        init{
            txtName = itemView.findViewById(R.id.txtNameFinished)
            txtDateBegin = itemView.findViewById(R.id.txtDateBeginFinished)
            txtTimeBegin = itemView.findViewById(R.id.txtTimeBeginFinished)
        }
    }

    fun updateList(updatedList:List<Route>){
        this.listFinishedRoute=updatedList;
        this.notifyDataSetChanged()
    }
}