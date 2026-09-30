using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[System.Serializable]
public class KeyValuePair
{   //딕셔너리는 직렬화가 되지 않아서 따로 key, value 비슷하게 제작
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    //모든 소리를 반드시 bgm, effect 중 하나를 선택해서 출력하기

    //오디오 설정창 x, 인스펙터 창에서 수정 시 적용
    [SerializeField]
    private AudioMixer mAudioMixer;
    [SerializeField]
    private GameObject bgmPlayer;
    [SerializeField]
    private GameObject effectPlayer;

    public GameObject EffectPlayer => effectPlayer;

    #region 볼륨 수정하는 변수들 
    [SerializeField][Range(-80, 20)] //오디오 믹서의 Volume의 경우 선형이 아닌 로그 스케일인 것 같음.
    private float currentMasterVolume;
    [SerializeField][Range(-80, 20)]
    private float currentBGMVolume;
    [SerializeField][Range(-80, 20)]
    private float currentEffectVolume;

    public float MasterVolume
    {
        get => currentMasterVolume;
        set
        {
            currentMasterVolume = Mathf.Clamp(value, -80, 20);
            mAudioMixer.SetFloat("MasterVolume", currentMasterVolume);
        }
    }
    public float BGMVolume
    {
        get => currentBGMVolume;
        set
        {
            currentBGMVolume = Mathf.Clamp(value, -80, 20);
            mAudioMixer.SetFloat("BGMVolume", currentBGMVolume);
        }
    }
    public float EffectVolume
    {
        get => currentEffectVolume;
        set
        {
            currentEffectVolume = Mathf.Clamp(value, -80, 20);
            mAudioMixer.SetFloat("EffectVolume", currentEffectVolume);
        }
    }
    #endregion

    [SerializeField]
    private List<KeyValuePair> bgmClips;
    [SerializeField]
    private List<KeyValuePair> effectClips;


    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null) //Awake이전 호출 시, 초기화
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("SoundManager");
                    instance = obj.AddComponent<SoundManager>();

                    DontDestroyOnLoad(obj);
                }
            }

            return instance;
        }
    }

    private void OnValidate() //인스펙터 창에서 값 수정 시 호출되는 함수
    {
        MasterVolume = currentMasterVolume;
        BGMVolume = currentBGMVolume;
        EffectVolume = EffectVolume;
    }
    private void Awake() //싱글톤 패턴, 씬로드 시 추가코드
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);

            return;
        }

        instance = this;

        DontDestroyOnLoad(this.gameObject);


        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayBGMAudio(AudioClip clip, float rate = 0.0f)
    {
        bgmPlayer.GetComponent<AudioSource>().clip = clip;

        bgmPlayer.GetComponent<AudioSource>().time = rate * clip.length;

        bgmPlayer.GetComponent<AudioSource>().Play();
    }

    public bool PlayBGMAudio(string clipName, float rate = 0.0f)
    {
        AudioClip resultClip = GetClip(clipName, bgmClips);

        if (resultClip == null)
        {
            Debug.Log($"BGMSound에서 {clipName}을 찾을 수 없습니다.");

            return false;
        }

        if(rate > 1.0f)
        {
            Debug.Log($"BGMSound의 Rate{rate} > 1.0f가 커서 실행 불가능");

            return false;
        }

        PlayBGMAudio(resultClip, rate);

        return true;
    }

    public bool PlayBGMAudio(string clipName, out AudioClip audioClip, float rate = 0.0f)
    {
        audioClip = GetClip(clipName, effectClips);

        if (audioClip == null)
        {
            Debug.Log($"BGMSound에서 {clipName}을 찾을 수 없습니다.");

            return false;
        }

        if (rate > 1.0f)
        {
            Debug.Log($"BGMSound의 Rate{rate} > 1.0f가 커서 실행 불가능");

            return false;
        }

        PlayBGMAudio(audioClip, rate);

        return true;
    }


    public void PlayEffectAudio(AudioClip clip, float volume = 1f)
    {
        effectPlayer.GetComponent<AudioSource>().PlayOneShot(clip, volume);
    }
    public bool PlayEffectAudio(string clipName, float volume = 1f)
    {
        AudioClip resultClip = GetClip(clipName, effectClips);

        if (resultClip == null)
        {
            Debug.Log($"EffectSound에서 {clipName}을 찾을 수 없습니다.");

            return false;
        }

        PlayEffectAudio(resultClip, volume);

        return true;
    }
    public bool PlayEffectAudio(string clipName, out AudioClip audioClip, float volume = 1f)
    {
        audioClip = GetClip(clipName, effectClips);

        if (audioClip == null)
        {
            Debug.Log($"EffectSound에서 {clipName}을 찾을 수 없습니다.");

            return false;
        }

        PlayEffectAudio(audioClip, volume);

        return true;
    }

    private AudioClip GetClip(string name, List<KeyValuePair> list)
    {
        foreach (KeyValuePair pair in list)
        {
            if (pair.name == name)
            {
                return pair.clip;
            }
        }

        return null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //다음 씬 로드시, 해당 씬에 사운드매니저가 존재한다면, 해당 사운드매니저의 초기값으로 초기화되고 삭제되어서
        //변수들 강제 할당해주기

        MasterVolume += 0;
        BGMVolume += 0;
        EffectVolume += 0;
    }
}
