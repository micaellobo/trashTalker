package pt.ipp.estg.trashtalkerapp.room

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route

@Database(entities = arrayOf(Route::class), version = 6)
abstract class RouteDb: RoomDatabase() {
    companion object{
        const val DATABASE_NAME = "GasStation_DB"

        @Volatile
        private var INSTANCE:RouteDb? = null

        fun getInstance(context: Context):RouteDb{
            synchronized(this){
                var instance = INSTANCE
                if(instance == null){
                    instance = Room.databaseBuilder(context.applicationContext,
                    RouteDb::class.java, DATABASE_NAME)
                        .fallbackToDestructiveMigration()
                        .build()
                    INSTANCE=instance
                }
                return instance
            }
        }
    }

    abstract fun routeDao():RouteDao
}