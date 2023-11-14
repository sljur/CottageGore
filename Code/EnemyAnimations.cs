using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public GameObject deathParticles;
    private Animator _animator;
    private Shroom _enemy;
    private EnemyHealth _enemyHealth;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Shroom>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hit");
    }

    private float GetCurrentAnimationLength()
    {
        float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLength;
    }

    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() * 0.3f);
        _enemy.ResumeMovement();
        _animator.SetTrigger("Run");

    }

    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        _enemy.ResumeMovement();
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(_enemy.gameObject);
    }

    private void EnemyHit(Shroom enemy)
    {
        if (_enemy = enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    private void EnemyDead(Shroom enemy)
    {
        if (_enemy = enemy)
        {
            StartCoroutine(PlayDead());
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
        EnemyHealth.OnEnemyKilled += EnemyDead;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
        EnemyHealth.OnEnemyKilled -= EnemyDead;
    }

}
