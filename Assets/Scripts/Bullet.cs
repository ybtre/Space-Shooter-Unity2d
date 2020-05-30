using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] float speed = 5f;
    [SerializeField] bool hasFired = false;
    [SerializeField] bool isAlive = false;
    Player player;
    PlayerShoot bulletInstance;
    Rigidbody2D myRigidBody2D;
    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<Player>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        Velocity();
        HandleDeathOnTimer();
    }

    private void HandleDeathOnTimer() {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        CheckCollisionWithEnemy(collider);
    }

    private void Velocity() {
        myRigidBody2D.velocity = new Vector2(-speed, 0);
    }

    private void CheckCollisionWithEnemy(Collider2D collider) {
        bool isEnemy = collider.CompareTag("Enemy");
        if (isEnemy == true) {
            // Destroy(gameObject);
            Debug.Log("Collision Detected with " + gameObject.name);
        }
    }
}
