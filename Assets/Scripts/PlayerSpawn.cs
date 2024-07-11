using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.transform.position = SpawnPoint.position;
    }

    
}
