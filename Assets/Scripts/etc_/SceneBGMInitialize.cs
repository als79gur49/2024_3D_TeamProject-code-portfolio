using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBGMInitialize : MonoBehaviour
{
    private static int prevSceneIndex;
    private static int currentSceneIndex;
    [SerializeField]
    private string sceneBGMName;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (prevSceneIndex <= 1 && currentSceneIndex <= 1) //Title, Menu
        {
            //continue
        }
        else
        {
            SoundManager.Instance.PlayBGMAudio(sceneBGMName);
        }

        prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
