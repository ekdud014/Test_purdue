using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBottle : MonoBehaviour
{

    public GameObject Bottle;
    public GameObject RightHand;

    public static bool click_bottle = false;

    // Use this for initialization
    void Start()
    {
        //print("오른손 위치 : " + RightHand.transform.position.x + ", " + RightHand.transform.position.y + ", " + RightHand.transform.position.z);
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
            if (ClickTowel.click_towel && hit.collider != null && hit.collider.gameObject.Equals(Bottle))
            {
                Bottle.transform.position = RightHand.transform.position;
                Bottle.transform.Translate(new Vector3(0, 0.3f, 0));

                //Towel.transform.position += new Vector3(LeftHand.transform.position.x, LeftHand.transform.position.y + 0.05f, LeftHand.transform.position.z);
                //Towel.transform.position = LeftHand.position;
                print("Click Bottle");
                //print("물병 위치 : " + Bottle.transform.position.x + ", " + Bottle.transform.position.y + ", " + Bottle.transform.position.z);
                click_bottle = true;
            }
        }
        if(click_bottle)
        {
            Bottle.transform.position = RightHand.transform.position;
            Bottle.transform.Translate(new Vector3(0, 0.3f, 0));
        }
    }

}

