using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    // Update HUD info every frame.
    void Update()
    {
        if(tag == "Score") GetComponent<Text>().text = "Score: " + Asteroid.score.ToString();
        else if (tag == "Best") GetComponent<Text>().text = "Best: " + Asteroid.best.ToString();
    }
}
