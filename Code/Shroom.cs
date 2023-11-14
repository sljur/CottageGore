using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour
{
    private float MoveSpeed = 2.0f;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastPointPosition;
    private int _currentWaypointIndex;
    private EnemyHealth _enemyHealth;

    public Waypoint Waypoint;
    public Action<Shroom> OnEndReached;

    private bool isMovementStopped;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (!isMovementStopped)
        {
            Move();
            Rotate();

            if (CurrentPointPositionReached())
            {
                UpdateCurrentPointIndex();
            }
        }
    }

    private void Move()
    {
        Vector3 targetPosition = Waypoint.GetWayPointPosition(_currentWaypointIndex);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,
            MoveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 currentPosition = transform.position;

        if (Waypoint.Points[_currentWaypointIndex].x > currentPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }

        _lastPointPosition = currentPosition;
    }


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

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void StopMovement()
    {
        isMovementStopped = true;
    }

    public void ResumeMovement()
    {
        isMovementStopped = false;
    }
}
