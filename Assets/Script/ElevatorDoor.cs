using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour {
	public GameObject ElevatorButton;
	public int count = 0;
	public bool click = false;
	public float speed;	//0.02 OR 0.01

	public GameObject smoke;
	int timer = 0;
	int waitingTime = 100;

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) {//엘베 버튼클릭시
			// 오브젝트 정보를 담을 변수 생성 
			RaycastHit hit; 

			// 터치 좌표를 담는 변수
			Ray touchray = Camera.main.ScreenPointToRay (Input.mousePosition); 

			// 터치한 곳에 ray를 보냄 
			Physics.Raycast (touchray, out hit); 
			// ray가 오브젝트에 부딪힐 경우 
			if (hit.collider != null && hit.collider.gameObject.Equals (ElevatorButton)) {
				click = true;
			}
		}

		if (click) {
			if (count < 100)
				transform.position += new Vector3 (speed, 0, 0);
			count++;

			if (timer < waitingTime) {
				timer += 1;
				//Delay ();
				smoke.transform.localScale += new Vector3 (0.002f, 0.002f, 0.002f);

			}

		}
			

	}
}
