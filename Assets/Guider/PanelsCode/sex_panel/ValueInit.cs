using UnityEngine;
using System.Collections;

public class ValueInit : MonoBehaviour {

	public UILabel text1;
	public UILabel text2;

	public GameObject man;
	public GameObject women;

	public GameObject age_window;


	public GameObject man_icon;
	public GameObject women_icon;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable(){
		if (ResourceInitFlag.sex_panel_reload_flag) {
			BoxCollider manbox = man.GetComponent<BoxCollider> () as BoxCollider;
			if (manbox != null)
				manbox.enabled = false;
			BoxCollider womenbox = women.GetComponent<BoxCollider> () as BoxCollider;
			if (womenbox != null)
				womenbox.enabled = false;
		

			NGUITools.SetActive (age_window,true);
			ViewStatus.age_keyboard_status = true;

			NGUITools.SetActive (man_icon,true);
			NGUITools.SetActive (women_icon,true);

			KeyBoardButton.str1 = "";
			KeyBoardButton.str2 = "";

			ResourceInitFlag.sex_panel_reload_flag = false;
		}




		if (ResourceInitFlag.age_reload_flag) {
			if (text2.name == "current_value1") {
				text2.text = "0".ToString ();
				ResourceInitFlag.age_reload_flag = false;
			}
		}
		if (ResourceInitFlag.weight_reload_flag) {
			if (text1.name == "current_value2") {
				text1.text = "0".ToString();
				ResourceInitFlag.weight_reload_flag = false;
			}

		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
