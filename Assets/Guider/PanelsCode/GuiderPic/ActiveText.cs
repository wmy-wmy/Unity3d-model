using UnityEngine;
using System.Collections;

public class ActiveText : MonoBehaviour {

	public GameObject tips;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable(){
		NGUITools.SetActive (tips, true);
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
