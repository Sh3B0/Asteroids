using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static Text gameOverText, timerText, scoreText, bestText;
    public static Button againButton;
    public static int score, best, minutes, seconds;
    public static bool timerEnabled;
    public static float timer;

    // Initialize HUD info.
    private void Start()
    {
        score = 0;
        timer = 0;
        minutes = 0;
        seconds = 0;
        timerEnabled = true;

        Text[] hudText = gameObject.GetComponentsInChildren<Text>();
        
        bestText = hudText[0];
        scoreText = hudText[1];
        timerText = hudText[2];
        gameOverText = hudText[3];
        againButton = gameObject.GetComponentInChildren<Button>();

        gameOverText.gameObject.SetActive(false);
        againButton.gameObject.SetActive(false);
        
    }

    // Update HUD info.
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        scoreText.text = "Score: " + score.ToString();
        bestText.text = "Best: " + best.ToString();
        if (timerEnabled && timer >= 1) // Display elapsed time.
        {
            timer = 0;
            seconds++;
            if (seconds == 60) { seconds = 0; minutes++; }
            StringBuilder s = new StringBuilder();
            if (minutes < 10) s.Append("0");
            s.Append(minutes.ToString());
            s.Append(":");
            if (seconds < 10) s.Append("0");
            s.Append(seconds.ToString());
            timerText.text = s.ToString();
        }
    }

}
