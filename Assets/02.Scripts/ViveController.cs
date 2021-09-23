using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveController : MonoBehaviour
{
    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources any = SteamVR_Input_Sources.Any;

    public SteamVR_Action_Boolean trigger;
    public SteamVR_Action_Boolean trigger2;

    // Start is called before the first frame update
    void Start()
    {
        trigger = SteamVR_Actions.default_InteractUI;
    }

    // Update is called once per frame
    void Update()
    {   
        if (trigger.GetStateDown(leftHand))
        {
            Debug.Log("Left Trigger Down");
        }

        if (trigger.GetStateDown(rightHand))
        {
            Debug.Log("Right Trigger Down");
        }
        
    }
}
