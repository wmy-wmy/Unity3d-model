﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Again : MonoBehaviour {

	// Use this for initialization
	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnClick()
	{

		RuntimeStatus.ScanQRSaveExit = true;
		NGUITools.SetActive(panel1, false);
		NGUITools.SetActive(panel3, false);
		NGUITools.SetActive(panel2, true);


	}
}
