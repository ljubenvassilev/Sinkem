using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable UnusedMember.Global

public class GameManager : MonoBehaviour
{
    private static float _originalTimeScale;
    public GameObject[] Panels;
    private int _enemies;
    
    private void Start()
    {
        _originalTimeScale = Time.timeScale;
        _enemies = 3;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        Time.timeScale = 0;
        ShowPanel(0);
    }
    
    public void LoadLevel(string sceneName)
    {
        HideAllPanels();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        _enemies = 3;
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
        Panels[panel].SetActive(true);
    }

    private void HideAllPanels()
    {
        if (Panels == null) return;
        foreach (var t in Panels)
        {
            t.SetActive(false);
        }
        Time.timeScale = _originalTimeScale;
    }

    public void EnemyDead()
    {
        _enemies--;
        if (_enemies <= 0)
        {
            ShowPanel(1);
        }
    }
}
