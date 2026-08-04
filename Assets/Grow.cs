using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    [Header("Black")]
    [SerializeField] private SpriteRenderer _blackFlowerRenderer;
    [SerializeField] private Sprite[] _blackFlowerSprites = new Sprite[5];

    [Header("White")]
    [SerializeField] private SpriteRenderer _whiteFlowerRenderer;
    [SerializeField] private Sprite[] _whiteFlowerSprites = new Sprite[5];

    [Header("Music")]
    [SerializeField] private MusicManager _musicManager;
    [SerializeField] private AudioClip _endingBGM;

    [SerializeField] private GameObject _endingPanel;

    [SerializeField] private Player _player;

    [SerializeField] private bool _triggerOnce = true;
    private bool _hasTriggered = false;

    private float _animationDuration = 3f;

    public void StartAnimation()
    {
        if (_triggerOnce && _hasTriggered)
        {
            return;
        }

        _hasTriggered = true;
        _player.StopMovement();
        StartCoroutine(AnimateFlowersToFive());
    }

    private IEnumerator AnimateFlowersToFive()
    {
        SetFlowerCount(1);

        int maxCount = 5;
        float interval = _animationDuration / (maxCount - 1);

        for (int count = 2; count <= maxCount; count++)
        {
            yield return new WaitForSeconds(interval);
            SetFlowerCount(count);
        }

        yield return new WaitForSeconds(1f);

        if (_endingPanel != null)
        {
            _endingPanel.SetActive(true);

            if (_musicManager != null && _endingBGM != null)
            {
                _musicManager.SetBGMClip(_endingBGM);
            }
        }
    }

    private void SetFlowerCount(int count)
    {
        int spriteIndex = Mathf.Clamp(count - 1, 0, 4);

        if (_blackFlowerRenderer != null && _blackFlowerSprites != null && _blackFlowerSprites.Length > spriteIndex)
        {
            _blackFlowerRenderer.sprite = _blackFlowerSprites[spriteIndex];
        }

        if (_whiteFlowerRenderer != null && _whiteFlowerSprites != null && _whiteFlowerSprites.Length > spriteIndex)
        {
            _whiteFlowerRenderer.sprite = _whiteFlowerSprites[spriteIndex];
        }
    }
}
