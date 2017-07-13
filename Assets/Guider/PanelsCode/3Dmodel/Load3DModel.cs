using UnityEngine;
using System.Collections;

public class Load3DModel : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject model = ModelImporter.Importer.Import("g:\\human.fbx");

		//GameObject.Instantiate (model,transform.position,transform.rotation);
		transform.parent = model.transform;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
