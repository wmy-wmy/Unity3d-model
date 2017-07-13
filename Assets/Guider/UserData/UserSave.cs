using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using BestHTTP.Cookies;
using System;
using System.IO;
using System.Text;


public class UserSave : MonoBehaviour {


    public UILabel genderStr;
    public UILabel birStr;

    public UILabel gender;
    public UILabel bir;


    private string post_url;
    // Use this for initialization
    void Start () {
        //post_url = "https://scanner-test.orbbec.me/user/edit";
        post_url = "http://10.10.6.222/user/edit";
        Debug.Log("uid = "+ Global.token);

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        Debug.Log("click  UserModify here!");
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/modify"), true);
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/save"), false);

        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/genderInput"), false);
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/birInput"), false);

        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/gendervalue"), true);
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/birvalue"), true);




        gender.text = genderStr.text;
        bir.text = birStr.text;

        postUserDataToService();

        Debug.Log("click  3 here!");
    }

    public void postUserDataToService() {
             HTTPRequest request = new HTTPRequest(new Uri(post_url),
             HTTPMethods.Post,
             OnRequestFinished);
        //  request.setHeader(Global.token_uid);
        request.SetHeader("Content-Type", "application/json");
        request.SetHeader("Authorization","Basic "+EncodeBase64(Encoding.UTF8, Global.token+":"));
        request.AddField("gender", genderStr.text);
        request.AddField("birthday", birStr.text);
      /*  string json = "{\"birthday\":\""+ birStr.text + "\",\"gender\":\"" + genderStr.text + "\"}";


        String encoderJson = System.Web.HttpUtility.UrlEncode.encode(json, HTTP.UTF_8);
        StringEntity se = new StringEntity(encoderJson);
        se.setContentType("text/json");
        se.setContentEncoding(new BasicHeader(HTTP.CONTENT_TYPE, "application/json"));
        request.setEntity(se);*/
        request.Send();

    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
     {
// 　　    Debug.Log("Request Finished! Text received: " + response.DataAsText);
     }

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

    /*
    public static string PostJson(string url, string jsonParam, string encode)
    {
        string strURL = url;
        System.Net.HttpWebRequest request;
        request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
        request.Method = "POST";
        request.ContentType = "application/json;charset=" + encode.ToUpper();
        string paraUrlCoded = jsonParam;
        byte[] payload;
        payload = System.Text.Encoding.GetEncoding(encode.ToUpper()).GetBytes(paraUrlCoded);
        request.ContentLength = payload.Length;
        Stream writer = request.GetRequestStream();
        writer.Write(payload, 0, payload.Length);
        writer.Close();
        System.Net.HttpWebResponse response;
        response = (System.Net.HttpWebResponse)request.GetResponse();
        System.IO.Stream s;
        s = response.GetResponseStream();
        string StrDate = "";
        string strValue = "";
        StreamReader Reader = new StreamReader(s, Encoding.GetEncoding(encode.ToUpper()));
        while ((StrDate = Reader.ReadLine()) != null)
        {
            strValue += StrDate + "\r\n";
        }
        return strValue;
    }

    public static string PostMoths(string url, string param)
    {
        string strURL = url;
        System.Net.HttpWebRequest request;
        request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
        request.Method = "POST";
        request.ContentType = "application/json;charset=UTF-8";
        string paraUrlCoded = param;
        byte[] payload;
        payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
        request.ContentLength = payload.Length;
        Stream writer = request.GetRequestStream();
        writer.Write(payload, 0, payload.Length);
        writer.Close();
        System.Net.HttpWebResponse response;
        response = (System.Net.HttpWebResponse)request.GetResponse();
        System.IO.Stream s;
        s = response.GetResponseStream();
        string StrDate = "";
        string strValue = "";
        StreamReader Reader = new StreamReader(s, Encoding.UTF8);
        while ((StrDate = Reader.ReadLine()) != null)
        {
            strValue += StrDate + "\r\n";
        }
        return strValue;
    }
    */
}
