using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //カメラの移動
    [SerializeField] GameObject player;
    private Vector3 mouseInput;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += player.transform.position - playerPos;
        playerPos = player.transform.position;
       
        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1))
        {
            // マウスの移動量
            mouseInput.x = Input.GetAxis("Mouse X");
            mouseInput.y = Input.GetAxis("Mouse Y");

            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(playerPos, Vector3.up, mouseInput.x * Time.deltaTime * 200f);

            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            transform.RotateAround(playerPos, transform.right, mouseInput.y * Time.deltaTime * 200f);
        }
    }
}
