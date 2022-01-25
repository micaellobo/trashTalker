package pt.ipp.estg.trashtalkerapp.ui.myAccount

import android.annotation.SuppressLint
import android.content.Context
import android.graphics.Bitmap
import android.graphics.drawable.Drawable
import android.media.Image
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.core.view.drawToBitmap
import pt.ipp.estg.trashtalkerapp.MainActivity
import pt.ipp.estg.trashtalkerapp.R
import pt.ipp.estg.trashtalkerapp.retrofitService.TrashtalkerAPI
import pt.ipp.estg.trashtalkerapp.retrofitService.UpdateUser
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Employee
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.http.GET
import java.io.File
import java.util.concurrent.Executors
import java.io.IOException

import java.net.MalformedURLException

import java.net.URL

import java.io.InputStream

import android.graphics.BitmapFactory

import android.widget.ImageView
import androidx.lifecycle.lifecycleScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.async
import kotlinx.coroutines.launch
import okio.ByteString
import java.net.UnknownServiceException
import okhttp3.ResponseBody





class MyAccount : Fragment() {
    private lateinit var userImg: ImageView
    private lateinit var myContext: Context
    private lateinit var txtPassword:TextView
    private lateinit var txtNewPassword:TextView
    private lateinit var txtFirstName:TextView
    private lateinit var txtLastName:TextView
    private lateinit var txtEmail:TextView
    private lateinit var txtStreet:TextView
    private lateinit var txtCity:TextView
    private lateinit var txtZipCode:TextView
    private lateinit var txtCountry:TextView
    private var listErrors = ArrayList<String>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onAttach(context: Context) {
        super.onAttach(context)
        myContext = context
    }

    @SuppressLint("WrongConstant")
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_my_account, container, false)

        userImg = view.findViewById(R.id.imageView)
        txtPassword = view.findViewById(R.id.txtPassword)
        txtNewPassword = view.findViewById(R.id.txtNewPassword)
        txtFirstName = view.findViewById(R.id.txtFirstName)
        txtEmail = view.findViewById(R.id.txtEmail)
        txtStreet = view.findViewById(R.id.txtStreet)
        txtCity = view.findViewById(R.id.txtCity)
        txtZipCode = view.findViewById(R.id.txtZipCode)
        txtCountry = view.findViewById(R.id.txtCountry)
        txtLastName = view.findViewById(R.id.txtLastName)

        val btn = view.findViewById<Button>(R.id.btnUpdateUser)

        val sh = myContext.getSharedPreferences(getString(R.string.sharedPreferences), Context.MODE_APPEND)
        val token:String? = sh.getString("token","")
        val username:String? = sh.getString("username","")

        val retrofitClient = token?.let { TrashtalkerAPI.getApi(it) }

        var userId:String = ""

        val callback = username?.let { retrofitClient?.getUserByUsername(it) }
        Executors.newFixedThreadPool(1).execute {
            callback?.enqueue(object: Callback<Employee> {
                override fun onResponse(call: Call<Employee>, response: Response<Employee>) {
                    if(response.body()!=null){
                        val employee = response.body() as Employee
                        userId = employee.id
                        txtFirstName.setText(employee.firstName)
                        txtLastName.setText(employee.lastName)
                        txtEmail.setText(employee.email)
                        txtStreet.setText(employee.street)
                        txtCity.setText(employee.city)
                        txtZipCode.setText(employee.zipCode)
                        txtCountry.setText(employee.country)
                    }
                    val call = retrofitClient?.getUserImage(userId)
                    Executors.newFixedThreadPool(1).execute {
                        call?.enqueue(object : Callback<ResponseBody?> {
                            override fun onResponse(call: Call<ResponseBody?>, response: Response<ResponseBody?>) {
                                if (response.isSuccessful) {
                                    if (response.body() != null) {
                                        val bmp = BitmapFactory.decodeStream(response.body()!!.byteStream())
                                        userImg.setImageBitmap(bmp)
                                    }
                                }else{
                                    Toast.makeText(myContext,"Erro a carregar imagem",Toast.LENGTH_LONG).show()
                                }
                            }

                            override fun onFailure(call: Call<ResponseBody?>, t: Throwable) {
                                Toast.makeText(myContext,"Erro a carregar imagem",Toast.LENGTH_LONG).show()
                            }
                        })
                    }
                }
                override fun onFailure(call: Call<Employee>, t: Throwable) {
                    Toast.makeText(myContext,"",Toast.LENGTH_LONG).show()
                }
            })
        }





        btn.setOnClickListener {
            if(validData()){
                Executors.newFixedThreadPool(1).execute {
                    val callback = retrofitClient?.updateUser("",
                        UpdateUser(txtPassword.text.toString(),txtEmail.text.toString(),txtStreet.text.toString(),
                            txtCity.text.toString(), txtZipCode.text.toString(),txtCountry.text.toString())
                    )

                    callback?.enqueue(object: Callback<Unit> {
                        override fun onResponse(call: Call<Unit>, response: Response<Unit>) {
                            if(response.body()!=null){
                                Toast.makeText(myContext,"Perfil Atualizado Com Sucesso",Toast.LENGTH_LONG).show()
                            }
                        }

                        override fun onFailure(call: Call<Unit>, t: Throwable) {
                            Toast.makeText(myContext,t.toString(),Toast.LENGTH_LONG).show()
                        }

                    })
                }
            }
            else{
                Toast.makeText(myContext,listErrors.toString(),Toast.LENGTH_LONG).show()
            }
        }

        return view
    }

    private fun requiredData(): Boolean{
        return !(txtPassword.text.toString().trim().equals("")) &&
                !(txtNewPassword.text.toString().trim().equals("")) &&
                !(txtEmail.text.toString().trim().equals("")) &&
                !(txtStreet.text.toString().trim().equals("")) &&
                !(txtEmail.text.toString().trim().equals("")) &&
                !(txtCity.text.toString().trim().equals("")) &&
                !(txtZipCode.text.toString().trim().equals("")) &&
                !(txtCountry.text.toString().trim().equals(""))

    }

    private fun validData():Boolean{
        this.listErrors.clear()

        if(!requiredData()){
            this.listErrors.add("Todos os campos são de preenchimento obrigatório.")
            return listErrors.size == 0
        }

        if(!(txtPassword.text.toString().equals(txtNewPassword.text.toString()))){
            this.listErrors.add("Passwords diferentes.Verifique novamente a password.")
            return listErrors.size == 0
        }

        if(txtPassword.text.length < 5){
            this.listErrors.add("A password tem de ter no minimo 5 caracteres.")
            return listErrors.size == 0
        }

        return listErrors.size == 0
    }
}