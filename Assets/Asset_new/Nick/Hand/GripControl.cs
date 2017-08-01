using UnityEngine;
using Aerowand;

[RequireComponent(typeof(Animator))]
public class GripControl : MonoBehaviour {
    [SerializeField]
    KeyCode grabKey = KeyCode.Mouse0;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        bool grip = //Device.AerowandHand.GetButton(Aerowand.Button.Trigger);//
            Input.GetKey(grabKey);

        anim.SetBool("Grab", grip);
    }
}
