using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using System.IO;
using System.Linq;


public class ViewStatus{
	public static bool age_keyboard_status = true;
	public static bool weight_keyboard_status = false;
	public static bool man_icon_status = false;
	public static bool woman_icon_status = false;

	public static bool age_input_over = false;
	public static bool weight_input_over = false;


	public static void clear(){
		age_input_over = false;
		weight_input_over = false;
		age_keyboard_status = false;
		weight_keyboard_status = false;

	}
}

public class ResourceInitFlag{
	public static bool age_reload_flag = false;
	public static bool weight_reload_flag = false;
	public static bool sex_panel_reload_flag = false;

	public static void clear(){
	
	
	}

}
//测量间硬件信息
public class ScannerMachineInfo{

	public static string scanner_id="0"; //测量间ID
	public static string scanner_address="";  //测量间地址
	public static string install_time="";  //第一次安装时间
	public static string last_calibrate_date=""; //最后一次标定时间

}


public enum WARNING_TYPE: int
{
	WARNING_LONGTIME_NO_ACTION = 0,
	WARNING_RETRY_CONNECT_NETWORK,
	WARNING_CONNECT_NETWORK_FAIL,
	WARNING_CONNECT_NETWORK_SUCCESS,
	WARNING_DATA_PROCESSING

}

public class CurrentPanel{
	public static GameObject curPanel;
	public static WARNING_TYPE warning_type = 0;//默认长久没操作退出警示类型

}
//获取二维码返回的json字段
public class JsonQRCodeData
{
	public int code;
	public string ticket;
	public string uid;

}

//全局变量
public class Global {
	public static int leng_json; 
	public static string token;//此次登录用户的token

	//用户的数据信息
	public static int gender; //性别 1 男 2 女
	public static int age; //年龄
	public static int weight;//体重

	//微信登录后的返回信息
	public static string name; //昵称
	public static int id; //微信ID 服务器上拟定

	//判断登录状态的全局flag
	public static int isScan; //是否测量过
	public static bool isLogin; //是否用微信登录测量
	public static bool isRegist;  //是否注册过
	public static bool isLoadLastDataPage; //是否要从服务器加载数据

	public static bool _stopRequest;
	public static bool isDialogShow;

	public static string model_url = "";

	public static bool isThreadingRunning = false;


	public static void clear(){
		leng_json = 0;
		token = null;
		gender = 0;
		age = 0;
		weight = 0;

		name = null;
		id = 0;

		isScan = 0;
		isLogin = false;
		isRegist = false;
		isLoadLastDataPage = false;

		_stopRequest = false;
		isDialogShow = false;
		Debug.Log ("global clear over");

	}

}


public class RuntimeStatus{
	public static bool LoginExit = false;
	public static bool ScanQRSaveExit = false;
}


//用户信息结构体
public class JsonLoginData
{
	public int code;
	public string token;//     #str类型，token，以后的请求都带上
	public string nickname;//  #str类型，昵称
	public int weight;
	public int gender;//    #int类型，性别，1是男，2是女
	public string birthday;//  #str类型，出生年月日，格式为2017-01-01
	public string avatar;//    #str类型，用户头像
	public string realname;// 
	public string phone;//     #str类型，手机号码
	public string address1;//   #str类型，收货地址
	public string address2;//   #str类型，收货地址
	public int iscan;
	public int id;
}

//用户测量数据结构体
public class JsonUserScanData{
	public  int tall;
	public  int weight;
	public  int data1;
	public  int data2;
	public  int data3;
	public  int data4;
	public  int data5;
	public  int data6;
	public  int data7;
	public  int data8;
	public  int data9;
	public  int data10;
	public  int data11;
	public  int data12;
	public  int data13;
	public  int data14;
	public  int data15;
	public  int data16;
	public  int data17;
	public  int data18;
	public  int data19;
	public  int data20;
	public  int data21;
	public  int data22;
	public  int data23;

}

//public class JsonUserScanData{
//	public  int tall;
//	public  int weight;
//	public  int Neck_Girth;
//	public  int Shoulder_Width;
//	public  int Left_Arm_Long;
//	public  int Right_Arm_Long;
//	public  int Left_Wrist_Girth;
//	public  int Right_Wrist_Girth;
//	public  int Up_Arm_Girth;
//	public  int Up_Body_Long;
//	public  int Front_Cloth_Long;
//	public  int Chest_Girth;
//	public  int Middle_Waist_Girth;
//	public  int Pant_Waist_Girth;
//	public  int Hip_Circumference;
//	public  int General_Gear;
//	public  int Left_Pant_Long;
//	public  int Right_Pant_Long;
//	public  int Big_Leg_Girth;
//	public  int Small_Leg_Girth;
//	public  int Knee_Girth;
//	public  int Ankle_Girth;
//	public  int DEPRECATED; //弃用，保留位置
//	public  int Chest_High_Point;
//	public  int Distance_BP;
//
//}


//获取二维码返回的json字段
public class JsonLastScanData
{
	public int code;
	public string json_data;
	public string fitted_file;

}


public struct UserInfo{
	public int id;
	public string name;
} 


public class GlobalVerboseWatching : MonoBehaviour {



	private bool exit = false;
	private Thread watch_net_status_t;

	public GameObject Dialogpanel;

	// Use this for initialization
	void Start () {
		StartCoroutine (watch_net_status());
		//Debug.Log("path"+Application);
	}


	IEnumerator watch_net_status(){
		while(!exit){
			Debug.Log ("watching the network exit!");
			if (Application.internetReachability == NetworkReachability.NotReachable) {

				Global._stopRequest = true;

				CurrentPanel.warning_type = WARNING_TYPE.WARNING_RETRY_CONNECT_NETWORK;
				NGUITools.SetActive (CurrentPanel.curPanel, false);
				NGUITools.SetActive (Dialogpanel, true);
				Debug.Log ("watching the network!");

			} else {
				if (Global.isDialogShow) {
					NGUITools.SetActive (Dialogpanel, false);
					CurrentPanel.warning_type = WARNING_TYPE.WARNING_CONNECT_NETWORK_SUCCESS;
					NGUITools.SetActive (Dialogpanel, true);
				}

			}
			yield return new WaitForSeconds(1.0f);
		}
	
	}
	// Update is called once per frame
	void Update () {
	
	}
}
