using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player Stats")]
    [SerializeField] float health = 500;
    [SerializeField] float flySpeed = 1;
    [SerializeField] float padding = 1f;

    [Header("Bullet Stats")]
    [SerializeField] Transform bullet;
    [SerializeField] float fireRate = 0.15f;


    float xMin, xMax;
    float yMin, yMax;
    float fireRateTimer;
    Transform bulletInstance;
    protected DamageDealer damageDealer;

    // Start is called before the first frame update
    void Start() {
        damageDealer = FindObjectOfType<Bullet>().GetComponent<DamageDealer>();
        // SetUpMoveBoundries();
    }

    // Update is called once per frame
    void Update() {
        HandleMovementInput();
        // Move();

        Fire();

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void Fire() {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer >= fireRate) {
            bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            fireRateTimer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        CheckCollisionWithEnemyBullet(collider);
    }

    private void CheckCollisionWithEnemyBullet(Collider2D collider) {
        bool isEnemyBullet = collider.CompareTag("EnemyLaser");
        Debug.Log("Collision Detected with " + gameObject.name);
        if (isEnemyBullet == true) {
            damageDealer = collider.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            DecrementHealth();
        }
    }

    public void DecrementHealth() {
        health -= damageDealer.GetDamage();
        Debug.Log("Player has taken damage" + gameObject.name);
    }

    private void HandleMovementInput() {
        if (Input.GetKey(KeyCode.W)) {
            PlayerMovement("up", flySpeed);
        }
        if (Input.GetKey(KeyCode.S)) {
            PlayerMovement("down", flySpeed);
        }
        if (Input.GetKey(KeyCode.A)) {
            PlayerMovement("left", flySpeed);
        }
        if (Input.GetKey(KeyCode.D)) {
            PlayerMovement("right", flySpeed);
        }
    }

    private void PlayerMovement(string direction, float speed) {
        Vector2 moveShip = new Vector2(transform.position.x, transform.position.y);

        switch (direction) {
            case "up":
                moveShip.y += speed * Time.deltaTime;
                break;
            case "down":
                moveShip.y += -speed * Time.deltaTime;
                break;
            case "left":
                moveShip.x += -speed * Time.deltaTime;
                break;
            case "right":
                moveShip.x += speed * Time.deltaTime;
                break;
            default:
                break;
        }

        transform.position = moveShip;
    }


    //TODO: works and is in general better, but is more floaty
    // private void Move() {
    //     var deltaX = Input.GetAxis("Horizontal") * flySpeed * Time.deltaTime;
    //     var deltaY = Input.GetAxis("Vertical") * flySpeed * Time.deltaTime;

    //     float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    //     float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

    //     transform.position = new Vector2(newXPos, newYPos);
    // }

    // private void SetUpMoveBoundries() {
    //     Camera gameCamera = Camera.main;
    //     xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
    //     xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

    //     yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
    //     yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    // }
}
