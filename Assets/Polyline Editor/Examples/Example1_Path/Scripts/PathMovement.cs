using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PathMovement : MonoBehaviour {
    public float speed = 1;
    public Polyline path;
    public bool enemyUpdating = false;
    int targetIndex = 1;
    Vector3 velocity;
    private Vector3 previousPos;

    public UnityEvent OnPathFinished;
    bool pathFinished = false;






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
                    pathFinished = true;
                    OnPathFinished?.Invoke();
                    return;
                }
                previousPos = Vector3.positiveInfinity;
                velocity = (path.nodes[targetIndex] - path.nodes[targetIndex - 1]).normalized * speed;
            }

            else if ((previousPos - waypoint).sqrMagnitude < (transform.position - waypoint).sqrMagnitude)
            {
                //transform.position = waypoint;
                velocity = (path.nodes[targetIndex] - this.transform.position).normalized * speed;
                previousPos = transform.position;
                transform.position += velocity * Time.deltaTime;

                Debug.LogWarning("Teleported");
            }

            else
            {
                previousPos = transform.position;
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
        velocity = (path.nodes[targetIndex] - path.nodes[targetIndex - 1]).normalized * speed;
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
