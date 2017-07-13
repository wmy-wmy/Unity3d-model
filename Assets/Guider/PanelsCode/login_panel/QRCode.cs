using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;//引入库  
using ZXing.QrCode;
using UnityEngine.UI;
using BestHTTP;
using BestHTTP.Cookies;
using System;
using System.IO;
using LitJson;


public class QRCode : MonoBehaviour {

    public enum JSONTYPE{
        JSON_QRCODE_TYPE=1,
        JSON_LOGIN_TYPE,
        JSON_END,

    };

	public string getQRstr = "https://scanner-test.orbbec.me/user/getqr";
	public string getLoginstr = "https://scanner-test.orbbec.me/user/login";

    public GameObject panel1;
    public GameObject panel2;
	public GameObject Dialogpanel;



    public  static JsonQRCodeData jsonQRdata;
	public  static JsonLoginData jsonLogindata;

    //定义Texture2D对象和用于对应网站的字符串  
    public UITexture UIencode;
    Texture2D encoded;
    private string Lastresult = null;
    //定义一个UI，来接收图片  
    public RawImage ima;
    bool ok = false;
    public Texture t1;
    // Use this for initialization
    void Start () {
		CurrentPanel.curPanel = panel1;


    }
	void OnEnable(){
	
		jsonQRdata = null;
		jsonLogindata = null;

		encoded = new Texture2D(256, 256);
		Global._stopRequest = false;

		//StartCoroutine (OnGetRequest());
		OnGetRequest();



		Debug.Log("login start!");
	
	}
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
    // Update is called once per frame
    void Update () {
		if (Lastresult != null) {
			var textForEncoding = Lastresult;
			// Debug.Log(""+ Lastresult);
			if (textForEncoding != null && ok) {
				//二维码写入图片  
				var color32 = Encode (textForEncoding, encoded.width, encoded.height);
				encoded.SetPixels32 (color32);
				encoded.Apply ();
				//生成的二维码图片附给RawImage  
				UIencode.mainTexture = encoded;

			}          
		}
    }



	IEnumerator ready2Login(){
		Debug.Log ("ready 2 login");
		if (jsonLogindata != null) {
			if (jsonLogindata.code != 99) {
				Debug.Log ("RETURN RETURN LOGIN");
				yield return null;
			}
		}
		onGetLoginRequest ();
		if (jsonLogindata == null) {
			Debug.Log ("jsonLogindata == null");
			yield return new WaitForSeconds (2.0f);
			StartCoroutine (ready2Login ());
		}
		else{
			Debug.Log ("jsonLogindata is not null login");
			if (jsonLogindata.code == 99) {
				yield return new WaitForSeconds (1.0f);
				if(jsonLogindata.code == 99)
					StartCoroutine (ready2Login ());
			} else {
				Debug.Log ("login success!");

			}
		}

	}


	void OnGetRequest()
    {
        HTTPRequest request = new HTTPRequest(new Uri(getQRstr), HTTPMethods.Get, OnRequestFinished);
        request.Send();
		//yield return 0;
    }
    public void onGetLoginRequest() {
        HTTPRequest request = new HTTPRequest(new Uri(getLoginstr+"?uid="+jsonQRdata.uid), HTTPMethods.Get, OnLoginFinished);
        request.Send();
        
    }

    //请求回调   request请求  response响应  这两个参数必须要有 委托类型是OnRequestFinishedDelegate  
    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {      
		if (!Global._stopRequest && Application.internetReachability != NetworkReachability.NotReachable) {
			if (response != null) {
				if (response.StatusCode == 200) {
					ReloadFamilyData (response.DataAsText, 1);
					// DisplayFamilyList(m_JsonList);
					if (jsonQRdata.code == 0) {
						if (jsonQRdata.ticket != null) {
							Lastresult = jsonQRdata.ticket;// System.Text.Encoding.Default.GetString(byteArray, 33, 45);
							Debug.Log (" qrcode UID=" + jsonQRdata.uid);
							ok = true;
							//onGetLoginRequest ();
							StartCoroutine (ready2Login());
						} 
					}
			
				} else {
					Debug.Log ("Request QRCode fail!");
			
				}
			}
		}
       
    }
    void OnLoginFinished(HTTPRequest request, HTTPResponse response) {
		if (!Global._stopRequest && Application.internetReachability != NetworkReachability.NotReachable) {
			if (response != null) {
				if (response.StatusCode == 200) {
					ReloadFamilyData (response.DataAsText, 2);
					Debug.Log ("login UID=" + jsonQRdata.uid);
					Debug.Log (response.DataAsText);
					if (jsonLogindata.code == 99) {
						Debug.Log ("OnLoginFinished fail");
					}
					if (jsonLogindata.code == 0) {
						Debug.Log ("OnLoginFinished ok");
						Global.token = jsonLogindata.token;
						Global.isScan = jsonLogindata.iscan;
						Global.isLogin = true;
						Global.name = jsonLogindata.nickname;
						Global.gender = jsonLogindata.gender;
						Global.id = jsonLogindata.id;
						if (jsonLogindata.birthday == "" && jsonLogindata.weight == 0) {
							Debug.Log ("birthday is null ");
							Global.isRegist = false;
						} else {
							Debug.Log ("birthday isn't null ");
							Global.isRegist = true;
							Global.weight = jsonLogindata.weight / 10;
							Debug.Log ("birthday isn't null + weight = " + Global.weight);
						}

						Debug.Log (response.DataAsText);



						NGUITools.SetActive (panel1, false);
						NGUITools.SetActive (panel2, true);

					}
				} else {
					Debug.Log ("Request login fail!");
				}
			}
		}


    }
 public static void WriteNewFile(string FileName, string Message)
{
    if (File.Exists(FileName))
    {
        File.Delete(FileName);
    }
    using (FileStream fileStream = File.Create(FileName))
    {
        byte[] bytes = System.Text.Encoding.Default.GetBytes(Message);
        fileStream.Write(bytes, 0, bytes.Length);
            Global.leng_json = bytes.Length;
        }
}
		
public static void ReloadFamilyData(string tmp,int type)
    {
        switch (type) {
            case 1:
                jsonQRdata = JsonMapper.ToObject<JsonQRCodeData>(tmp);
                break;
            case 2:
                jsonLogindata = JsonMapper.ToObject<JsonLoginData>(tmp);       
                break;
            default:
                break;


        } 
       
    }

}
