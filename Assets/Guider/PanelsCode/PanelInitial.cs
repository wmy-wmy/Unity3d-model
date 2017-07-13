using UnityEngine;
using System.Collections;

public class PanelInitial : MonoBehaviour {


	public GameObject thispanel;
	// Use this for initialization
	void Start () {
		CurrentPanel.curPanel = thispanel;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
