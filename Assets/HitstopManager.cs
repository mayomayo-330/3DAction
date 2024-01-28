using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitstopManager : MonoBehaviour
{
    // �ǂ�����ł��Ăяo����悤�ɂ���
    public static HitstopManager instance;

    private void Start()
    {
        instance = this;
    }

    // �q�b�g�X�g�b�v���J�n����֐�
    public void StartHitStop(float duration)
    {
        instance.StartCoroutine(instance.HitStopCoroutine(duration));
    }

    // �R���[�`���̓��e
    private IEnumerator HitStopCoroutine(float duration)
    {
        // �q�b�g�X�g�b�v�̊J�n
        Time.timeScale = 0f;

        // �w�肵�����Ԃ�����~
        yield return new WaitForSecondsRealtime(duration);

        // �q�b�g�X�g�b�v�̏I��
        Time.timeScale = 1f;
    }
}