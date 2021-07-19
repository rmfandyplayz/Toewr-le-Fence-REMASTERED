using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour
{

    private void Start()
    {
        SpawningManagement.aliveEnemies++;
    }

    void OnDestroy()
    {
        SpawningManagement.aliveEnemies--;
    }

}
