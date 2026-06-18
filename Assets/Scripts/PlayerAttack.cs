using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;

    private float fireRate = 0.8f;
    private float nextFireTime;

    private PlayerAge playerAge;

    void Start()
    {
        playerAge = GetComponent<PlayerAge>();
    }

    void Update()
    {
        // Child can't attack
        if (playerAge.currentAge < 20)
            return;

        // Age-based fire rate
        if (playerAge.currentAge < 40)
        {
            fireRate = 0.8f;
        }
        else if (playerAge.currentAge < 70)
        {
            fireRate = 0.4f;
        }
        else
        {
            fireRate = 0.2f;
        }

        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        Transform target = GetNearestEnemy();

        if (target == null)
            return;

        Vector3 direction =
            (target.position - transform.position).normalized;

        // Adult
        if (playerAge.currentAge < 60)
        {
            ShootProjectile(direction);
        }
        // Elder
        else
        {
            ShootProjectile(direction);

            ShootProjectile(
                Quaternion.Euler(0, -20, 0) * direction);

            ShootProjectile(
                Quaternion.Euler(0, 20, 0) * direction);
        }
    }

    void ShootProjectile(Vector3 direction)
    {
        GameObject projectile =
            Instantiate(
                projectilePrefab,
                transform.position + direction,
                Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = direction * 10f;

        Destroy(projectile, 3f);
    }

    Transform GetNearestEnemy()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
            return null;

        Transform nearest = null;

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance =
                Vector3.Distance(
                    transform.position,
                    enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }
}