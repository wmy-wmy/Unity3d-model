using UnityEngine;
using System.Collections;

public class ShowWarnning : MonoBehaviour {


	public GameObject warn_text;

	public GameObject dialog;
	public GameObject welpanel;
	public GameObject text;

	public GameObject btn1;
	public GameObject btn2;
	public GameObject btn_center;

	int i=10;
	int j=15;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable(){
		if (CurrentPanel.warning_type == WARNING_TYPE.WARNING_RETRY_CONNECT_NETWORK) {
			Global.isDialogShow = true;
		}

		i = 10;
		j = 15;
		switch(CurrentPanel.warning_type){
		case WARNING_TYPE.WARNING_LONGTIME_NO_ACTION:
			warn_text.GetComponent<UILabel> ().text = "长时间没有操作，是否需要继续？".ToString ();
			break;	
		case WARNING_TYPE.WARNING_RETRY_CONNECT_NETWORK:
			warn_text.GetComponent<UILabel> ().text = "网络连接出现故障，正在尝试重新连接！".ToString ();
			break;
		case WARNING_TYPE.WARNING_CONNECT_NETWORK_FAIL:
			warn_text.GetComponent<UILabel> ().text = "非常抱歉,网络连接失败 \n 请联系服务人员.".ToString ();
			break;
		case WARNING_TYPE.WARNING_CONNECT_NETWORK_SUCCESS:
			warn_text.GetComponent<UILabel> ().text = "网络连接成功,请点击继续使用！".ToString ();
			break;
		case WARNING_TYPE.WARNING_DATA_PROCESSING:
			warn_text.GetComponent<UILabel> ().text = "您上一次的详细身材数据正在计算中，请稍后在微信公众号中查看！".ToString ();
			break;
		default:
			break;
		}

		if (CurrentPanel.warning_type == WARNING_TYPE.WARNING_RETRY_CONNECT_NETWORK) {
			NGUITools.SetActive (btn1,false);
			NGUITools.SetActive (btn2,false);
			StartCoroutine (retryConnectInternet());
		} 
		if(CurrentPanel.warning_type == WARNING_TYPE.WARNING_LONGTIME_NO_ACTION){
			if (!btn1.GetComponent<UIButton> ().isActiveAndEnabled || !btn2.GetComponent<UIButton> ().isActiveAndEnabled) {
				NGUITools.SetActive (btn1,true);
				NGUITools.SetActive (btn2,true);
			}
		
			StartCoroutine (Read2Exit ());
		}
		if(CurrentPanel.warning_type == WARNING_TYPE.WARNING_CONNECT_NETWORK_SUCCESS){
			if (!btn1.GetComponent<UIButton> ().isActiveAndEnabled || !btn2.GetComponent<UIButton> ().isActiveAndEnabled) {
				NGUITools.SetActive (btn1,true);
				NGUITools.SetActive (btn2,true);
			}
		}
		if(CurrentPanel.warning_type == WARNING_TYPE.WARNING_DATA_PROCESSING){
			if (!btn1.GetComponent<UIButton> ().isActiveAndEnabled || !btn2.GetComponent<UIButton> ().isActiveAndEnabled) {
				NGUITools.SetActive (btn1,false);
				//btn2.transform.position = new Vector3(597f,-313f,0f);
				NGUITools.SetActive (btn2,false);
				NGUITools.SetActive (btn_center,true);
			}
		}
			
	}
	IEnumerator retryConnectInternet(){
		yield return new WaitForSeconds(1.0f);

		//GameObject.Find ("UI Root/Dialog/exit/Label").GetComponent<UILabel> ().text = "离开("+(i).ToString()+")";
		warn_text.GetComponent<UILabel>().text = "网络连接出现故障，正在尝试重新连接("+(j).ToString()+")";
		j = j - 1;

		if (j != -1) {
			StartCoroutine (retryConnectInternet ());
		} else {
			Debug.Log ("ready to call someone"+i);
			if (Application.internetReachability != NetworkReachability.NotReachable) {
				warn_text.GetComponent<UILabel> ().text = "网络已恢复连接.".ToString ();
				yield return new WaitForSeconds(1.0f);
				NGUITools.SetActive (dialog,false);
				//NGUITools.SetActive (CurrentPanel.curPanel,true);
				NGUITools.SetActive (welpanel,true);

			}
			warn_text.GetComponent<UILabel> ().text = "非常抱歉,网络连接失败 \n 请联系服务人员.".ToString ();
		}
	}

	IEnumerator Read2Exit(){
		yield return new WaitForSeconds(1.0f);

		Debug.Log ("ready to WaitSecond panel here");

		//GameObject.Find ("UI Root/Dialog/exit/Label").GetComponent<UILabel> ().text = "离开("+(i).ToString()+")";
		text.GetComponent<UILabel>().text = "("+(i).ToString()+")";
		i = i - 1;

		if (i != -1) {
			StartCoroutine (Read2Exit ());
		} else {
			Debug.Log ("ready to break"+i);

			NGUITools.SetActive (dialog,false);
			NGUITools.SetActive (CurrentPanel.curPanel,false);
			NGUITools.SetActive (welpanel,true);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
