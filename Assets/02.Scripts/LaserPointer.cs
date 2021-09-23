using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Input_Sources hand;
    private LineRenderer line;
    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
