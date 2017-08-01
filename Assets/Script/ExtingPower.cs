using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtingPower : MonoBehaviour {

	public GameObject Exting; //물
	public GameObject Fire;
	public float delayTime = 2;
	private const float IncreaseStep = 2.0f;
	public bool stop = false;
	public bool fire = true;
	public float hitForce = 300f;
	RaycastHit hit;
	public bool bdown = false;    

	// Use this for initialization
	void Start()
	{
		Exting.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
    {
		Exting.transform.Rotate(Vector3.down, Input.GetAxis("Mouse Y"));
		Exting.transform.Rotate(Vector3.left, Input.GetAxis("Mouse X") / 50.0f);
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (ClickPin.click_pin) //소화기에 안전핀을 뽑았으면 물이 나오게
        {
            if (Input.GetMouseButtonDown(0))
            {
                Exting.SetActive(true);
                bdown = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Exting.SetActive(false);
                bdown = false;
            }
        }

        if (fire)
        {
            if (Physics.Raycast(ray, out hit) && Exting.activeSelf)
            {
                if (hit.distance <= 2.0f)
                {
                    //UnityEngine.Debug.Log ("dd");

                    Fire.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
                    if (Fire.transform.localScale.x < 0.3f)
                    {
                        Destroy(Fire);
                        fire = false;
                    }
                }
            }
        }
        if (Physics.Raycast(ray, out hit) && hit.rigidbody != null && bdown)  //만약 고체라면
        {
            hit.rigidbody.AddForce(-hit.normal * hitForce);  //힘을 받은 만큼 움직이게 한다. -방향으로
        }
    }
}
