using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using LitJson;
using System.Net;
using System.Net.Sockets;
using ZXing;//引入库  
using ZXing.QrCode;
using BestHTTP;
using BestHTTP.Cookies;
using System.Net.Security;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Bson;



public class UserDataProcess : MonoBehaviour {



    public UILabel user_name;
    public UILabel id;
    public UILabel date;

    public string PicUrl;
    public UISprite pic;
	public UITexture UIencode;

    public string jsondata; //要上传的用户测量数据
	public string scanQRGetToken; //
    public JsonLoginData jsonUserdata;

    private char[] charData;//= new char[1000];
    private byte[] byData;//= new byte[100];
	private string []data_str;

	public GameObject this_panel;
	public GameObject bgsp;
	public GameObject model_panel;
	public GameObject qrwindow;
	public UILabel scanner_address;
	public GameObject dialog;

	//get QRCode
	private string getQRstr = "https://scanner-test.orbbec.me/user/getqr";
	private string getLoginstr = "https://scanner-test.orbbec.me/user/login";
	private string postUserinfostr = "https://scanner-test.orbbec.me/user/edit";
	private string getScanDatastr = "https://scanner-test.orbbec.me/measurement/data";
	private string upload_file_url = "http://app-test.orbbec.me/mytool/upload_scanner_file/";

	private string raw_json_file_name="json.txt";//为了减少上传时间先上传一个假的文件  
	private string raw_model_file_name="body.obj";

	public bool login_success_flag=false;
	public bool login_first_flag=false;

	//定义Texture2D对象和用于对应网站的字符串  
	public UITexture QRencode;
	Texture2D encoded;
	public string Lastresult;
	//定义一个UI，来接收图片  
	public RawImage ima;
	bool ok = false;
	public Texture t1;

	public static JsonQRCodeData jsonQRdata;
	public static JsonLoginData jsonLogindata;

	public JsonUserScanData jsonUserScanData;
	public static JsonLastScanData json_Data;

	public GameObject childTitleWindow;


	public static JsonModelDataReturn jsonModel;
	public static string raw_tmp="";
	public static string filter_tmp="";
	public bool raw_upload_ok;
	public bool filter_upload_ok ;

	private Thread upload_raw;
	public Thread getQRCodeThread;

	public int json_length=0;

	private string scandata_json = "output/measure_result.json";

	private string jsondata_str;

    // Use this for initialization
    void Start () {
		CurrentPanel.curPanel = this_panel;
			
       // showData();
    }
	void OnEnable(){
		jsonModel = null;
		jsonQRdata = null;
		jsonLogindata = null;
		login_success_flag=false;
		login_first_flag=false;

		jsonUserScanData = new JsonUserScanData ();
		json_length = (int)GetFileSize (scandata_json);
		byData = new byte[json_length ];
		data_str = new string[24];
		//upload_raw = new Thread(thread_raw);//上传数据线程初始化

		upload_raw = new Thread (new ParameterizedThreadStart(thread_raw));


		if (Global.isLoadLastDataPage) {
			Debug.Log("DATA_LoadDataFromService");
			showUserInfo("昵称:" + Global.name, "ID:微信用户" + Global.id.ToString ());
			DATA_LoadDataFromService ();

		} else {

			showAddress ();
			NGUITools.SetActive (qrwindow,false);


			if (Global.isLogin) {
				Debug.Log ("UserDataProce isLogin = "+Global.isLogin);
				showUserInfo ("昵称:" + Global.name, "ID:微信用户" + Global.id.ToString ());
				onUploadUserBaseInfo (Global.token);
				ParseLocalDataFromKaola ();
				UploadData2Service ();
			}
			else{
				NGUITools.SetActive (childTitleWindow,false);
				Debug.Log("ParseLocalDataFromKaola");
				ParseLocalDataFromKaola ();
				showQRCodeUI ();
				getQRCode ();

			}

		}
	}

	void showQRCodeUI(){
		NGUITools.SetActive (qrwindow,true);
		encoded = new Texture2D(256, 256);
	
	}

	void showAddress (){
		if (scanner_address != null) {
			scanner_address.text = ScannerMachineInfo.scanner_address.ToString ();
		}
	}
		

	public static long GetFileSize(string sFullName)
	{
		long lSize = 0;
		if (File.Exists(sFullName))
			lSize = new FileInfo(sFullName).Length;
		return lSize;
	}

