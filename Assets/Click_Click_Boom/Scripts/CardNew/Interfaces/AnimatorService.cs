using System;
using UnityEngine;

public interface IAnimatorService
{
    void AnimateFlip(Transform target, Action onMidFlip, Action onComplete);
    bool IsAnimating { get; }
}
