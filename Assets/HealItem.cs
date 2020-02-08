using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public AudioClip obtain;
    private bool Check = false;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!Check && collision.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().PlayOneShot(obtain);
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().IncreaseHp1();
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
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().IncreaseHp1();
            Destroy(this.gameObject);
        }
    }
}
