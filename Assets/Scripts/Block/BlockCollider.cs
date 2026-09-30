using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BlockCollider : MonoBehaviour
{
    //Top -> Bottom BlockInfo의 maxInteractableBlock 초과 시 BlockManager의 Blocks List 초기화, hp--
    //Top -> Side side블럭 삭제, hp--
    //Side -> Bottom Bottom블럭 삭제, hp--
    //Side -> Side Side블럭 삭제, hp--

    [SerializeField]
    protected GameObject mainBlock;
    protected PlayableMove movement;
    protected BlockInfo info;

    public GameObject MainBlock { get => mainBlock; set => mainBlock = value; }

    protected virtual void Start()
    {
        movement = mainBlock.GetComponent<PlayableMove>();
        info = mainBlock.GetComponent<BlockInfo>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {   //충돌 가능 블럭, 고정된 상태에서 떨어지는 블럭에 대해서만, 다중충돌 방지
        if (IsPlayableBlock(other.gameObject, out BlockCollider otherBlock, out PlayableMove otherMovement) &&
           !IsFalling(movement) && IsFalling(otherMovement) && 
           MainBlock != otherBlock.MainBlock)
        {
            if (other.GetComponent<BottomBlockCollider>())
            {   //해당 부분 override 통해서 수정
                BottomCollidedLogic(otherBlock, otherMovement);
            }
            else if (other.GetComponent<SideBlockCollider>())
            {   //해당 부분 override 통해서 수정
                SideCollidedLogic(otherBlock, otherMovement);
            }
            else if (other.GetComponent<TopBlockCollider>())
            {
                // x
            }
        }
    }

    protected virtual void BottomCollidedLogic(BlockCollider otherBlock, PlayableMove otherMovement) { }
    protected virtual void SideCollidedLogic(BlockCollider otherBlock, PlayableMove otherMovement) { }


    private bool IsPlayableBlock(GameObject obj, out BlockCollider collide, out PlayableMove block)
    {
        BlockCollider blockCollide = obj.GetComponent<BlockCollider>();
        block = null;

        return (obj.TryGetComponent<BlockCollider>(out collide) && 
               blockCollide.MainBlock.TryGetComponent<PlayableMove>(out block));
    }

    private bool IsFalling(PlayableMove block)
    {
        return block.IsMoving && block.IsFalling;
    }
}