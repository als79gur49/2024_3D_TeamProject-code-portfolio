using UnityEngine;

public class NormalButton : MonoBehaviour
{
    [SerializeField]
    private string buttonClipName;
    public void PlaySound()
    {
        if(SoundManager.Instance.PlayEffectAudio(buttonClipName))
        {   
        }

    //    if (audioSource != null && buttonClickSound != null)
    //    {
    //        Debug.Log("효과음 재생 중");
    //        audioSource.PlayOneShot(buttonClickSound);
    //    }
    //    else
    //    {
    //        if (audioSource == null)
    //        {
    //            Debug.LogWarning("AudioSource가 할당되지 않았습니다.");
    //        }
    //        if (buttonClickSound == null)
    //        {
    //            Debug.LogWarning("AudioClip이 할당되지 않았습니다.");
    //        }
    //    }
    }
}
