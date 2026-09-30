using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockManager : MonoBehaviour
{
    //블럭 저장 및 처리
    private static Stack<GameObject> blocks = new Stack<GameObject>();
    public static Stack<GameObject> Blocks {  get { return blocks; }  private set { blocks = value; } }

    public static int BlocksHeight { get; private set; } = 10;

    public static void PushBlock(GameObject block)
    {
        Blocks.Push(block);

        AdjustBlockHeight(block);

        SetBlocksHeight();
    }

    //충돌 감지가 Is Trigger이기에 쌓이는 위치 수동으로 조정
    private static void AdjustBlockHeight(GameObject block)
    {
        float targetYAxis = (BlocksHeight * 0.1f)  + (-5);

        block.transform.position = new Vector3(block.transform.position.x, targetYAxis, block.transform.position.z);
    }

    public static void DestroyAllBlocks()
    {
        BuildingSpawner spawner = FindObjectOfType<BuildingSpawner>();
        spawner.DelaySpawn(0.7f);

        while (Blocks.TryPop(out GameObject block))
        {
            Debug.Log("Deleted");

            block?.GetComponent<BlockInfo>()?.DelayDestroyBlock(0.3f);
        }

        SetBlocksHeight();
        Camera.main.GetComponent<CameraController>().SetCameraPosition();
    }

    public static void SetBlocksHeight()
    {
        BlocksHeight = 10; // 기반 블럭의 높이 

        foreach(GameObject block in Blocks)
        {
            BlocksHeight += block?.GetComponent<BlockInfo>().Height ?? 0;
        }

        StageManager.Instance.CurrentHeight = BlocksHeight;

        //Debug.Log($"재설정된 블럭높이:{BlocksHeight}");
    }

    //스테이지 시작 할 경우 StageManager에서 수동 초기화
    public static void InitializeBlocks()
    {
        Blocks.Clear();
        SetBlocksHeight();
    }
}
