using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Holding score and best score.
    public static int score = 0, best;

    // Destroy asteroid and bullet when they collide.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Camera.main.GetComponents<AudioSource>()[1].Play(); // asteroid_ex
            Destroy(col.GetComponent<GameObject>());
            Destroy(gameObject);
            score++;
            if (score > best) best = score;
        }
    }
}
