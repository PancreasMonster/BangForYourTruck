using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionGate : MonoBehaviour
{
    public bool flameThrower, machineGun, cannonGun, shotgun;
    public int teamGateNum;
    AudioSource audio;

    [Header("Text Animation")]
    public float timeForFirstLerp = .25f;
    public float timeForSecondLerp = 1;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            if (col.GetComponent<Health>().teamNum == teamGateNum)
            {
               
                if (machineGun)
                {
                    //col.GetComponent<FlamethrowerWeapon>().enabled = false;
                    //col.GetComponent<FlamethrowerWeapon>().model.SetActive(false);
                    col.GetComponent<MachinegunWeapon>().canFire = true;
                    col.GetComponent<MachinegunWeapon>().model.SetActive(true);
                    col.GetComponent<CannonWeapon>().canFire = false;
                    col.GetComponent<CannonWeapon>().model.SetActive(false);
                    col.GetComponent<CannonWeapon>().engineModel.SetActive(false);
                    col.GetComponent<ShotgunWeapon>().canFire = false;
                    col.GetComponent<ShotgunWeapon>().model.SetActive(false);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Machine Gun").gameObject.SetActive(true);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Cannon").gameObject.SetActive(false);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Shotgun").gameObject.SetActive(false);
                    StartCoroutine(playerTextPopUp(col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("WeaponSelectionText").gameObject, "Machine Gun"));
                }
                else if (cannonGun)
                {
                    //col.GetComponent<FlamethrowerWeapon>().enabled = false;
                    //col.GetComponent<FlamethrowerWeapon>().model.SetActive(false);
                    col.GetComponent<MachinegunWeapon>().canFire = false;
                    col.GetComponent<MachinegunWeapon>().model.SetActive(false);
                    col.GetComponent<CannonWeapon>().canFire = true;
                    col.GetComponent<CannonWeapon>().model.SetActive(true);
                    col.GetComponent<CannonWeapon>().engineModel.SetActive(true);
                    col.GetComponent<ShotgunWeapon>().canFire = false;
                    col.GetComponent<ShotgunWeapon>().model.SetActive(false);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Machine Gun").gameObject.SetActive(false);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Cannon").gameObject.SetActive(true);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Shotgun").gameObject.SetActive(false);
                    StartCoroutine(playerTextPopUp(col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("WeaponSelectionText").gameObject, "Cannon"));
                }

                else if (shotgun)
                {
                    //col.GetComponent<FlamethrowerWeapon>().enabled = false;
                    //col.GetComponent<FlamethrowerWeapon>().model.SetActive(false);
                    col.GetComponent<MachinegunWeapon>().canFire = false;
                    col.GetComponent<MachinegunWeapon>().model.SetActive(false);
                    col.GetComponent<CannonWeapon>().canFire = false;
                    col.GetComponent<CannonWeapon>().model.SetActive(false);
                    col.GetComponent<CannonWeapon>().engineModel.SetActive(false);
                    col.GetComponent<ShotgunWeapon>().canFire = true;
                    col.GetComponent<ShotgunWeapon>().model.SetActive(true);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Machine Gun").gameObject.SetActive(false);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Cannon").gameObject.SetActive(false);
                    col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("Shotgun").gameObject.SetActive(true);
                    StartCoroutine(playerTextPopUp(col.GetComponent<FindPlayerStats>().stats.transform.Find("Weapon Icons").transform.Find("WeaponSelectionText").gameObject, "Shotgun"));
                }

                /*else if(flameThrower)
                {
                    col.GetComponent<FlamethrowerWeapon>().enabled = true;
                    col.GetComponent<FlamethrowerWeapon>().model.SetActive(true);
                    col.GetComponent<MachinegunWeapon>().enabled = false;
                    col.GetComponent<MachinegunWeapon>().model.SetActive(false);
                    col.GetComponent<CannonWeapon>().enabled = false;
                    col.GetComponent<CannonWeapon>().model.SetActive(false);
                    col.GetComponent<CannonWeapon>().engineModel.SetActive(false);
                    col.GetComponent<ShotgunWeapon>().enabled = false;
                    col.GetComponent<ShotgunWeapon>().model.SetActive(false);

                }*/
                audio.Play();
            }
        }
    }

    public IEnumerator playerTextPopUp(GameObject text, string name)
    {
        float alpha = 1;
        Text textComponent = text.GetComponent<Text>();
        textComponent.text = name;
        textComponent.color = new Color(1, 1, 1, alpha);        

        while (alpha > 0)
        {
            alpha -= (Time.deltaTime / timeForSecondLerp);
            textComponent.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}
