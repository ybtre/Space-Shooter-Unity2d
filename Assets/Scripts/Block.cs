using UnityEngine;

public class Block : MonoBehaviour {
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;


    // cached reference
    Level level;

    [SerializeField] int timesHit; // TODO only serialized for debug purposes

    void Start() {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks() {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable") {
            level.IncreaseBlockCount();
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo) {
        if (tag == "Breakable") {
            HandleHit();
        }
    }

    private void HandleHit() {
        timesHit++;
        int blockHealth = hitSprites.Length + 1;
        // blockHealth--;
        // if (blockHealth <= 0) {
        if (timesHit >= blockHealth) {
            DestroyBlock();
        } else {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite() {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null) {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else {
            Debug.LogError("Block Sprite is missing from array. Name: " + gameObject.name);
        }
    }

    private void DestroyBlock() {
        PlayBlockDestroySound();

        // gameObject with small "g" means this gameObject
        Destroy(gameObject);
        level.DecreaseBlockCount();
        TriggleSparklesVFX();
    }

    private void PlayBlockDestroySound() {
        // another way to call a function from a different class(finds the first object of the class), instead of initialising it with a variable at the start of this script
        FindObjectOfType<GameStatus>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 0.5f);
    }

    private void TriggleSparklesVFX() {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
