using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ArPlacementManger : MonoBehaviour
{
    ARRaycastManager m_raycast;
    public Camera arCamera;
    static List<ARRaycastHit> raycast_Hits = new List<ARRaycastHit>();
    public GameObject table;
    private void Awake()
    {
        m_raycast = GetComponent<ARRaycastManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 centor = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = arCamera.ScreenPointToRay(centor);
        if (m_raycast.Raycast(ray, raycast_Hits,TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycast_Hits[0].pose;
            Vector3 position = hitPose.position;
            table.transform.position = position;
        }


    }
}
