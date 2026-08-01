using System;
using UnityEngine;
using DG.Tweening;

public class FadeControl : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _blackSprite;
    [SerializeField] private float _fadeDuration;

    private Tween _fadeTween;

    private void Awake()
    {
        if (_blackSprite == null)
            return;

        SetAlpha(0f);
    }

    public void FadeOut(Action onComplete)
    {
        if (_blackSprite == null)
        {
            onComplete?.Invoke();
            return;
        }

        _fadeTween?.Kill();

        _fadeTween = _blackSprite
            .DOFade(1f, _fadeDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                _fadeTween = null;
                SetAlpha(0f);
                onComplete?.Invoke();
            });
    }

    private void SetAlpha(float alpha)
    {
        Color color = _blackSprite.color;
        color.a = alpha;
        _blackSprite.color = color;
    }
}
