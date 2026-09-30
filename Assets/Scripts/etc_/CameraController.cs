using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //카메라 컨트롤: 블럭이 쌓인 위치로 이동
    [SerializeField]
    private int blockLimit;
    private int blockCount;
    //기본 위치 0m 카메라 위치
    [SerializeField]
    private Transform originPosition;

    [SerializeField][Range(-5,10)]
    private float additionalYAxis;
    [SerializeField][Range(1,10)]
    private float moveSpeed; //카메라가 움직이지 않을 경우 인스펙터 창에서 값 바꿔보기

    private float currentHeight;
    private float targetHeight;
    private float time;
    void Start()
    {
        blockCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCameraPosition();
    }

    private void MoveCameraPosition()
    {
        time = Mathf.Clamp01(time + (Time.deltaTime * moveSpeed));

        transform.position = new Vector3(0, Mathf.Lerp(currentHeight, targetHeight, time), -10);
    }
    public void SetCameraPosition()
    {
        time = 0f;
        currentHeight = transform.position.y;

        blockCount = BlockManager.Blocks.Count;

        if (blockLimit > blockCount)
        {
            targetHeight = originPosition.position.y;
        }
        else
        {
            targetHeight = BlockManager.Blocks.Peek().transform.position.y + additionalYAxis;
        }
    }
}
