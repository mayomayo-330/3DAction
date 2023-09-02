using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public AudioClip HitSE;
    public GameObject GameClearCanvas;
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(NewPlayerController.coinCount == 7)
        {
            AudioSource.PlayClipAtPoint(HitSE, transform.position);
            GameClearCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //¡“®‚¢‚Ä‚¢‚éƒV[ƒ“‚ğÄ‹N“®
    }
}
