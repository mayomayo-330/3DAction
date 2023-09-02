using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //�J�����̈ړ�
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
       
        // �}�E�X�̉E�N���b�N�������Ă����
        if (Input.GetMouseButton(1))
        {
            // �}�E�X�̈ړ���
            mouseInput.x = Input.GetAxis("Mouse X");
            mouseInput.y = Input.GetAxis("Mouse Y");

            // target�̈ʒu��Y���𒆐S�ɁA��]�i���]�j����
            transform.RotateAround(playerPos, Vector3.up, mouseInput.x * Time.deltaTime * 200f);

            // �J�����̐����ړ��i���p�x�����Ȃ��A�K�v��������΃R�����g�A�E�g�j
            transform.RotateAround(playerPos, transform.right, mouseInput.y * Time.deltaTime * 200f);
        }
    }
}
