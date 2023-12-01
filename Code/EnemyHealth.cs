using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // declare variables
    public static Action<Shroom> OnEnemyKilled;
    public static Action<Shroom> OnEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;
    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }

    private Image _healthBar;
    private Shroom _enemy;

    void Start()
    {
        // set the health bar and get the Shroom component for the shrooms
        CreateHealthBar();
        CurrentHealth = initialHealth;
        _enemy = GetComponent<Shroom>();
    }

    // set the amount in the health bar with the current health
    private void Update()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount,
            CurrentHealth / maxHealth, Time.deltaTime * 10f);

    }

    // create the health bar and the fill amount
    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);
        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    // deal damage to the shrooms and kill if the health is less than or equal to 0
    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }

    // kill the enemy and set the enemy to false
    public void Die()
    {
        OnEnemyKilled?.Invoke(_enemy);
        gameObject.SetActive(false);
    }

    // set the health back to the initial health
    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
    }
}
