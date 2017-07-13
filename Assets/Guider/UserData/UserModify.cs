using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserModify : MonoBehaviour {

    public UILabel genderStr;
    public UILabel birStr;

    public UILabel gender;
    public UILabel bir;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        Debug.Log("click  UserModify here!");
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/modify"), false);
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/save"), true);

        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/gendervalue"), false);
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/birvalue"), false);



        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/genderInput"), true);
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/display_panel/Window1/birInput"), true);
        genderStr.text = gender.text;
        birStr.text = bir.text;
        Debug.Log("click  3 here!");
    }


}
