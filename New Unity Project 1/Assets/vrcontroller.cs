using UnityEngine;
using System.Collections;

public class vrcontroller : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    [SerializeField]
    RaycastHit hit;
    public bool bhit;
    [SerializeField]
    GameObject hitObject;
    Quaternion hitObjectRot;
    [SerializeField]
    bool carry = false;
    [SerializeField]
    Vector3 carryVector;

    // Use this for initialization
    void Start () {
	    trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if(controller == null)
        {
            Debug.Log("Controller is not initialized");
            return;
        }


	    gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);
        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);


        if(gripButtonDown)
        {
            Debug.Log("gripButton was pressed");
        }

        if(gripButtonUp)
        {
            Debug.Log("gripButton was just unpressed");
        }

        if(triggerButtonDown)
        {
            Debug.Log("triggerButton was pressed");
        }

        if(triggerButtonUp)
        {
            Debug.Log("triggerButton was just unpressed");
        }

        
        Ray raycast = new Ray(transform.position, transform.forward);
        bhit = Physics.Raycast(raycast, out hit, 10.0f);

        if (carry)
        {
            carryVector = transform.forward * 2;
            hitObject.transform.position = transform.position + carryVector/* + new Vector3(0, 1, 0)*/;
            hitObject.transform.rotation = transform.rotation;

            if (gripButtonDown)
            {
                carry = false;
                Rigidbody hitObjectR = hitObject.GetComponent<Rigidbody>();
                Vector3 launchForce = transform.forward * 100;
                hitObjectR.AddForce(launchForce, ForceMode.Impulse);
            }
        }

        if (gripButtonUp && !carry && bhit)
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
