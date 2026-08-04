using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPower : MonoBehaviour
{
    [SerializeField] private GameObject _lightMask;

    [SerializeField] private bool _playSound = true;
    [SerializeField] private MusicManager _musicManager;

    private bool _isOn = false;

    public void TurnOn()
    {
        _lightMask.SetActive(true);
        if (!_isOn && _playSound && _musicManager != null)
        {
            _musicManager.PlaySE(2);
        }
        _isOn = true;
    }

    public void TurnOff()
    {
        _lightMask.SetActive(false);
        if (_playSound && _musicManager != null)
        {
            _musicManager.PlaySE(3);
        }
        _isOn = false;
    }
}
