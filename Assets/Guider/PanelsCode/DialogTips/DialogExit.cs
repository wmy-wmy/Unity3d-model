using UnityEngine;
using System.Collections;



public class DialogExit : MonoBehaviour {


	public GameObject dialog;
	public GameObject welpanel;
	public GameObject text;
	public GameObject center_btn;
	int i=10;

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
		NGUITools.SetActive (welpanel,true);
		if (center_btn != null) {
			NGUITools.SetActive (center_btn,false);
		}
	}
}
