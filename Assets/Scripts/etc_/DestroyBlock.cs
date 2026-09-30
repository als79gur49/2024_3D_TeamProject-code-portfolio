using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<BlockCollider>(out BlockCollider block) &&
           block.MainBlock.TryGetComponent<PlayableMove>(out PlayableMove movement))
        {
            if(movement.IsMoving)
            {
                StageManager.Instance.CurrentHealth--;
                block.MainBlock.GetComponent<BlockInfo>().DestroyBlock();
            }
        }
    }
}
