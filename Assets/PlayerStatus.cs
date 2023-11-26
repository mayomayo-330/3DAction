using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameObject Main;
    public int HP;
    public int MaxHP;
    public float ResetTime;
    public Image HPGage;
    public static int gameover;

    public GameObject item;

    public GameObject Effect;
    public AudioSource audioSource;
    public AudioClip HitSE;

    Collider Damagecollider;

    //点滅
    [SerializeField] private Renderer _head;
    [SerializeField] private Renderer _hair;
    [SerializeField] private Renderer _torso;
    [SerializeField] private Renderer _leg;
    [SerializeField] private Renderer _weapon;
    // 点滅周期[s]
    [SerializeField] private float _cycle = 1;
    private double _time;
    bool isDamege;

    public string TagName;
    // Start is called before the first frame update

    void Start()
    {
        Damagecollider = GetComponent<Collider>();   //当たり判定
        gameover = 0;
        isDamege = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            var effect = Instantiate(Effect);      
            effect.transform.position = this.transform.position;
            gameover = 1;       //ゲームオーバーフラグを立てる
            
        }
        float percent = (float)HP / MaxHP;
        HPGage.fillAmount = percent;

        //ダメージをくらうと
        if (isDamege == true)
        {
            // 内部時刻を経過させる
            _time += Time.deltaTime;

            // 周期cycleで繰り返す値の取得
            // 0～cycleの範囲の値が得られる
            var repeatValue = Mathf.Repeat((float)_time, _cycle);

            // 内部時刻timeにおける明滅状態を反映
            _head.enabled = repeatValue >= _cycle * 0.3f;
            _hair.enabled = repeatValue >= _cycle * 0.3f;
            _torso.enabled = repeatValue >= _cycle * 0.3f;
            _leg.enabled = repeatValue >= _cycle * 0.3f;
            _weapon.enabled = repeatValue >= _cycle * 0.3f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagName)
        {
            Damage();
            Damagecollider.enabled = false;
            Invoke("ColliderReset", ResetTime);  //ResetTime秒後に関数を呼び出す
        }
    }

    void Damage()
    {
        audioSource.PlayOneShot(HitSE);
        HP--;
        isDamege = true;
        
    }

    void ColliderReset()
    {
        Damagecollider.enabled = true;
        isDamege = false;

        _head.enabled = true;
        _hair.enabled = true;
        _torso.enabled = true;
        _leg.enabled = true;
        _weapon.enabled = true;
    }
}