	// Update is called once per frame
	void Update () {

		var textForEncoding = Lastresult;
		// Debug.Log(""+ Lastresult);
		if (textForEncoding != null && ok)
		{
			//二维码写入图片  
			var color32 = Encode(textForEncoding, encoded.width, encoded.height);
			encoded.SetPixels32(color32);
			encoded.Apply();
			//生成的二维码图片附给RawImage  
			QRencode.mainTexture = encoded;

		}          
		
	}


	void showUserInfo(string name,string id_str){
		NGUITools.SetActive (childTitleWindow, true);
		user_name.text = name;
		id.text = id_str;
		date.text = System.DateTime.Now.ToShortDateString ().ToString ();



	}
	void getQRCode(){
		
		OnGetRequest();
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

	public void OnGetRequest()
	{
		HTTPRequest request = new HTTPRequest(new Uri(getQRstr), HTTPMethods.Get, OnRequestFinished);
		request.Send();
	}
	public void onGetLoginRequest() {
		HTTPRequest request = new HTTPRequest(new Uri(getLoginstr+"?uid="+jsonQRdata.uid), HTTPMethods.Get, OnLoginFinished);
		request.Send();

	}

	//请求回调   request请求  response响应  这两个参数必须要有 委托类型是OnRequestFinishedDelegate  
	void OnRequestFinished(HTTPRequest request, HTTPResponse response)
	{      
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			if (response != null) {
				if (response.StatusCode == 200) {
					ReloadFamilyData (response.DataAsText, 1);
					if (jsonQRdata.code == 0) {
						Debug.Log ("start coroutine");
						StartCoroutine (ready2Login());
					}
					if (jsonQRdata.ticket != null)
						Lastresult = jsonQRdata.ticket;// System.Text.Encoding.Default.GetString(byteArray, 33, 45);
					ok = true;
				}
			}
		}
	}
		
	IEnumerator ready2Login(){
		if(!login_success_flag)
			onGetLoginRequest ();
		yield return new WaitForSeconds(1.0f);
		if(!login_success_flag)
			StartCoroutine (ready2Login());
	}

