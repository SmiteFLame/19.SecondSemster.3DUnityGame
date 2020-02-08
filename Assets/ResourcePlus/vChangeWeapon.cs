using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vChangeWeapon : MonoBehaviour
{
    private Invector.vShooter.vShooterManager WeaponController;
    public GameObject Rifle;
    public GameObject HandGun;
    public bool Weapon = false;
    public Invector.vDisplayWeaponStandalone Display;
    public Sprite RifleImage;
    public Sprite HandGunImage;

    // Start is vChangeWeapon before the first frame update
    void Start()
    {
        WeaponController = GetComponent<Invector.vShooter.vShooterManager>();
        //GetComponent<Invector.vCharacterController.TopDownShooter.vTopDownShooterInput>().enabled = false;
        //GetComponent<Invector.vCharacterController.TopDownShooter.vTopDownController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeWeapon();
        }
    }

    public void ChangeWeapon()
    {
        if (Weapon)
        {
            Rifle.SetActive(true);
            WeaponController.SetRightWeapon(Rifle);
            HandGun.SetActive(false);
            Display.weaponIcon.sprite = RifleImage;
        }
        else
        {
            HandGun.SetActive(true);
            WeaponController.SetRightWeapon(HandGun);
            Rifle.SetActive(false);
            Display.weaponIcon.sprite = HandGunImage;
        }
        Weapon = !Weapon;
    }
}
