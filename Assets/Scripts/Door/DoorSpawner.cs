using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : DoorManual
{
    public float delayTime = 3.0f;

    ZombieSpawner spawner;
    Action onOpen;

    protected override void Awake()
    {
        base.Awake();
        spawner = GetComponentInChildren<ZombieSpawner>();
        onOpen += spawner.MakeZombie;
    }

    protected override void Open()
    {
        base.Open();
        StartCoroutine(DelaySpawn());
    }

    IEnumerator DelaySpawn()                          //�� ������� �÷��̾� �i�� ����ô�.
    {
        yield return new WaitForSeconds(delayTime);

        onOpen?.Invoke();
        onOpen = null;
    }
}
