using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    Collider Coincollider;
    //public AudioSource audioSource;
    //public AudioClip HitSE;

    // Start is called before the first frame update
    void Start()
    {
        Coincollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1f, 0, 0));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Coincollider.enabled = false;
            //audioSource.PlayOneShot(HitSE);
            Destroy(this.gameObject);
        }
    }
}