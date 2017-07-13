using UnityEngine;
using System.Collections;

public class DialogOK : MonoBehaviour {

	public GameObject dialog;
	public GameObject welpanel;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable(){


	}
	// Update is called once per frame
	void Update () {
	
	}
	void OnClick(){

		Global.isDialogShow = false;
		NGUITools.SetActive (dialog,false);
		NGUITools.SetActive (CurrentPanel.curPanel,false);
		NGUITools.SetActive (CurrentPanel.curPanel,true);
		if (CurrentPanel.warning_type == WARNING_TYPE.WARNING_CONNECT_NETWORK_SUCCESS) {
			NGUITools.SetActive (welpanel, true);
		}
			
	}
}
