using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {

	public int count = 0;
	void Update()
	{
		if(count <100)
			transform.position += new Vector3 (0, 0, -0.01f);
		count++;
		//transform.position.x = Speed * Time.deltaTime;
	}
}
