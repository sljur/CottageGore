using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    // declare variables
    private Animator _animator;
    private Shroom _enemy;
    private EnemyHealth _enemyHealth;

    void Start()
    {
        // get the components for animator, enemy, and enemyhealth
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Shroom>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    // play the hit animation when the weapon hits the enemy
    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hit");
    }

    // get the length of the animation
    private float GetCurrentAnimationLength()
    {
        float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLength;
    }

    // full range of motion for the hurt animation
    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() * 0.3f);
        _enemy.ResumeMovement();
        _animator.SetTrigger("Run");

    }

    // full range of motion for the dead animation
    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        yield return new WaitForSeconds(0.1f);
        _enemy.ResumeMovement();
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(_enemy.gameObject);
    }

    // play the hit animation when finding the enemy
    private void EnemyHit(Shroom enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    // play the dead animation when finding the enemy
    private void EnemyDead(Shroom enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayDead());
        }
    }
    
    // on enable for enemy hit and enemy killed
    private void OnEnable()
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
        EnemyHealth.OnEnemyKilled += EnemyDead;
    }

    // on disable for enemy hit and enemy killed
    private void OnDisable()
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
        EnemyHealth.OnEnemyKilled -= EnemyDead;
    }

}
