using UnityEngine;
using System.Collections;
using Aerowand;

public class ReferencedTransform : MonoBehaviour {
    [SerializeField]
    Tracker tracker;

	Device device;
	static Vector3 aerowandReferencePosition = Vector3.zero;
	static Quaternion aerowandReferenceOrientation = Quaternion.identity;
	static Vector3 sceneReferencePosition = Vector3.zero;
	static Quaternion sceneReferenceOrientation = Quaternion.identity;
	Transform targetTransform;
	Transform aerowandTransform;

	Vector3 defaultPosition;
	Quaternion defaultOrientation;

	void Awake()
	{
		defaultPosition = transform.localPosition;
		defaultOrientation = Quaternion.Euler(0.0f, transform.localRotation.eulerAngles.y, 0.0f);
	}

	IEnumerator Start ()
	{
		targetTransform = new GameObject(gameObject.name + " Reference").transform;
		aerowandTransform = new GameObject(gameObject.name + " Device").transform;
		device = tracker == Tracker.Head ? Device.AerowandHead : Device.AerowandHand;
		if (device.tracker == Tracker.Head)
		{
            foreach (ReferencedTransform referenceTransform in Object.FindObjectsOfType<ReferencedTransform>())
            {
                if (gameObject.transform.parent != referenceTransform.transform.parent)
                {
                    Debug.LogWarning(gameObject.name + " and " + referenceTransform.gameObject.name + " have different parent GameObjects, tracking will not work correctly");
                }
            }
			// Wait until Aerowand has returned non-zero transform data to set reference centers
			while ( device.position == Vector3.zero ) yield return null;
			sceneReferencePosition = defaultPosition;
			sceneReferenceOrientation = defaultOrientation;
			CenterAerowand();

			// reset the scene which has been messed up by having changed the gameobject's position and orientation changed by the VR headset
			UpdateTransform();
			transform.localPosition = defaultPosition;
			transform.localRotation = defaultOrientation;
		} else {
			// Wait until Aerowand has returned non-zero transform data indicating it is receiving data
			while ( device.position == Vector3.zero ) yield return null;
		}
		Camera.onPreCull += LastPossibleUpdateTransform;

		while ( true )
		{
			UpdateTransform();
			yield return new WaitForFixedUpdate();
		}
	}

	void LastPossibleUpdateTransform (Camera cam)
	{
		UpdateTransform();
	}

	void CenterAerowand ()
	{
		aerowandReferencePosition = device.position;
		aerowandReferenceOrientation = Quaternion.Euler(0.0f, device.rotation.eulerAngles.y, 0.0f);
	}

	void UpdateTransform ()
	{
		device.ForceUpdate();
		if ( device.GetButtonUp(Button.Trigger) && device.tracker == Tracker.Head )
		{
			Debug.Log("Resetting offset");
			CenterAerowand();
		}

		// find offset in Aerowand's IRL space
		aerowandTransform.position = aerowandReferencePosition;
		aerowandTransform.rotation = aerowandReferenceOrientation;
		Vector3 aerowandOffsetIrlSpace = aerowandTransform.InverseTransformPoint(device.position);

		// apply Aerowand IRL space offset in Unity space
		targetTransform.position = sceneReferencePosition;
		targetTransform.rotation = sceneReferenceOrientation;
		targetTransform.Translate(aerowandOffsetIrlSpace);

		// find how the Aerowand has reoriented since it's reference was taken
		Quaternion aerowandOrientationOffset = Quaternion.Inverse(aerowandReferenceOrientation) * device.rotation;
		targetTransform.rotation = sceneReferenceOrientation * aerowandOrientationOffset;

		transform.localRotation = targetTransform.rotation;
		transform.localPosition = targetTransform.position;
		
		if (Tracker.Head.Equals(device.tracker)) {
			Camera cam = GetComponent<Camera>();

			Matrix4x4 worldToCameraMatrix = transform.worldToLocalMatrix;
			
			// Approximate offset from the Aerowand sensor to the display for Daydream
			worldToCameraMatrix[1, 3] += 0.104f;
			worldToCameraMatrix[2, 3] += 0.111f;

			// The z axis forward from the camera is negative in camera space,
			// inverting since local space would be postitive in the same rotation
			Vector4 zInvert = worldToCameraMatrix.GetRow(2) * -1.0f;
			worldToCameraMatrix.SetRow(2, zInvert);
			cam.worldToCameraMatrix = worldToCameraMatrix;

			Matrix4x4 viewL = worldToCameraMatrix;
			Matrix4x4 viewR = worldToCameraMatrix;

			float stereoOffset = cam.stereoSeparation / 2.0f;
			viewL[0,3] += stereoOffset;
			viewR[0,3] -= stereoOffset;
            cam.SetStereoViewMatrix(Camera.StereoscopicEye.Left, viewL);
            cam.SetStereoViewMatrix(Camera.StereoscopicEye.Right, viewR);
#if false
			Debug.Log(cam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right));
#endif
		}
	}

	void OnDestroy ()
	{
		Camera.onPreCull -= LastPossibleUpdateTransform;
	}
}
