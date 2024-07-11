using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    SpawningNPCs spawner;
    State currentState;
    
    [HideInInspector] public BoxCollider col;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;

    //Waypoints config
    public Transform[] waypoints;
    [SerializeField] float speed;
    [SerializeField] float runSpeed;
    [HideInInspector]public int index;
    //PanicConfig
    [SerializeField]bool panic;
    [SerializeField]float rayRange;
    [SerializeField] Transform rayPos;

    //Character Config
    [SerializeField]List<GameObject> CharacterModels = new List<GameObject>();
    int randomCharacter;
    [SerializeField] float maxHealth;
    string npcQueue;
    float currentHealth;
    //refs for FSM
    public Transform[] Waypoints => waypoints;
    public float Speed => speed;
    public Transform Raypos => rayPos;
    public float RayRange => rayRange;
    public float RunSpeed => runSpeed;
    public bool PanicState => panic;
    public string NPCQueue => npcQueue;

    void Awake()
    {
        col = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        randomCharacter = UnityEngine.Random.Range(0, CharacterModels.Count);
        CharacterModels[randomCharacter].SetActive(true);
        currentHealth = maxHealth;

        index = 0;
        Roam();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void TakeDamage(float dmg)
    {
        panic = true;
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SpawningNPCs.Instance.RemoveFromList(NPCQueue);
        Destroy(gameObject, 5);
        SetState(new DeadState(this));
    }

    public void SetState(State state)
    {
        currentState = state;
        currentState.Start();
    }

    void Roam()
    {
        SetState(new RoamState(this));
    }
    void Panic()
    {
        SetState(new RoamState(this));
    }

    public void GiveWaypoints(Transform[] array)
    {
        waypoints = array;
    }
    public void GiveQueue(string queueName)
    {
        npcQueue = queueName;
    }
}
