using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController: MonoBehaviour {
    public event Action OnAttackStartHitbox;
    public event Action OnAttackEndHitbox;
    public event Action OnAnimationStart;
    public event Action OnAnimationFinish;

    public void AnimationStartTrigger() => OnAnimationStart?.Invoke();
    public void AnimationFinishTrigger() => OnAnimationFinish?.Invoke();

    public void AttackStartHitbox() => OnAttackStartHitbox?.Invoke();
    public void AttackEndHitbox() => OnAttackEndHitbox?.Invoke();
}
