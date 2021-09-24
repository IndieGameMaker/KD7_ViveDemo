using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabMgr : MonoBehaviour
{
    private Transform grabObject;
    private SteamVR_Action_Boolean grab;
    private SteamVR_Input_Sources hand;
    private SteamVR_Action_Pose pose;
    private bool isTouched = false;

    void Start()
    {
        grab = SteamVR_Actions.default_GrabGrip;
        pose = SteamVR_Actions.default_Pose;
        hand = SteamVR_Input_Sources.RightHand;
    }

    // Update is called once per frame
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
            var rb = grabObject.GetComponent<Rigidbody>();
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
