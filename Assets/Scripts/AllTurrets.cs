using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AllTurrets : MonoBehaviour {
    private Transform target;
    [Header("Game Setup Fields")]
    public string enemyTag = "Enemy";
    public string FastEnemyTag = "Fast Enemy";
    public string ObsidianEnemyTag = "Obsidian Enemy";
    public string ShieldedEnemyTag = "Shielded Enemy";
    public string ShieldedFastEnemyTag = "Shielded Fast Enemy";
    public string ShieldedObsidianEnemyTag = "Shielded Obsidian Enemy";
    public string KnucklesEnemyTag = "Knuckles Enemy";
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public GameObject FlamethrowerParticlePrefab;
    public Animator anim;
    
    [Header("Floats and Boolians")]
    public bool isTNTYeeter = false;
    public bool is8Shooter;
    public bool HasAnimation;
    public bool isnotblack;
    public bool IsFlamethrower = false;
    public float fireDamage;
    public float turnSpeed = 10f;
    public bool CanRotate = true;
    public float range = 15f;
    public float fireRate = 0.75f;
    public float firecountdown = 0.5f;
    public bool IsFreezeTower = false;
    public float SlowRate = 1f;
    public bool dragging;
    public bool despacito = true;   //whether to track the object or not
    public bool iscollidingwithtower = false;
    public float buyPrice = 0;
    public turretbullet bulletobject;
    private bool flameStreamActive = false;






  
    // Use this for initialization
    void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        isnotblack = true;

        bulletobject.CopyRefrence(bulletPrefab.GetComponent<BulletScript>());

    }


    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject[] Fastenemies = GameObject.FindGameObjectsWithTag(FastEnemyTag);
        GameObject[] Obsidianenemies = GameObject.FindGameObjectsWithTag(ObsidianEnemyTag);
        GameObject[] ShieldedEnemies = GameObject.FindGameObjectsWithTag(ShieldedEnemyTag);
        GameObject[] ShieldedFastEnemies = GameObject.FindGameObjectsWithTag(ShieldedFastEnemyTag);
        GameObject[] ShieldedObsidianEnemies = GameObject.FindGameObjectsWithTag(ShieldedObsidianEnemyTag);
        GameObject[] KnucklesEnemies = GameObject.FindGameObjectsWithTag(KnucklesEnemyTag);
        List<GameObject> allenemies = new List<GameObject>();
        allenemies.AddRange(enemies);
        allenemies.AddRange(Fastenemies);
        allenemies.AddRange(Obsidianenemies);
        allenemies.AddRange(ShieldedEnemies);
        allenemies.AddRange(ShieldedFastEnemies);
        allenemies.AddRange(ShieldedObsidianEnemies);
        allenemies.AddRange(KnucklesEnemies);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in allenemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }

    }
    // Update is called once per frame
    void Update() {
        if (dragging)
            return;
        if (target == null)
        {
            if (IsFlamethrower == true)
            {
                StopFlameStream();

            }
            else
            {
                return;
            }

        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        
        if (CanRotate == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
        if (firecountdown <= 0f && dragging == false)
        {

            if (HasAnimation == true)
            {
                anim.SetTrigger("Throw");
                StartCoroutine(animationwait());

            }
            else
            {
                Shoot();
                


            }
            //firecountdown = 1f / fireRate;
            firecountdown = fireRate;
        }

        
        else {
            firecountdown -= Time.deltaTime;
        }
        
    }

    IEnumerator animationwait()
    {
        yield return new WaitForSeconds(0.5f);
        Shoot();
    }

    void StopFlameStream()
    {
        if(flameStreamActive == true)
        {
            flameStreamActive = false;
            Destroy(this.GetComponentInChildren<FirePoint>().GetComponentInChildren<FlameStreamCache>().gameObject, 2);
            this.GetComponentInChildren<FirePoint>().GetComponentInChildren<ParticleSystem>().Stop();
        }
    }





    void Shoot()
    {
        {
            foreach (Transform firePoint in firePoints)
            {
                if (despacito) {
                    firePoint.LookAt(target);
                }
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                
                if (IsFlamethrower)
                {
                    if (flameStreamActive == false)
                    {
                        bulletGO.AddComponent<FlameStreamCache>().GetComponent<FlameStreamCache>().SetParent(this);
                        bulletGO.transform.parent = this.GetComponentInChildren<FirePoint>().transform;
                        flameStreamActive = true;
                    }
                    else
                    {

                        Destroy(bulletGO);
                    }
                }
                if (is8Shooter == true)
                {
                    Destroy(bulletGO, 5f);
                }
                //bulletGO.transform.rotation = firePoint.rotation;
                try
                {
                    
                    BulletScript bullet = bulletGO.GetComponent<BulletScript>();
                    bullet.CopyRefrence(bulletobject);
                    bullet.Seek(target);
                    if (bullet != null && CanRotate == true)
                        bullet.Seek(target);
                }
                catch {
                    eightshooterbullet bullet = bulletGO.GetComponent<eightshooterbullet>();

                }
            }
        }
    }
    private void OnCollisonEnter(Collision collide)
    {
        if (collide.gameObject.GetComponent<AllTurrets>() != null)
        {
            iscollidingwithtower = true;
        }
    }

    private void OnCollisonStay(Collision collide)
    {
        if (collide.gameObject.GetComponent<AllTurrets>() != null)
        {
            iscollidingwithtower = true;
        }
    }

    private void OnCollisonExit(Collision collide)
    {
        if (collide.gameObject.GetComponent<AllTurrets>() != null)
        {
            iscollidingwithtower = false;
        }
    }





    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.green;
    }

}

[System.Serializable]
public class turretbullet
{
    public float speed = 70f;
    public float bulletDamage = 1;
    //public bool followTarget = true;
    //public bool CanSlowEnemies = false;
    public float FreezeTowerSlowdown = 0;
    //public bool hasExplosion = false;
    public float explosionDamage_subtractfrombulletdamage = -10f;
    //public GameObject exposionPrefab;
    public float explosionRadius = 5;
    public float freezeDuration = 0;
    public float realTNTFuse;
    //public bool HasParticles = false;
    //public GameObject ParticlePrefab;
    //public bool stopMoving;
    //public bool isTNT = false;
    //public bool otherbullets = true;









    public void CopyRefrence(BulletScript bullet)
    {
        this.speed = bullet.speed;
        this.bulletDamage = bullet.bulletDamage;
        this.FreezeTowerSlowdown = bullet.FreezeTowerSlowdown;
        this.explosionDamage_subtractfrombulletdamage = bullet.explosionDamage_subtractfrombulletdamage;
        this.explosionRadius = bullet.explosionRadius;
        this.realTNTFuse = bullet.realTNTFuse;
    }

























}

