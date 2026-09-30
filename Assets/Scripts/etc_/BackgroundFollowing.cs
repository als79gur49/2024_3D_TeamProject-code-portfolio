using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFollowing : MonoBehaviour
{
    [SerializeField]
    private GameObject skyObject;
    [SerializeField]
    private GameObject cityObject;

    [SerializeField][Range(0, 1)]
    private float skyFollowingPercent = 1.0f;
    [SerializeField][Range(0,1)]
    private float cityFollowingPercent = 1.0f;

    private float mainCameraYAxisDifference;
    private float prevMainCameraYAxis;

    void Update()
    {
        if (prevMainCameraYAxis != Camera.main.transform.position.y)
        {
            mainCameraYAxisDifference = Camera.main.transform.position.y - prevMainCameraYAxis;

            skyObject?.transform.Translate(new Vector3(0, mainCameraYAxisDifference * skyFollowingPercent, 0));
            cityObject?.transform.Translate(new Vector3(0, mainCameraYAxisDifference * cityFollowingPercent, 0));

            prevMainCameraYAxis = Camera.main.transform.position.y;
        }
    }
}
