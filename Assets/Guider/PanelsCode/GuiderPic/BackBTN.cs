using UnityEngine;
using System.Collections;

public class BackBTN : MonoBehaviour {

	public GameObject panel1;
	public GameObject panel2;//last data display
	public GameObject panel3;//scan qrcode panel
	public GameObject panel4;//sex select
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


	}
	void OnClick()
	{



		if (Global.isRegist) {
			if (Global.isScan == 1) {
				Global.isLoadLastDataPage = true;
				NGUITools.SetActive(panel1, false);
				NGUITools.SetActive(panel2, true);
			}
			if (Global.isScan == 0) {

				NGUITools.SetActive(panel1, false);
				NGUITools.SetActive(panel3, true);		
			}

		}
		else{
			
			NGUITools.SetActive(panel1, false);
			NGUITools.SetActive(panel4, true);
		}
	}
}
