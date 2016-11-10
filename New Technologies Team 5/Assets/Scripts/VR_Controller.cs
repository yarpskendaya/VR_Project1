using UnityEngine;
using System.Collections;

//Class to acces the Vive controller and use the buttons for actions (Picking up objects, throwing objects)
public class VR_Controller : MonoBehaviour
{
    //Booleans to store the button states and to debug if neccesary
    [SerializeField]
    public bool gripButtonDown = false;
    [SerializeField]
    public bool gripButtonUp = false;
    [SerializeField]
    public bool gripButtonPressed = false;
    [SerializeField]
    public bool triggerButtonDown = false;
    [SerializeField]
    public bool triggerButtonUp = false;
    [SerializeField]
    public bool triggerButtonPressed = false;

    //Getting button ID's
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    //Getting the Vive controllers
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    //Variables for the raycast
    [SerializeField]
    RaycastHit hit;
    [SerializeField]
    bool rhit;
    [SerializeField]
    float grabRange = 400.0f;

    //Variables for the object hit by the raycast
    [SerializeField]
    GameObject hitObject;
    Quaternion hitObjectRot;
    [SerializeField]
    bool carry = false;
    [SerializeField]
    Vector3 carryVector;

    //Speed that the object is launched with
    public int launchSpeed = 200000;

    //----------------------------------------------------------------------------------------------------//

    void Start()
    {
        //Get the vr tracked object
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    
    void Update()
    {
        //in case a controller is null
        if (controller == null)
        {
            Debug.Log("Controller is not initialized");
            return;
        }

        //reading in all the booleans for the button states
        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);
        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        //The raycast shot from the controller along the z axis up to a certain distance, returns the hit object as "hit"
        Ray raycast = new Ray(transform.position, transform.forward);
        rhit = Physics.Raycast(raycast, out hit, grabRange);

        //In case the controller is already holding an object, the object follows the position of the vive controller holding said object
        if (carry)
        {
            carryVector = transform.forward * 20;
            hitObject.transform.position = transform.position + carryVector/* + new Vector3(0, 1, 0)*/;
            hitObject.transform.rotation = transform.rotation;

            //If the carry button is released, the held object is shot forward with a set velocity
            if (gripButtonUp)
            {
                carry = false;
                Rigidbody hitObjectR = hitObject.GetComponent<Rigidbody>();
                Vector3 launchForce = transform.forward * launchSpeed;
                hitObjectR.AddForce(launchForce, ForceMode.Impulse);
            }
        }
        else if (gripButtonDown && rhit)//If the controller is not holding an object, the objec has the correct tag and the object is within the corract range, an objcet is picked up. 
        {
            hitObject = hit.collider.gameObject;
            if (hitObject.tag == "Grabable")
            {
                hitObjectRot = hitObject.transform.rotation;
                carry = true;
            }
        }
    }
}