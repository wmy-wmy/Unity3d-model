using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using LitJson;
using System.Net;
using System.Net.Sockets;


public class EnglishStringValue{
	public static string welcome_str1_label;
	public static string welcome_str2_label;
	public static string login_single_click_label;
	public static string login_single_click_tips_label;
	public static string login_qrcode_tips_label;
	public static string login_success_tips_label;
	public static string sexpanel_title_label;
	public static string sexpanel_age_label;
	public static string Guiderpic_tips_1_label;
	public static string Guiderpic_tips_2_label;
	public static string Guiderpic_readybtn_label;
	public static string posture_title_label;
	public static string posture_startbtn_label;
	public static string readytime_title_label;
	public static string loaddata_title_label;

	public static string display_nickname_label;
	public static string display_againbtn_label;
	public static string display_endbtn_label;

	//显示数据item
	public static string display_tall_label;
	public static string display_weight_label;
	public static string display_bmi_label;

	//测量各个部位的数据
	public static string display_1_label;
	public static string display_2_label;
	public static string display_3_label;
	public static string display_4_label;
	public static string display_5_label;
	public static string display_6_label;
	public static string display_7_label;
	public static string display_8_label;
	public static string display_9_label;
	public static string display_10_label;
	public static string display_11_label;
	public static string display_12_label;
	public static string display_13_label;
	public static string display_14_label;
	public static string display_15_label;
	public static string display_16_label;
	public static string display_17_label;
	public static string display_18_label;
	public static string display_19_label;
	public static string display_20_label;
	public static string display_21_label;
	public static string display_22_label;
	public static string display_23_label;
	public static string display_24_label;
	public static string display_25_label;

}


public class ChinesesStringValue{
	public static string welcome_str1_label;
	public static string welcome_str2_label;
	public static string login_single_click_label;
	public static string login_single_click_tips_label;
	public static string login_qrcode_tips_label;
	public static string login_success_tips_label;
	public static string sexpanel_title_label;
	public static string sexpanel_age_label;
	public static string Guiderpic_tips_1_label;
	public static string Guiderpic_tips_2_label;
	public static string Guiderpic_readybtn_label;
	public static string posture_title_label;
	public static string posture_startbtn_label;
	public static string readytime_title_label;
	public static string loaddata_title_label;

	public static string display_nickname_label;
	public static string display_againbtn_label;
	public static string display_endbtn_label;

	//显示数据item
	public static string display_tall_label;
	public static string display_weight_label;
	public static string display_bmi_label;

	//测量各个部位的数据
	public static string display_1_label;
	public static string display_2_label;
	public static string display_3_label;
	public static string display_4_label;
	public static string display_5_label;
	public static string display_6_label;
	public static string display_7_label;
	public static string display_8_label;
	public static string display_9_label;
	public static string display_10_label;
	public static string display_11_label;
	public static string display_12_label;
	public static string display_13_label;
	public static string display_14_label;
	public static string display_15_label;
	public static string display_16_label;
	public static string display_17_label;
	public static string display_18_label;
	public static string display_19_label;
	public static string display_20_label;
	public static string display_21_label;
	public static string display_22_label;
	public static string display_23_label;
	public static string display_24_label;
	public static string display_25_label;

}
public class StringValue : MonoBehaviour {
	ChinesesStringValue longstr;
	private char[] charData;//= new char[1000];
	private byte[] byData;//= new byte[100];

	// Use this for initialization
	void Start () {
		Read ();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Read()
	{
		
		try
		{
			FileStream file = new FileStream("Assets/Guider/language/language.txt", FileMode.Open);
			file.Seek(0, SeekOrigin.Begin);
			file.Read(byData, 0, Global.leng_json); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
			string jsondata = System.Text.Encoding.Default.GetString(byData);
			Debug.Log(jsondata);
			longstr = JsonMapper.ToObject<ChinesesStringValue>(System.Text.Encoding.UTF8.GetString(byData));           
			file.Close();
		}
		catch (IOException e)
		{
			Console.WriteLine(e.ToString());
		}

		Debug.Log ("");
	}




}
