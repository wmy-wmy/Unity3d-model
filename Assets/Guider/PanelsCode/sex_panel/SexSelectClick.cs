using UnityEngine;
using System.Collections;
using System.Threading;

public class SexSelectClick : MonoBehaviour {

	public GameObject panel1;
	public GameObject current_obj;
	public GameObject panel2;
	public GameObject age_value;
	public GameObject weight_value;
	public GameObject btn_hover;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnEnable(){
		if (Global.gender == 1) {
			if (btn_hover.name == "man_hover") {
				NGUITools.SetActive (btn_hover, true);
			}
			if (btn_hover.name == "woman_hover") {
				NGUITools.SetActive (btn_hover, false);
			}
		} else if (Global.gender == 2) {
			if (btn_hover.name == "woman_hover") {
				NGUITools.SetActive (btn_hover, true);
			}
			if (btn_hover.name == "man_hover") {
				NGUITools.SetActive (btn_hover, false);
			}
		} else {
			NGUITools.SetActive (btn_hover, false);
		}



	}

	void OnClick(){
		if (ViewStatus.age_input_over && ViewStatus.weight_input_over) {

			NGUITools.SetActive (btn_hover, true);
			Debug.Log("btn_hover=");
			if (current_obj.GetComponent<UIButton> ().name == "woman") {
				if (Global.gender == 1) {
					NGUITools.SetActive (GameObject.Find("man_hover"), false);
				}
				Global.gender = 2;
			}
			if (current_obj.GetComponent<UIButton> ().name == "man") {
				if (Global.gender == 2) {
					NGUITools.SetActive (GameObject.Find("woman_hover"), false);
				}
				Global.gender = 1;
			}
				
			Global.age = int.Parse (age_value.GetComponent<UILabel> ().text);
			string ws = weight_value.GetComponent<UILabel> ().text.ToString ();
			Global.weight = int.Parse (ws);;//int.Parse (weight_value.GetComponent<UILabel> ().text);
			Debug.Log("11111111111111111="+Global.weight);
			if(current_obj.GetComponent<UIButton>().name == "woman"){
				Global.gender = 2;
			}
			if(current_obj.GetComponent<UIButton>().name == "man"){
				Global.gender = 1;
			}

			StartCoroutine (jumpPanel());



		} else {


		}

	}
	IEnumerator jumpPanel(){

		yield return new WaitForSeconds (1.0f);

		Debug.Log ("age is " + Global.age);
		NGUITools.SetActive (panel1, false);
		NGUITools.SetActive (panel2, true);
	}


}
