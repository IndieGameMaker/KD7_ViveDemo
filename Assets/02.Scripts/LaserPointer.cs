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

    [Range(5.0f,30.0f)]
    public float maxDistance = 10.0f;

    public Color color = Color.blue;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
        tr = GetComponent<Transform>();
        CreateLine();
    }

    void CreateLine()
    {
        line = this.gameObject.AddComponent<LineRenderer>();

        line.useWorldSpace = false;

        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, new Vector3(0, 0, maxDistance));

        line.startWidth = 0.03f;
        line.endWidth = 0.005f;

        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = this.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(tr.position, tr.forward, out hit, maxDistance))
        {
            line.SetPosition(1, new Vector3(0,0, hit.distance));
        }
        else
        {
            line.SetPosition(1, new Vector3(0, 0, maxDistance));
        }
    }
}
