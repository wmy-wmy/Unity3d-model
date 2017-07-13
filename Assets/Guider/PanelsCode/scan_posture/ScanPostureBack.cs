using UnityEngine;
using System.Collections;

public class ScanPostureBack : MonoBehaviour {

	// Use this for initialization
	public GameObject panel1;
	public GameObject panel2;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnClick()
	{
		
		NGUITools.SetActive(panel1, false);
		NGUITools.SetActive(panel2, true);
	}
}
