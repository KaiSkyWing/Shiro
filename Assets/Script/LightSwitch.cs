using UnityEngine.Events;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPressed;
    [SerializeField] private UnityEvent _onReleased;
    [SerializeField] private bool _onlyTriggerOnce = true;

    /*
    [SerializeField] private Sprite _whiteButtonSprite;
    [SerializeField] private Sprite _whiteButtonPressedSprite;
    [SerializeField] private Sprite _blackButtonSprite;
    [SerializeField] private Sprite _blackButtonPressedSprite;

    private SpriteRenderer _whiteButton;
    private SpriteRenderer _blackButton;

    private void Awake()
    {

        _whiteButton = transform.Find("WhiteButton")?.GetComponent<SpriteRenderer>();
        _blackButton = transform.Find("BlackButton")?.GetComponent<SpriteRenderer>();
        SetLightButtonSprites(false);
    }
    */

    private void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("LightSwitch Triggered by: " + collider.name);
        if (collider.CompareTag("LightMask"))
        {
            Debug.Log("LightSwitch Activated by LightMask");
            _onPressed.Invoke();
            //SetLightButtonSprites(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (_onlyTriggerOnce)
            return;
        Debug.Log("LightSwitch Trigger Exit by: " + collider.name);
        if (collider.CompareTag("LightMask"))
        {
            _onReleased.Invoke();
            //SetLightButtonSprites(false);
        }
    }

    /*
    private void SetLightButtonSprites(bool pressed)
    {
        if (_whiteButton != null)
            _whiteButton.sprite = pressed ? _whiteButtonPressedSprite : _whiteButtonSprite;

        if (_blackButton != null)
            _blackButton.sprite = pressed ? _blackButtonPressedSprite : _blackButtonSprite;
    }
    */
}
