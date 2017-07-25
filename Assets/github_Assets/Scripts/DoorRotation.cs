using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    public Vector3 PivotPoint = new Vector3(0.0f, 0.0f, -6.88f);
    public float Speed = 10.0f;

    void Update()
    {
        transform.RotateAround(PivotPoint, Vector3.up, Speed * Time.deltaTime);
    }
}