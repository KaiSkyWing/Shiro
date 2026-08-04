using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseIconObject;
    [SerializeField] private Player _player;
    [SerializeField] private MusicManager _musicManager;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _seSlider;

    private void Start()
    {
        ResumeGame();

        if (_bgmSlider != null)
        {
            _bgmSlider.onValueChanged.AddListener(val => {
                if (_musicManager != null) _musicManager.SetBGMVolume(val);
            });
        }

        if (_seSlider != null)
        {
            _seSlider.onValueChanged.AddListener(val => {
                if (_musicManager != null) _musicManager.SetSEMasterVolume(val);
            });
        }
    }

    public void PauseGame()
    {
        Debug.Log("Pause");
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _pauseIconObject.SetActive(false);
        _player.IsPaused = true;
        if (_musicManager != null)
        {
            if (_bgmSlider != null) _bgmSlider.value = _musicManager.GetBGMVolume();
            if (_seSlider != null) _seSlider.value = _musicManager.GetSEMasterVolume();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
        _pauseIconObject.SetActive(true);
        _player.IsPaused = false;
    }
}
