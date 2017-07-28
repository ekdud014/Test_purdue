using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtingSeal : MonoBehaviour {

	public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown (0)) {
			print("sss");
			if (target == this.gameObject) {  //타겟 오브젝트가 스크립트가 붙은 오브젝트라면
				print("ddd");
			} 
		}
	}
}
