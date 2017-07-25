using UnityEngine;

public class FireAlarmOn : MonoBehaviour
{
    public AudioSource AudioFile;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && AudioFile != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == name)
                {
                    AudioFile.loop = true;
                    AudioFile.Play();
                }
            }
        }
    }
}
