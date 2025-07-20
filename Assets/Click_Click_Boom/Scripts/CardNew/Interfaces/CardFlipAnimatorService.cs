using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipAnimatorService : MonoBehaviour, IAnimatorService
{
    private bool _isAnimating;
    public bool IsAnimating => _isAnimating;

    public void AnimateFlip(Transform target, Action onMidFlip, Action onComplete)
    {
        if (_isAnimating) return;
        StartCoroutine(FlipCoroutine(target, onMidFlip, onComplete));
    }

    private IEnumerator FlipCoroutine(Transform target, Action onMidFlip, Action onComplete)
    {
        Debug.Log("Starting flip...");
        _isAnimating = true;
        float duration = 0.5f;
        float halfTime = duration / 2f;
        float time = 0f;

        while (time < halfTime)
        {
            time += Time.deltaTime;
            float angle = Mathf.Lerp(0, 90, time / halfTime);
            target.localEulerAngles = new Vector3(0, angle, 0);
            yield return null;
        }

        onMidFlip?.Invoke();

        time = 0f;
        while (time < halfTime)
        {
            time += Time.deltaTime;
            float angle = Mathf.Lerp(90, 180, time / halfTime);
            target.localEulerAngles = new Vector3(0, angle, 0);
            yield return null;
        }

        target.localEulerAngles = Vector3.zero;
        _isAnimating = false;
        onComplete?.Invoke();
    }
}
