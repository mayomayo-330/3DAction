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

    //�_��
    [SerializeField] private Renderer _head;
    [SerializeField] private Renderer _hair;
    [SerializeField] private Renderer _torso;
    [SerializeField] private Renderer _leg;
    [SerializeField] private Renderer _weapon;
    // �_�Ŏ���[s]
    [SerializeField] private float _cycle = 1;
    private double _time;
    bool isDamege;

    public string TagName;
    // Start is called before the first frame update

    void Start()
    {
        Damagecollider = GetComponent<Collider>();   //�����蔻��
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
            gameover = 1;
            
        }
        float percent = (float)HP / MaxHP;
        HPGage.fillAmount = percent;

        if (isDamege == true)
        {
            // �����������o�߂�����
            _time += Time.deltaTime;

            // ����cycle�ŌJ��Ԃ��l�̎擾
            // 0�`cycle�͈̔͂̒l��������
            var repeatValue = Mathf.Repeat((float)_time, _cycle);

            // ��������time�ɂ����閾�ŏ�Ԃ𔽉f
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
            Invoke("ColliderReset", ResetTime);  //ResetTime�b��Ɋ֐����Ăяo��
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
