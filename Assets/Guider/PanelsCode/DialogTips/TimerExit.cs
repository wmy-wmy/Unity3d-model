using UnityEngine;
using System.Collections;

public class TimerExit : MonoBehaviour {


	public int seconds = 10;
	public GameObject Dialogpanel;
	public GameObject thispanel;
	public int warn_type=0;
	// Use this for initialization
	void Start () {
	
	}

	void OnEnable(){
		CurrentPanel.curPanel = thispanel;
		CurrentPanel.warning_type = getWarnType();
		StartCoroutine (DialogShow ());


	}

	WARNING_TYPE getWarnType(){
		switch (warn_type) {
		case 0:
			return WARNING_TYPE.WARNING_LONGTIME_NO_ACTION;
		case 1:
			return WARNING_TYPE.WARNING_RETRY_CONNECT_NETWORK;
		case 2:
			return WARNING_TYPE.WARNING_CONNECT_NETWORK_FAIL;
		case 3:
			return WARNING_TYPE.WARNING_CONNECT_NETWORK_SUCCESS;
		case 4:
			return WARNING_TYPE.WARNING_DATA_PROCESSING;
		default:
			return WARNING_TYPE.WARNING_LONGTIME_NO_ACTION;
			break;
		}
	}

	IEnumerator DialogShow(){
	
		yield return new WaitForSeconds((float)seconds);
		if (thispanel.name == "display_panel" || thispanel.name == "last_display")
			NGUITools.SetActive (thispanel,false);
		NGUITools.SetActive (Dialogpanel,true);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
