using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;





public class Enter3DBtn : MonoBehaviour {


    public GameObject panel1;
    public GameObject panel2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnClick(){

		initStatus ();
		Global._stopRequest = true;


		NGUITools.SetActive(panel1, false);
		NGUITools.SetActive(panel2, true);
	}
	void initStatus (){
		Global.isRegist = false;
		Global.isLogin = false;
		Global.isScan = 0;
		Global.isLoadLastDataPage = false;
	
	}
}
