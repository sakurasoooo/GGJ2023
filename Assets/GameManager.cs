using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static GameManager gameManager;
    bool win = false;
    private void Awake()
    {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;

        }
        gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!win)
        {
            if (score >= 30)
            {
                win = true;
                Win();
            }
        }
    }

    public void AddScore()
    {
        score++;
    }

    public void Lose()
    {
        Invoke("LoadLose", 3);
    }

    private void LoadLose()
    {
        SceneManager.LoadScene("Lose");
    }

    public void Win()
    {
        Invoke("LoadWin", 3);
    }

    private void LoadWin()
    {
        SceneManager.LoadScene("Win");
    }
}
