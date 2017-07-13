using UnityEngine;
using System.Collections;

public class SelectNotice : MonoBehaviour {


	public GameObject age_shinning;
	public GameObject weight_shinning;
	GameObject item;
	public int type=0;
	bool shinning_flag = false;
	bool isEnable = false;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable(){
		item = age_shinning;
		StartCoroutine (shine());
	}

	IEnumerator shine(){
		
		yield return new WaitForSeconds (0.5f);

		if(!isEnable)
			NGUITools.SetActive (item,shinning_flag);

		if (shinning_flag)
			shinning_flag = false;
		else if(!false)
			shinning_flag = true;
		if (type == 0) {
			
		}
		if (ViewStatus.age_keyboard_status) {
			item = age_shinning;
			isEnable = false;
			NGUITools.SetActive (weight_shinning, false);
			StartCoroutine (shine ());

		} else if (ViewStatus.weight_keyboard_status) {
			item = weight_shinning;
			isEnable = false;
			NGUITools.SetActive (age_shinning, false);
			StartCoroutine (shine ());
		} else {
			NGUITools.SetActive (age_shinning, false);
			NGUITools.SetActive (weight_shinning, false);
			isEnable = true;
			StartCoroutine (shine ());
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
