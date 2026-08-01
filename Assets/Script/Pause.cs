using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseIconObject;
    [SerializeField] private Player _player;

    private void Start()
    {
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _pauseIconObject.SetActive(false);
        _player.IsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
        _pauseIconObject.SetActive(true);
        _player.IsPaused = false;
    }
}
