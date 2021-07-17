using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour{

    public InputField inputID;
    
    public void Save(){
        PlayerPrefs.SetString("ID", inputID.text);
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("third");

    }
}
