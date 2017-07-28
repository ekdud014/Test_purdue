using UnityEngine;
using System.Collections;

public class LeavingBuilding : MonoBehaviour
{
    public static bool leftBuilding = false;
    public static bool postedAction = false;

    void Start()
    {
        leftBuilding = false;
        postedAction = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Character")
        {
            leftBuilding = true;
        }

        if (!postedAction && leftBuilding)
        {
            WebService.PostAction(this, "evac");
        }
    }
}
