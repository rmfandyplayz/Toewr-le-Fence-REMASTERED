using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ZeusUnite.Audio;
//using ZeusUnite.Audio;


public enum damageIndicatorType
{
    normieDamage, dankDamage, surrealDamage, mlgNoScope
};

public class DamageIndicatorManager : MonoBehaviour
{
    public SerializedDictionary<damageIndicatorType, DamageIndicatorPoolInfo> damageDictionary;

    private void Start()
    {
        foreach (var damagePrefab in damageDictionary.Values)
        {
            ObjectPooling.Preload(damagePrefab.indicatorPrefab, damagePrefab.startAmount);
        }
    }





    public void GenerateIndicator(DamageInfo damageInfo)
    {
        Debug.Log($"Now Playing {GetComponent<AudioSource>()}");
        GameObject damagePrefab = null;
        if (damageDictionary.ContainsKey(damageInfo.damageType))
        {
            damagePrefab = ObjectPooling.GetGameObject(damageDictionary[damageInfo.damageType].indicatorPrefab, alreadyActive : false);
        }
        if (damagePrefab != null)
        {
            damagePrefab.GetComponent<DamageIndicator>().InitializeIndicator(damageInfo.damage, damageInfo.damageType, damageInfo.position, damageInfo.velocity);
            damagePrefab.SetActive(true);
        }

        /*
        if(damageInfo.damageType == damageinfo.damageType.dank)
        {
            AudioController
            PlaySound(<sound>)
            PlayMusicOnRepeat()
            PlayMusic()
            StopMusic()
            StopSound()
        }


         */
    }








}

[System.Serializable]
public class DamageIndicatorPoolInfo
{
    public int startAmount = 75;
    public GameObject indicatorPrefab;
}
