using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pausePanel;
    public void SetIsPaused(bool pause) { isPaused = pause; }
    public bool GetIsPaused() { return isPaused; }

    public void PauseGame(bool setPause)
    {
        SetIsPaused(setPause);
        pausePanel.SetActive(setPause);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
