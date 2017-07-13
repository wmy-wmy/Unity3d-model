using UnityEngine;
using System.Collections;

public class EndAndRestart : MonoBehaviour {


	public  GameObject thispanel;
	public GameObject targetpanel;


	// Use this for initialization
	void Start () {
	
	}
	void OnEnable(){

		//ReloadSoftwareData ();

	}


	// Update is called once per frame
	void Update () {
	
	}


	void OnClick(){
		NGUITools.SetActive(thispanel, false);
		NGUITools.SetActive(targetpanel, true);

	}
}
