using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBlockCollider : BlockCollider
{  
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame

    protected override void BottomCollidedLogic(BlockCollider otherBlock, PlayableMove otherMovement)
    {     

    }
    protected override void SideCollidedLogic(BlockCollider otherBlock, PlayableMove otherMovement)
    {

    }
    
}
