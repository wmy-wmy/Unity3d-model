using UnityEngine;
using System.Collections;

public class PanelInit : MonoBehaviour {
	public GameObject video2;
	public GameObject gameob;
	public GameObject thispanel;
	// Use this for initialization
	void Start () {
		CurrentPanel.curPanel = thispanel;
	}
	void OnEnable()
	{
		NGUITools.SetActive (video2,false);
		NGUITools.SetActive (gameob,false);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
