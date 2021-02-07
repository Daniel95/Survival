using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static int highScore;

    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text highScoreText;

    private string HIGH_SCORE_STRING = "High Score: ";
    private string CURRENT_SCORE_STRING = "Current Score: ";

    private float currentScore;
    private bool introCompleted;

    private void Update()
    {
        if(!introCompleted) { return; }

        currentScore += Time.deltaTime;

        currentScoreText.text = CURRENT_SCORE_STRING + ((int)currentScore).ToString();

        if(currentScore > highScore)
        {
            highScore = (int)currentScore;
            highScoreText.text = HIGH_SCORE_STRING + ((int)highScore).ToString();
        }
    }

    private void OnIntroComplete()
    {
        introCompleted = true;
    }

    private void Awake()
    {
        highScoreText.text = HIGH_SCORE_STRING + highScore;
        currentScoreText.text = CURRENT_SCORE_STRING + ((int)currentScore).ToString();
    }

    private void OnEnable()
    {
        IntroManager.IntroCompleteEvent += OnIntroComplete;
    }

    private void OnDisable()
    {
        IntroManager.IntroCompleteEvent -= OnIntroComplete;
    }
}
