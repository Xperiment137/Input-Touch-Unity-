using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AndroidTouch : MonoBehaviour
{
   private Vector3 FirstPoint;
    private Vector3 SecondPoint;
   private float xAngle;
    private float yAngle;
   private  float xAngleTemp;
   private  float yAngleTemp;
    private List<RaycastResult> results = new List<RaycastResult>();
    private PointerEventData ped = new PointerEventData(null);

    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                results.Clear();
                ped.position = touch.position;
                GameObject.Find("Canvas").GetComponent<GraphicRaycaster>().Raycast(ped, results);
                if (results.Count == 0)
                {
                    SecondPoint = touch.position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                results.Clear();
                ped.position = touch.position;
                GameObject.Find("Canvas").GetComponent<GraphicRaycaster>().Raycast(ped, results);
                if (results.Count == 0)
                {
                    FirstPoint = touch.position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                    yAngle = Mathf.Clamp(yAngle, -50f, 66f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(yAngle, xAngle, 0.0f), Time.deltaTime * 20f);
                }
            }
        }
    }

    }
