using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float EnemySpeed;

    GameObject Target;
    public float Timer;
    public float ChangeTime;

    Vector3 rot;
    
    float rand;

    void Start()
    {
        
    }

    void Update()
    {
        var speed = Vector3.zero;
        speed.z = EnemySpeed * Time.deltaTime;
        rot = transform.eulerAngles;

        if (Target)
        {
            transform.LookAt(Target.transform);
            rot = transform.eulerAngles;
        }
        else
        {
            Timer += Time.deltaTime;
            if(ChangeTime <= Timer)
            {
                rand = Random.Range(0, 360);
                rot.y = rand;
                Timer = 0;
            }
        }
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;
        this.transform.Translate(speed);
    }

    Vector3 normalVector;

    private void OnCollisionEnter(Collision other)
    {
        // 壁にぶつかると方向かえる
        if (other.gameObject.tag == "Plane")
        {
            normalVector = other.contacts[0].normal;
            if (normalVector.y < 0.02f)
            {
                rand = Random.Range(0, 360);
                rot.y = rand;
                Timer = 0;

            }
        }
    }

    // CollisionDetector.csのonTriggerStayにセットし、衝突中に実行される。
    private void OnTriggerEnter(Collider other)
    {
        // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (other.tag == "Player")
        {
            Target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Target = null;
        }
    }
}
