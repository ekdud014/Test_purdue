using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTowel : MonoBehaviour
{

    public GameObject Towel;
    public GameObject LeftHand;

    public static bool click_towel = false;

    // Use this for initialization
    void Start ()
    {
        //print("왼손 위치 : " + LeftHand.transform.position.x + ", " + LeftHand.transform.position.y + ", " + LeftHand.transform.position.z);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 오브젝트 정보를 담을 변수 생성 
            RaycastHit hit; // 터치 좌표를 담는 변수
            Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition); // 터치한 곳에 ray를 보냄 
            Physics.Raycast(touchray, out hit); // ray가 오브젝트에 부딪힐 경우 
            if (hit.collider != null && hit.collider.gameObject.Equals(Towel))
            {
                Towel.transform.position = LeftHand.transform.position;
                Towel.transform.Translate(new Vector3(0, 0.3f, 0));

                //Towel.transform.position += new Vector3(LeftHand.transform.position.x, LeftHand.transform.position.y + 0.05f, LeftHand.transform.position.z);
                //Towel.transform.position = LeftHand.position;
                print("Click Towel");
                //print("수건 위치 : " + Towel.transform.position.x + ", " + Towel.transform.position.y + ", " + Towel.transform.position.z);
                click_towel = true;
            }
        }
        if(click_towel)
        {
            Towel.transform.position = LeftHand.transform.position;
            Towel.transform.Translate(new Vector3(0, 0.3f, 0));
        }
    }

}

