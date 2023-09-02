using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public GameObject Main;
    public int HP;
    public int MaxHP;
    public float ResetTime ;
    public Image HPGage;

    public GameObject Coin;
    Vector3 CoinPos;

    public GameObject Effect;
    public AudioSource audioSource;
    public AudioClip HitSE;

    Collider Damagecollider;

    public string TagName;
    // Start is called before the first frame update

    void Start()
    {
        Damagecollider = GetComponent<Collider>();   //“–‚½‚è”»’è
    }


    // Update is called once per frame
    void Update()
    {

        if (HP <= 0)
        {
            var effect = Instantiate(Effect);
            effect.transform.position = this.transform.position;
            Destroy(effect, 3);
            Destroy(Main);
            if(this.tag == "enemy")
            {
                CoinPos = this.transform.position;
                CoinPos.y += 2;
                Instantiate(Coin, CoinPos, this.transform.rotation);
            }
        }
        float percent=(float)HP / MaxHP;
        HPGage.fillAmount = percent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagName)
        {
            Damage();
            Damagecollider.enabled = false;
            Invoke("ColliderReset", ResetTime);  //ResetTime•bŒã‚ÉŠÖ”‚ðŒÄ‚Ño‚·
        }
    }

    void Damage()
    {
        audioSource.PlayOneShot(HitSE);
        HP--;
    }

    void ColliderReset()
    {
        Damagecollider.enabled = true;
    }
}
