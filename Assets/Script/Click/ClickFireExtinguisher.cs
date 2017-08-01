using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFireExtinguisher : MonoBehaviour
{
    public GameObject FireExtinguisher;
    public GameObject LeftHand;

    public static bool click_FireExtinguisher = false;

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
            if (ClickBottle.click_bottle && hit.collider != null && hit.collider.gameObject.Equals(FireExtinguisher))
            {
                FireExtinguisher.transform.position = LeftHand.transform.position;
                FireExtinguisher.transform.Translate(new Vector3(0, 0.3f, 0.15f));

                //Towel.transform.position += new Vector3(LeftHand.transform.position.x, LeftHand.transform.position.y + 0.05f, LeftHand.transform.position.z);
                //Towel.transform.position = LeftHand.position;
                print("Click FireExtinguisher");
                //print("소화기 위치 : " + FireExtinguisher.transform.position.x + ", " + FireExtinguisher.transform.position.y + ", " + FireExtinguisher.transform.position.z);
                click_FireExtinguisher = true;
            }
        }
        if (click_FireExtinguisher)
        {
            FireExtinguisher.transform.position = LeftHand.transform.position;
            FireExtinguisher.transform.Translate(new Vector3(0, -0.3f, 0.15f));
            FireExtinguisher.transform.localScale = new Vector3(7, 7, 7);

            if(ClickHorn.click_horn)
            {
                FireExtinguisher.transform.Translate(new Vector3(0, -1.5f, 0));
            }
        }
    }

}
