using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    // Restarts the game when "Again" button in clicked.
    public Button btn;
    private void Start()
    {
        btn.onClick.AddListener(Restart);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
