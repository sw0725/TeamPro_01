using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    
    public float interval = 1.5f;
    public int totalSpawn = 4;



    private void Start()
    {
        MakeZombie();
    }



    private void MakeZombie()
    {
        StartCoroutine(SpawnCoroutine());
    }


    IEnumerator SpawnCoroutine()
    {
        for(int i = 0; i < totalSpawn; i++)
        {
            yield return new WaitForSeconds(interval);
            Spawn();
        }
        
    }

    void Spawn()
    {
        EnemyBace zombie = Factory.Instance.GetZombie(GetSpawnPosition());
        zombie.transform.SetParent(transform);
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 pos = transform.position;
        //pos.z += Random.Range();

        return pos;
    }

}
