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
    public SteamVR_Action_Boolean trackPadTouch; //트랙패드 터치 여부
    public SteamVR_Action_Vector2 trackPadPosition; //트랙패드의 좌푯값(x,y)
    private SteamVR_Action_Boolean teleport;

    // Start is called before the first frame update
    void Start()
    {
        trigger = SteamVR_Actions.default_InteractUI;
        trackPadTouch = SteamVR_Actions.default_TrackPadTouch;
        trackPadPosition = SteamVR_Actions.default_TrackPadPosition;
        teleport = SteamVR_Actions.default_Teleport;
    }

    // Update is called once per frame
    void Update()
    {   
        if (trigger.GetStateDown(rightHand))
        {
            Debug.Log("Right Trigger Down");
        }

        if (trackPadTouch.GetState(any))
        {
            Vector2 pos = trackPadPosition.GetAxis(any);
            Debug.Log($"x={pos.x}, y={pos.y}");
        }
        
        if (teleport.GetStateUp(any))
        {
            Debug.Log("Teleport Up");
        }
    }
}
