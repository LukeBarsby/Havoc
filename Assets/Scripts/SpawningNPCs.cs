using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningNPCs : Singleton<SpawningNPCs>
{
    public float NPCDeathCount;
    NPCScript npcController;
    [SerializeField] GameObject npcPrefab;
    [SerializeField] GameObject NPCHolder;
    [SerializeField] float spawnDelay;

    [Header("Block 0")]
    public Queue<GameObject> Block0 = new Queue<GameObject>();
    public Queue<GameObject> Block0Outside = new Queue<GameObject>();
    public Transform[] Block0WaypointsInside;
    public Transform[] Block0Waypoints;
    public float block0SpawnCount; 
    [Header("Block 1")]
    public Queue<GameObject> Block1 = new Queue<GameObject>();
    public Transform[] Block1Waypoints;
    public float block1SpawnCount;
    [Header("Block 2")]
    public Queue<GameObject> Block2 = new Queue<GameObject>();
    public Transform[] Block2Waypoints;
    public float block2SpawnCount;
    [Header("Block 3")]
    public Queue<GameObject> Block3 = new Queue<GameObject>();
    public Transform[] Block3Waypoints;
    public float block3SpawnCount;
    [Header("Block 4")]
    public Queue<GameObject> Block4 = new Queue<GameObject>();
    public Transform[] Block4Waypoints;
    public float block4SpawnCount;
    [Header("Block 5")]
    public Queue<GameObject> Block5 = new Queue<GameObject>();
    public Transform[] Block5Waypoints;
    public float block5SpawnCount;
    [Header("Block 6")]
    public Queue<GameObject> Block6 = new Queue<GameObject>();
    public Transform[] Block6Waypoints;
    public float block6SpawnCount;
    [Header("Block 7")]
    public Queue<GameObject> Block7 = new Queue<GameObject>();
    public Transform[] Block7Waypoints;
    public float block7SpawnCount;
    [Header("Block 8")]
    public Queue<GameObject> Block8 = new Queue<GameObject>();
    public Transform[] Block8Waypoints;
    public float block8SpawnCount;
    [Header("Edge")]
    public Queue<GameObject> BlockEdge = new Queue<GameObject>();
    public Transform[] BlockEdgeWaypoints;
    public float blockEdgeSpawnCount;

    public void StartGame()
    {
        NPCDeathCount = 0;
        StartCoroutine(SpawnNPC());
    }


    IEnumerator SpawnNPC()
    {
        yield return new WaitForEndOfFrame();
        
        StartCoroutine(SpawnBlock(Block0Outside, Block0WaypointsInside, block0SpawnCount, spawnDelay, "Block0Outside"));
        StartCoroutine(SpawnBlock(Block0, Block0Waypoints, block0SpawnCount, spawnDelay, "Block0"));
        StartCoroutine(SpawnBlock(Block1, Block1Waypoints, block1SpawnCount, spawnDelay, "Block1"));
        StartCoroutine(SpawnBlock(Block2, Block2Waypoints, block2SpawnCount, spawnDelay, "Block2"));
        StartCoroutine(SpawnBlock(Block3, Block3Waypoints, block3SpawnCount, spawnDelay, "Block3"));
        StartCoroutine(SpawnBlock(Block4, Block4Waypoints, block4SpawnCount, spawnDelay, "Block4"));
        StartCoroutine(SpawnBlock(Block5, Block5Waypoints, block5SpawnCount, spawnDelay, "Block5"));
        StartCoroutine(SpawnBlock(Block6, Block6Waypoints, block6SpawnCount, spawnDelay, "Block6"));
        StartCoroutine(SpawnBlock(Block7, Block7Waypoints, block7SpawnCount, spawnDelay, "Block7"));
        StartCoroutine(SpawnBlock(Block8, Block8Waypoints, block8SpawnCount, spawnDelay, "Block8"));
        StartCoroutine(SpawnBlock(BlockEdge, BlockEdgeWaypoints, blockEdgeSpawnCount, spawnDelay * 4, "BlockEdge"));
    }

    IEnumerator SpawnBlock(Queue<GameObject> queue, Transform[] waypoints, float amount, float spawnTime, string queueName)
    {
        while (queue.Count < amount)
        {
            GameObject npc = (GameObject)Instantiate(npcPrefab, Vector3.zero, Quaternion.identity);
            if (npc.GetComponent<NPCScript>() != null)
            {
                npc.GetComponent<NPCScript>().GiveWaypoints(waypoints);
                npc.GetComponent<NPCScript>().GiveQueue(queueName);
            }
            queue.Enqueue(npc);
            yield return new WaitForSeconds(spawnTime);
        }

    }

    public void RemoveFromList(string queue)
    {
        NPCDeathCount++;
        switch (queue)
        {
            case "Block0":
                Block0.Dequeue();
                AddNpc(Block0, Block0Waypoints, spawnDelay, "Block0");
                break;
            case "Block0Outside":
                Block0Outside.Dequeue();
                StartCoroutine(AddNpc(Block0Outside, Block0WaypointsInside, spawnDelay, "Block0Outside"));
                break;
            case "Block1":
                Block1.Dequeue();
                StartCoroutine(AddNpc(Block1, Block1Waypoints, spawnDelay, "Block1"));
                break;
            case "Block2":
                Block2.Dequeue();
                StartCoroutine(AddNpc(Block2, Block2Waypoints, spawnDelay, "Block2"));
                break;
            case "Block3":
                Block3.Dequeue();
                StartCoroutine(AddNpc(Block3, Block3Waypoints, spawnDelay, "Block3"));
                break;
            case "Block4":
                Block4.Dequeue();
                StartCoroutine(AddNpc(Block4, Block4Waypoints, spawnDelay, "Block4"));
                break;
            case "Block5":
                Block5.Dequeue();
                StartCoroutine(AddNpc(Block5, Block5Waypoints, spawnDelay, "Block5"));
                break;
            case "Block6":
                Block6.Dequeue();
                StartCoroutine(AddNpc(Block6, Block6Waypoints, spawnDelay, "Block6"));
                break;
            case "Block7":
                Block7.Dequeue();
                StartCoroutine(AddNpc(Block7, Block7Waypoints, spawnDelay, "Block7"));
                break;
            case "Block8":
                Block8.Dequeue();
                StartCoroutine(AddNpc(Block8, Block8Waypoints, spawnDelay, "Block8"));
                break;
            case "BlockEdge":
                BlockEdge.Dequeue();
                StartCoroutine(AddNpc(Block8, Block8Waypoints, spawnDelay, "Block8"));
                break;

            default:
                Debug.Log("Not In list to Remove, name was: " + name);
                break;
        }
    }

    IEnumerator AddNpc(Queue<GameObject> queue, Transform[] waypoints, float spawnTime, string queueName)
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject npc = (GameObject)Instantiate(npcPrefab, Vector3.zero, Quaternion.identity);
        
        if (npc.GetComponent<NPCScript>() != null)
        {
            npc.GetComponent<NPCScript>().GiveWaypoints(waypoints);
            npc.GetComponent<NPCScript>().GiveQueue(queueName);
        }
        queue.Enqueue(npc);
    }

    public void ClearQueuess()
    {
        if (Block0.Count > 0)
        {
            Block0.Clear();
        }
        if (Block0Outside.Count > 0)
        {
            Block0.Clear();
        }
        if (Block1.Count > 0)
        {
            Block1.Clear();
        }
        if (Block2.Count > 0)
        {
            Block2.Clear();
        }
        if (Block3.Count > 0)
        {
            Block3.Clear();
        }
        if (Block4.Count > 0)
        {
            Block4.Clear();
        }
        if (Block5.Count > 0)
        {
            Block5.Clear();
        }
        if (Block6.Count > 0)
        {
            Block6.Clear();
        }
        if (Block7.Count > 0)
        {
            Block7.Clear();
        }
        if (Block8.Count > 0)
        {
            Block8.Clear();
        }
        if (BlockEdge.Count > 0)
        {
            BlockEdge.Clear();
        }

    }


}
