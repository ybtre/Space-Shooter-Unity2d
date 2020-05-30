using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] Transform bullet;
    [SerializeField] float fireRate = 0.15f;

    float fireRateTimer;
    Transform bulletInstance;

    Player player;
    void Start() {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update() {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer >= fireRate) {
            bulletInstance = Instantiate(bullet, player.transform.position, Quaternion.identity);
            fireRateTimer = 0;
        }
    }
}
