using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtingHold : MonoBehaviour {

	public GameObject target;
	public Camera cam;
	public Vector3 cam_v;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		//cam_v = cam.transform.LookAt;

		if (Input.GetMouseButtonDown (0)) {
			if (target == this.gameObject) {  //타겟 오브젝트가 스크립트가 붙은 오브젝트라면
				//transform.position = new Vector3 (cam_v.x, cam_v.y, cam_v.z);
			} 
		}

	}
}
