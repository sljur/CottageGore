using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shroom : MonoBehaviour
{
    // declare variables
    public float MoveSpeed = 2.0f;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastPointPosition;
    private int _currentWaypointIndex;
    private EnemyHealth _enemyHealth;
    public EnemyHealth EnemyHealth;
    private Vector3 CurrentPointPosition;

    public Waypoint Waypoint;
    public Action<Shroom> OnEndReached;

    public bool isMovementStopped;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    private Weapon _enemies;

    void Start()
    {
        // get the components for enemy health, enemies, and renderer
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyHealth = GetComponent<EnemyHealth>();
        EnemyHealth = GetComponent<EnemyHealth>();
        _enemies = GetComponent<Weapon>();

        // set the number of hearts based on the level
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "_Scene2")
        {
            Destroy(heart1);
        }
        else if (currentSceneName == "_Scene3")
        {
            Destroy(heart2);
        }
    }

    void Update()
    {
        // move the shroom on the waypoints
        Move();
        CurrentPointPosition = Waypoint.GetWayPointPosition(_currentWaypointIndex);
        Rotate();

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }

        _lastPointPosition = Waypoint.GetWayPointPosition(_currentWaypointIndex); ;
    }

    // move the shrooms along the waypoint
    private void Move()
    {
        Vector3 targetPosition = Waypoint.GetWayPointPosition(_currentWaypointIndex);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,
            MoveSpeed * Time.deltaTime);
    }

    // rotate the Shroom based on the waypoints
    private void Rotate()
    {
        if (CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }


    // check if the shrooms have gotten to the next waypoint
    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - Waypoint.GetWayPointPosition(_currentWaypointIndex)).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }
        return false;
    }

    // get the current waypoint
    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    // if the shroom reaches the end point
    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        EnemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);

        // destroy a heart based on how many hearts are left and how many shrooms have crossed
        if (heart1 != null)
        {
            Destroy(heart1);
        }
        else if (heart2 != null)
        {
            Destroy(heart2);
        }
        else if (heart3 != null)
        {
            Destroy(heart3);
            SceneManager.LoadScene("GameOverScene");
        }


    }

    // set the movement to stop and set to true
    public void StopMovement()
    {
        isMovementStopped = true;
    }

    // resume the movement and set to false
    public void ResumeMovement()
    {
        isMovementStopped = false;
    }

}
