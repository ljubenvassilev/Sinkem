using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static float originalTimeScale;
    public GameObject[] panels;
    private int enemies;
    
    private void Start()
    {
        originalTimeScale = Time.timeScale;
        enemies = 3;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            ShowPanel(0);
        }
    }
    
    public void LoadLevel(string name)
    {
        HideAllPanels();
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        enemies = 3;
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void Resume()
    {
        HideAllPanels();
    }

    public void ShowPanel(int panel)
    {
        Time.timeScale = 0;
        panels[panel].SetActive(true);
    }

    private void HideAllPanels()
    {
        if (panels == null) return;
        foreach (var t in panels)
        {
            t.SetActive(false);
        }
        Time.timeScale = originalTimeScale;
    }

    public void EnemyDead()
    {
        enemies--;
        if (enemies <= 0)
        {
            ShowPanel(1);
        }
    }
}
