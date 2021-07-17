using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arcade : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("A_start");

    }
}
