using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] float health = 100;
    [SerializeField] float speed;
    [SerializeField] float laserSpeed = 20;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 1f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] bool isAlive = true;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject deathVFX;

    Player player;
    private Transform playerTransform;
    private Rigidbody2D myRigidBody;
    protected DamageDealer damageDealer;

    // Start is called before the first frame update
    void Start() {
        // playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = FindObjectOfType<Player>();
        // playerTransform = player.GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();

        damageDealer = FindObjectOfType<Bullet>().GetComponent<DamageDealer>();

        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update() {
        // if (IsAlive()) {
        //     ChasePlayer();
        // }

        ShootCounter();
        if (health <= 0) {
            HandleDead();
        }

        if (!IsAlive()) {
            Explosion();
        }
        // if (!IsAlive()) {
        // if (spawnTimer >= spawnInterval) {
        // Instantiate(gameObject);
        // spawnTimer = 0;
        // }
        // }

    }


    private void OnTriggerEnter2D(Collider2D collider) {
        CheckCollisionWithBullet(collider);
    }

    private void CheckCollisionWithBullet(Collider2D collider) {
        bool isBullet = collider.CompareTag("Bullet");
        Debug.Log("Collision Detected with " + gameObject.name);
        if (isBullet == true) {
            damageDealer = collider.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; };
            DecrementHealth();
        }
    }

    private void ShootCounter() {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire() {
        GameObject laser = Instantiate(
             projectile,
             transform.position,
             Quaternion.identity
         ) as GameObject;
        laser.GetComponent<Transform>().Rotate(0, 0, 90);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
    }
    private void HandleDead() {
        Debug.Log("Dead " + gameObject.name);
        myRigidBody.gravityScale = 10;
        Destroy(gameObject, 1f);
        SetAlive(false);
    }

    private void Explosion() {
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
    }

    private void ChasePlayer() {
        // only move towards player while distance between the 2 objects is 2 units
        if (Vector2.Distance(transform.position, playerTransform.position) > 1) {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
        // For those who want this to chase only on a single axis, since there have been no good replies:
        // Create a new vector 2 that uses the x component from the target, but uses the enemies current y position as the y component.
        // var targetPos = new Vector2(target.position.x, transform.position.y)
        // transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        // Same thing would work if you want y-axis movement only, just swap them.
    }

    public void DecrementHealth() {
        health -= damageDealer.GetDamage();
        Debug.Log("Enemey has taken damage" + gameObject.name);
    }

    public void SetAlive(bool state) {
        isAlive = state;
    }

    public bool IsAlive() {
        return isAlive;
    }
}
