using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rowing : MonoBehaviour
{

    public void SceneChange()
    {
        SceneManager.LoadScene("Rowing");

    }


    public void Onclick(int TargetDistance){
        PlayerPrefs.SetInt("TargetDistance", TargetDistance);
    }

    
}