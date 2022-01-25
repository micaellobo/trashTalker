package pt.ipp.estg.trashtalkerapp.ui.problems

import android.annotation.SuppressLint
import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import pt.ipp.estg.trashtalkerapp.R
import pt.ipp.estg.trashtalkerapp.UpdateViewActivity
import pt.ipp.estg.trashtalkerapp.retrofitService.Issue
import pt.ipp.estg.trashtalkerapp.retrofitService.TrashtalkerAPI
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.util.concurrent.Executors

/**
 * A simple [Fragment] subclass.
 * Use the [ProblemsFragment.newInstance] factory method to
 * create an instance of this fragment.
 */
class ProblemsFragment : Fragment() {
    private lateinit var myContext: UpdateViewActivity
    private lateinit var issueType:String

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
        }
    }

    override fun onAttach(context: Context) {
        super.onAttach(context)
        myContext = context as UpdateViewActivity
    }

    @SuppressLint("WrongConstant")
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        myContext.updateView(false)

        val view = inflater.inflate(R.layout.fragment_problems, container, false)

        var txtIssueDescription = view.findViewById<EditText>(R.id.editIssue)
        val btnSubmitIssue = view.findViewById<Button>(R.id.btnCloseRoute)
        var radioGroup = view.findViewById<RadioGroup>(R.id.radioGroup_typeIssue)

        val sh = (myContext as Context).getSharedPreferences(getString(R.string.sharedPreferences), Context.MODE_APPEND)

        val token:String? = sh.getString("token","")
        val retrofitClient = token?.let { TrashtalkerAPI.getApi(it) }

        btnSubmitIssue.setOnClickListener {
            val issueDescription = txtIssueDescription.text.toString()
            var serverResponse = ""
            if(issueDescription.isNotEmpty() && radioGroup.checkedRadioButtonId != -1){
                updateRadioButtonSelected(radioGroup.checkedRadioButtonId)
                Executors.newFixedThreadPool(1).execute {
                    //Update Api
                    val callback = retrofitClient?.submitIssue(Issue(issueDescription,issueType))

                    callback?.enqueue(object: Callback<Unit> {
                        override fun onResponse(call: Call<Unit>, response: Response<Unit>) {
                            if(response.body()!=null){
                                serverResponse = "Problema foi reportado com sucesso!"
                                Toast.makeText(myContext as Context,serverResponse,Toast.LENGTH_LONG).show()
                            }
                        }

                        override fun onFailure(call: Call<Unit>, t: Throwable) {
                            serverResponse = "Todos os campos são de preenchimento obrigatório!"
                            Toast.makeText(myContext as Context,serverResponse,Toast.LENGTH_LONG).show()
                        }
                    })
                }

                txtIssueDescription.setText("")
                radioGroup.clearCheck()
            }
        }

        // Inflate the layout for this fragment
        return view
    }

    override fun onDestroyView() {
        super.onDestroyView()
        myContext.updateView(true)
    }

    fun updateRadioButtonSelected(id:Int){
        when(id){
            R.id.radio_system -> issueType = "SYSTEM_FAILURE"
            R.id.radio_container -> issueType = "CONTAINER"
            R.id.radio_sensor -> issueType = "SENSOR"
            R.id.radio_other -> issueType = "OTHER"
        }
    }

}

