using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Particle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Debug.Log("Hit_Particle");
            Rigidbody body = col.GetComponent<Rigidbody>();
            if (body)
            {
                Vector3 direction = col.transform.position - transform.position;
                direction = direction.normalized;
                body.AddForce(direction * 50);
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Hit_Particle");
            Rigidbody body = other.GetComponent<Rigidbody>();
            if(body)
            {
                Vector3 direction = other.transform.position - transform.position;
                direction = direction.normalized;
                body.AddForce(direction * 50);
            }
        }

    }
}
