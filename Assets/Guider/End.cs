using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

	public GameObject welpanel;
	public GameObject thispanel;
	public GameObject modelpanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnClick(){
		RuntimeStatus.ScanQRSaveExit = true;

		NGUITools.SetActive (thispanel,false);
		NGUITools.SetActive (modelpanel,false);
		NGUITools.SetActive (welpanel,true);
	}
}
