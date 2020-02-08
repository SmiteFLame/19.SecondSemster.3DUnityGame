using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOpponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MONSTER")
        {
            Destroy(collision.gameObject);
        }
    }
  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MONSTER")
        {
            Destroy(other.gameObject);
        }
    }
}
