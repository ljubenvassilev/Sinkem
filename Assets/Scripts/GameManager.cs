using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float originalTimeScale;
    public GameObject panel;

    public void LoadLevel(string name)
    {
        Time.timeScale = originalTimeScale;
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }
    }

    public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = originalTimeScale;
    }

    private void Start()
    {
        originalTimeScale = Time.timeScale;
    }
}
