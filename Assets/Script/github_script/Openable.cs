using UnityEngine;
using UnityEngine.SceneManagement;

public class Openable : MonoBehaviour
{
    public Vector3 OpenPosition;
    public Vector3 OpenRotation;

    private Vector3 closePosition;
    private Vector3 closeRotation;
    private bool isOpen = false;

    void Start()
    {
        WebService.LastScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance <= 4.0f && hit.collider.transform == transform)
                {
                    if (!isOpen)
                    {
                        OpenObject();
                    }
                    else
                    {
                        CloseObject();
                    }
                }
            }
        }
    }

    void OpenObject()
    {
        closeRotation = transform.rotation.eulerAngles;
        closePosition = transform.position;

        transform.rotation = Quaternion.Euler(OpenRotation.x, OpenRotation.y, OpenRotation.z);
        transform.localPosition = OpenPosition;

        isOpen = true;
    }

    void CloseObject()
    {
        transform.rotation = Quaternion.Euler(closeRotation.x, closeRotation.y, closeRotation.z);
        transform.localPosition = closeRotation;

        isOpen = false;
    }
}
