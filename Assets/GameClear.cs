using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public AudioClip HitSE;
    public GameObject GameClearCanvas;

    //�^�C���\�L
    private int minute = 0;
    private float seconds = 0;
    public GameObject timer_object = null;

    // Start is called before the first frame update
    void Start()
    {
        // ��ʂ𓮂���
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //�^�C���𑪂�
        seconds += Time.deltaTime;
        if(seconds >= 60f)
        {
            minute++;
            seconds = seconds - 60;
        }
        // �R�C���������W�߂��
        if(NewPlayerController.coinCount == 7)
        {
            AudioSource.PlayClipAtPoint(HitSE, transform.position);         //�t�@���t�@�[��
            GameClearCanvas.SetActive(true);  //�N���A��ʕ\��
            Time.timeScale = 0;   //��ʂ��~�߂�
            Text timer_text = timer_object.GetComponent<Text>();
            timer_text.text = "<color=#ffff00>Congratulations!</color>\nyour time : " + minute.ToString("00")+":"+seconds.ToString("n2");
        }
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //�������Ă���V�[�����ċN��
    }
}
