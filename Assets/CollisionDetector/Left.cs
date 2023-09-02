using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    public static bool l = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (Input.GetKey(KeyCode.A) && other.tag == "Plane")
        {
            l = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plane")
        {
            l = true;
        }
    }
}