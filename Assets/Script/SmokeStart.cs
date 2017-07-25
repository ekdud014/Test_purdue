using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeStart : MonoBehaviour {

	public float delayTime = 3;
	public GameObject obj;
	float timer = 0.0f;
	int waitingTime = 5;
	// Use this for initialization
//	IEnumerator  Start () {
//		yield return new WaitForSeconds (delayTime);
//		Instantiate (obj, new Vector3 (-5, 0, -4), Quaternion.AngleAxis(-90, new Vector3(1,0,0)));
//	}
	IEnumerator Delay()
	{
		yield return new WaitForSeconds (delayTime);
	}
	void Update()
	{
		timer += 1;
		Delay ();
		if (timer > waitingTime)
		{
			obj.transform.localScale += new Vector3 (0.001f, 0.001f, 0.001f);
		}
		//UnityEngine.Debug.Log ("dd");
		//obj.transform.localScale += new Vector3 (2, 2, 2);
	}
}
