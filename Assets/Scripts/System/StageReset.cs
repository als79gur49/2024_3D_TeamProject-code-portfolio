using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageReset : MonoBehaviour
{
    
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs has been reset to default values.");
    }
}
