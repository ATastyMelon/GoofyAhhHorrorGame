using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtons : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    public void UnpauseGame()
    {
        pauseMenu.setPaused(false);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
