using System.Collections;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    [SerializeField]
    private string buttonClipName; //사운드매니저의 인덱스에서 가져오고 싶다.

    public ChangeScenes changeScenes;

    public float waitTimeMultiplier = 1.0f;  

    public void PlaySoundAndChangeScene()
    {
        StartCoroutine(PlaySoundAndTriggerSceneChange());
    }

    private IEnumerator PlaySoundAndTriggerSceneChange()
    {                                            //"Button"
        if (SoundManager.Instance.PlayEffectAudio(buttonClipName, out AudioClip audioClip))
        {
            yield return new WaitForSeconds(audioClip.length * waitTimeMultiplier);
        }

        // ChangeScenes 스크립트의 Load() 메서드를 호출하여 씬을 전환합니다.
        if (changeScenes != null)
        {
            changeScenes.Load();
        }
    }
}
