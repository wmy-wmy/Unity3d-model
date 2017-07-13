using UnityEngine;
using System.Collections;





public class ItemClickBtn : MonoBehaviour {


	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;

	public UILabel age;
	public UILabel weight;

	public GameObject man;
	public GameObject women;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnClick(){

		BoxCollider manbox = man.GetComponent<BoxCollider> () as BoxCollider;
		if (manbox != null) {
			manbox.enabled = false;
		}
		BoxCollider womenbox = women.GetComponent<BoxCollider> () as BoxCollider;
		if (womenbox != null) {
			womenbox.enabled = false;
		}

		switch(panel1.GetComponent<UIButton>().name){
		case "click_age":
			Debug.Log ("click_age");
			if (!ViewStatus.age_input_over) {
				age.text = "0";
			}

			KeyBoardButton.str1 = "";
			KeyBoardButton.str2 = "";

			if (!ViewStatus.age_keyboard_status) {
				NGUITools.SetActive (panel2, true);
				ViewStatus.age_keyboard_status = true;

			} else {
//				NGUITools.SetActive (panel2, false);
//				ViewStatus.age_keyboard_status = false;
			}

			if (ViewStatus.weight_keyboard_status) {
				NGUITools.SetActive (panel3, false);
				ViewStatus.weight_keyboard_status = false;
			}
			break;
		case "click_wt":
			Debug.Log ("click_wt");
			if (!ViewStatus.weight_input_over) {
				weight.text = "0";
			}

			KeyBoardButton.str1 = "";
			KeyBoardButton.str2 = "";

			if (!ViewStatus.weight_keyboard_status) {
				NGUITools.SetActive (panel2, true);
				ViewStatus.weight_keyboard_status = true;

			} else {
				//NGUITools.SetActive (panel2, false);
				//ViewStatus.weight_keyboard_status = false;

			}

			if (ViewStatus.age_keyboard_status) {
				NGUITools.SetActive (panel3, false);
				ViewStatus.age_keyboard_status = false;
			}
			break;

		default:
			Debug.Log ("1111111111111111");
			break;
		
		}

	}
}
