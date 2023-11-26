using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerController : MonoBehaviour
{
    public float PlayerSpeed;
    public float RotationSpeed;
    Vector3 speed = Vector3.zero;
    Vector3 rot = Vector3.zero;

    //�v���C���[��Rigidbody
    private Rigidbody rb = null;

    //�n�ʂɒ��n���Ă��邩���肷��ϐ�
    public bool Grounded;
    private bool IsGrounded;

    //�@���x�N�g��
    public Vector3 NormalOfStickingWall { get; private set; } = Vector3.zero;

    //�ړ�
    float x;
    float z;
    Vector3 movingDirecion;
    public float speedMagnification; //�����K�v�@��10
    public Vector3 movingVelocity;

    //�W�����v��
    public float Jumppower;

    //�J��������
    private Vector3 cameraForward;
    private Vector3 moveForward;

    //�A�j���[�V����
    public Animator PlayerAnimator;
    bool isRun;
    bool isJump;

    //�U��
    public Collider WeaponCollider;
    bool canMove = true;
    bool canAttack = true;

    public AudioSource audioSource;
    public AudioClip AttackSE;
    public AudioClip WalljumpSE;
    public AudioClip DashSE;

    //�Ŕ���
    public GameObject SignboardCanvas;    //�������
    public GameObject SignboardCanvas2;     //�n�[�h���[�h

    //�R�C������
    public static int coinCount;

    //�Q�[���I�[�o�[����
    public static int falling;

    //�ǃW�����v
    private float time;
    public GameObject Effect;


    // Start is called before the first frame update
    void Start()
    {
        time = 3f;
        rb = this.GetComponent<Rigidbody>();
        WeaponCollider.enabled = false;
        coinCount = 0;
        falling = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        IsGrounded = CheckGrounded();

        if (IsGrounded == true)
        {
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
            movingDirecion = new Vector3(x, 0, z);
            movingDirecion.Normalize();//�΂߂̋����������Ȃ�̂�h���܂�


            //�n��L�[���͎󂯎��
            if (Front.f == false)
            {
                if (z > 0)
                {
                    movingDirecion = new Vector3(x, 0, 0);
                }
            }
            if (Back.b == false)
            {
                if (z < 0)
                {
                    movingDirecion = new Vector3(x, 0, 0);
                }
            }
            if (Left.l == false)
            {
                if (x < 0)
                {
                    movingDirecion = new Vector3(0, 0, z);
                }
            }
            if (Right.r == false)
            {
                if (x > 0)
                {
                    movingDirecion = new Vector3(0, 0, z);
                }
            }
            movingVelocity = movingDirecion * speedMagnification * PlayerSpeed;

            Jump();
            Attack();
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");

            movingDirecion = new Vector3(x, 0, z);
            movingDirecion.Normalize();//�΂߂̋����������Ȃ�̂�h���܂�

            //�󒆃L�[���͎󂯎��
            if (Front.f == false)
            {
                if (z != 0)
                {
                    movingDirecion = new Vector3(x, 0, 0);
                }
            }
            if (Back.b == false)
            {
                if (z != 0)
                {
                    movingDirecion = new Vector3(x, 0, 0);
                }
            }
            if (Left.l == false)
            {
                if (x != 0)
                {
                    movingDirecion = new Vector3(0, 0, z);
                }
            }
            if (Right.r == false)
            {
                if (x != 0)
                {
                    movingDirecion = new Vector3(0, 0, z);
                }
            }

            movingVelocity = movingDirecion * speedMagnification * PlayerSpeed;

            // �W�����v�A�j���[�V����
            isJump = true;
            PlayerAnimator.SetBool("jump", isJump); 

            //�ǃW�����v����
            if (time < 0.1f)
            {
                if (Input.GetKeyDown(KeyCode.Space))//  �����A�X�y�[�X�L�[�������ꂽ�Ȃ�A  
                {

                    //this.transform.forward = this.NormalOfStickingWall;

                    rb.velocity = new Vector3(this.NormalOfStickingWall.x * 15 + rb.velocity.x,
                    0, this.NormalOfStickingWall.z * 15 + rb.velocity.z);
                    rb.AddForce(transform.up * Jumppower * 100);//  ���JumpPower���͂�������
                    audioSource.PlayOneShot(WalljumpSE);
                    var effect = Instantiate(Effect);
                    effect.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+2.0f, this.transform.position.z);
                }
            }
        }

        // �����ŃQ�[���I�[�o�[�t���O
        if(this.transform.position.y <= -100)
        {
            falling = 1;
        }
    }

    Vector3 latestPos;

    public void FixedUpdate()
    {
        //�J������]�������Ƃ����ʕ�������]������
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveForward = cameraForward * z + Camera.main.transform.right * x;
        rb.velocity = moveForward * PlayerSpeed * 4 + new Vector3(0, rb.velocity.y, 0);

        //rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);

        //�O�t���[���Ƃ̈ʒu�̍�����i�s����������o���Ă��̕����ɉ�]���܂��B
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;

        // �V�[�����X�ɕ����]��
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            //�~�܂��Ă���Ƃ�
            if (movingDirecion == new Vector3(0, 0, 0))
            {
                isRun = false;
                PlayerAnimator.SetBool("run", isRun);
                return;
            }
            //�ړ�
            else
            {
                Quaternion rot = Quaternion.LookRotation(differenceDis);
                rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.2f);
                this.transform.rotation = rot;
                isRun = true;
                PlayerAnimator.SetBool("run", isRun);
            }
        }

        
    }

    void Move()
    {

        speed = Vector3.zero;
        rot = Vector3.zero;
        isRun = false;

        if (canMove == false)
        {
            return;
        }
        
        
        
        
        if (Input.GetKey(KeyCode.W))
        {
            if (Front.f == false)
            {
                return;
            }
            //rot.y = 0;
            //transform.eulerAngles = Camera.transform.eulerAngles + rot;
            isRun = true;

            
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Left.l == false)
            {
                return;
            }
            //rot.y = -90;
            //transform.eulerAngles = Camera.transform.eulerAngles + rot;
            isRun = true;

            

        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Back.b == false)
            {
                return;
            }
            //rot.y = 180;
            //transform.eulerAngles = Camera.transform.eulerAngles + rot;
            isRun = true;

            
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Right.r == false)
            {
                return;
            }

            //rot.y = 90;
            //transform.eulerAngles = Camera.transform.eulerAngles + rot;
            isRun = true;

        }

        
        PlayerAnimator.SetBool("run", isRun);
    }

    Vector3 normalVector;

    void OnCollisionEnter(Collision other)//  ���I�u�W�F�N�g�ɐG�ꂽ���̏���
    {
        if (other.gameObject.tag == "Plane")//  ����Plane�Ƃ����^�O�������I�u�W�F�N�g�ɐG�ꂽ��A
        {
            normalVector = other.contacts[0].normal;
            if (normalVector.y < 0.02f)
            {
                time = 0;
                
            }
            else
            {
                isJump = false;//  Grounded��true�ɂ���
                PlayerAnimator.SetBool("jump", isJump);
            }
        }
    }

    private bool CheckGrounded()// ���n����
    {
        var ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        float distance = 1f;
        return Physics.Raycast(ray, distance);
    }

    //void OnCollisionExit(Collision other)//  ���I�u�W�F�N�g�ɐG�ꂽ���̏���
    //{
    //    if (other.gameObject.tag == "Plane")//  ����Planet�Ƃ����^�O�������I�u�W�F�N�g���痣�ꂽ��A
    //    {
    //        if (normalVector.x < 0.001f && normalVector.y > 0.999f)
    //        {
    //            Grounded = false;
    //        }
            
    //    }
    //}
    



    //void MoveSet()
    //{
    //    speed.z = PlayerSpeed;
    //    transform.eulerAngles = Camera.transform.eulerAngles + rot;
    //    isRun = true;
    //}

    //void Rotation()
    //{
    //    var speed = Vector3.zero;
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        speed.y = -RotationSpeed;
    //    }
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        speed.y = RotationSpeed;
    //    }
    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {
    //        speed.x = RotationSpeed;
    //    }
    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        speed.x = -RotationSpeed;
    //    }

    //    Camera.transform.eulerAngles += speed;
    //}

    void Jump()
    {

        if (IsGrounded == true)//  �����AGrounded��true�Ȃ�A
        {
                if (Input.GetKeyDown(KeyCode.Space))//  �����A�X�y�[�X�L�[�������ꂽ�Ȃ�A  
                {
                    isJump = true;
                    rb.AddForce(transform.up * Jumppower * 100);//  ���JumpPower���͂�������
                    PlayerAnimator.SetBool("jump", isJump);
                    audioSource.PlayOneShot(WalljumpSE);
                }
        }
    }



    void Attack()
    {
        if (canAttack == false)
        {
            return;
        }

        // ����U��
        if (Input.GetMouseButton(0))
        {
            PlayerAnimator.SetBool("attack", true);
            canAttack = false;
            audioSource.PlayOneShot(AttackSE);
        }
    }

    int coincollider = 0;

    private void OnTriggerEnter(Collider other)
    {
        //�R�C���ɐG����
        if (coincollider == 0 && other.tag == "Coin")
        {
            coinCount++;  //�\������+1
            coincollider = 1;
            Invoke("CoinDetect", 1);
        }
    }

    //�R�C���̏d���l����h��
    private void CoinDetect()
    {
        coincollider = 0;
    }


    private void OnTriggerStay(Collider other)
    {
        //�Ŕ̑O�ɗ���
        if (other.tag == "signboard")
        {
            canAttack = false;  //�O�ł͍U���ł��Ȃ�����

            //�U���{�^����������
            if (Input.GetMouseButton(0))
            {
                SignboardCanvas.SetActive(true);   //����������b�Z�[�W�\��
            }
        }
        if (other.tag == "signboard2")
        {
            canAttack = false;  //�O�ł͍U���ł��Ȃ�����

            //�U���{�^����������
            if (Input.GetMouseButton(0))
            {
                SignboardCanvas2.SetActive(true);   //�n�[�h���[�h���b�Z�[�W�\��
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //�����ƃ��b�Z�[�W����
        if (other.tag == "signboard")
        {
            SignboardCanvas.SetActive(false);
            canAttack = true;
        }
        //�����ƃ��b�Z�[�W����
        if (other.tag == "signboard2")
        {
            SignboardCanvas2.SetActive(false);
            canAttack = true;
        }
    }

    //����U��Ԃ͍U�����������
    void WeaponON()
    {
        WeaponCollider.enabled = true;
    }

    //�U�肨����Ɣ��������
    void WeaponOFF()
    {
        WeaponCollider.enabled = false;
        PlayerAnimator.SetBool("attack", false);
    }

    void CanMove()
    {
        canMove = true;
        canAttack = true;
    }

    void DashON()
    {
        audioSource.PlayOneShot(DashSE);
    }

}
