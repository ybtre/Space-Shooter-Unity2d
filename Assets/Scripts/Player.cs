using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] int maxHealth = 5;
    [SerializeField] float flySpeed = 1;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
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
}
