using System.Collections;
using UnityEngine;

public class eightshooterbullet : MonoBehaviour
{
    private Transform target;
    private Vector3 destination;
    public float speed = 70f;
    public float bulletDamage = 1;
    bool enemyHasShield;
    public bool CanSlowEnemies = false;
    public float FreezeTowerSlowdown = 0;
    public bool hasExplosion = false;
    public GameObject exposionPrefab;
    public float explosionRadius = 5;
    public bool HasParticles = false;
    public GameObject ParticlePrefab;
    public bool stopMoving;
    public bool isTNT = false;
    public bool otherbullets = true;
    public float freezeDuration;


    // Update is called once per frame
    void Update()
    {
        if (!stopMoving)
        {
            destination = transform.TransformDirection(transform.forward * speed);//
                                                  //destination = Vector3.forward * speed;//

            transform.Translate(destination * Time.deltaTime);

        }
    }
    void HitTarget()
    {
        if (hasExplosion)
        {

            Collider[] enemies = Physics.OverlapSphere(transform.TransformDirection(transform.position), explosionRadius);
            DrawCircle(gameObject, explosionRadius, 2);
            foreach (Collider enemy in enemies)
            {

                if (enemy != target)
                {
                    if (enemy.GetComponent<Enemy>())
                    {
                        Debug.Log(enemy.name);
                        enemy.GetComponent<Enemy>().TakeDamage(bulletDamage / 2.0f, FreezeTowerSlowdown, freezeDuration);
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
            else
            {
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

    public void DrawCircle(GameObject container, float radius, float lineWidth)
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

    IEnumerator setFuse()
    {

        yield return new WaitForSeconds(5f);
        HitTarget();
    }
    //
}
