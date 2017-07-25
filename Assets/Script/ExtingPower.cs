using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtingPower : MonoBehaviour {

	public GameObject Exting;
	public GameObject Fire;
	public float delayTime = 2;
	private const float IncreaseStep = 2.0f;

	// Use this for initialization
	void Start()
	{
		Exting.SetActive (false);
	}

	IEnumerator delay()
	{
		yield return new WaitForSeconds (delayTime);
		UnityEngine.Debug.Log ("dd");
	}

	// Update is called once per frame
	void Update () {
		Exting.transform.Rotate(Vector3.down, Input.GetAxis("Mouse Y"));
		Exting.transform.Rotate(Vector3.left, Input.GetAxis("Mouse X") / 50.0f);

		if (Input.GetMouseButtonDown(0))
		{
			Exting.SetActive (true);
		}

		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit) && Exting.activeSelf)
		{
			if (hit.distance <= 10.0f)
			{
				//UnityEngine.Debug.Log ("dd");

				delay ();
				Fire.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
				if (Fire.transform.localScale.x < 0.3f) 
				{
					Destroy (Fire);
				}
			}
		}
		
	}

}
