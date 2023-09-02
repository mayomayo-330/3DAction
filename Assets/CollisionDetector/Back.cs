using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public static bool b = true;
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
        if (Input.GetKey(KeyCode.S) && other.tag == "Plane")
        {
            b = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plane")
        {
            b = true;
        }
    }
}
