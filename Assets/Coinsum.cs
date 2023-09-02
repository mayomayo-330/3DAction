using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coinsum : MonoBehaviour
{
    public GameObject coin_object = null;
    public int coin_sum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coin_sum = NewPlayerController.coinCount;
        Text coin_text = coin_object.GetComponent<Text>();
        coin_text.text = "Å~" + coin_sum;     
    }
}
