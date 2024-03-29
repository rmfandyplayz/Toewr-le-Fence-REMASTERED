﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class PathMovement : MonoBehaviour {
    public float speed = 1;
    private float currentMaxSlowLevel = 0;
    public Polyline path;
    public bool enemyUpdating = false;
    int targetIndex = 1;
    Vector3 velocity;
    public Vector3 Velocity => velocity;
    Vector3 previousPos;

    public UnityEvent OnPathFinished;
    bool pathFinished = false;
    [SerializeField] bool pathWillLoop = false;

    public void UpdateSpeed()
    {
        velocity = (path.nodes[targetIndex] - this.transform.position).normalized * (speed - (speed * currentMaxSlowLevel/10)) ;
    }

	void Update () {
        if (!pathFinished && enemyUpdating == true)
        {
            var waypoint = path.nodes[targetIndex];
            if ((transform.position - path.nodes[targetIndex]).magnitude < (velocity * Time.deltaTime).magnitude)
            {
                transform.position = path.nodes[targetIndex];
                targetIndex++;
                if (targetIndex == path.nodes.Count)
                {
                    if(pathWillLoop)
                    {
                        targetIndex = 1;
                    }
                    else
                    {                    
                        pathFinished = true;
                        OnPathFinished?.Invoke();
                        return;
                    }
                }
                previousPos = Vector3.positiveInfinity;
                UpdateSpeed();
            }

            else if ((previousPos - waypoint).sqrMagnitude < (transform.position - waypoint).sqrMagnitude)
            {
                //transform.position = waypoint;
                UpdateSpeed();
                previousPos = transform.position;
                transform.position += velocity * Time.deltaTime;
            }

            else
            {
                previousPos = transform.position;
                UpdateSpeed();
                transform.position += velocity * Time.deltaTime;
            }
        }
	}

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public int ReturnIndex() => targetIndex;

    public void InitializePath(Polyline path, float speed, int targetIndex, Vector3 startPos)
    {
        this.path = path;
        this.speed = speed;
        this.targetIndex = targetIndex;
        this.transform.position = path.nodes[0];
    }

    public void Move()
    {
        previousPos = transform.position;
        UpdateSpeed();
        enemyUpdating = true;
    }

    private IEnumerator Delay (float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        Move();
    }

    public void StartDelay(float delay)
    {
        StartCoroutine(Delay(delay));
    }



}
