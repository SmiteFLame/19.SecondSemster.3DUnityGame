using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int EnemyHp = 30;
    private Animator anim;
    public int hashDeath = Animator.StringToHash("Anim_Death");
    public int hashHit = Animator.StringToHash("Anim_Hit");
    private bool isDeath;
    int randomnumber;

    public GameObject money;
    public GameObject item1;
    public GameObject item2;
    public GameObject Damage;
    public AudioClip DeathSound;

    Transform trans;
    void Start()
    {
        anim = GetComponent<Animator>();
        isDeath = false;
        trans = GetComponent<Transform>();
    }
    public void Dmg(int DMGcount)
    {
        EnemyHp -= DMGcount;
    }

    void Update()
    {

        if (EnemyHp <= 0 && isDeath == false)
        {
            isDeath = true;
            //gameObject.tag = "Dead"; // send it to TowerTrigger to stop the shooting
            StartCoroutine(Death());

        }
    }

    IEnumerator hit()
    {
        this.GetComponent<TestAgentScript>().stopit();
        anim.SetTrigger(hashHit);
        yield return new WaitForSeconds(1.00f);
        this.GetComponent<TestAgentScript>().moveup();

    }

    IEnumerator Death()
    {
        GetComponent<AudioSource>().PlayOneShot(DeathSound);
        this.GetComponent<TestAgentScript>().stopit();
        anim.SetTrigger(hashDeath);
        yield return new WaitForSeconds(1.0f);
        Vector3 v = new Vector3(0, 1, 0);
        randomnumber = Random.Range(0, 8);
        if (randomnumber < 4)
        {
            Instantiate(item1, trans.position + v, trans.rotation);
        }
        else if (randomnumber == 4)
        {
            Instantiate(item2, trans.position + v, trans.rotation);
        }
        else if (randomnumber > 4)
        {
            Instantiate(money, trans.position + v, trans.rotation);
        }
        //Instantiate(money, trans.position+v, trans.rotation);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("weapon"))
        {
            if (this.gameObject.name == "Troll_1_A(Clone)")
            {
                Dmg(Random.Range(10, 30));
            }
            else
            {
                StartCoroutine(hit());
                Dmg(Random.Range(30, 60));

            }
            Instantiate(Damage, collision.transform.position, trans.rotation);
        }
    }

}