using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPin : MonoBehaviour
{
    public GameObject Pin;

    public static bool click_pin = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 오브젝트 정보를 담을 변수 생성 
            RaycastHit hit; // 터치 좌표를 담는 변수
            Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition); // 터치한 곳에 ray를 보냄 
            Physics.Raycast(touchray, out hit); // ray가 오브젝트에 부딪힐 경우 
            if (ClickFireExtinguisher.click_FireExtinguisher && hit.collider != null && hit.collider.gameObject.Equals(Pin))
            { 
                print("Click Pin");
                click_pin = true;
            }
        }
        if (click_pin)
        {
            print("Remove Pin");
            Destroy(Pin);
        }
    }

}
