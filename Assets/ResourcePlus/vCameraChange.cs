using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vCameraChange : MonoBehaviour
{
    public GameObject ThirdGameObject;
    public GameObject ThirdCamera;
    public GameObject TopGameObject;
    public GameObject TopCamera;
    private bool ChangePlayer = false; //false 3인칭 - true topView
    private vChangeWeapon ThirdWeaponState;
    private vChangeWeapon TopWeaponState;
    private Invector.vItemManager.vAmmoManager ThirdAmmoScript;
    private Invector.vItemManager.vAmmoManager TopAmmoScript;


    private void Start()
    {
        ThirdWeaponState = ThirdGameObject.GetComponent<vChangeWeapon>();
        TopWeaponState = TopGameObject.GetComponent<vChangeWeapon>();
        ThirdAmmoScript = TopGameObject.GetComponent<Invector.vItemManager.vAmmoManager>();
        TopAmmoScript = TopGameObject.GetComponent<Invector.vItemManager.vAmmoManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ThirdGameObject.SetActive(ChangePlayer);
            ThirdCamera.SetActive(ChangePlayer);
            TopGameObject.SetActive(!ChangePlayer);
            TopCamera.SetActive(!ChangePlayer);

            if (ChangePlayer)
            {
                ThirdGameObject.transform.position = TopGameObject.transform.position;
                ThirdGameObject.transform.rotation = TopGameObject.transform.rotation;
                if (ThirdWeaponState.Weapon != TopWeaponState.Weapon)
                    ThirdWeaponState.ChangeWeapon();
                //ThirdGameObject.GetComponent<Invector.vItemManager.vAmmoManager>().ammoListData = TopGameObject.GetComponent<Invector.vItemManager.vAmmoManager>().ammoListData;
                //ThirdGameObject.GetComponent<Invector.vItemManager.vAmmoManager>().ammos[0] = TopGameObject.GetComponent<Invector.vItemManager.vAmmoManager>().ammos[0];
            }
            else
            {

                TopGameObject.transform.position = ThirdGameObject.transform.position;
                TopGameObject.transform.rotation = ThirdGameObject.transform.rotation;
                if (ThirdWeaponState.Weapon != TopWeaponState.Weapon)
                    TopWeaponState.ChangeWeapon();

                //TopGameObject.GetComponent<Invector.vItemManager.vAmmoManager>().ammoListData = ThirdGameObject.GetComponent<Invector.vItemManager.vAmmoManager>().ammoListData;
                //TopGameObject.GetComponent<Invector.vItemManager.vAmmoManager>().ammos[0] = ThirdGameObject.GetComponent<Invector.vItemManager.vAmmoManager>().ammos[0];
            }

            ChangePlayer = !ChangePlayer;
            Cursor.visible = false;
        }
    }
}
