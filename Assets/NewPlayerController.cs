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

    //プレイヤーのRigidbody
    private Rigidbody rb = null;

    //地面に着地しているか判定する変数
    public bool Grounded;
    private bool IsGrounded;

    //法線ベクトル
    public Vector3 NormalOfStickingWall { get; private set; } = Vector3.zero;

    //移動
    float x;
    float z;
    Vector3 movingDirecion;
    public float speedMagnification; //調整必要　例10
    public Vector3 movingVelocity;

    //ジャンプ力
    public float Jumppower;

    //カメラ正面
    private Vector3 cameraForward;
    private Vector3 moveForward;

    //アニメーション
    public Animator PlayerAnimator;
    bool isRun;
    bool isJump;

    //攻撃
    public Collider WeaponCollider;
    bool canMove = true;
    bool canAttack = true;

    public AudioSource audioSource;
    public AudioClip AttackSE;
    public AudioClip WalljumpSE;
    public AudioClip DashSE;

    //看板処理
    public GameObject SignboardCanvas;    //操作説明
    public GameObject SignboardCanvas2;     //ハードモード

    //コイン処理
    public static int coinCount;

    //ゲームオーバー処理
    public static int falling;

    //壁ジャンプ
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
            movingDirecion.Normalize();//斜めの距離が長くなるのを防ぎます


            //地上キー入力受け取り
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
            movingDirecion.Normalize();//斜めの距離が長くなるのを防ぎます

            //空中キー入力受け取り
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

            // ジャンプアニメーション
            isJump = true;
            PlayerAnimator.SetBool("jump", isJump); 

            //壁ジャンプ処理
            if (time < 0.1f)
            {
                if (Input.GetKeyDown(KeyCode.Space))//  もし、スペースキーがおされたなら、  
                {

                    //this.transform.forward = this.NormalOfStickingWall;

                    rb.velocity = new Vector3(this.NormalOfStickingWall.x * 15 + rb.velocity.x,
                    0, this.NormalOfStickingWall.z * 15 + rb.velocity.z);
                    rb.AddForce(transform.up * Jumppower * 100);//  上にJumpPower分力をかける
                    audioSource.PlayOneShot(WalljumpSE);
                    var effect = Instantiate(Effect);
                    effect.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+2.0f, this.transform.position.z);
                }
            }
        }

        // 落下でゲームオーバーフラグ
        if(this.transform.position.y <= -100)
        {
            falling = 1;
        }
    }

    Vector3 latestPos;

    public void FixedUpdate()
    {
        //カメラ回転させたとき正面方向も回転させる
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        moveForward = cameraForward * z + Camera.main.transform.right * x;
        rb.velocity = moveForward * PlayerSpeed * 4 + new Vector3(0, rb.velocity.y, 0);

        //rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);

        //前フレームとの位置の差から進行方向を割り出してその方向に回転します。
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;

        // シームレスに方向転換
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            //止まっているとき
            if (movingDirecion == new Vector3(0, 0, 0))
            {
                isRun = false;
                PlayerAnimator.SetBool("run", isRun);
                return;
            }
            //移動
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

    void OnCollisionEnter(Collision other)//  他オブジェクトに触れた時の処理
    {
        if (other.gameObject.tag == "Plane")//  もしPlaneというタグがついたオブジェクトに触れたら、
        {
            normalVector = other.contacts[0].normal;
            if (normalVector.y < 0.02f)
            {
                time = 0;
                
            }
            else
            {
                isJump = false;//  Groundedをtrueにする
                PlayerAnimator.SetBool("jump", isJump);
            }
        }
    }

    private bool CheckGrounded()// 着地処理
    {
        var ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        float distance = 1f;
        return Physics.Raycast(ray, distance);
    }

    //void OnCollisionExit(Collision other)//  他オブジェクトに触れた時の処理
    //{
    //    if (other.gameObject.tag == "Plane")//  もしPlanetというタグがついたオブジェクトから離れたら、
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

        if (IsGrounded == true)//  もし、Groundedがtrueなら、
        {
                if (Input.GetKeyDown(KeyCode.Space))//  もし、スペースキーがおされたなら、  
                {
                    isJump = true;
                    rb.AddForce(transform.up * Jumppower * 100);//  上にJumpPower分力をかける
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

        // 剣を振る
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
        //コインに触れると
        if (coincollider == 0 && other.tag == "Coin")
        {
            coinCount++;  //表示数に+1
            coincollider = 1;
            Invoke("CoinDetect", 1);
        }
    }

    //コインの重複獲得を防ぐ
    private void CoinDetect()
    {
        coincollider = 0;
    }


    private void OnTriggerStay(Collider other)
    {
        //看板の前に立つと
        if (other.tag == "signboard")
        {
            canAttack = false;  //前では攻撃できなくする

            //攻撃ボタンを押すと
            if (Input.GetMouseButton(0))
            {
                SignboardCanvas.SetActive(true);   //操作説明メッセージ表示
            }
        }
        if (other.tag == "signboard2")
        {
            canAttack = false;  //前では攻撃できなくする

            //攻撃ボタンを押すと
            if (Input.GetMouseButton(0))
            {
                SignboardCanvas2.SetActive(true);   //ハードモードメッセージ表示
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //離れるとメッセージ消す
        if (other.tag == "signboard")
        {
            SignboardCanvas.SetActive(false);
            canAttack = true;
        }
        //離れるとメッセージ消す
        if (other.tag == "signboard2")
        {
            SignboardCanvas2.SetActive(false);
            canAttack = true;
        }
    }

    //剣を振る間は攻撃判定をつける
    void WeaponON()
    {
        WeaponCollider.enabled = true;
    }

    //振りおえると判定を消す
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
