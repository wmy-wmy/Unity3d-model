using UnityEngine;
using System.Collections;

public class KeyBoardButton : MonoBehaviour {

	public int keycode;
	public GameObject obj1;
	public GameObject obj2;
	public UILabel result;

	public GameObject manclick;
	public GameObject womanclick;
	public  static string str1="";
	public  static string str2="";

	public GameObject man;
	public GameObject woman;

	public GameObject bg1;

	public GameObject age_sure_status;
	public GameObject weight_sure_status;


	static int age_length=0;
	static int weight_length=0;

	void OnHover(bool isOver){


		Debug.Log ("press is " + isOver);
		if (isOver) {
			NGUITools.SetActive (bg1,true);
//			if (keycode == 11) {
//				NGUITools.SetActive (bg1,false);
//			}
		}
		if (!isOver) {
			NGUITools.SetActive (bg1,false);
		}
	}



	void OnClick(){

//		Debug.Log ("STR1="+str1+"str2="+str2+"str2 lenth="+str2.Length);
		if (keycode == 0 || keycode == 1 || keycode == 2 ||
		   keycode == 3 || keycode == 4 || keycode == 5 ||
		   keycode == 6 || keycode == 7 || keycode == 8 || keycode == 9) {
//
//			weight_length = (str1 + keycode.ToString ()).Length;
//			age_length = (str2 + keycode.ToString ()).Length;
//			Debug.Log ("bf weight_length="+weight_length);
//			Debug.Log ("bf age_length="+age_length);
			//if (age_length <= 2 && weight_length <= 3) {
				//str1 = str1 + keycode.ToString ();
				if (ViewStatus.weight_keyboard_status) {
					
					if ((str1 + keycode.ToString ()).Length <= 3) {

						str1 = str1 + keycode.ToString ();
						result.text = str1;
		
					}
				} else {
					//result.text = str1;
					
					if ((str2 + keycode.ToString ()).Length <= 2) {
					
						str2 = str2 + keycode.ToString ();
						result.text = str2;


					}

				}

				Debug.Log ("r=" + result.text);
			//}
		}
		if (keycode == 10) {
			if (str1 != null) {
				if (ViewStatus.weight_keyboard_status) {
					
					str1 = "";
					result.text = "0";
				} else {
					str2 = "";
					result.text = "0";
				}


			}
		}
		if (keycode == 11) {
			if (obj1.GetComponent<UIButton> ().name == "enter_age") {

				ViewStatus.age_input_over = true;
				NGUITools.SetActive (age_sure_status,true);

				NGUITools.SetActive (GameObject.Find("keyboard_age"),false);
				ViewStatus.age_keyboard_status = false;
				if (!ViewStatus.weight_input_over) {
					Debug.Log ("weight11");
//					BoxCollider manbox = man.GetComponent<BoxCollider> () as BoxCollider;
//					if (manbox != null) {
//						manbox.enabled = true;
//					}
					ViewStatus.weight_keyboard_status = true;
					NGUITools.SetActive (obj2, true);

				}

			}
			if (obj1.GetComponent<UIButton> ().name == "enter_weight") {
				ViewStatus.weight_input_over = true;
				NGUITools.SetActive (weight_sure_status,true);


//				BoxCollider womanbox = woman.GetComponent<BoxCollider> () as BoxCollider;
//				if (womanbox != null) {
//					womanbox.enabled = true;
//				}
				NGUITools.SetActive (GameObject.Find("keyboard_weight"),false);
				//NGUITools.SetActive (obj2, false);
				ViewStatus.weight_keyboard_status = false;

				if (!ViewStatus.age_input_over) {
					ViewStatus.age_keyboard_status = true;
					NGUITools.SetActive (obj2, true);

				}


			}
			Debug.Log ("1="+ViewStatus.age_input_over+"2="+ViewStatus.weight_input_over+"3="+ViewStatus.age_keyboard_status+"4="+ViewStatus.weight_keyboard_status);
			if (ViewStatus.age_input_over && ViewStatus.weight_input_over && !ViewStatus.age_keyboard_status && !ViewStatus.weight_keyboard_status) {
				Debug.Log ("ENTER IT");
				NGUITools.SetActive (manclick,false);
				BoxCollider manbox = man.GetComponent<BoxCollider> () as BoxCollider;
				if (manbox != null) {
					manbox.enabled = true;
				}

				NGUITools.SetActive (womanclick,false);
				BoxCollider womanbox = woman.GetComponent<BoxCollider> () as BoxCollider;
				if (womanbox != null) {
					womanbox.enabled = true;
				}

			}

			if (keycode == 11) {
				NGUITools.SetActive (bg1,false);
			}

		}
	
	}
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		if (!ViewStatus.age_input_over) {
			NGUITools.SetActive (age_sure_status,false);
		}
		if (!ViewStatus.weight_input_over) {
			NGUITools.SetActive (weight_sure_status,false);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
