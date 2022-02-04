using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour
{

    void OnEnable()
    {
        SpawningManagement.aliveEnemies++;
    }

    void OnDisable()
    {
        SpawningManagement.aliveEnemies--;
    }

}
