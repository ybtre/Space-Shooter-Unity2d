using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] float speed = 5f;
    [SerializeField] bool hasFired = false;
    [SerializeField] bool isAlive = false;
    Player player;
    Rigidbody2D myRigidBody2D;
    Vector2 bulletSpawnOnGunVector;
    // Start is called before the first frame update
    void Start() {
        // bulletSpawnOnGunVector = transform.position - player.transform.position;
        player = FindObjectOfType<Player>();
        myRigidBody2D = GetComponent<Rigidbody2D>();

        // bulletSpawnOnGunVector.x = player.transform.position.x + 2;
        // spawnBulletAtGunPosition();
    }

    // Update is called once per frame
    void Update() {
        // spawnBulletAtGunPosition();
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.position = player.transform.position;
            myRigidBody2D.velocity = new Vector2(-speed, 0);
        }
    }

    // private void spawnBulletAtGunPosition() {
    //     Vector2 playerShipPos = new Vector2(player.transform.position.x, player.transform.position.y);
    //     transform.position = playerShipPos + bulletSpawnOnGunVector;
    // }
}
