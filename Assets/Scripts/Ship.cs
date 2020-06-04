using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject expPrefab, bulletPrefab;
    const float rotationSpeed = 2.5f, shipSpeed = 7f, bulletSpeed = 500f;
    float timer = 0.0f;
    Queue<GameObject> bullets = new Queue<GameObject>();
    AudioSource aud;
    AudioClip ship_ex, weapon;

    private void Start()
    {
        aud = Camera.main.GetComponent<AudioSource>();
        ship_ex = Resources.Load<AudioClip>("ship_ex");
        weapon = Resources.Load<AudioClip>("weapon");
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

        // Fire bullets every 0.25 second.
        timer += Time.deltaTime;
        if (timer >= 0.25f)
        {
            timer = 0;
            aud.PlayOneShot(weapon, 0.25f); // Weapon shooting audio
            GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            bullets.Enqueue(bullet);
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * Force);
        }

        // Allow maximum 20 bullets at a time.
        if (bullets.Count > 20) Destroy(bullets.Dequeue());
    }

    // Destroy ship when it collides with an asteroid. Ends the game, and asks to play again.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BigAsteroid") || col.CompareTag("SmallAsteroid"))
        {
            aud.PlayOneShot(ship_ex);                                        // Ship explosion audio.
            Destroy(gameObject);                                             // Destroy the ship.
            Instantiate(expPrefab, transform.position, Quaternion.identity); // Show explosion.
            HUD.gameOverText.gameObject.SetActive(true);                     // Show gameover text.
            HUD.againButton.gameObject.SetActive(true);                      // Show play again button.
            HUD.timerEnabled = false;
        }
    }

}
