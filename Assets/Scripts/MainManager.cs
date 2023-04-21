using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject setting;
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Setting()
    {
        setting.SetActive(true);
    }
}
