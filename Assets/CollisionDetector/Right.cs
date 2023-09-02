using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    public static bool r = true;
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
        if (Input.GetKey(KeyCode.D) && other.tag == "Plane")
        {
            r = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plane")
        {
            r = true;
        }
    }
}
