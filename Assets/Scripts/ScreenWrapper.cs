using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    public static float L, R, T, B;
    private float error = 0.2f;
    void Start()
    {
        init();
    }

    // Initialize Screen Boundaries coordinates.
    public static void init()
    {
        Vector3 lowerLeftScreen, upperRightScreen, lowerLeftWorld, upperRightWorld;
        float screenZ = -Camera.main.transform.position.z;
        lowerLeftScreen = new Vector3(0, 0, screenZ);
        upperRightScreen = new Vector3(Screen.width, Screen.height, screenZ);
        lowerLeftWorld = Camera.main.ScreenToWorldPoint(lowerLeftScreen);
        upperRightWorld = Camera.main.ScreenToWorldPoint(upperRightScreen);
        L = lowerLeftWorld.x;
        R = upperRightWorld.x;
        T = upperRightWorld.y;
        B = lowerLeftWorld.y;
    }

    // Screen Wrap object if it got outside.
    void Update()
    {
        Vector2 position = transform.position;

        // check left, right, top, and bottom sides
        if (position.x < L)
            position.x = R - error;
        else if (position.x > R)
            position.x = L + error;
        else if (position.y > T)
            position.y = B + error;
        else if (position.y < B)
            position.y = T - error;

        // Change the object loacation.
        transform.position = position;
    }
}
