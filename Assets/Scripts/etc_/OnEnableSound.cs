using UnityEngine;

public class OnEnableSound : MonoBehaviour
{
    [SerializeField]
    private string onEnableClipName;


    private void OnEnable()
    {
        SoundManager.Instance.PlayBGMAudio(onEnableClipName);
    }

}
