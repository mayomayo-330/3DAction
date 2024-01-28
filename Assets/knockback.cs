using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    private Rigidbody rb = null;

    public string TagName;

    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagName)
        {
            if (!flag)
            {
                // ヒットストップ処理挿入
                HitstopManager.instance.StartHitStop(0.08f);
                rb.AddForce(-transform.forward * 6f, ForceMode.VelocityChange);
                flag = true;
                Invoke("Back", 0.5f);
            }
        }
    }

    //コインの重複獲得を防ぐ
    private void Back()
    {
        flag = false;
    }


}
