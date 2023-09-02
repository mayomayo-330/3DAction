using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLimit : MonoBehaviour
{
    //�ǉ��@X��Y�̏��
    public float xmin;
    public float xmax;
    public float zmin;
    public float zmax;



    void Update()
    {
        //�ǉ��@���݂̃|�W�V������ێ�����
        Vector3 currentPos = this.transform.position;

        //�ǉ��@Mathf.Clamp��X,Y�̒l���ꂼ�ꂪ�ŏ��`�ő�͈͓̔��Ɏ��߂�B
        //�͈͂𒴂��Ă�����͈͓��̒l��������
        currentPos.x = Mathf.Clamp(currentPos.x, xmin, xmax);
        currentPos.z = Mathf.Clamp(currentPos.z, zmin, zmax);

        //�ǉ��@position��currentPos�ɂ���
        this.transform.position = currentPos;

    }
}
