using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTrap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
