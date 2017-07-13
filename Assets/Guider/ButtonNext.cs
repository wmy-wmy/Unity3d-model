using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNext : MonoBehaviour {

	public GameObject panel1;
	public GameObject panel2;
	public GameObject modelpanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
		NGUITools.SetActive(modelpanel, false);
        NGUITools.SetActive(panel1, false);
        NGUITools.SetActive(panel2, true);
        Debug.Log("click here!");
    }
}
