using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DrawMgr : MonoBehaviour
{
    public SteamVR_Input_Sources leftHand;
    public SteamVR_Action_Boolean trigger;
    private SteamVR_Action_Pose pose;

    private LineRenderer line;
    public float lineWidth = 0.01f;
    public Color lineColor = Color.white;

    void Start()
    {
        leftHand = SteamVR_Input_Sources.LeftHand;
        trigger = SteamVR_Actions.default_InteractUI;
        pose = SteamVR_Actions.default_Pose;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.GetStateDown(leftHand))
        {
            CreateLine();
        }

        if (trigger.GetState(leftHand))
        {
            Vector3 pos = pose.GetLocalPosition(leftHand);
            ++line.positionCount;
            line.SetPosition(line.positionCount-1, pos);
        }
    }

    void CreateLine()
    {
        GameObject lineObject = new GameObject("Line");
        line = lineObject.AddComponent<LineRenderer>();

        Material mt = new Material(Shader.Find("Unlit/Color"));
        mt.color = lineColor;
        line.material = mt;
        line.useWorldSpace = false;
        line.numCapVertices = 20;

        line.startWidth = line.endWidth = lineWidth;
        line.positionCount = 1;
        Vector3 pos = pose.GetLocalPosition(leftHand);
        line.SetPosition(0, pos);
    }
}
