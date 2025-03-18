using System;
using System.Collections;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    public event Action TimerTicked;

    private Coroutine _corutine;

    public void StartTimer(float delay)
    {
        _corutine = StartCoroutine(Countdown(delay));
    }

    private void OnDisable()
    {
        StopCoroutine(_corutine);
    }

    private IEnumerator Countdown(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            TimerTicked?.Invoke();

            yield return wait;
        }
    }
}