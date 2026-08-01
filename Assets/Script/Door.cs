using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _isVerticalDoor;
    [SerializeField] private GameObject _doorTop;
    [SerializeField] private GameObject _doorBottom;

    [SerializeField] private float _doorOpenSpeed = 2f;

    private const float _doorOpenDistance = 2.5f;

    //define a transform variable to store the original position of the door
    private Vector3 _originalPositionTop;
    private Vector3 _originalPositionBottom;

    private Vector3 _targetPositionRight;
    private Vector3 _targetPositionLeft;

    //define a transform variable to store the target position of the door
    private Vector3 _targetPositionTopAndBottom;

    public void Start()
    {
        _originalPositionTop = _doorTop.transform.position;
        _originalPositionBottom = _doorBottom.transform.position;

        if (_isVerticalDoor)
        {
            _targetPositionTopAndBottom = new Vector3(_originalPositionTop.x, _originalPositionTop.y + _doorOpenDistance, _originalPositionTop.z);
        }
        else
        {
            _targetPositionRight = new Vector3(_originalPositionTop.x - _doorOpenDistance/2, _originalPositionTop.y, _originalPositionTop.z);
            _targetPositionLeft = new Vector3(_originalPositionTop.x + _doorOpenDistance/2, _originalPositionTop.y, _originalPositionTop.z);
        }
    }

    public void OpenDoor()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //move the door to the target position using dotween
        if (_isVerticalDoor)
        {
            _doorTop.transform.DOMove(_targetPositionTopAndBottom, _doorOpenSpeed);
            _doorBottom.transform.DOMove(_targetPositionTopAndBottom, _doorOpenSpeed);
        }
        else
        {
            _doorTop.transform.DOMove(_targetPositionRight, _doorOpenSpeed);
            _doorBottom.transform.DOMove(_targetPositionLeft, _doorOpenSpeed);
        }
    }

    public void CloseDoor()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //move the door back to the original position using dotween
        _doorTop.transform.DOMove(_originalPositionTop, _doorOpenSpeed);
        _doorBottom.transform.DOMove(_originalPositionBottom, _doorOpenSpeed);
    }
}
