using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
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
        CreateHealthBar();
        CurrentHealth = initialHealth;
        _enemy = GetComponent<Shroom>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f);
        }


        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, 
            CurrentHealth / maxHealth, Time.deltaTime * 10f);

    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);
        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

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

    public void Die()
    {
        OnEnemyKilled?.Invoke(_enemy);
        gameObject.SetActive(false);
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
    }
}
