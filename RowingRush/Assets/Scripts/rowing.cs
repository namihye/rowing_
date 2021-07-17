using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class rowing : MonoBehaviour
{

    public void SceneChange()
    {
        SceneManager.LoadScene("Rowing");

    }


    public void Onclick(string TargetDistance){
        string isTargetDistance = TargetDistance;
        Debug.Log(isTargetDistance);
    }

    
}