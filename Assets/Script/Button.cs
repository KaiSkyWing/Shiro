using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] private bool _isEventButton = true;
    [SerializeField] private bool _playSound = true;
    [SerializeField] private MusicManager _musicManager;

    [Header("For Event Button")]
    [SerializeField] private bool _triggerOnlyOnce = true;
    [SerializeField] private Sprite _whiteButtonSprite;
    [SerializeField] private Sprite _whiteButtonPressedSprite;
    [SerializeField] private Sprite _blackButtonSprite;
    [SerializeField] private Sprite _blackButtonPressedSprite;
    
    [SerializeField] private UnityEvent _onPressed;
    [SerializeField] private UnityEvent _onReleased;

    private SpriteRenderer _whiteButton;
    private SpriteRenderer _blackButton;

    [Header("For BoxReset Button")]
    [SerializeField] private GameObject[] _boxesToReset;
    [SerializeField] private Sprite _whiteResetButtonSprite;
    [SerializeField] private Sprite _whiteResetButtonPressedSprite;
    [SerializeField] private Sprite _blackResetButtonSprite;
    [SerializeField] private Sprite _blackResetButtonPressedSprite;

    private SpriteRenderer _whiteResetButton;
    private SpriteRenderer _blackResetButton;
    private Vector3[] _boxInitialPosition;


    private bool _pressed;

    private void Awake()
    {

        if (_isEventButton)
        {
            _whiteButton = transform.Find("WhiteButton")?.GetComponent<SpriteRenderer>();
            _blackButton = transform.Find("BlackButton")?.GetComponent<SpriteRenderer>();
            SetEventButtonSprites(false);
        }
        else
        {
            _boxInitialPosition = new Vector3[_boxesToReset.Length];

            for (int i = 0; i < _boxesToReset.Length; i++)
            {
                _boxInitialPosition[i] = _boxesToReset[i].transform.position;
            }

            _whiteResetButton = transform.Find("WhiteResetButton")?.GetComponent<SpriteRenderer>();
            _blackResetButton = transform.Find("BlackResetButton")?.GetComponent<SpriteRenderer>();
            SetResetButtonSprites(false);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Box")&& !other.CompareTag("CurlingStone"))
            return;

        if (_playSound && _musicManager != null)
        {
            _musicManager.PlaySE(0);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_pressed && _triggerOnlyOnce)
            return;

        if (!other.CompareTag("Player") && !other.CompareTag("Box")&& !other.CompareTag("CurlingStone"))
            return;

        _pressed = true;

        if(_isEventButton)
        {
            SetEventButtonSprites(true);
            _onPressed.Invoke();
        }
        else
        {
            SetResetButtonSprites(true);

            foreach (GameObject box in _boxesToReset)
            {
                if (box != null)
                {
                    int index = System.Array.IndexOf(_boxesToReset, box);
                    box.transform.position = _boxInitialPosition[index];
                }
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_triggerOnlyOnce || !other.CompareTag("Player") && !other.CompareTag("Box") && !other.CompareTag("CurlingStone"))
            return;

        _pressed = false;

        if (_isEventButton)
        {
            SetEventButtonSprites(false);
            _onReleased.Invoke();
        }
        else
        {
            SetResetButtonSprites(false);
        }

        if (_playSound && _musicManager != null)
        {
            _musicManager.PlaySE(1);
        }
    }

    private void SetEventButtonSprites(bool pressed)
    {
        if (_whiteButton != null)
            _whiteButton.sprite = pressed ? _whiteButtonPressedSprite : _whiteButtonSprite;

        if (_blackButton != null)
            _blackButton.sprite = pressed ? _blackButtonPressedSprite : _blackButtonSprite;
    }

    private void SetResetButtonSprites(bool pressed)
    {
        if (_whiteResetButton != null)
            _whiteResetButton.sprite = pressed ? _whiteResetButtonPressedSprite : _whiteResetButtonSprite;

        if (_blackResetButton != null)
            _blackResetButton.sprite = pressed ? _blackResetButtonPressedSprite : _blackResetButtonSprite;
    }
}