using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIBehaviours : MonoBehaviour
{
    public GameObject target;
    public float speed, health, maxhealth;
    public bool player1;

    [HideInInspector]
    

    public void Awake()
    {
        
    }
}
