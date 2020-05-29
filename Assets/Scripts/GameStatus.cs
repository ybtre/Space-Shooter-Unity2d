using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour {
    //config params
    [Range(0.1f, 20f)] [SerializeField] float timeSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 78;
    [SerializeField] Text scoreText;
    [SerializeField] bool autoPlayToggle = false;

    //state variables
    [SerializeField] int currentScore = 0;

    //cached references


    // implementation of a singleton pattern, there can only be 1 GameStatus class
    // Awake() is the very first thing that the script executes, before Start()
    private void Awake() {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;

        if (gameStatusCount > 1) {
            // if there is more than 1 of you, destroy yourself
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            // if there is none of you, dont destoy yourself
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update() {
        Time.timeScale = timeSpeed;
    }

    public void AddToScore() {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public bool IsAutoPlayEnabled() {
        return autoPlayToggle;
    }

    public void ResetGame() {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
