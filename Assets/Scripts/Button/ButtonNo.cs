using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ButtonNo : MonoBehaviour
{
    public Button rejectButton;   // 거절 버튼
    public GameObject contentImage; // 비활성화할 이미지

    void Start()
    {
        // 거절 버튼이 눌렸을 때 이미지 비활성화
        rejectButton.onClick.AddListener(HideImage);
    }

 
    void HideImage()
    {
        contentImage.SetActive(false);
    }
}
