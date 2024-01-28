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
        // �ǂɂԂ���ƕ���������
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

    // CollisionDetector.cs��onTriggerStay�ɃZ�b�g���A�Փ˒��Ɏ��s�����B
    private void OnTriggerEnter(Collider other)
    {
        // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
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
