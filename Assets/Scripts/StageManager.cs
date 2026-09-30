using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    //스테이지별 클리어 조건 설정, 게임 성공, 실패 판정
    //CurrentHeight => GameClear, CurrentHealth => GameFail
    [Header("일정 높이 이상")]
    [SerializeField]
    private bool targetOnOffSwitch;
    [SerializeField]
    [Range(0, 500)]
    private int targetHeight;

    private int currentHeight = 10;
    private int currentHealth = 3;

    public delegate void ChangedHealth(int currenthealth);
    public static event ChangedHealth OnHealthChanged;

    public int CurrentHeight
    {
        get => currentHeight;
        set
        {
            currentHeight = value;

            if (currentHeight >= targetHeight && targetOnOffSwitch)
            {
                InGameUI inGameUI = FindAnyObjectByType<InGameUI>();
                if (inGameUI != null)
                {
                    inGameUI.gameClear();
                }
            }
        }
    }
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;

            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                InGameUI inGameUI = FindAnyObjectByType<InGameUI>();
                if (inGameUI != null)
                {
                    inGameUI.gameOver();
                }
            }
        }
    }

    public int TargetHeight
    {
        get => targetHeight;
    }

    public bool TargetOnOffSwitch
    {
        get => targetOnOffSwitch;
    }

    private static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null) //Awake이전 호출 시, 초기화
            {
                instance = FindObjectOfType<StageManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("StageManager");
                    instance = obj.AddComponent<StageManager>();
                    //DontDestroyOnLoad(obj);
                }
            }

            return instance;
        }
    }

    private void Awake() //싱글톤 패턴
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);

            return;
        }

        instance = this;

        BlockManager.InitializeBlocks();
    }
}
