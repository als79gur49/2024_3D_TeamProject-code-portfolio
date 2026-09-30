using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
 
    public string sceneName;

    // 씬을 변경하는 메서드
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
