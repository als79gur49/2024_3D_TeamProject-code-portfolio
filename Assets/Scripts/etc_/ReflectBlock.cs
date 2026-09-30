using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class ReflectBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //원래는 BlockCollider를 통해서 작동시켰는데, BlockCollider가 여러개 있을 때
        //충돌하면 한 번에 여러번 충돌하는 문제때문에, 지금은 임시로 Sprite를 통해서 사용 중

        if(other.gameObject.name.Equals("Sprite") &&
            other.gameObject.transform.parent.TryGetComponent<PlayableMove>(out PlayableMove movement))
        {
            movement.HorizontalReflect();
        }

     //   if(other.gameObject.TryGetComponent<BlockCollider>(out BlockCollider block) &&
     //       block.MainBlock.TryGetComponent<PlayableMove>(out PlayableMove movement))
     //   {
     //       movement.HorizontalReflect();
     //   }
    }
}
