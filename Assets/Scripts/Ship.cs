using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    const float rotationSpeed = 2.5f, shipSpeed = 7f, bulletSpeed = 500f;
    float timer = 0.0f;
    public GameObject expPrefab, bulletPrefab, gameOver, again;
    private Queue<GameObject> bullets = new Queue<GameObject>();
    private AudioSource[] aud;

    private void Start()
    {
        aud = Camera.main.GetComponents<AudioSource>();
        gameOver.SetActive(false);
        again.SetActive(false);
    }

    void FixedUpdate()
    {
        // Detect user input
        float H = Input.GetAxis("Horizontal"), J = Input.GetAxis("Jump");

        // Position and direction of the ship.
        Vector2 pos = transform.position;
        float dir = transform.eulerAngles.z + 90;

        // Rotate the ship
        transform.RotateAround(pos, Vector3.back, H * rotationSpeed);

        // Force in direction of ship
        Vector2 Force = new Vector2(Mathf.Cos(dir * Mathf.Deg2Rad), Mathf.Sin(dir * Mathf.Deg2Rad));

        // Thrust if Jump is pressed
        GetComponent<Rigidbody2D>().AddForce(shipSpeed * J * Force);

        // Fire bullets every 0.5 second.
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            timer = 0;
            aud[2].Play(); // weapon
            GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            bullets.Enqueue(bullet);
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * Force);
        }

        // Allow maximum 5 bullets at a time.
        if (bullets.Count > 5) Destroy(bullets.Dequeue());
    }

    // Destroy ship when it collides with an asteroid. Ends the game, and asks to play again.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Asteroid"))
        {
            aud[0].Play(); // ship_ex
            Destroy(gameObject);
            GameObject o = Instantiate(expPrefab, transform.position, Quaternion.identity);
            gameOver.SetActive(true);
            again.SetActive(true);
            Asteroid.score = 0;
        }
    }

}
