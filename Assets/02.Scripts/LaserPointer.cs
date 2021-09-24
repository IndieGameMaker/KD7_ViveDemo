using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Input_Sources hand;
    private LineRenderer line;
    private Transform tr;

    private SteamVR_Action_Boolean teleport;

    [Range(5.0f,30.0f)]
    public float maxDistance = 10.0f;

    public Color color = Color.blue;

    private RaycastHit hit;
    private GameObject pointerPrefab;
    private GameObject pointer;

    //이벤트를 전달할 버튼의 저장변수
    private GameObject currObject;
    private GameObject prevObject;

    // 트리거 액션설정
    private SteamVR_Action_Boolean trigger;

    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
        teleport = SteamVR_Actions.default_Teleport;
        trigger = SteamVR_Actions.default_InteractUI;

        tr = GetComponent<Transform>();
        pointerPrefab = Resources.Load<GameObject>("Pointer");
        pointer = Instantiate<GameObject>(pointerPrefab);

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

    void Update()
    {
        if (Physics.Raycast(tr.position, tr.forward, out hit, maxDistance))
        {
            line.SetPosition(1, new Vector3(0,0, hit.distance));
            pointer.transform.position = hit.point + (hit.normal * 0.01f);
            pointer.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            line.SetPosition(1, new Vector3(0, 0, maxDistance));
            pointer.transform.position = tr.position + (tr.forward * maxDistance);
            pointer.transform.rotation = Quaternion.LookRotation(tr.forward);
        }

        if (teleport.GetStateDown(hand))
        {
            if (Physics.Raycast(tr.position, tr.forward, out hit, maxDistance, 1<<8))
            {
                //Fade Out
                SteamVR_Fade.Start(Color.black, 0);
                StartCoroutine(Teleport(hit.point));
            }
        }

        if (Physics.Raycast(tr.position, tr.forward, out hit, maxDistance, 1<<9))
        {
            //현재 버튼객체를 저장
            currObject = hit.collider.gameObject;

            //현재 버튼과 이전 버튼이 다른 경우 => 새로운 버튼을 지시
            if (currObject != prevObject)
            {
                // 현재 버튼 PointerEnter 이벤트를 전달
                ExecuteEvents.Execute(currObject
                                    , new PointerEventData(EventSystem.current)
                                    , ExecuteEvents.pointerEnterHandler);

                // 이전 버튼 PointerExit 이벤트를 전달
                ExecuteEvents.Execute(prevObject
                                    , new PointerEventData(EventSystem.current)
                                    , ExecuteEvents.pointerExitHandler);  

                prevObject = currObject;            
            }

            if (trigger.GetStateDown(hand))
            {
                ExecuteEvents.Execute(currObject
                                    , new PointerEventData(EventSystem.current)
                                    , ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            if (prevObject != null)
            {
                ExecuteEvents.Execute(prevObject
                                    , new PointerEventData(EventSystem.current)
                                    , ExecuteEvents.pointerExitHandler); 
                prevObject = null;                
            }
        }
    }

    IEnumerator Teleport(Vector3 pos)
    {
        tr.parent.position = pos;
        yield return new WaitForSeconds(0.3f);
        //Fade In
        SteamVR_Fade.Start(Color.clear, 0.2f);
    }
}
