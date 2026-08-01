using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoader : MonoBehaviour
{
    [SerializeField] private bool _turnOnCamera;
    [SerializeField] private GameObject _cameraPrefab;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (_turnOnCamera)
            {
                _cameraPrefab.SetActive(true);
            }
            else
            {
                _cameraPrefab.SetActive(false);
            }
        }
    }
}
