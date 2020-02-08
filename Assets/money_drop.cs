using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money_drop : MonoBehaviour
{
    public AudioSource musciPlayer;
    public AudioClip obtain;
    private bool Check = false;
    // Start is called before the first frame update
    void Start()
    {
        this.musciPlayer = this.gameObject.AddComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (!Check && other.gameObject.CompareTag("Player"))
        {
            Check = true;
            GetComponent<AudioSource>().PlayOneShot(obtain);
            Debug.Log("moneydrop");
            GameObject.Find("Canvas").transform.Find("MoneyUI").transform.Find("moneyText").GetComponent<Getmoney>().getMoney();
            //Debug.Log("other");
            StartCoroutine("Destoryed");
        }
    }
    IEnumerator Destoryed()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}