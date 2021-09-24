using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabMgr : MonoBehaviour
{
    private SteamVR_Input_Sources hand;
    private SteamVR_Action_Boolean grab;
    private SteamVR_Action_Pose pose;

    private Transform grabObject;
    private bool isTouched = false;

    void Start()
    {
        hand = SteamVR_Input_Sources.RightHand;
        grab = SteamVR_Actions.default_GrabGrip;
        pose = SteamVR_Actions.default_Pose;        
    }

    void Update()
    {   
        if (isTouched && grab.GetStateDown(hand))
        {
            grabObject.SetParent(this.transform);
            grabObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (grab.GetStateUp(hand))
        {
            grabObject.SetParent(null);
            Vector3 _velocity = pose.GetLastVelocity(hand);
            Rigidbody rb = grabObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = _velocity;

            isTouched = false;
            grabObject = null;
        }
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("GRAB_OBJECT"))
        {
            isTouched = true;
            grabObject = coll.transform;
        }
    }
}
