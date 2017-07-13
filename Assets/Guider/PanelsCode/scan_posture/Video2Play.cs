using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

public class Video2Play : MonoBehaviour {

	public MovieTexture movTexture;
	//	public MovieTexture movTexture2;
	int play_flag = 0;
	public float timer = 1.0f;
	public GameObject texture;

	void Start()
	{



		//		Thread watchMovieStatus_thread = new Thread(thread_watch);
		//		watchMovieStatus_thread.Start(); // 

		//StartCoroutine(DownLoadMovie());
	}
	void OnEnable(){
		Debug.Log ("video 2 here");
		GetComponent<Renderer>().material.mainTexture = movTexture;  

		movTexture.loop = true;

		movTexture.Stop ();
		movTexture.Play ();
		StartCoroutine (Timer1());

	}
	IEnumerator Timer1(){
		while(true){

			yield return new WaitForSeconds(8.0f);

			Debug.Log ("paly here");
			movTexture.Stop();
			NGUITools.SetActive (texture,true);
			break;
			//OnGUI ();
		}


	}

	void Update(){

		transform.localScale = new Vector3 (1,1,1);
	}




}
