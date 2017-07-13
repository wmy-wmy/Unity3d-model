using UnityEngine;
using System.Collections;


public class WaitSecond : MonoBehaviour {


	public GameObject panel1;
	public GameObject panel2;
	public GameObject countpic;
	int i ;

	public GameObject pic1;
	public GameObject pic2;
	public GameObject pic3;
	public GameObject pic4;
	public GameObject pic5;

	public float timer = 1;  
	public float floattimer = 0;
	public bool start=false;
	// Use this for initialization
	void Start () {

	}
	void OnEnable(){
		NGUITools.SetActive(GameObject.Find ("UI Root/ready_time/start_scan"),false);
		i = 5;
		//countpic.GetComponent<UITexture> ().
		//GameObject.Find ("UI Root/ready_time/5").GetComponent<UILabel> ().text = (i).ToString();
		NGUITools.SetActive(pic1, true);	
		Debug.Log ("ready to WaitSecond");
		StartCoroutine (Timer());
	}

	IEnumerator Timer(){
	//	while(!start){

			yield return new WaitForSeconds(1.0f);

			Debug.Log ("ready to WaitSecond panel here"+i);

		NGUITools.SetActive(GameObject.Find ("UI Root/ready_time/"+i),false);
		NGUITools.SetActive(GameObject.Find ("UI Root/ready_time/"+(i-1)),true);
			i = i - 1;
		
			if (i != -1) {
				StartCoroutine (Timer ());
			} else {
				Debug.Log ("ready to break");
				
				NGUITools.SetActive(GameObject.Find ("UI Root/ready_time/"+i),false);
				NGUITools.SetActive(GameObject.Find ("UI Root/ready_time/start_scan"),true);
			StartCoroutine (panelChange());
			}
		//}

	}

	IEnumerator panelChange(){

		yield return new WaitForSeconds(3.0f);
		NGUITools.SetActive(panel1, false);
		NGUITools.SetActive(panel2, true);	
		
	}
	// Update is called once per frame
	void Update () {

	}
}
