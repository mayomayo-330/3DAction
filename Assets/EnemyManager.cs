using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
    {
    public GameObject enemy1;
    public GameObject enemy2;

    // �G�̈ʒu
    public Transform EnemyPlace1;
    public Transform EnemyPlace2;

    float TimeCount;
    // Start is called before the first frame update
    void Start()
    {
        // �G�̐���
        Instantiate(enemy1, EnemyPlace1.position, Quaternion.identity);
        Instantiate(enemy2, EnemyPlace2.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
