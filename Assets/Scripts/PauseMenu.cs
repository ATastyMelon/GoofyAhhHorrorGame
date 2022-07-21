using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (!isPaused)
        {
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public bool getPaused()
    {
        return isPaused;
    }

    public void setPaused(bool paused)
    {
        this.isPaused = paused;
    }
}
