package pt.ipp.estg.trashtalkerapp

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import pt.ipp.estg.trashtalkerapp.databinding.ActivityMainBinding
import pt.ipp.estg.trashtalkerapp.retrofitService.TrashtalkerAPI
import pt.ipp.estg.trashtalkerapp.retrofitService.User
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import com.auth0.android.jwt.JWT

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        //Create shared preferences file
        val sharedPreferences = getSharedPreferences(getString(R.string.sharedPreferences), Context.MODE_PRIVATE);
        val myEdit = sharedPreferences.edit()

        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.loginbtn.setOnClickListener {
            val username: String? = binding.username.text?.toString()
            val password: String? = binding.password.text?.toString()

            var user:User? = null
            if(!username.isNullOrEmpty() && !password.isNullOrEmpty()){
                user = User(username,password)
            }

            //Sign in client session
            val retrofitClient = TrashtalkerAPI.getApi()
            val callback = user?.let { it1 -> retrofitClient.login(it1) }


            if (callback != null) {
                callback.enqueue(object: Callback<Unit> {
                    override fun onResponse(call: Call<Unit>, response: Response<Unit>) {
                        if(response.code() === 200){
                            //Get Token from API response
                            val token = response.headers()?.values("authorization")?.get(0).toString()
                            val jwtToken = JWT(token)
                            val claim = jwtToken.getClaim("unique_name").asString()

                            //Save token within shared preferences
                            myEdit.putString("token",token)
                            myEdit.putString("username",username)
                            var string = getString(R.string.username)
                            string = username.toString()
                            myEdit.commit()

                            //Open home activity
                            val i= Intent(baseContext,AppActivity::class.java)
                            i.putExtra("username",claim)
                            startActivity(i)
                        }else{
                            Toast.makeText(baseContext,"Password e username inválidos!", Toast.LENGTH_LONG).show()
                        }
                    }

                    override fun onFailure(call: Call<Unit>, t: Throwable) {
                        Toast.makeText(baseContext,t.message.toString(), Toast.LENGTH_LONG).show()
                    }
                })
            }
        }
    }
}