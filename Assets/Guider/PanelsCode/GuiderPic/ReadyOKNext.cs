using UnityEngine;
using System.Collections;

public class ReadyOKNext : MonoBehaviour {

	public GameObject panel1;
	public GameObject panel2;
	public GameObject text;
	public GameObject hover;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnEnable(){
		NGUITools.SetActive(hover, false);
	
	}

	void OnClick()
	{
		NGUITools.SetActive(hover, true);
		NGUITools.SetActive(panel1, false);
		//NGUITools.SetActive(text, false);
		NGUITools.SetActive(panel2, true);

	}
}
