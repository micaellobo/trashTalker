package pt.ipp.estg.trashtalkerapp.ui.route

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.FragmentManager
import androidx.fragment.app.FragmentPagerAdapter
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import pt.ipp.estg.trashtalkerapp.R
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route
import pt.ipp.estg.trashtalkerapp.ui.home.HomeViewModel
import pt.ipp.estg.trashtalkerapp.ui.route.utils.routeAdapter
import androidx.recyclerview.widget.DefaultItemAnimator
import androidx.viewpager.widget.ViewPager
import com.google.android.material.tabs.TabLayout

class RouteFragment : Fragment() {
    private lateinit var viewPager:ViewPager
    private lateinit var tabLayout:TabLayout

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
        val view = inflater.inflate(R.layout.fragment_route, container, false)

        val plannedRoutes = PlannedRoutes()
        val finishedRoutes = FinishedRoutes()

        viewPager = view.findViewById(R.id.view_pager)
        tabLayout = view.findViewById(R.id.tabLayout)

        tabLayout.setupWithViewPager(viewPager)

        val viewPageAdapter = ViewPagerAdapter(childFragmentManager ,0)
        viewPageAdapter.addFragment(plannedRoutes,"Planeadas")
        viewPageAdapter.addFragment(finishedRoutes,"Terminadas")
        viewPager.adapter=viewPageAdapter

        tabLayout.getTabAt(0)?.setIcon(R.drawable.ic_baseline_planned)
        tabLayout.getTabAt(1)?.setIcon(R.drawable.ic_baseline_done)

        return view
    }

    inner class ViewPagerAdapter(fm: FragmentManager, behavior: Int) :
        FragmentPagerAdapter(fm, behavior) {

        private val mFragmentList = ArrayList<Fragment>()
        private val mFragmentTitleList = ArrayList<String>()

        override fun getCount(): Int {
            return mFragmentList.size
        }

        override fun getItem(position: Int): Fragment {
            return mFragmentList[position]
        }

        override fun getPageTitle(position: Int): CharSequence? {
            return mFragmentTitleList[position]
        }

        fun addFragment(fragment: Fragment,title:String){
            mFragmentList.add(fragment)
            mFragmentTitleList.add(title)
        }
    }
}