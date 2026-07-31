using System.Collections;
using UnityEngine;

public class Lightening : MonoBehaviour
{
    [SerializeField] private bool willLightening = false;
    [SerializeField] private GameObject lighteningMask;
    [SerializeField] private float minLighteningDuration = 0.5f;
    [SerializeField] private float maxLighteningDuration = 1f;
    [SerializeField] private float minLighteningDelay = 5f;
    [SerializeField] private float maxLighteningDelay = 10f;

    private Coroutine lighteningCoroutine;

    private void Start()
    {
        if (lighteningMask != null)
        {
            lighteningMask.SetActive(false);
        }

        if (willLightening)
        {
            StartLightening();
        }
    }

    private void Update()
    {
        if (willLightening && lighteningCoroutine == null)
        {
            StartLightening();
        }
        else if (!willLightening && lighteningCoroutine != null)
        {
            StopLightening();
        }
    }

    private void StartLightening()
    {
        if (lighteningMask == null)
            return;

        lighteningCoroutine = StartCoroutine(LighteningRoutine());
    }

    private void StopLightening()
    {
        if (lighteningCoroutine != null)
        {
            StopCoroutine(lighteningCoroutine);
            lighteningCoroutine = null;
        }

        if (lighteningMask != null)
        {
            lighteningMask.SetActive(false);
        }
    }

    private IEnumerator LighteningRoutine()
    {
        while (true)
        {
            float activeTime = Random.Range(
                Mathf.Min(minLighteningDuration, maxLighteningDuration),
                Mathf.Max(minLighteningDuration, maxLighteningDuration)
            );
            float restTime = Random.Range(
                Mathf.Min(minLighteningDelay, maxLighteningDelay),
                Mathf.Max(minLighteningDelay, maxLighteningDelay)
            );

            lighteningMask.SetActive(true);
            yield return new WaitForSeconds(activeTime);

            lighteningMask.SetActive(false);
            yield return new WaitForSeconds(restTime);
        }
    }
}
