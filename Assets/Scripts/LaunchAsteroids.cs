using UnityEngine;
public class LaunchAsteroids : MonoBehaviour
{
    public GameObject asteroidPrefab;
    const float asteroidSpeed = 3;
    float timer = 0.0f;
    Vector2[] sides = new Vector2[4];
    void Start()
    {
        // Initialize screen boundaries.
        ScreenWrapper.init();

        // Which side of the screen the asteroid will come from.
        sides[0] = new Vector2(ScreenWrapper.L, 0);
        sides[1] = new Vector2(ScreenWrapper.R, 0);
        sides[2] = new Vector2(0, ScreenWrapper.T);
        sides[3] = new Vector2(0, ScreenWrapper.B);

        // Debug
        // foreach (Vector2 v in sides) print(v);
    }

    private void Update()
    {
        // Launch an asteroid every 2 seconds approximately.
        timer += Time.deltaTime;
        if(timer >= 2)
        {
            timer = 0;
            // Instantiate an asteroid from a random side.
            GameObject Asteroid = Instantiate(asteroidPrefab, sides[Random.Range(0, 4)], Quaternion.identity);

            // Give that asteroid a random nonzero direction.
            Vector2 dir = Vector2.zero;
            while (dir == Vector2.zero)
                dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

            // Let the asteroid go.
            Asteroid.GetComponent<Rigidbody2D>().AddForce(asteroidSpeed * dir, ForceMode2D.Impulse);
        }
    }
}