using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public LevelLock levelLock; // LevelLock 스크립트 참조

    public static GameManager Instance => instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        levelLock = FindObjectOfType<LevelLock>(); // LevelLock 찾기
        if (levelLock == null)
        {
            Debug.LogWarning("LevelLock not found in the scene.");
        }
    }

    public void StageClear(int stageIndex)
    {
        Debug.Log($"StageClear called for stage {stageIndex}");

        if (levelLock != null)
        {
            levelLock.MarkStageClear(stageIndex);
            Debug.Log("MarkStageClear called.");
        }


        int levelReached = PlayerPrefs.GetInt("levelReached", 0); // 현재 레벨 불러오기
        if (stageIndex >= levelReached)
        {


            int nextStage = levelReached + 1;

            Debug.Log($"Current levelReached: {levelReached}, NextStage: {nextStage}");

            // 다음 스테이지 락 해제
            if (levelLock != null)
            {
                levelLock.UnlockNextStage(stageIndex);
                Debug.Log("UnlockNextStage called.");
            }
        }
        else
        {
            Debug.Log("Stage already cleared. No new stage unlocked.");
        }


    }
        
    public void StageFail(int stageIndex)
    {
        //이곳은 임시로 StageManager에서 Game Fail시 호출하는 곳
    }


}
