using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDoor : MonoBehaviour {

	public float Speed;
	public int count = 0;
	public int direction;	//-1 OR 1

	void Update()
	{
		if (count < 120) {
			count++;
			transform.Rotate (new Vector3 (0, 0, Speed * Time.deltaTime * direction));

		}
	}
}
