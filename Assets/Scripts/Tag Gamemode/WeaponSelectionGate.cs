using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionGate : MonoBehaviour
{
    public bool flameThrower, machineGun, cannonGun;
    public int teamGateNum;
    AudioSource audio;

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
               if(flameThrower)
                {
                    col.GetComponent<FlamethrowerWeapon>().enabled = true;
                    col.GetComponent<FlamethrowerWeapon>().model.SetActive(true);
                    col.GetComponent<MachinegunWeapon>().enabled = false;
                    col.GetComponent<MachinegunWeapon>().model.SetActive(false);
                    col.GetComponent<CannonWeapon>().enabled = false;
                    col.GetComponent<CannonWeapon>().model.SetActive(false);

                }
                else if (machineGun)
                {
                    col.GetComponent<FlamethrowerWeapon>().enabled = false;
                    col.GetComponent<FlamethrowerWeapon>().model.SetActive(false);
                    col.GetComponent<MachinegunWeapon>().enabled = true;
                    col.GetComponent<MachinegunWeapon>().model.SetActive(true);
                    col.GetComponent<CannonWeapon>().enabled = false;
                    col.GetComponent<CannonWeapon>().model.SetActive(false);
                }
                else if (cannonGun)
                {
                    col.GetComponent<FlamethrowerWeapon>().enabled = false;
                    col.GetComponent<FlamethrowerWeapon>().model.SetActive(false);
                    col.GetComponent<MachinegunWeapon>().enabled = false;
                    col.GetComponent<MachinegunWeapon>().model.SetActive(false);
                    col.GetComponent<CannonWeapon>().enabled = true;
                    col.GetComponent<CannonWeapon>().model.SetActive(true);
                }


                audio.Play();
            }
        }
    }
}
