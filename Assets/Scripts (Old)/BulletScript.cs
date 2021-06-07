using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Transform target;
    private Vector3 destination;
    public float speed = 70f;
    public float bulletDamage = 1;
    public bool followTarget = true;
    bool enemyHasShield;
    public bool CanSlowEnemies = false;
    public float FreezeTowerSlowdown = 0;
    public bool hasExplosion = false;
    public float explosionDamage_subtractfrombulletdamage = -10f;
    public GameObject exposionPrefab;
    public float explosionRadius = 5;
    public bool HasParticles = false;
    public GameObject ParticlePrefab;
    public bool stopMoving;
    public bool isTNT = false;
    public bool otherbullets = true;
    public float freezeDuration = 0;
    public float realTNTFuse;

    public void CopyRefrence(turretbullet bullet)
    {
        this.speed = bullet.speed;
        this.bulletDamage = bullet.bulletDamage;
        this.FreezeTowerSlowdown = bullet.FreezeTowerSlowdown;
        this.explosionDamage_subtractfrombulletdamage = bullet.explosionDamage_subtractfrombulletdamage;
        this.explosionRadius = bullet.explosionRadius;
        this.freezeDuration = bullet.freezeDuration;
        this.realTNTFuse = bullet.realTNTFuse;

    }

    public void Seek(Transform _target)
    {
        target = _target;
        destination = _target.position;
    }
    
    private void Start()
    {
        Seek(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && followTarget == true)
        {
            Destroy(gameObject);
            return;
        }
        if (!stopMoving)
        {
            if (followTarget == true)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (distance <1)
                {
                    stopMoving = true;
                    if (isTNT)
                    {
                        StartCoroutine(setFuse());
                    }
                    else
                    {
                        HitTarget();
                    }
                    return;
                }
                    
                    destination = Vector3.forward * speed;
                    Seek(target);
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                
                // transform.Translate(dir.normalized * distanceThisFrame, Space.World);
                // destination = transform.TransformDirection(destination);
            }
            else
            {
               // destination = Vector3.forward * speed;
                float distance = Vector3.Distance(transform.position, destination);


                // destination = transform.TransformDirection(destination);
                if (distance < 1)
                {
                    stopMoving = true;
                    if (isTNT)
                    {
                        StartCoroutine(setFuse());
                    }
                    else
                    {
                        HitTarget();
                    }
                    return;
                }

                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.rotation.y*transform.forward*speed, speed * Time.deltaTime);
                //transform.Translate(destination * Time.deltaTime, );
            }
        }
    }
    void HitTarget()
    {
        if (hasExplosion)
        {
            
            Collider[] enemies = Physics.OverlapSphere(transform.TransformDirection(transform.position), explosionRadius);
            DrawCircle(gameObject, explosionRadius, 2);
            foreach (Collider enemy in enemies) {
                
                if (enemy != target) {
                    if (enemy.GetComponent<Enemy>())
                    {
                        Debug.Log(enemy.name);
                        enemy.GetComponent<Enemy>().TakeShieldDamage(bulletDamage + explosionDamage_subtractfrombulletdamage, FreezeTowerSlowdown,freezeDuration);
                    }
                }
            }

            GameObject clone = Instantiate(exposionPrefab, transform.position, transform.rotation);
            Destroy(clone, 1.8f);
        }
        Destroy(gameObject);

        if (HasParticles)
        {
            GameObject clone = Instantiate(ParticlePrefab, transform.position, transform.rotation);
            Destroy(clone, 1.8f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isTNT)
            {
                StartCoroutine(setFuse());
            }
            else {
                enemyHasShield = false;     
                target = collision.gameObject.transform;
                target.GetComponent<Enemy>().TakeShieldDamage(bulletDamage, FreezeTowerSlowdown, freezeDuration);
                HitTarget();
            }
            
        }
        if (collision.gameObject.CompareTag("Fast Enemy"))
        {
            if (isTNT)
            {
                StartCoroutine(setFuse());
            }
            else
            {
                enemyHasShield = false;
                target = collision.gameObject.transform;
                target.GetComponent<Enemy>().TakeShieldDamage(bulletDamage, FreezeTowerSlowdown, freezeDuration);
                HitTarget();
            }
        }
        if (collision.gameObject.CompareTag("Obsidian Enemy"))
        {
            if (isTNT)
            {
                StartCoroutine(setFuse());
            }
            else
            {
                enemyHasShield = false;
                target = collision.gameObject.transform;
                target.GetComponent<Enemy>().TakeShieldDamage(bulletDamage, FreezeTowerSlowdown, freezeDuration);
                HitTarget();
            }
        }
        if (collision.gameObject.CompareTag("Shielded Enemy"))
        {
            if (isTNT)
            {
                StartCoroutine(setFuse());
            }
            else
            {
                enemyHasShield = true;
                target = collision.gameObject.transform;
                target.GetComponent<Enemy>().TakeShieldDamage(bulletDamage, FreezeTowerSlowdown, freezeDuration);
                HitTarget();
            }
        }
        if (collision.gameObject.CompareTag("Shielded Fast Enemy"))
        {
            if (isTNT)
            {
                StartCoroutine(setFuse());
            }
            else
            {
                enemyHasShield = true;
                target = collision.gameObject.transform;
                target.GetComponent<Enemy>().TakeShieldDamage(bulletDamage, FreezeTowerSlowdown, freezeDuration);
                HitTarget();
            }
        }
        if (collision.gameObject.CompareTag("Shielded Obsidian Enemy"))
        {
            if (isTNT)
            {
                StartCoroutine(setFuse());
            }
            else
            {
                enemyHasShield = true;
                target = collision.gameObject.transform;
                target.GetComponent<Enemy>().TakeShieldDamage(bulletDamage, FreezeTowerSlowdown, freezeDuration);
                HitTarget();
            }
        }
        if (collision.gameObject.CompareTag("Knuckles Enemy"))
        {
            if (isTNT)
            {
                StartCoroutine(setFuse());
            }
            else
            {
                enemyHasShield = true;
                target = collision.gameObject.transform;
                target.GetComponent<Enemy>().TakeShieldDamage(bulletDamage, FreezeTowerSlowdown, freezeDuration);
                HitTarget();
            }
        }
    }

    public  void DrawCircle(GameObject container, float radius, float lineWidth)
    {
        var segments = 360;
        var line = container.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }

    IEnumerator setFuse() {

        yield return new WaitForSeconds(realTNTFuse);
        HitTarget();
    }
    
}
