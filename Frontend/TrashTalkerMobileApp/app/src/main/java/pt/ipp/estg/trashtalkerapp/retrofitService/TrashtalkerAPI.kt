package pt.ipp.estg.trashtalkerapp.retrofitService

import android.provider.MediaStore
import android.widget.ImageView
import okhttp3.OkHttpClient
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import okhttp3.Request
import okhttp3.ResponseBody
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Employee
import pt.ipp.estg.trashtalkerapp.room.roomEntities.Route
import retrofit2.http.*
import java.io.File

interface TrashtalkerAPI {
    @POST("loginMobile")
    fun login(@Body user:User): Call<Unit>

    @POST("picking")
    fun insertPicking(@Body picking:Picking): Call<Unit>

    @GET("routes/myRoutes")
    fun getAllRoutes():Call<List<Route>>

    @PUT("routes/start/{id}")
    fun startRoute(@Path("id") id:String):Call<Unit>

    @PUT("routes/finish/{id}")
    fun finishRoute(@Path("id") id:String,@Body finishRoute:FinishRoute):Call<Unit>

    @POST("alert")
    fun submitIssue(@Body issue:Issue):Call<Unit>

    @PUT("user/{id}")
    fun updateUser(@Path("id") id:String,@Body updatedUser:UpdateUser):Call<Unit>

    @GET("user/{username}")
    fun getUserByUsername(@Path("username")username:String):Call<Employee>

    @GET("images/{id}")
    fun getUserImage(@Path("id")id:String):Call<ResponseBody>

    companion object{
    //Use only with emulator
    private const val BASE_URL = "http://10.0.2.2:5000/api/v1/"

    fun retrofitInstance(token:String = ""):Retrofit{
        val client = OkHttpClient.Builder().addInterceptor { chain ->
            val newRequest: Request = chain.request().newBuilder()
                .addHeader("Authorization", "Bearer $token")
                .build()
            chain.proceed(newRequest)
        }.build()

      return Retrofit.Builder()
          .client(client)
          .baseUrl(BASE_URL)
          .addConverterFactory(GsonConverterFactory.create())
          .build()
  }

  fun getApi(token:String = ""):TrashtalkerAPI{
      return this.retrofitInstance(token).create(TrashtalkerAPI::class.java)
  }

}
}