using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class home : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("first");

    }
}
