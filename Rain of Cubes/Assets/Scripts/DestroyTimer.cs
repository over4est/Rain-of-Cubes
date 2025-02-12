using System;
using System.Collections;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public event Action TimerTicked;

    public void StartTimer(float delay)
    {
        StartCoroutine(Countdown(delay));
    }

    private IEnumerator Countdown(float delay)
    {
        yield return new WaitForSeconds(delay);

        TimerTicked?.Invoke();
    }
}