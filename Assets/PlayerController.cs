using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform Camera;
    public float PlayerSpeed;
    public float RotationSpeed;
    Vector3 speed = Vector3.zero;
    Vector3 rot = Vector3.zero;

    //プレイヤーのRigidbody
    private Rigidbody Rig = null;

    //地面に着地しているか判定する変数
    public bool Grounded;

    //ジャンプ力
    public float Jumppower;

    public Animator PlayerAnimator;
    bool isRun;
    bool isJump;

    public Collider WeaponCollider;
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        Rig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Grounded == true)
        {
            Move();
            Rotation();
            Jump();
            Attack();
        }
        Camera.transform.position = transform.position;
    }

    void Move()
    {
        if(canMove == false)
        {
            return;
        }

        speed = Vector3.zero;
        rot = Vector3.zero;
        isRun = false;

        if (Input.GetKey(KeyCode.W))
        {
            rot.y = 0;
            MoveSet();
        }
        if (Input.GetKey(KeyCode.A))
        {
            rot.y = -90;
            MoveSet();
        }
        if (Input.GetKey(KeyCode.S))
        {
            rot.y = 180;
            MoveSet();
        }
        if (Input.GetKey(KeyCode.D))
        {
            rot.y = 90;
            MoveSet();
        }
        
        transform.Translate(speed);
        PlayerAnimator.SetBool("run",isRun);
    }
    void OnCollisionEnter(Collision other)//  他オブジェクトに触れた時の処理
    {
        if (other.gameObject.name== "Plane")//  もしPlanetというタグがついたオブジェクトに触れたら、
        {
            Grounded = true;
            isJump = false;//  Groundedをtrueにする
            PlayerAnimator.SetBool("jump", isJump);
        }
    }


    void MoveSet()
    {
        speed.z = PlayerSpeed;
        transform.eulerAngles = Camera.transform.eulerAngles + rot;
        isRun = true;
    }

    void Rotation()
    {
        var speed = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speed.y =-RotationSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            speed.y = RotationSpeed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed.x = RotationSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed.x = -RotationSpeed;
        }

        Camera.transform.eulerAngles += speed;
    }

    void Jump()
    {
        
        if (Grounded == true)//  もし、Groundedがtrueなら、
        {
            if (Input.GetKeyDown(KeyCode.Space))//  もし、スペースキーがおされたなら、  
            {
                isJump = true;
                Grounded = false;//  Groundedをfalseにする
                Rig.AddForce(transform.up * Jumppower * 100);//  上にJumpPower分力をかける
                PlayerAnimator.SetBool("jump", isJump);
            }
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerAnimator.SetBool("attack", true);
            canMove = false;
        }
    }

    void WeaponON()
    {
        WeaponCollider.enabled = true;
    }

    void WeaponOFF()
    {
        WeaponCollider.enabled = false;
        PlayerAnimator.SetBool("attack", false);
    }

    void CanMove()
    {
        canMove = true;
    }
}
