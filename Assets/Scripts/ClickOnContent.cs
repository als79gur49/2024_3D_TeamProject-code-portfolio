using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ClickOnContent : MonoBehaviour
{
    public Button[] buttons; 
    public GameObject[] contentImages;
    string test;

    void Start()
    {
        // 각 버튼에 클릭 이벤트를 연결
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 이벤트에서 사용할 인덱스 변수
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

  
    void OnButtonClick(int index)
    {
        // 모든 이미지를 비활성화
        foreach (GameObject image in contentImages)
        {
            image.SetActive(false);
        }

        // 눌린 버튼에 해당하는 이미지만 활성화
        contentImages[index].SetActive(true);

    }
}
