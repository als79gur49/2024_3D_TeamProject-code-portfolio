using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    private string buttonClipName; // 사운드 매니저의 클립 이름
    public float waitTimeMultiplier = 1.0f;

    public void QuitGameWithSound()
    {
        StartCoroutine(PlaySoundAndQuit());
    }

    private IEnumerator PlaySoundAndQuit()
    {
        if (SoundManager.Instance.PlayEffectAudio(buttonClipName, out AudioClip audioClip))
        {
            yield return new WaitForSeconds(audioClip.length * waitTimeMultiplier);
        }

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
