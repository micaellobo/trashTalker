package pt.ipp.estg.trashtalkerapp

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.TextView
import android.widget.Toast
import com.google.android.material.navigation.NavigationView
import androidx.navigation.findNavController
import androidx.drawerlayout.widget.DrawerLayout
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.ui.*
import com.google.android.material.bottomnavigation.BottomNavigationView
import pt.ipp.estg.trashtalkerapp.databinding.ActivityAppBinding
import pt.ipp.estg.trashtalkerapp.retrofitService.TrashtalkerAPI
import pt.ipp.estg.trashtalkerapp.room.RepositoryRoute
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route
import pt.ipp.estg.trashtalkerapp.ui.home.HomeViewModel
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.util.concurrent.Executors

class AppActivity : AppCompatActivity(),UpdateViewActivity {
    private lateinit var appBarConfiguration: AppBarConfiguration
    private lateinit var binding: ActivityAppBinding
    private lateinit var routeRepository:RepositoryRoute
    private var routeOnGoing = false

    @SuppressLint("WrongConstant")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityAppBinding.inflate(layoutInflater)
        setContentView(binding.root)

        routeRepository = RepositoryRoute(this.application)

        var homeViewModel = ViewModelProvider(this).get(HomeViewModel::class.java)
        homeViewModel.getCurrentOnGoingRoute().observe(this, Observer {
                routeOnGoing = it.isNotEmpty()
        })

        //Get the user references
        val sh = getSharedPreferences(getString(R.string.sharedPreferences), Context.MODE_APPEND)

        setSupportActionBar(binding.appBarApp.toolbar)
        binding.appBarApp.fab.setOnClickListener { view ->
            if(routeOnGoing){
                //Open Qr code activity
                val i= Intent(baseContext,QrCodeScannerActivity::class.java)
                startActivity(i)
            }
            else{
                Toast.makeText(this,"Nenhuma rota está a decorrer!",Toast.LENGTH_LONG).show()
            }
        }

        var headerView = binding.navView.getHeaderView(0);
        var txtUser = headerView.findViewById<TextView>(R.id.txtUsername)

        txtUser.setText(intent.getStringExtra("username"))

        val drawerLayout: DrawerLayout = binding.drawerLayout
        val navView: NavigationView = binding.navView
        val navController = findNavController(R.id.nav_host_fragment_content_app)

        //End up session
        navView.getMenu().getItem(4).setOnMenuItemClickListener {
            logout()
            true
        }

        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.nav_home
            ), drawerLayout
        )
        setupActionBarWithNavController(navController, appBarConfiguration)
        navView.setupWithNavController(navController)

        //BottomViewNav sync with navigation controller
        val bottom_nav_view=findViewById<BottomNavigationView>(R.id.bottom_nav_view)
        NavigationUI.setupWithNavController(bottom_nav_view,navController)

        //End up Session
        bottom_nav_view.menu.getItem(3).setOnMenuItemClickListener{
            logout()
            true
        }

        //Badge
        var badge = bottom_nav_view.getOrCreateBadge(R.id.routeFragment)
        badge.isVisible = true

        // An icon only badge will be displayed unless a number is set:
        badge.number = 1

        //Clear notifications bagger
        bottom_nav_view.getMenu().getItem(1).setOnMenuItemClickListener {
            val badgeDrawable = bottom_nav_view.getBadge(R.id.routeFragment)
            if (badgeDrawable != null) {
                badgeDrawable.isVisible = false
                badgeDrawable.clearNumber()
            }
            false
        }

        //Get token session
        val token: String? = sh.getString("token","")

        //Update Room Db
        val retrofitClient = token?.let { TrashtalkerAPI.getApi(it) }
        val callback = retrofitClient?.getAllRoutes()

        callback?.enqueue(object: Callback<List<Route>> {
            override fun onResponse(call: Call<List<Route>>, response: Response<List<Route>>) {
                if(response.body()!=null){
                    updateLocalDb(response.body() as List<Route>)
                }
            }

            override fun onFailure(call: Call<List<Route>>, t: Throwable) {
                Toast.makeText(baseContext,t.message.toString(), Toast.LENGTH_LONG).show()
            }
        })
    }

    private fun updateLocalDb(listRoutes:List<Route>){
        Executors.newFixedThreadPool(2).execute {
            routeRepository.deleteAll()
            listRoutes.forEach {
                it.timeBegin = it.dateBegin.subSequence(11, 19).toString()
                it.dateBegin = it.dateBegin.subSequence(0,10).toString()
                routeRepository.insertRoute(it)
            }
        }
    }

    override fun onSupportNavigateUp(): Boolean {
        val navController = findNavController(R.id.nav_host_fragment_content_app)
        return navController.navigateUp(appBarConfiguration) || super.onSupportNavigateUp()
    }

    private fun logout() {
        val i= Intent(baseContext,MainActivity::class.java)
        startActivity(i)
    }

    override fun updateView(visible: Boolean) {
        binding.appBarApp.fab.visibility = if (visible) View.VISIBLE else View.GONE
    }
}


interface UpdateViewActivity{
    fun updateView(visible:Boolean)
}