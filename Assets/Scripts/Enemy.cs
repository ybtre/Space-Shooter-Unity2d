using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] int health = 3;
    [SerializeField] float speed;
    [SerializeField] bool isAlive = true;

    Player player;
    private Transform playerTransform;
    private Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start() {
        // playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = FindObjectOfType<Player>();
        playerTransform = player.GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (isAlive == true) {
            ChasePlayer();
        }

        HandleDead();


    }


    private void OnTriggerEnter2D(Collider2D collider) {
        CheckCollisionWithBullet(collider);
    }

    private void CheckCollisionWithBullet(Collider2D collider) {
        bool isBullet = collider.CompareTag("Bullet");
        if (isBullet == true) {
            DecrementHealth();
            Debug.Log("Collision Detected with " + gameObject.name);
        }
    }

    private void HandleDead() {
        if (health <= 0) {
            myRigidBody.gravityScale = 2;
            SetAlive(false);
            // Destroy(gameObject);
            Debug.Log("Dead " + gameObject.name);
        }
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
        health--;
        Debug.Log("Enemey has taken damage" + gameObject.name);
    }

    public void SetAlive(bool state) {
        isAlive = state;
    }

    public bool IsAlive() {
        return isAlive;
    }
}
