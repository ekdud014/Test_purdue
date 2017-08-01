using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDoor : MonoBehaviour {

	public float Speed;
	public int count = 0;
	public int direction;	//-1 OR 1
	public GameObject player;

	void Update()
	{
		var heading = this.transform.position - player.transform.position;
		if (heading.sqrMagnitude < 9.0f) {
			if (count < 120) {
				count++;
				transform.Rotate (new Vector3 (0, 0, Speed * Time.deltaTime * direction));

			}

		}

	}
}
