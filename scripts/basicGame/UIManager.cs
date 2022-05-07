using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// UI manager for game scene
/// </summary>
public class UIManager : MonoBehaviour
{
    public TimeScaler ts;
    public GameObject panel;
    public TextMeshProUGUI EndTitle, startTitle;
    
    public void SetPanel(string massage)
    {
        panel.SetActive(true);
        EndTitle.text = massage;
        ts.timeScale = 0;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            startTitle.enabled = false;
    }
}
