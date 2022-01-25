package pt.ipp.estg.trashtalkerapp

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.content.pm.PackageManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.budiyev.android.codescanner.AutoFocusMode
import com.budiyev.android.codescanner.CodeScanner
import com.budiyev.android.codescanner.DecodeCallback
import com.budiyev.android.codescanner.ErrorCallback
import com.budiyev.android.codescanner.ScanMode
import pt.ipp.estg.trashtalkerapp.databinding.ActivityQrCodeScannerBinding
import pt.ipp.estg.trashtalkerapp.retrofitService.Picking
import pt.ipp.estg.trashtalkerapp.retrofitService.TrashtalkerAPI
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.text.SimpleDateFormat
import java.util.*
import java.util.concurrent.Executors

private const val CAMERA_REQUEST_CODE = 101

class QrCodeScannerActivity : AppCompatActivity() {
    private lateinit var codeScanner:CodeScanner
    private lateinit var binding: ActivityQrCodeScannerBinding
    private lateinit var sharedPref:SharedPreferences

    @SuppressLint("WrongConstant")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //Get the user references
        sharedPref = getSharedPreferences(getString(R.string.sharedPreferences), Context.MODE_APPEND)

        binding = ActivityQrCodeScannerBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.btnBack.setOnClickListener {
            //Back to home activity
            val i= Intent(baseContext,AppActivity::class.java)
            startActivity(i)
        }
        setupPermissions()
        codeScanner()
    }

    private fun codeScanner(){
        codeScanner = CodeScanner(this,binding.scannerView)

        codeScanner.apply {
            camera = CodeScanner.CAMERA_BACK
            formats = CodeScanner.ALL_FORMATS

            autoFocusMode = AutoFocusMode.SAFE

            scanMode = ScanMode.SINGLE
            isAutoFocusEnabled = true
            isFlashEnabled = false

            decodeCallback = DecodeCallback {
                runOnUiThread{
                    binding.tvTextView.text = it.text
                    submitPicking(it.text)
                }
            }

            errorCallback = ErrorCallback {
                runOnUiThread{
                    Log.e("Main","Camera initialization error: ${it.message}")
                }
            }

            binding.scannerView.setOnClickListener{
                codeScanner.startPreview()
            }
        }
    }

    override fun onResume() {
        super.onResume()
        codeScanner.startPreview()
    }

    override fun onPause() {
        codeScanner.releaseResources()
        super.onPause()
    }

    private fun setupPermissions(){
        val permission=ContextCompat.checkSelfPermission(this,
        android.Manifest.permission.CAMERA)

        if(permission!=PackageManager.PERMISSION_GRANTED){
            makeRequest()
        }
    }

    private fun makeRequest(){
        ActivityCompat.requestPermissions(this, arrayOf( android.Manifest.permission.CAMERA),
        CAMERA_REQUEST_CODE)
    }

    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<out String>,
        grantResults: IntArray
    ) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults)
        when(requestCode){
            CAMERA_REQUEST_CODE ->{
                if (grantResults.isEmpty() || grantResults[0]!= PackageManager.PERMISSION_GRANTED){
                    Toast.makeText(this,"É necessária a permissão da camera para utilizar a app!",
                        Toast.LENGTH_LONG).show()
                }
            }
        }
    }

    private fun submitPicking(idContainer:String){
        Executors.newFixedThreadPool(1).execute {
            //Get token session
            val token: String? = sharedPref.getString("token","")

            val retrofitClient = token?.let { TrashtalkerAPI.getApi(it) }
            val sdf = SimpleDateFormat("dd/M/yyyy hh:mm:ss")

            val callback = retrofitClient?.insertPicking(Picking(idContainer,4,sdf.format(Date())))

            if (callback != null) {
                callback.enqueue(object: Callback<Unit> {
                    override fun onResponse(call: Call<Unit>, response: Response<Unit>) {
                        if(response.code()==201){
                            val i= Intent(baseContext,AppActivity::class.java)
                            Toast.makeText(baseContext,"Recolha Efetuada com Sucesso!", Toast.LENGTH_LONG).show()
                            startActivity(i)
                        }else{
                            Toast.makeText(baseContext,"Qr Code Inválido " + response.code(), Toast.LENGTH_LONG).show()
                        }
                    }

                    override fun onFailure(call: Call<Unit>, t: Throwable) {
                        Toast.makeText(baseContext,"Não foi possivel ler QR Code "+t.message.toString(), Toast.LENGTH_LONG).show()
                        val i= Intent(baseContext,AppActivity::class.java)
                        startActivity(i)
                    }
                })
            }
        }
    }
}