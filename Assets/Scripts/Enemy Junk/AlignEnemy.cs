using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignEnemy : MonoBehaviour
{
    private PathMovement pathMovement;
    private Vector3 targetPos;
    
    public void Initialization(PathMovement path, Vector3 vector, float delay, float speed)
    {
        pathMovement = GetComponent<PathMovement>();
        targetPos = vector;

        StartCoroutine(Delay(delay, speed));
    }

    private IEnumerator Delay(float delay2, float speed2)
    {
        float movementDistance;

        pathMovement.enabled = false;
        movementDistance = Vector3.Distance(this.transform.position, targetPos) * 100/speed2;
        yield return new WaitForSeconds(delay2);
        for (int i = 0; i <= movementDistance; i++)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, i/movementDistance);
            yield return new WaitForSeconds(0.01f);
        }
        pathMovement.enabled = true;
    }


}
