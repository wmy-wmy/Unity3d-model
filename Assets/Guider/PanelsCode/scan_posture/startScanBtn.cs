using UnityEngine;
using System.Collections;

public class startScanBtn : MonoBehaviour {


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
		//NGUITools.Destroy (panel1);
		NGUITools.SetActive(panel2, true);

	}
}
