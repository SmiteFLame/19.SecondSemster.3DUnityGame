using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp : MonoBehaviour
{
    public AudioClip obtain;
    private bool Check = false;
    private void OnTriggerEnter(Collider collision)
    {
        if (!Check && collision.gameObject.CompareTag("Player"))
        {
            Check = true;
            GetComponent<AudioSource>().PlayOneShot(obtain);
            GameObject.Find("Player").transform.GetChild(1).transform.GetChild(2).
                transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).
                transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).
                transform.GetChild(6).transform.GetChild(0).GetComponent<Invector.vShooter.vShooterWeapon>().
                shootFrequency *= 0.95f;

            StartCoroutine("Destoryed");
        }
    }

    IEnumerator Destoryed()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(obtain);
            collision.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).
                transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).
                transform.GetChild(6).transform.GetChild(0).GetComponent<Invector.vShooter.vShooterWeapon>().
                shootFrequency /= (3.0f/2.0f);
            Debug.Log(collision.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).
                transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).
                transform.GetChild(6).transform.GetChild(0).GetComponent<Invector.vShooter.vShooterWeapon>().
                shootFrequency);
        }
    }
}