	void OnLoginFinished(HTTPRequest request, HTTPResponse response) {
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			if (response != null) {
				if (response.StatusCode == 200) {
					ReloadFamilyData (response.DataAsText, 2);
					Debug.Log ("OnLoginFinished uid = " + jsonQRdata.uid);

					if (jsonLogindata.code == 99) {
						Debug.Log ("OnLoginFinished fail");

						login_success_flag = false ;
						//onGetLoginRequest ();
					}
					if (jsonLogindata.code == 0 && !login_first_flag) {
						login_success_flag = true;
						login_first_flag = true;
						Debug.Log ("OnLoginFinished ok");

						scanQRGetToken = jsonLogindata.token;//save
						showUserInfo ("昵称:" + jsonLogindata.nickname, "ID:微信用户" + jsonLogindata.id);


						onUploadUserBaseInfo (scanQRGetToken);
						UploadData2Service ();
					}
				} else {
					Debug.Log ("OnLoginFinished request login fail!");
				}
			}
		}

	}

	void onUploadUserBaseInfo (string token_tmp){
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			HTTPRequest request = new HTTPRequest (new Uri (postUserinfostr),
				                     HTTPMethods.Post,
				                     onUploadUserInfoFinished);
			//  request.setHeader(Global.token_uid);
			request.SetHeader ("Content-Type", "application/json");
			request.SetHeader ("Authorization", "Basic " + EncodeBase64 (Encoding.UTF8, token_tmp + ":"));
			request.AddField ("gender", Global.gender.ToString ());
			request.AddField ("age", Global.age.ToString ());
			int w = Global.weight * 10;
			request.AddField ("weight",w.ToString());
			request.Send ();
		}

	}
	void onUploadUserInfoFinished(HTTPRequest request, HTTPResponse response){
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			if (response != null) {
				Debug.Log ("onUploadUserInfoFinished success!");
				Debug.Log (response.DataAsText);
			}
			else {
				Debug.Log ("uoload baseinfo fail!");
			}
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


	/****************************************************
	*
	**
	**解析从服务获取的数据
	**
	**
	*****************************************************/
	/*
	*从服务器加载上次测量数据到本地
	*	
	*/
	int DATA_LoadDataFromService(){
		HTTPRequest request = new HTTPRequest(new Uri(getScanDatastr), HTTPMethods.Get, OnServiceBackData);
		request.SetHeader("Authorization", "Basic " + EncodeBase64(Encoding.UTF8, Global.token + ":"));
		request.Send();

		return 0;
	
	}
	//服务返回数据
	void OnServiceBackData(HTTPRequest request, HTTPResponse response) {
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			if (response != null) {
				if (response.StatusCode == 200) {

					JObject json_return = JObject.Parse (response.DataAsText.ToString ());
					if (json_return != null) {
						if (json_return ["code"].ToString () == "0") {
							Debug.Log ("upload success!");
							Debug.Log (response.DataAsText);
							json_Data = JsonMapper.ToObject<JsonLastScanData> (response.DataAsText);


							Debug.Log (json_Data.json_data);
							Global.model_url = json_Data.fitted_file;

							Debug.Log ("return:" + json_Data.fitted_file);

							if (Global.model_url.Length >= 5) {
								NGUITools.SetActive (model_panel, true);
								ParserServiceData (json_Data.json_data);
							} else {
								Debug.Log ("show warnning log!");
								CurrentPanel.warning_type = WARNING_TYPE.WARNING_DATA_PROCESSING;
								NGUITools.SetActive (this_panel,false);
			
								NGUITools.SetActive (dialog,true);

							}
						} else {
							//some deal

						}
					}

				} else {
					Debug.Log ("request service data fail!");
				}
			}
		}
	}
	//弃用
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
	//BASE 64 method
	public static string EncodeBase64(Encoding encode, string source)
	{

		byte[] bytes = encode.GetBytes(source);
		try
		{
			source = Convert.ToBase64String(bytes);
		}
		catch
		{
			//  encode = source;
		}
		return source;
	}
	/****************************************************
	*
	**
	**解析从服务器下载的测量数据
	**
	**
	*****************************************************/
	void ParserServiceData(string data){

		jsonUserScanData = JsonMapper.ToObject<JsonUserScanData>(data);   

		jsonUserScanData.data12 = 0;
		jsonUserScanData.data21 = 0;
		DATA_preprocessdata ();
		UI_showDataOnSreen (2);
	}




	/****************************************************
	*
	**
	**解析从kaola那边获取的数据
	**
	**
	*****************************************************/
	void ParseLocalDataFromKaola (){	
		jsonfile_read ();
		//DATA_Read ();
		Debug.Log("ParseLocalDataFromKaola:");
		DATA_preprocessdata ();
		UI_showDataOnSreen (1);
	
	}

	//部分数据预处理
	void DATA_preprocessdata(){
		data_str[0] = (jsonUserScanData.data1).ToString ();
		data_str[1] = (jsonUserScanData.data2).ToString ();
		data_str[2] = (jsonUserScanData.data3).ToString ();
		data_str[3] = (jsonUserScanData.data4).ToString ();
		data_str[4] = (jsonUserScanData.data5).ToString ();
		data_str[5] = (jsonUserScanData.data6).ToString ();
		data_str[6] = (jsonUserScanData.data7).ToString ();
		data_str[7] = (jsonUserScanData.data8).ToString ();
		data_str[8] = (jsonUserScanData.data9).ToString ();
		data_str[9] = (jsonUserScanData.data10).ToString ();
		data_str[10] = (jsonUserScanData.data11).ToString ();
		data_str[11] = (jsonUserScanData.data12).ToString ();
		data_str[12] = (jsonUserScanData.data13).ToString ();
		data_str[13] = (jsonUserScanData.data14).ToString ();
		data_str[14] = (jsonUserScanData.data15).ToString ();
		data_str[15] = (jsonUserScanData.data16).ToString ();
		data_str[16] = (jsonUserScanData.data17).ToString ();
		data_str[17] = (jsonUserScanData.data18).ToString ();
		data_str[18] = (jsonUserScanData.data19).ToString ();
		data_str[19] = (jsonUserScanData.data20).ToString ();
		data_str[20] = (jsonUserScanData.data21).ToString ();
		data_str[21] = (jsonUserScanData.data22).ToString ();
		data_str[22] = (jsonUserScanData.data23).ToString ();
	
	}

	/****************************************************
	*
	**
	**	显示数据到屏幕上----- UI
	**
	**
	*****************************************************/

	void UI_showDataOnSreen (int panel_index){
		string panel = "";
		if (panel_index == 1) {
			panel = "display_panel";
		} 
		if (panel_index == 2){
			panel = "last_display";
		}
		//int w = jsonUserScanData.weight / 10;

		GameObject.Find ("UI Root/"+panel+"/data/base_data/tall_value").GetComponent<UILabel> ().text = (jsonUserScanData.tall).ToString();
		GameObject.Find ("UI Root/"+panel+"/data/base_data/weight_value").GetComponent<UILabel> ().text = jsonUserScanData.weight.ToString();
		GameObject.Find ("UI Root/"+panel+"/data/base_data/bmi_value").GetComponent<UILabel> ().text = Convert.ToDouble((double)(jsonUserScanData.weight) / Math.Pow ((float)jsonUserScanData.tall / 100, 2)).ToString("0.0");

		for (int i = 1;  i < 24;i++) {
			if (panel_index == 2) {
				if (i != 21 || i != 12)
					setValue ("UI Root/" + panel + "/data/scan_data/data" + i, data_str [i - 1]);
			} else {
				//if(i != 21)
				setValue ("UI Root/" + panel + "/data/scan_data/data" + i, data_str [i - 1]);
			}
		}

	}
	/****************************************************
	*
	**
	**		//unity 控件的值得赋值
	**
	**
	*****************************************************/

	void setValue (string label,string value){
		GameObject.Find (label).GetComponent<UILabel> ().text = value;
	}
	/****************************************************
	*
	**
	**		//读取kaola测量数据json文件到缓存
	**
	**
	*****************************************************/

	void jsonfile_read(){
		string json = File.ReadAllText(scandata_json, Encoding.UTF8);
		JObject obj = JObject.Parse (json);


		DATA_process_json_data (obj);


	
	}
	/****************************************************
	*
	**
	**	测量数据的预处理,去掉小数点,单位转成厘米
	**
	**
	*****************************************************/

	int proccess_string(string str){
		string s = str;
		string[] arr = s.Split ('.');

		int value = int.Parse (arr[0])/10;
		Debug.Log ("int value = "+value);
		return value;
	}

	/****************************************************
	*
	**
	**	解析json测量数据,保存感兴趣的item
	**
	**
	*****************************************************/
	void DATA_process_json_data (JObject obj ){
		Debug.Log (obj ["Abdomen Height"]["value"].ToString());

		//proccess_string(obj ["Abdomen Height"]["value"].ToString());
		//int va = int.Parse (obj ["Abdomen Height"]["value"].ToString());
		Debug.Log ("Global.weight = :"+Global.weight);

		jsonUserScanData.tall = proccess_string(obj ["Body Height"]["value"].ToString());//tall
		jsonUserScanData.weight = Global.weight;
		jsonUserScanData.data1 = proccess_string(obj ["Neck Circumference"]["value"].ToString());//颈围
		jsonUserScanData.data2 = proccess_string(obj ["Shoulder Width"]["value"].ToString()); //肩宽
		jsonUserScanData.data3 = proccess_string(obj ["Left Upper Arm Girth"]["value"].ToString()); //
		jsonUserScanData.data4 = proccess_string(obj ["Right Upper Arm Girth"]["value"].ToString());
		jsonUserScanData.data5 = proccess_string(obj ["Left Elbow Girth"]["value"].ToString());
		jsonUserScanData.data6 = proccess_string(obj ["Right Elbow Girth"]["value"].ToString());
		jsonUserScanData.data7 = proccess_string(obj ["Chest Girth"]["value"].ToString());
		jsonUserScanData.data8 = proccess_string(obj ["Waist Girth"]["value"].ToString());
		jsonUserScanData.data9 = proccess_string(obj ["Hip Girth"]["value"].ToString());
		jsonUserScanData.data10 = proccess_string(obj ["Back Waist to Seat"]["value"].ToString());
		jsonUserScanData.data11 = proccess_string(obj ["Left OutSide Leg Length"]["value"].ToString());
		jsonUserScanData.data12 = proccess_string(obj ["Right OutSide Leg Length"]["value"].ToString());
		jsonUserScanData.data13 = proccess_string(obj ["Left Thigh Girth"]["value"].ToString());
		jsonUserScanData.data14 = proccess_string(obj ["Right Thigh Girth"]["value"].ToString());
		jsonUserScanData.data15 = proccess_string(obj ["Left Calf Girth"]["value"].ToString());
		jsonUserScanData.data16 = proccess_string(obj ["Right Calf Girth"]["value"].ToString());
		jsonUserScanData.data17 = proccess_string(obj ["Left Knee Girth"]["value"].ToString());
		jsonUserScanData.data18 = proccess_string(obj ["Right Knee Girth"]["value"].ToString());
		jsonUserScanData.data19 = proccess_string(obj ["Left Minimum Leg Girth"]["value"].ToString());
		jsonUserScanData.data20 = proccess_string(obj ["Right Minimum Leg Girth"]["value"].ToString());
		jsonUserScanData.data21 = proccess_string(obj ["Chest Height"]["value"].ToString());
		jsonUserScanData.data22 = proccess_string(obj ["Bust to Bust"]["value"].ToString());
		jsonUserScanData.data23 = proccess_string(obj ["Back Waist Length"]["value"].ToString());


		jsondata_str = JsonMapper.ToJson(jsonUserScanData);
		Debug.Log (jsondata_str);
	
	}
	//弃用
    public void DATA_Read()
	{Debug.Log("DATA_Read:");
        try
        {
			Debug.Log("DATA_Read: before file:"+scandata_json);
            FileStream file = new FileStream(scandata_json, FileMode.Open);
			Debug.Log("111111111111:"+json_length);
            file.Seek(0, SeekOrigin.Begin);
			file.Read(byData, 0, json_length); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
            //jsondata = System.Text.Encoding.Default.GetString(byData);
           // Debug.Log("th end is:"+jsondata);
			//jsonUserScanData = JsonMapper.ToObject<JsonUserScanData>(System.Text.Encoding.UTF8.GetString(byData));           
            file.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.ToString());
        }
    }

	/****************************************************
	*
	**
	**	开始上传服务器线程
	**
	**
	*****************************************************/
	void UploadData2Service(){
		startUploadDataThread ();

	}
	//弃用
    public void processData()
    {
        if (jsonUserdata == null)
        {
            return;
        }
        PicUrl = jsonUserdata.avatar;
        string filepath = "Assets/Guider/UserData/pic.jpg";
        WebClient mywebclient = new WebClient();
        mywebclient.DownloadFile(PicUrl, filepath);

        LoadByIO();

    }
	//弃用
    private void LoadByIO()
    {
        double startTime = (double)Time.time;
        //创建文件读取流
        FileStream fileStream = new FileStream("Assets/Guider/resources/temp/test.ogg", FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        //创建文件长度缓冲区
        byte[] bytes = new byte[fileStream.Length];
        //读取文件
        fileStream.Read(bytes, 0, (int)fileStream.Length);

        //释放文件读取流
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;

        //创建Texture
        int width = 300;
        int height = 372;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);

       UIencode.mainTexture = texture;

        //创建Sprite
        //  Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        // image.sprite = sprite;

        startTime = (double)Time.time - startTime;
        Debug.Log("IO加载用时:" + startTime);
    }



	/****************************************************
	*
	**
	**		//上传模型数据到文件服务器
	**
	**
	*****************************************************/

	 void startUploadDataThread(){
		string current_exe_path = System.Environment.CurrentDirectory;
		Debug.Log ("thread is started!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

		string token;
		if (Global.isLogin) {
			token = Global.token;
		}
		else{
			//Debug.Log("not login!");
			token = scanQRGetToken;
		}

		upload_raw.Start(token);
		Global.isThreadingRunning = true;
		//upload_raw.IsBackground = false;// 线程随着页面的消失而销毁
	}
		
	// 线程
	void thread_raw(object obj)
	{

		Debug.Log ("Thread starting!");
		string model_download_url,json_download_url;
		//Debug.Log("modelpath： = " + System.Environment.CurrentDirectory+"/output/"+raw_model_file_name);
		int ret = HttpUploadFile(upload_file_url, System.Environment.CurrentDirectory+"/output/"+raw_model_file_name,1);
		if (ret == 0) {
			//Debug.Log ("upload data SUCCESS!");
			raw_upload_ok = true;

		} else {
			ret = HttpUploadFile(upload_file_url, System.Environment.CurrentDirectory+"/output/"+raw_model_file_name,1);
			if (ret == 0) {
				//Debug.Log ("upload data SUCCESS!");
				raw_upload_ok = true;
			}else{
				raw_tmp = "upload model fail!";
				//Debug.Log ("upload model data fail!");
			}
		}
//		Debug.Log("modelpath： = " + System.Environment.CurrentDirectory+"/output/"+raw_json_file_name);
//		int ret2 = HttpUploadFile(upload_file_url, System.Environment.CurrentDirectory+"/output/"+raw_json_file_name,2);
//		if (ret2 == 0) {
//			Debug.Log ("upload data SUCCESS!");
//
//			raw_upload_ok = true;
//
//		} else {
//			ret2 = HttpUploadFile(upload_file_url, System.Environment.CurrentDirectory+"/output/"+raw_json_file_name,2);
//			if (ret2 == 0) {
//				Debug.Log ("upload data SUCCESS!");
//
//				raw_upload_ok = true;
//			} else {
//				filter_tmp = "upload json fail!";
//				Debug.Log ("upload json data fail!");
//			}
//		}

		sendURL2Service(obj.ToString());
		Global.isThreadingRunning = false;
		Debug.Log ("Thread end!");

	}

	public static int HttpUploadFile(string url1, string path,int type)
	{

		Debug.Log("url = "+url1);
		// 设置参数
		HttpWebRequest request = WebRequest.Create(url1) as HttpWebRequest;
		CookieContainer cookieContainer = new CookieContainer();
		request.CookieContainer = cookieContainer;
		request.AllowAutoRedirect = true;
		request.Method = "POST";
		string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
		request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
		byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
		byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

		int pos = path.LastIndexOf("\\");
		string fileName = path.Substring(pos + 1);

		//请求头部信息 
		StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
		byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

		FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

		byte[] bArr = new byte[fs.Length];
		fs.Read(bArr, 0, bArr.Length);
		fs.Close();


		Stream postStream = request.GetRequestStream();
		postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
		postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
		postStream.Write(bArr, 0, bArr.Length);
		postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
		postStream.Close();

		//发送请求并获取相应回应数据
		HttpWebResponse response = request.GetResponse() as HttpWebResponse;
		//直到request.GetResponse()程序才开始向目标网页发送Post请求
		Stream instream = response.GetResponseStream();
		StreamReader sr = new StreamReader(instream, Encoding.UTF8);
		//返回结果网页（html）代码
		if (sr != null) {

			string content = sr.ReadToEnd ();
	
			jsonModel = JsonMapper.ToObject<JsonModelDataReturn> (content);

			if (jsonModel.code == 0) {

				if (type == 1)
					raw_tmp = jsonModel.download;
				if (type == 2)
					filter_tmp = jsonModel.download;
				int status = jsonModel.code;
				//Debug.Log ("type=" + type + "response=" + content);
				return 0;
			} else {
				return -1;
			}
		} else {
			return -1;
		}
	}

	public void sendURL2Service(string token)
	{
//		if (filter_upload_ok && raw_upload_ok)
//			postUserDataToService();
//		else
//			Debug.Log("not ok");
		postUserDataToService(token);
	}

	/****************************************************
	*
	**
	**		//上传用户数据，模型下载地址到服务器
	**
	**
	*****************************************************/
	public void postUserDataToService(string token)
	{
		

		//Debug.Log(" ok"+ adress_url);
		HTTPRequest request = new HTTPRequest(new Uri(getScanDatastr),
			HTTPMethods.Post,
			OnUploadDataFinished);


		//Debug.Log (raw_tmp);
		//Debug.Log (filter_tmp);


		request.SetHeader("Content-Type", "application/x-www-form-urlencoded");
		request.SetHeader("Authorization", "Basic " + EncodeBase64(Encoding.UTF8, token + ":"));
		request.AddField("raw_file", raw_tmp);//模型源文件
		request.AddField("scanner_id", ScannerMachineInfo.scanner_id.ToString()); //测量间唯一id
		request.AddField("fitted_file", "");
		request.AddField("raw_txt", ""); //先不上传任何测量数据文件
		request.AddField("json_data",jsondata_str); //json格式测量数据



		request.Send();

	}

	void OnUploadDataFinished(HTTPRequest request, HTTPResponse response)
	{
		
		Debug.Log ("return:"+response.DataAsText);
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			if (response != null) {
				if (response.StatusCode == 200) {
					Debug.Log ("Request Finished! Text received: " + response.DataAsText);
					JObject json_return = JObject.Parse (response.DataAsText.ToString ());
					if (json_return != null) {
						if (json_return ["code"].ToString () == "0") {
							Debug.Log ("upload success!");
						} else {
							//some deal
					
						}
					}

				} else {

					Debug.Log ("upload fail!");
				}
			}
		}
		Debug.Log ("Thread data callback end!");
	}



}
