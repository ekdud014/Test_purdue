using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDoor : MonoBehaviour {

	public float Speed = 10.0f;
	void Update()
	{
		transform.Rotate (new Vector3(0, 0, Speed * Time.deltaTime));
	}
}
