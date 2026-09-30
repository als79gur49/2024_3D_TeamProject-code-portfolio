using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBlockCollider : BlockCollider
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void BottomCollidedLogic(BlockCollider otherBlock, PlayableMove otherMovement)
    {
        if (info.PrevGameObject != otherBlock.MainBlock.gameObject)
        {
            Debug.Log("S->B");
            otherBlock.MainBlock.GetComponent<BlockInfo>().DestroyBlock();
            StageManager.Instance.CurrentHealth--;
        }
        info.PrevGameObject = otherBlock.MainBlock.gameObject;
    }
    protected override void SideCollidedLogic(BlockCollider otherBlock, PlayableMove otherMovement)
    {
        if (info.PrevGameObject != otherBlock.MainBlock.gameObject)
        {
            Debug.Log("S->S");
            otherBlock.MainBlock.GetComponent<BlockInfo>().DestroyBlock();
            StageManager.Instance.CurrentHealth--;
        }
        info.PrevGameObject = otherBlock.MainBlock.gameObject;   
    }

}
