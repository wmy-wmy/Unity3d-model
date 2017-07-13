using UnityEngine;
using System.Collections;

public class login_sucess : MonoBehaviour {


	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;
	public GameObject panel4;
	// Use this for initialization
	void Start () {
		
	}
	void OnEnable(){

		StartCoroutine (Timer());

	}

	IEnumerator Timer(){
		while(true){

			yield return new WaitForSeconds(2.0f);

			Debug.Log ("ready to jump another panel here:"+Global.isScan);
			if (Global.isRegist) {
				if (Global.isScan == 1) {
					Global.isLoadLastDataPage = true;
					NGUITools.SetActive(panel1, false);
					NGUITools.SetActive(panel2, true);
				}
				if (Global.isScan == 0) {
					Global.isLoadLastDataPage = false;
					NGUITools.SetActive(panel1, false);
					NGUITools.SetActive(panel4, true);		
				}

			}
			else{
				Global.isLoadLastDataPage = false;
				NGUITools.SetActive(panel1, false);
				NGUITools.SetActive(panel3, true);
			}


		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
