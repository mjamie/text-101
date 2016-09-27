using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

	public class EmptyClass : MonoBehaviour 
	{
		public Text text;

		void start ()
		{
			StartCoroutine(Examlpe());
		}

		IEnumerator Examlpe ()
		{
		text.text("ferwferf");
		yield return new WaitForSeconds (5);
			text.text ("ewtf4ergtre");
		}
	}


