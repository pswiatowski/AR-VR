using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController instance;
    public static GameController Instance { get { return instance; } }

    public Text scoreText;

    private int score;

    // Use this for initialization
    void Start() {
        instance = this;

        score = 0;
    }

    // Update is called once per frame
    void Update() {


    }

    public void IncrementScore(int amount) {
        score += amount;
        scoreText.text = score.ToString();

        // Swish sound
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }
}
