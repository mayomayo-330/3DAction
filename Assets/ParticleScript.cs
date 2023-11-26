using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    //�@�p�[�e�B�N���V�X�e��
    private ParticleSystem ps;
    //�@ScaleUp�p�̌o�ߎ���
    private float elapsedScaleUpTime = 0f;
    //�@Scale��傫������Ԋu����
    [SerializeField]
    private float scaleUpTime = 0.05f;
    //�@ScaleUp���銄��
    [SerializeField]
    private float scaleUpParam = 0.1f;
    //�@�p�[�e�B�N���폜�p�̌o�ߎ���
    private float elapsedDeleteTime = 0f;
    //�@�p�[�e�B�N�����폜����܂ł̎���
    [SerializeField]
    private float deleteTime = 0.5f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedScaleUpTime += Time.deltaTime;
        elapsedDeleteTime += Time.deltaTime;

        if (elapsedDeleteTime >= deleteTime)
        {
            Destroy(gameObject);
        }

        if (elapsedScaleUpTime > scaleUpTime)
        {
            transform.localScale += new Vector3(scaleUpParam, scaleUpParam, scaleUpParam);
            elapsedScaleUpTime = 0f;
        }
    }

   
}