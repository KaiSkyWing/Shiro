using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPower : MonoBehaviour
{
    [SerializeField] private GameObject _lightMask;

    public void TurnOn()
    {
        _lightMask.SetActive(true);
    }

    public void TurnOff()
    {
        _lightMask.SetActive(false);
    }
}
