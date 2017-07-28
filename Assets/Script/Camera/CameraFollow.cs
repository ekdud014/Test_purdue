using UnityEngine;
using System.Collections;

public class CameraFollow: MonoBehaviour {

	public Transform target;            // 추적할 타겟 게임오브젝트의 Transform 변수.
	public float dist = 1.0f;          // 카메라와의 일정 거리.
	public float height = 1.0f;         // 카메라와의 높이 설정.
	public float dampRotate = 5.0f;     // 부드러운 회전을 위한 변수.

	private Transform tr;               // 카메라 자신의 Transform 변수.


	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform>(); // 카메라 자신의 Transform 을 할당.
	}


	/* 
     * 추적할 타겟의 이동이 종료된 이후에 카메라가 추적하기 위해 LateUpdate() 사용.
     */
	void LateUpdate()
	{
		// 카메라 Y축을 타겟의 Y축 회전각도로 부드럽게 회전.
		// Mathf.LerpAngle(a,b,t) : t시간동안 a부터 b까지 변경되는 각도.
		float currYAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y-180, dampRotate * Time.deltaTime);
		float currXAngle = Mathf.LerpAngle(target.eulerAngles.x, tr.eulerAngles.x, dampRotate * Time.deltaTime);
		// 쿼터니언 데이터 타입으로 변환
		Quaternion rot = Quaternion.Euler(-currXAngle, currYAngle, 0);

		// 카메라의 위치를 타겟이 회전한 각도만큼 회전한 이후, dist만큼 뒤쪽으로 배치하고 height 만큼 위로 올림.
		tr.position = target.position - (rot * Vector3.forward * dist)/20;

		// 카메라가 타겟 게임 오브젝트를 바라보게 설정.
		tr.LookAt(target);
	}

}
