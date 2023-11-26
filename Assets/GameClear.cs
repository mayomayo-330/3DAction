using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public AudioClip HitSE;
    public GameObject GameClearCanvas;

    //タイム表記
    private int minute = 0;
    private float seconds = 0;
    public GameObject timer_object = null;

    // Start is called before the first frame update
    void Start()
    {
        // 画面を動かす
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //タイムを測る
        seconds += Time.deltaTime;
        if(seconds >= 60f)
        {
            minute++;
            seconds = seconds - 60;
        }
        // コインを七枚集めると
        if(NewPlayerController.coinCount == 7)
        {
            AudioSource.PlayClipAtPoint(HitSE, transform.position);         //ファンファーレ
            GameClearCanvas.SetActive(true);  //クリア画面表示
            Time.timeScale = 0;   //画面を止める
            Text timer_text = timer_object.GetComponent<Text>();
            timer_text.text = "<color=#ffff00>Congratulations!</color>\nyour time : " + minute.ToString("00")+":"+seconds.ToString("n2");
        }
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //今動いているシーンを再起動
    }
}
