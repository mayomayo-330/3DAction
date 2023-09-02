using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject Player;
    public GameObject GameOverCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerStatus.gameover == 1 || NewPlayerController.falling == 1)
        {
            GameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //¡“®‚¢‚Ä‚¢‚éƒV[ƒ“‚ğÄ‹N“®
    }
}
