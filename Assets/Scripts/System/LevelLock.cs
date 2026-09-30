using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    public GameObject stageNumObject; // 스테이지 버튼들을 포함한 오브젝트
    public GameObject[] checkMarks;  // 체크 표시 오브젝트 배열
    [SerializeField]
    int levelReached; // 현재 오픈한 스테이지 번호
    private GameManager gameManager;
    private static LevelLock instance;
    public GameObject[] lockObjects;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (stageNumObject != null)
            {
                DontDestroyOnLoad(stageNumObject);
            }
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }



    void Start()
    {
        InitializeStages();
    }

    void OnLevelWasLoaded(int level)
    {
        InitializeStages(); // 새로운 씬이 로드될 때 다시 초기화
    }
    void InitializeStages()
    {
        if (stageNumObject == null)
        {
            //Debug.LogError("stageNumObject is missing or has been destroyed");
            return;
        }

        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        // 저장된 스테이지 번호 불러오기
        levelReached = PlayerPrefs.GetInt("levelReached", 0); // 기본값 0 (첫 스테이지)

        if (stages != null)
        {
            // 스테이지 잠금 설정
            for (int i = 0; i < stages.Length; i++)
            {
                if (i <= levelReached)
                {
                    stages[i].interactable = true; // 잠금 해제
                    if (i < checkMarks.Length && checkMarks[i] != null)
                    {
                        checkMarks[i].SetActive(i < levelReached); // 클리어된 스테이지에 체크 표시 활성화
                    }
                }
                else
                {
                    stages[i].interactable = false; // 잠금
                    if (i < checkMarks.Length && checkMarks[i] != null)
                    {
                        checkMarks[i].SetActive(false); // 체크 표시 비활성화
                    }
                }

                if (i < lockObjects.Length && lockObjects[i] != null)
                {
                    lockObjects[i].SetActive(i > levelReached);
                }
            }
        }
        else
        {
            //Debug.LogWarning("StageNumObject is not properly assigned or has no children.");
        }
    }


        public void UnlockNextStage(int currentStageIndex)
        {

        int levelReached = PlayerPrefs.GetInt("levelReached", 0); // 기본값 0
        int nextStage = currentStageIndex + 1;

        if (currentStageIndex >= levelReached)
        {
            PlayerPrefs.SetInt("levelReached", nextStage);
            PlayerPrefs.Save();

            //Debug.Log($"Stage {nextStage} unlocked.");

            if (nextStage - 1 < lockObjects.Length && lockObjects[nextStage - 1] != null)
            {
                lockObjects[nextStage - 1].SetActive(false);
                //Debug.Log($"Lock object for stage {nextStage} disabled.");
            }
        }
        else
        {
            //Debug.Log("Stage already cleared. No new stage unlocked.");
        }
    }

    public void MarkStageClear(int stageIndex)
    {
      

        if (stageIndex < checkMarks.Length && checkMarks[stageIndex] != null)
        {
            checkMarks[stageIndex].SetActive(true); // 체크 표시 활성화
            
        }
        else
        {
            //Debug.LogWarning($"Checkmark for stage {stageIndex} is null or out of bounds.");
        }
    }



}
