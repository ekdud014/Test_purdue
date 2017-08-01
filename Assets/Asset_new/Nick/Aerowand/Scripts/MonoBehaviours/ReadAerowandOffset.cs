using UnityEngine;
using UnityEngine.UI;
using System;
using Aerowand;

[RequireComponent ( typeof ( Text ) ) ]
public class ReadAerowandOffset : MonoBehaviour
{
#if false
    Device aerowandHead;
	Device aerowandHand;
	Text textObject;
   
    void Awake () {
		textObject = GetComponent<Text> () as Text;
        aerowandHead = Device.AerowandHead;
        aerowandHand = Device.AerowandHand;
	}

    void Update () {
		Vector3 headPositionHeadSpace = aerowandHead.gameObject.transform.InverseTransformPoint ( aerowandHead.position );
		Vector3 handPositionHeadSpace = aerowandHead.gameObject.transform.InverseTransformPoint ( aerowandHand.position );
		Vector3 offsetPosition = headPositionHeadSpace - handPositionHeadSpace;
		Quaternion offsetRotation = Quaternion.Inverse ( aerowandHead.rotation ) * aerowandHand.rotation;
		textObject.text = 
			"--- Device Offset Head Space ---" + Environment.NewLine
			+ offsetPosition + Environment.NewLine
			+ "--- Orientation Offset ---" + Environment.NewLine
			+ offsetRotation.eulerAngles;
		;
	}
#endif
}
