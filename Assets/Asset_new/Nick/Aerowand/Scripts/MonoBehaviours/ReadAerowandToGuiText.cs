using UnityEngine;
using UnityEngine.UI;
using System;
using Aerowand;

[RequireComponent ( typeof ( Text ) ) ]
public class ReadAerowandToGuiText : MonoBehaviour {
    [SerializeField]
    Tracker tracker;
    [SerializeField]
    String deviceName = "";
	Text textObject;
    Device aerowand;

	void Awake () {
		textObject = GetComponent<Text> () as Text;
        switch (tracker)
        {
            case Tracker.Head:
                aerowand = Device.AerowandHead;
                break;
            case Tracker.Pointer:
                aerowand = Device.AerowandHand;
                break;
            default:
                break;
        }
	}

	void Update () {
        Vector3 positionInCm = aerowand.positionRaw * 100.0f;
        positionInCm.z = positionInCm.z * -1.0f;
        Vector3 orientationPYR = aerowand.rotation.eulerAngles;
        float roll = orientationPYR.x;
        orientationPYR.x = orientationPYR.y;
        orientationPYR.y = roll;

        textObject.text = 
			"Aerowand " + deviceName + Environment.NewLine
			+ "Position (cm)" + Environment.NewLine
			+ positionInCm + Environment.NewLine
			+ "Orientation (deg)" + Environment.NewLine
			+ orientationPYR + Environment.NewLine
            ;
	}
}
