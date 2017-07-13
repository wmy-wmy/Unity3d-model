using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Bson;
using System.Text;
using System.IO;
using System;
public class Main : MonoBehaviour {


    public GameObject sp;
    public GameObject panel1;
    public GameObject panel2;
    // Use this for initialization
    void Start () {

		CurrentPanel.curPanel = panel1;
        
		readMachineIni ();

    }


	void OnEnable(){
		ReloadSoftwareData ();
	
	}
	public void ReloadSoftwareData (){
		ResourceInitFlag.age_reload_flag = true;
		ResourceInitFlag.weight_reload_flag = true;
		ResourceInitFlag.sex_panel_reload_flag = true;


		ClearSexpanelStatus ();
		ClearGlobalVerbose ();

	}

	void ClearSexpanelStatus(){
		ViewStatus.clear ();

	}

	void ClearGlobalVerbose (){
		
		Global.clear ();
	
	}


	public void readMachineIni (){

		string current_exe_path = System.Environment.CurrentDirectory;
		Debug.Log ("cur = "+current_exe_path);

		string json = File.ReadAllText(current_exe_path+"/orbbec.ini", Encoding.UTF8);
		JObject obj = JObject.Parse (json);

		ScannerMachineInfo.scanner_id = obj ["scanner_machine_id"].ToString ();
		ScannerMachineInfo.scanner_address = obj ["scanner_address"].ToString ();
		ScannerMachineInfo.install_time = obj ["install_time"].ToString ();
		ScannerMachineInfo.last_calibrate_date = obj ["last_calibrate_date"].ToString ();


	//	GameObject.Find ("UI Root/welcome_panel/welcompanel/path").GetComponent<UILabel>().text = obj["last_calibrate_date"].ToString();

		Debug.Log ("ScannerMachineInfo.scanner_id = "+ScannerMachineInfo.scanner_id);
	}
    // Update is called once per frame
    void Update () {

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Debug.Log("touch here");
            if (panel1 != null || panel2 != null)
            {
                Debug.Log("touch down");
                NGUITools.SetActive(panel1, false);
                NGUITools.SetActive(panel2, true);

            }
        }
        

    }
}
