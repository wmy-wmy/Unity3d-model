using UnityEngine;
using System.Collections;
using System.Linq;
using System.Threading;

namespace orbbec{
	
	public class ThreadManager {

		public static ThreadManager instance = null;
		private ThreadManager(){
			start ();
		}
		public static ThreadManager getInstance(){
			if (instance == null)
				return new ThreadManager ();
			else {
				return instance;
			}
		}
		public void startThread(){


		}

		private void start(){
			//Thread _thread = new Thread ();

		}
	}


}

