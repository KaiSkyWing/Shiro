using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [Header("BGM")]
    [SerializeField] private AudioClip _bgmClip;
    [SerializeField] private float _bgmVolume = 1f;
    [SerializeField] private bool _bgmEnabled = true;

    [SerializeField] private Slider _bgmVolumeSlider;
    [SerializeField] private Slider _seVolumeSlider;

    [System.Serializable]
    private class SEEntry
    {
        public AudioClip Clip;
        [Range(0f, 1f)] public float Volume = 1f;
    }

    [Header("SE")]
    [SerializeField] private SEEntry[] _seEntries;
    [SerializeField] private float _seMasterVolume = 1f;
    [SerializeField] private bool _seEnabled = true;

    private AudioSource _bgmSource;
    private AudioSource _seSource;

    private void Awake()
    {
        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = false;
        _bgmSource.volume = _bgmVolume;

        _seSource = gameObject.AddComponent<AudioSource>();
        _seSource.loop = false;
        _seSource.playOnAwake = false;
        _seSource.volume = _seMasterVolume;
    }

    private void Start()
    {
        if (_bgmEnabled)
        {
            PlayBGM();
        }
    }

    public void PlayBGM()
    {
        if (!_bgmEnabled || _bgmClip == null)
        {
            return;
        }

        if (_bgmSource.clip != _bgmClip)
        {
            _bgmSource.clip = _bgmClip;
        }

        if (!_bgmSource.isPlaying)
        {
            _bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if (_bgmSource.isPlaying)
        {
            _bgmSource.Stop();
        }
    }

    public void SetBGMEnabled(bool enabled)
    {
        _bgmEnabled = enabled;

        if (enabled)
        {
            PlayBGM();
        }
        else
        {
            StopBGM();
        }
    }

    public void ToggleBGM()
    {
        SetBGMEnabled(!_bgmEnabled);
    }

    public void SetBGMClip(AudioClip clip, bool playImmediately = true)
    {
        _bgmClip = clip;
        if (playImmediately && _bgmEnabled)
        {
            PlayBGM();
        }
    }

    public void PlaySE(int index)
    {
        if (!_seEnabled || _seEntries == null || index < 0 || index >= _seEntries.Length)
        {
            return;
        }

        SEEntry entry = _seEntries[index];
        if (entry == null || entry.Clip == null)
        {
            return;
        }

        float volume = Mathf.Clamp01(entry.Volume * _seMasterVolume);
        _seSource.PlayOneShot(entry.Clip, volume);
    }

    public void PlaySE(AudioClip clip, float volume = 1f)
    {
        if (!_seEnabled || clip == null)
        {
            return;
        }

        float finalVolume = Mathf.Clamp01(volume * _seMasterVolume);
        _seSource.PlayOneShot(clip, finalVolume);
    }

    public void SetSEEnabled(bool enabled)
    {
        _seEnabled = enabled;
    }

    public void SetSEMasterVolume(float volume)
    {
        _seMasterVolume = Mathf.Clamp01(volume);
        if (_seSource != null)
        {
            _seSource.volume = _seMasterVolume;
        }
    }

    public float GetSEMasterVolume()
    {
        return _seMasterVolume;
    }

    public void SetBGMVolume(float volume)
    {
        _bgmVolume = Mathf.Clamp01(volume);
        if (_bgmSource != null)
        {
            _bgmSource.volume = _bgmVolume;
        }
    }

    public float GetBGMVolume()
    {
        return _bgmVolume;
    }

    public void SetSEVolume(int index, float volume)
    {
        if (_seEntries == null || index < 0 || index >= _seEntries.Length)
        {
            return;
        }

        _seEntries[index].Volume = Mathf.Clamp01(volume);
    }

    public void ToggleSE()
    {
        SetSEEnabled(!_seEnabled);
    }

    public bool IsBGMEnabled()
    {
        return _bgmEnabled;
    }

    public bool IsSEEnabled()
    {
        return _seEnabled;
    }
}
