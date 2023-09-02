using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour
{
    [SerializeField] GameObject player;

    private Vector3 angle;

    private Vector3 primary_angle;

    private Vector3 playerPos;

    void Start()
    {
        angle = this.gameObject.transform.localEulerAngles;

        primary_angle = this.gameObject.transform.localEulerAngles;
        playerPos = player.transform.position;
    }


    void Update()
    {
        transform.position += player.transform.position - playerPos;
        playerPos = player.transform.position;

        angle.y += Input.GetAxis("Mouse X");

        angle.x -= Input.GetAxis("Mouse Y");

        if (angle.x <= primary_angle.x - 20f)
        {
            angle.x = primary_angle.x - 20f;
        }
        if (angle.x >= primary_angle.x + 20f)
        {
            angle.x = primary_angle.x + 20f;
        }

        this.gameObject.transform.localEulerAngles = angle;
    }
}
