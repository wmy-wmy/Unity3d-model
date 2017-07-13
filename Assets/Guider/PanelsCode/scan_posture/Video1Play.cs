using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;



public class Video1Play : MonoBehaviour {

 	public MovieTexture movTexture;
//	public MovieTexture movTexture2;
	int play_flag = 0;
	public float timer = 1.0f;
	public GameObject texture1;
	public GameObject texture2;

    void Start()
    {


        
//		Thread watchMovieStatus_thread = new Thread(thread_watch);
//		watchMovieStatus_thread.Start(); // 

        //StartCoroutine(DownLoadMovie());
    }
	void OnEnable(){
		Debug.Log ("enter viceo 1");

		GetComponent<Renderer>().material.mainTexture = movTexture;  

		movTexture.loop = true;

		movTexture.Play ();

		StartCoroutine (Timer1());
	
	}
	IEnumerator Timer1(){
	//	while(true){

			yield return new WaitForSeconds(9.0f);

			Debug.Log ("paly here");
			movTexture.Stop();

			NGUITools.SetActive (texture2,true);


			//OnGUI ();
		//}
	
	
	}

	void Update(){
		
		transform.localScale = new Vector3 (1,1,1);
	}
    void OnGUI()
    {
//	
//		Debug.Log ("OnGUI here");
//
//		Debug.Log ("time1");
//		GUI.DrawTexture (new Rect (150, 150, 200, 200), movTexture, ScaleMode.StretchToFill); 
//		if (play_flag == 0) {
//			movTexture.Play ();
//			play_flag = 1;
//		}
//		GUI.DrawTexture (new Rect (150, 500, 200, 200), movTexture2, ScaleMode.StretchToFill); 
//


		//play_flag = 1;
//       if (GUILayout.Button("播放/继续"))
//        {
//            //播放/继续播放视频
//            if (!movTexture1.isPlaying)
//            {
//				Debug.Log ("paly here");
//                movTexture1.Play();
//            }
//        }
//
//        if (GUILayout.Button("暂停播放"))
//        {
//            //暂停播放
//			Debug.Log ("stop here");
//            movTexture1.Pause();
//        }
//
//        if (GUILayout.Button("停止播放"))
//        {
//            //停止播放
//			Debug.Log ("over here");
//            movTexture1.Stop();
//        }
		

    }
		


}
