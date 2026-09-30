using UnityEngine;

public class Timer : MonoBehaviour
{
    public float gameTime = 0.0f;
    public float displayTime = 0.0f;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            //Debug.Log("Timer Update is being called");
            gameTime += Time.deltaTime;
            displayTime = gameTime;
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        gameTime = 0.0f;
        displayTime = 0.0f;
        isRunning = true;
    }
}
