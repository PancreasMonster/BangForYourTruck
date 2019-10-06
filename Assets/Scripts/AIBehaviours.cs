using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIBehaviours : MonoBehaviour
{
    public GameObject target, homeBase;
    public float speed, health, maxhealth;
    public bool inAir = true, player1;

    [HideInInspector]
    public NavMeshAgent NMA;

    public void Awake()
    {
        NMA = GetComponent<NavMeshAgent>();
    }
}
