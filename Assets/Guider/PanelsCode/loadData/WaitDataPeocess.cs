using UnityEngine;
using System.Collections;

public class WaitDataPeocess : MonoBehaviour {

	public GameObject panel1;
	public GameObject panel2;

	public GameObject Animation;
	public GameObject Animation_ok;
	// Use this for initialization
	void Start () {



	}
	void OnEnable(){
		NGUITools.SetActive (Animation_ok, false);
		NGUITools.SetActive (Animation, true);
		//var engine = IronPython.Hosting.Python.CreateEngine();
		//engine.CreateScriptSourceFromString("print 'hello world!'").Execute();



		StartCoroutine (Timer());
	
	}

	IEnumerator Timer(){
		while(true){

			yield return new WaitForSeconds(5.0f);

			Debug.Log ("ready to jump another panel here");
			Global.isLoadLastDataPage = false;

			NGUITools.SetActive (Animation, false);

			NGUITools.SetActive (Animation_ok, true);
			yield return new WaitForSeconds(2.0f);
			NGUITools.SetActive (Animation_ok, false);
			NGUITools.SetActive(panel1, false);
			NGUITools.SetActive(panel2, true);		


		}


	}

	// Update is called once per frame
	void Update () {

	}
}
