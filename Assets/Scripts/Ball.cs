using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] Paddle paddle_1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomOffset = 0.2f;

    // state
    Vector2 paddleToBallVector;
    bool hasLaunched = false;

    // cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start() {
        paddleToBallVector = transform.position - paddle_1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (!hasLaunched) {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);

            hasLaunched = true;
        }
    }

    private void LockBallToPaddle() {
        Vector2 paddlePos = new Vector2(paddle_1.transform.position.x, paddle_1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        float randomX = Random.Range(0, randomOffset);
        float randomY = Random.Range(0, randomOffset);

        Vector2 velocityOffset = new Vector2(randomX, randomY);

        if (hasLaunched) {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityOffset;
        }
    }
}
