using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_health : MonoBehaviour
{
    public int WeaponHp = 700;
    //private Animator anim;
    //private int hashDeath = Animator.StringToHash("Anim_Death");
    //private int hashHit = Animator.StringToHash("Anim_Hit");
    private bool isDeath;
    //public GameObject money;
    Transform trans;
    void Start()
    {
        //anim = GetComponent<Animator>();
        isDeath = false;
        trans = GetComponent<Transform>();
    }
    public void Dmg(int DMGcount)
    {
        WeaponHp -= DMGcount;
    }

    void Update()
    {

        if (WeaponHp <= 0 && isDeath == false)
        {
            isDeath = true;
            //gameObject.tag = "Dead"; // send it to TowerTrigger to stop the shooting
            StartCoroutine(Death());

        }
    }

   

    IEnumerator Death()
    {
        //anim.SetTrigger(hashDeath);
        yield return new WaitForSeconds(1.0f);
        //Vector3 v = new Vector3(0, 1, 0);
        //Instantiate(money, trans.position + v, trans.rotation);
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
              //Dmg(Random.Range(1,2));
        }
    }

}
