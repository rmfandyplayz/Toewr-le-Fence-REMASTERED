using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PathMovement : MonoBehaviour {
    public float speed = 1;
    public Polyline path;
    public bool enemyUpdating = false;
    int targetIndex = 1;
    Vector3 velocity;

    public UnityEvent OnPathFinished;

    // Update is called once per frame
    bool pathFinished = false;
	void Update () {
        if (!pathFinished && enemyUpdating == true)
        {
            if ((transform.position - path.nodes[targetIndex]).magnitude < (velocity * Time.deltaTime).magnitude)
            {
                transform.position = path.nodes[targetIndex];
                targetIndex++;
                Debug.Log(targetIndex);
                if (targetIndex == path.nodes.Count)
                {
                    pathFinished = true;
                    OnPathFinished?.Invoke();
                    return;
                }
                velocity = (path.nodes[targetIndex] - path.nodes[targetIndex - 1]).normalized * speed;
            }
            else
                transform.position += velocity * Time.deltaTime;
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
