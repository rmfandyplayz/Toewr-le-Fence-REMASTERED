using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveObject", menuName = "Object/WaveObject")]
public class WaveObjects : ScriptableObject
{
    public List<EnemySetup> nonBossEnemies;
    public List<EnemySetup> bossEnemies;
    public int startWave = 0;
    public int endWave = 0;
    public int waveEnemyConstant = 0;
    public int waveEnemyMultiplier = 1;
}
