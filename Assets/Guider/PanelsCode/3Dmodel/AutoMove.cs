using UnityEngine;
using System.Collections;

public class AutoMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	


	}


	
	// Update is called once per frame
	void Update () {
		transform.localRotation *= Quaternion.Euler (0,30*Time.deltaTime,0);
	
	}
}
