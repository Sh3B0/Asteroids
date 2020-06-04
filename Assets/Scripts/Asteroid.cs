using UnityEngine;

public class Asteroid : MonoBehaviour
{
    AudioClip asteroid_ex;
    AudioSource aud;
    public GameObject smallAsteroidPrefab;
    private void Start()
    {
        aud = Camera.main.GetComponent<AudioSource>();
        asteroid_ex = Resources.Load<AudioClip>("asteroid_ex");
    }
    // Destroy asteroid and bullet when they collide.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            aud.PlayOneShot(asteroid_ex);                   // Play asteroid explosion sound.
            Destroy(col.GetComponent<GameObject>());        // Destroy the bullet.

            if (gameObject.CompareTag("BigAsteroid"))
            {
                HUD.score++;
                // print("Now I will create two smaller asteroids");
                LaunchAsteroids.Fly(Instantiate(smallAsteroidPrefab, gameObject.transform.position, Quaternion.identity));
                LaunchAsteroids.Fly(Instantiate(smallAsteroidPrefab, gameObject.transform.position, Quaternion.identity));
            }
            else HUD.score += 2; // Two points for hitting a small one.
            
            Destroy(gameObject);                            // Destroy the asteroid.
            
            if (HUD.score > HUD.best) HUD.best = HUD.score; // Update best score.
        }
    }
}
