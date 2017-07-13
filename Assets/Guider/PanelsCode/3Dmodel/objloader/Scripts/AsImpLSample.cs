using UnityEngine;
using System.IO;
using System.Text;
using System;


namespace AsImpL
{
    namespace Examples
    {
        /// <summary>
        /// Demonstrate how to load a model with ObjectImporter.
        /// </summary>
        public class AsImpLSample : MonoBehaviour
        {
            public string filePath = "";
			public int loadmodel_type = 0;
            private ObjectImporter objImporter;

            private void Awake()
            {
#if (UNITY_ANDROID || UNITY_IPHONE)
                filePath = Application.persistentDataPath + "/" + filePath;
#endif
                objImporter = gameObject.AddComponent<ObjectImporter>();

				if (loadmodel_type == 0) { //load model from the local path
					string current_exe_path = System.Environment.CurrentDirectory;
					Debug.Log ("cur = " + current_exe_path);
					filePath = current_exe_path + "/output/body.obj";

					if (File.Exists (filePath)) {
						Debug.Log ("model exist!");
					} else {
						Debug.Log ("model not exist!");
						filePath = "output/body.obj";
					}
				}

            }
			private void OnEnable ()
			{


				if (loadmodel_type == 1) {
					//filePath = "http://appbucket.test.orbbec.me/test/scanner/standard_model/22.obj";
					Debug.Log ("network url = " + Global.model_url);
					if (Global.model_url != null)
						filePath = Global.model_url;
					else {
						if (Global.gender == 1) {
							filePath = "http://appbucket.test.orbbec.me/test/scanner/standard_model/man.obj";
						} else {
							filePath = "http://appbucket.test.orbbec.me/test/scanner/standard_model/woman.obj";
						}

					}

				}

				Debug.Log ("filepath url = " + filePath);

				ImportOptions options = new ImportOptions();
				options.modelScaling = 1f;
				options.zUp = false;

				GameObject model1obj = GameObject.Find ("UI Root/display_panel/Orc Pivot/Orc/MyObject");
				if (model1obj) {
					Destroy (model1obj);
				}
				GameObject model2obj = GameObject.Find ("UI Root/last_display/Orc Pivot/Orc/MyObject");
				if (model2obj) {
					Destroy (model2obj);
				}

				objImporter.ImportModelAsync("MyObject", filePath, transform, options);


			}



			void Load3DModelFromService(){



			}
            private void Start()
            {
               
            }
			void Update(){
				foreach(Transform tran in GetComponentsInChildren<Transform>()){//遍历当前物体及其所有子物体
					tran.gameObject.layer = 5;//更改物体的Layer层
				}
				transform.localRotation *= Quaternion.Euler (0,60*Time.deltaTime,0); //旋转
			}


        }
    }
}
