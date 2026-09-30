using UnityEngine;
using TMPro;

public class BestTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] bestTimeTexts; // 각 스테이지의 최고기록을 표시할 TextMeshProUGUI 배열

    void Start()
    {

        for (int i = 0; i < bestTimeTexts.Length; i++)
        {
            float bestTime = PlayerPrefs.GetFloat($"BestTime_{i}", float.MaxValue);
            //Debug.Log($"Loaded best time for stage {i}: {bestTime}");

            if (bestTime == float.MaxValue)
            {
                bestTimeTexts[i].text = $"최고 기록: 00:00:00.000";
            }
            else
            {
                int hours = (int)bestTime / 3600;
                int minutes = ((int)bestTime % 3600) / 60;
                int seconds = (int)bestTime % 60;
                int milliseconds = (int)((bestTime - Mathf.Floor(bestTime)) * 1000);

                //Debug.Log($"Formatted best time for stage {i}: {string.Format("{0:00}:{1:00}:{2:00}.{3:000}", hours, minutes, seconds, milliseconds)}");

                bestTimeTexts[i].text = string.Format("최고 기록: {1:00}:{2:00}:{3:00}.{4:000}", i + 1, hours, minutes, seconds, milliseconds);
             
            }
        }
    }
}
