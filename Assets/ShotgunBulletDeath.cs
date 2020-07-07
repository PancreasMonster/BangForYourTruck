using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ShotgunBulletDeath : MonoBehaviour
{
    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.layer == 14) 
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
