using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Front : MonoBehaviour
{
    public static bool f = true;
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
        if (Input.GetKey(KeyCode.W) && other.tag == "Plane")
        {
            f = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plane")
        {
            f = true;
        }
    }
}
