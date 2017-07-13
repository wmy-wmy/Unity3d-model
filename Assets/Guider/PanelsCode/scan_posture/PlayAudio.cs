using UnityEngine;
using System.Collections;
 
public class PlayAudio : MonoBehaviour {
 
    //音乐文件
    public AudioSource music;
    //音量
    public float musicVolume;	
	public int welcome_type=0;
 
    void Start() {

    }
	void OnEnable(){

		//设置默认音量
		musicVolume = 0.5F;
		if (!music.isPlaying){
			//播放音乐
			music.Play();
		}
		if(welcome_type == 1)
			StartCoroutine (replay_audio());

	}
	IEnumerator replay_audio(){

		yield return new WaitForSeconds(60f);
		if (!music.isPlaying){
			//播放音乐
			music.Play();
		}
		StartCoroutine (replay_audio());

	}
	void OnGUI() {
 

	
	}
}