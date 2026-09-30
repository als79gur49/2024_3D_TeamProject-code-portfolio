using System.Collections;
using UnityEngine;

public class BlockInfo : MonoBehaviour
{
    //자신의 위에 붙을 수 있는 최대 블럭 수
    [SerializeField]
    private int maxInteractableBlock = 1;
    
    public int MaxInteractableBlock { get => maxInteractableBlock;}
    private int interactableBlock = 0;
    public int InteractableBlock { get => interactableBlock; set => interactableBlock = value; }
    public GameObject PrevGameObject { get; set; }

    [SerializeField][Range(0, 20)]
    private int height;
    public int Height { get => height; }

    public void DestroyBlock()
    {
        if(SoundManager.Instance.EffectPlayer.GetComponent<AudioSource>().isPlaying)
        {   //소리 중첩 방지
            SoundManager.Instance.PlayEffectAudio("Fail", 0.4f);
        }
        else
        {
            SoundManager.Instance.PlayEffectAudio("Fail");
        }
        //파괴 시 추가 기능
        AdditionalCondition();

        Destroy(gameObject);
    }

    private void AdditionalCondition()
    {
        if(GetComponentInChildren<SpriteRenderer>().sprite.name == "Object_00_02")
        {
            Debug.Log("노란색 블럭 삭제");
            StageManager.Instance.CurrentHealth = 0;
        }
    }

    //한 번에 파괴될 경우 지연삭제
    public void DelayDestroyBlock(float delayTime)
    {
        StartCoroutine(ChangeColor(delayTime));
    }

    private IEnumerator ChangeColor(float duration)
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();

        Color originColor = sprite.color;

        sprite.color = Color.red;
        yield return new WaitForSeconds(duration);

        sprite.color = originColor;

        DestroyBlock();
    }    
}