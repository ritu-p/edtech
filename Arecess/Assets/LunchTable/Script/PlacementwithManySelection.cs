using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementwithManySelection : MonoBehaviour
{
    public PlacementObject[] placementobjects;
    public Camera arcamera;
    private Vector2 touchPosition = default;
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arcamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray,out hitObject))
                {
                    PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();
                    if (placementObject != null)
                    {
                        
                    RemoveSelectedObject(placementObject);
                         hitObject.transform.gameObject.SetActive(false);
                    }
                }
            }


        }
    }

    void RemoveSelectedObject(PlacementObject selected)
    {
        foreach(PlacementObject current in placementobjects)
        {
            if (selected != current)
            {
                current.IsSelected = false;
            }
            else
            {
                current.IsSelected = true;
             //   current.SetActive(false);
            }
        }
    }
}
