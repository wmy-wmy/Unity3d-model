using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Security;
using System.IO;
using System.Text;
using System;
using System.Linq;
using LitJson;
using BestHTTP;
using BestHTTP.Cookies;
using System.Threading;

public class JsonModelDataReturn
{
    public int code;
    public string download;

}

public class UploadData : MonoBehaviour {
    public static JsonModelDataReturn jsonModel;
    public static string raw_file_url;
    public static string fitted_file_url;
    public static string adress_url = "https://scanner-test.orbbec.me/measurement/data";
   // public static string adress_url = "http://10.10.6.222/measurement/data";

    public static string raw_tmp;
    public static string filter_tmp;

    public bool raw_upload_ok;
    public bool filter_upload_ok ;

    // Use this for initialization
    void Start () {

       
      
    }
	void OnEnable(){
		raw_file_url = "http://app-test.orbbec.me/mytool/upload_scanner_raw_file/";
		fitted_file_url = "http://app-test.orbbec.me/mytool/upload_scanner_fitted_file/";
		Debug.Log("raw_file_url = " + raw_file_url);
		raw_upload_ok = false;
		filter_upload_ok = false;
		Thread upload_raw = new Thread(thread_raw);
		upload_raw.Start(); //  
	
	}
    // Update is called once per frame
    void Update () {
		
	}

    void thread_raw()
    {
        Debug.Log("raw_file_url = " + raw_file_url);
        int ret = HttpUploadFile(raw_file_url, "Assets/Guider/testzip",1);
        if (ret == 0)
        {
            raw_upload_ok = true;
        }
        Debug.Log("fitted_file_url = " + fitted_file_url);
        ret = HttpUploadFile(fitted_file_url, "Assets/Guider/pose.png", 2);
        if (ret == 0)
        {
            filter_upload_ok = true;
            
        }
        sendURL2Service();

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
        string content = sr.ReadToEnd();

        jsonModel = JsonMapper.ToObject<JsonModelDataReturn>(content);
        if(type == 1)
            raw_tmp = jsonModel.download;
        if (type == 2)
            filter_tmp = jsonModel.download;
        int status = jsonModel.code;
        Debug.Log("type="+type+"response="+content);
        return status;
    }

    public void sendURL2Service()
    {
        if (filter_upload_ok && raw_upload_ok)
            postUserDataToService();
        else
            Debug.Log("not ok");
    }

    public void postUserDataToService()
    {
        Debug.Log(" ok"+ adress_url);
        HTTPRequest request = new HTTPRequest(new Uri(adress_url),
        HTTPMethods.Post,
        OnRequestFinished);

        Debug.Log(" raw_tmp" + raw_tmp);
        Debug.Log(" filter_tmp" + filter_tmp);
        //  request.setHeader(Global.token_uid);
        //  request.SetHeader("Content-Type", "application/json");
        request.SetHeader("Content-Type", "application/x-www-form-urlencoded");
        request.SetHeader("Authorization", "Basic " + EncodeBase64(Encoding.UTF8, Global.token + ":"));
        request.AddField("raw_file", raw_tmp);
        request.AddField("scanner_id", "23");
        request.AddField("fitted_file", filter_tmp);
        request.AddField("json_data", jsonModel.download);
        // request.AddField("birthday", birStr.text);

        request.Send();

    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
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
}
