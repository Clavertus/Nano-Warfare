using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadMap()
    {
        SceneManager.LoadScene(2);
    }

    public void loadTut()
    {
        SceneManager.LoadScene(1);
    }
}
