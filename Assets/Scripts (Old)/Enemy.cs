using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [Header("Floats")]
    public float speed = 10f;
    public float overallHealth = 1000;
    public float currentHealth;
    public float currentShield;
    public float overallShields = 15;
    public float firedamageamount = 0.05f;
    public float currentFire;
    private float firedmgpertick = 1f;
    [Header("Boolians")]
    public bool hasShield;
    public bool IsSlow;
    public bool TakesFireDmg = false;
    [Header("Enemy Money Earning Config")]
    public bool isFastEnemy = false;
    public bool isObsidianEnemy = false;
    public bool isShieldedEnemy = false;
    public bool isShieldedFastEnemy = false;
    public bool isShieldedObsidianEnemy = false;
    public bool isKnucklesEnemy = false;
    [Header("Other")]
    [SerializeField]
    private Transform target;
    private int wavepointIndex = 0;
    Tower_Health towerhealth;
    public Image healthbar;
    public Image Shieldbar;
    public Image firebar;
    //public GameObject DeathEffect;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = overallHealth;
        if (hasShield == true)
        {
            currentShield = overallShields;
        }
        target = WaypointsSetting.points[0];
        //towerhealth = GameObject.Find("Tower").GetComponent<Tower_Health>();
    }

    void Update()
    {
        UpdateHealthBar();
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            GetNextWaypoint();
        }

        if (TakesFireDmg == true && currentHealth >= 1)
        {
            //if health <= 1/3 then: takesfire
            TakeShieldDamage(firedamageamount, 1f,0);
            //change this part!
            
        }

    }


    void UpdateHealthBar()
    {
        healthbar.fillAmount = currentHealth / overallHealth;
        if (TakesFireDmg == true)
        {
            firedmgpertick = currentFire * 0.000833333333f;
        }

        if (TakesFireDmg == false && currentFire > 0)
        {
            currentFire -= firedmgpertick;
            currentFire = Mathf.Max(0, currentFire);

        }

        firebar.fillAmount = (currentFire + currentHealth) / overallHealth;
        if (hasShield == true)
            Shieldbar.fillAmount = currentShield / overallShields;
    }


    void GetNextWaypoint()
    {
        if (wavepointIndex >= WaypointsSetting.points.Length - 1)
        {
            //towerhealth.Damage(1);
            //GameObject effect = Instantiate(DeathEffect, transform.position, transform.rotation);
            //Destroy(effect, 5);
            Destroy(gameObject);
        }
        wavepointIndex++;
        if (wavepointIndex >= WaypointsSetting.points.Length)
        {
            return;
        }
        target = WaypointsSetting.points[wavepointIndex];

    }
    public void TakeShieldDamage(float sd, float SlowDown,float slowDuration) //sd = shield damage
    {
        if (!IsSlow)
        {
            StartCoroutine(Slowdown(SlowDown,slowDuration));
        }
        if (!hasShield) {
            TakeDamage(sd, SlowDown,slowDuration);
            return;
        }
        currentShield -= sd;

        if (currentShield <= 0)
        {
            Destroy(Shieldbar);
            transform.tag = "Enemy";
            hasShield = false;
        }


        Shieldbar.fillAmount = currentShield / overallShields;
        DamagePopup.Create(healthbar.transform.position, sd);

    }
    public void TakeDamage(float d, float SlowDown, float slowDuration) //d = damage
    {
        if (!IsSlow) {
            StartCoroutine(Slowdown(SlowDown,slowDuration));
        }
        

        if (TakesFireDmg == true)
        {
            if (currentHealth > 0)
            {
                currentFire += d;
                currentHealth -= d;
                currentHealth = Mathf.Clamp(currentHealth, 0, overallHealth);
            }
            else
            {
                currentFire -= d;
            }

           
        }
        else
        {
            if (currentHealth > 0)
            {
                currentHealth -= d;
                currentHealth = Mathf.Clamp(currentHealth, 0, overallHealth);
            }
            else
            {
                currentFire -= d;
            }
        }

        UpdateHealthBar(); //currentHealth divided by overallHealth
        DamagePopup.Create(healthbar.transform.position, d); //<DamagePopup> Instantiate
        if (currentHealth + currentFire <= 0)
        {

            //GameObject effect = Instantiate(DeathEffect, transform.position, transform.rotation * Quaternion.Euler(new Vector3 (90, 0 ,0)));
            //Destroy(effect, 5);

            GameObject.Find("Money Controller").GetComponent<money>().giveMoney(30); //gives the player $20 after each kill
            GameObject.Find("Score Counter").GetComponent<scoreCounter>().IncreaseScore(5);
            Destroy(gameObject);
            if(isFastEnemy == true)
            {
                GameObject.Find("Money Controller").GetComponent<money>().giveMoney(10);
                GameObject.Find("Score Counter").GetComponent<scoreCounter>().IncreaseScore(2);
            }
            if (isObsidianEnemy == true)
            {
                GameObject.Find("Money Controller").GetComponent<money>().giveMoney(10);
                GameObject.Find("Score Counter").GetComponent<scoreCounter>().IncreaseScore(3);
            }
            if (isShieldedEnemy == true)
            {
                GameObject.Find("Money Controller").GetComponent<money>().giveMoney(10);
                GameObject.Find("Score Counter").GetComponent<scoreCounter>().IncreaseScore(5);
            }
            if (isShieldedFastEnemy == true)
            {
                GameObject.Find("Money Controller").GetComponent<money>().giveMoney(5);
                GameObject.Find("Score Counter").GetComponent<scoreCounter>().IncreaseScore(2);
            }
            if (isShieldedObsidianEnemy == true)
            {
                GameObject.Find("Money Controller").GetComponent<money>().giveMoney(10);
                GameObject.Find("Score Counter").GetComponent<scoreCounter>().IncreaseScore(5);
            }
            if (isKnucklesEnemy == true)
            {
                GameObject.Find("Money Controller").GetComponent<money>().giveMoney(15);
                GameObject.Find("Score Counter").GetComponent<scoreCounter>().IncreaseScore(8);
            }
        }
    }

    public float TakeHP()
    {
        return currentHealth;
    }

    IEnumerator Slowdown(float SlowAmount, float slowDuration)
    {
        IsSlow = true;
        float OldSpeed = speed;
        speed = (1-(SlowAmount/100))*speed;
        yield return new WaitForSeconds(slowDuration);
        IsSlow = false;
        speed = OldSpeed;
    }
    private void OnParticleCollision(GameObject other)
    {
        //Debug.LogWarning(other.gameObject);
        AllTurrets flamethrower = other.gameObject.GetComponent<FlameStreamCache>().GetParent();
        Debug.LogWarning(flamethrower);
        if (flamethrower?.IsFlamethrower ?? false && currentHealth > 1.5f)
        {
            TakesFireDmg = true;
            TakeDamage(flamethrower.fireDamage, 0,0);
            firedamageamount = flamethrower.fireDamage;
            StartCoroutine (TakeFireDmg());
            
        }
        
    }
    
    IEnumerator TakeFireDmg()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        TakesFireDmg = false;
    }

    private void OnDestroy()
    {

        WaveSpawner.RemoveEnemies(1);
        
    }

    //Tag is Night

}