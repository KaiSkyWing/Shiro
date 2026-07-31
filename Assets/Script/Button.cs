using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private bool _triggerOnlyOnce = true;
    [SerializeField] private Sprite _whiteButtonSprite;
    [SerializeField] private Sprite _whiteButtonPressedSprite;
    [SerializeField] private Sprite _blackButtonSprite;
    [SerializeField] private Sprite _blackButtonPressedSprite;
    
    [SerializeField] private UnityEvent _onPressed;
    [SerializeField] private UnityEvent _onReleased;

    private SpriteRenderer _whiteButton;
    private SpriteRenderer _blackButton;

    private bool _pressed;

    private void Awake()
    {
        if (_whiteButton == null)
            _whiteButton = transform.Find("WhiteButton")?.GetComponent<SpriteRenderer>();

        if (_blackButton == null)
            _blackButton = transform.Find("BlackButton")?.GetComponent<SpriteRenderer>();

        SetButtonSprites(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_pressed && _triggerOnlyOnce)
            return;

        if (!other.CompareTag("Player") && !other.CompareTag("Box"))
            return;

        _pressed = true;
        SetButtonSprites(true);
        _onPressed.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_triggerOnlyOnce || !other.CompareTag("Player") && !other.CompareTag("Box"))
            return;

        _pressed = false;
        SetButtonSprites(false);
        _onReleased.Invoke();
    }

    private void SetButtonSprites(bool pressed)
    {
        if (_whiteButton != null)
            _whiteButton.sprite = pressed ? _whiteButtonPressedSprite : _whiteButtonSprite;

        if (_blackButton != null)
            _blackButton.sprite = pressed ? _blackButtonPressedSprite : _blackButtonSprite;
    }
}