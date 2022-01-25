package pt.ipp.estg.trashtalkerapp.room.roomEntities

import androidx.annotation.Nullable
import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity
data class Route(
    @PrimaryKey val id:String,
    var name:String,
    var dateBegin:String,
    var timeBegin:String?,
    var status:String,
    var dateCriation:String,
    var duration:String?,
    var dateEnd:String?,
    var distanceTravelledKm:Int,
    var estimatedDuration:String,
    var distanceEstimatedKm:Int
)