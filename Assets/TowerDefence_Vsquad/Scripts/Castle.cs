using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    



    void OnTriggerEnter(Collider other)

    {
        if (other.tag == "enemyMain")
        { 
            Destroy(other.gameObject);
            
        }
    }




}
