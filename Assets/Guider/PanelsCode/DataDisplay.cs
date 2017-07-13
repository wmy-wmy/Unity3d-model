using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {

        NGUITools.SetActive(GameObject.Find("UI Root/Camera/data_display"), false);
        NGUITools.SetActive(GameObject.Find("UI Root/Camera/end"), true);
        Debug.Log("click dis here!");
    }
}
