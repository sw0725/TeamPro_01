using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType
{
    Zombie = 0,
    Noise,
}

public class Factory : Singleton<Factory>
{
    ZombiePool zombiePool;
    NoisePool noisePool;
    public GameObject defaultItemPrefab;
    public float noisePower = 0.5f;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        zombiePool = GetComponentInChildren<ZombiePool>();
        if (zombiePool != null) zombiePool.Initialize();

        noisePool = GetComponentInChildren<NoisePool>();
        if (noisePool != null) noisePool.Initialize();
    }
    public GameObject GetObject(PoolObjectType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.Zombie:
                result = zombiePool.GetObject(position, euler).gameObject;
                break;
        }

        return result;
    }

    public EnemyBace GetZombie()
    {
        return zombiePool.GetObject();
    }

    public EnemyBace GetZombie(Vector3 position, float angle = 0.0f)
    {
        return zombiePool.GetObject(position, angle * Vector3.forward);
    }

    public NoiseSystem GetNoise(float radius, Transform target)
    {
        NoiseSystem noise = noisePool.GetObject();
        noise.DataSet(radius, target);

        return noise;
    }

    public NoiseSystem GetNoise(float radius, Transform target, Vector3 position, float angle = 0.0f)
    {
        NoiseSystem noise = noisePool.GetObject(position, angle * Vector3.forward);
        noise.DataSet(radius, target);

        return noise;
    }

    //�������� �����ϴ� �Լ�
    public GameObject MakeItem(GameObject prefab, Vector3 position, Transform transform, bool useNoise = false)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity, transform); // �������� �ν��Ͻ�ȭ�Ͽ� ����
        if (useNoise)
        {
            Vector2 rand = Random.insideUnitCircle * noisePower;
            obj.transform.position += new Vector3(rand.x, 0f, rand.y); // ������ ����
        }
        return obj;
    }

    public GameObject MakeItem(GameObject prefab, Vector3 position, bool useNoise = false)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity); // �������� �ν��Ͻ�ȭ�Ͽ� ����
        if (useNoise)
        {
            Vector2 rand = Random.insideUnitCircle * noisePower;
            obj.transform.position += new Vector3(rand.x, 0f, rand.y); // ������ ����
        }
        return obj;
    }

    // �������� ���� �� �����ϴ� �Լ�
    public GameObject[] MakeItems(GameObject prefab, uint count, Vector3 position, Transform transform, bool useNoise = false)
    {
        GameObject[] items = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            items[i] = MakeItem(prefab, position, transform, useNoise);
        }
        return items;
    }

    public GameObject[] MakeItems(GameObject prefab, uint count, Vector3 position, bool useNoise = false)
    {
        GameObject[] items = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            items[i] = MakeItem(prefab, position, useNoise);
        }
        return items;
    }

    // �������� ������ �����ϴ� �Լ� (�������� ��ü�� �� �⺻ ������ �������� ���)
    public GameObject[] MakeItems(ItemCode code, uint count, Vector3 position, Transform transform, bool useNoise = false)
    {
        // �ڵ带 �̿��Ͽ� ������ �������� ������ (�� �κ��� �ڵ忡 ���� �ٸ��� ������ �� ����)
        GameObject prefab = GetItemPrefabByCode(code);

        // ������ ���������� ������ ����
        if (prefab != null)
        {
            return MakeItems(prefab, count, position, transform, useNoise);
        }
        else
        {
            // �⺻ ������ �������� ����Ͽ� ����
            return MakeItems(defaultItemPrefab, count, position, transform, useNoise);
        }
    }

    public GameObject[] MakeItems(ItemCode code, uint count, Vector3 position, bool useNoise = false)
    {
        // �ڵ带 �̿��Ͽ� ������ �������� ������ (�� �κ��� �ڵ忡 ���� �ٸ��� ������ �� ����)
        GameObject prefab = GetItemPrefabByCode(code);

        // ������ ���������� ������ ����
        if (prefab != null)
        {
            return MakeItems(prefab, count, position, useNoise);
        }
        else
        {
            // �⺻ ������ �������� ����Ͽ� ����
            return MakeItems(defaultItemPrefab, count, position, useNoise);
        }
    }

    // �������� �����ϴ� �Լ� (�������� ��ü�� �� �⺻ ������ �������� ���)
    public GameObject MakeItem(ItemCode code, Vector3 position, bool useNoise = false)
    {
        // �ڵ带 �̿��Ͽ� ������ �������� ������ (�� �κ��� �ڵ忡 ���� �ٸ��� ������ �� ����)
        GameObject prefab = GetItemPrefabByCode(code);

        // ������ ���������� ������ ����
        if (prefab != null)
        {
            return MakeItem(prefab, position, transform, useNoise);
        }
        else
        {
            // �⺻ ������ �������� ����Ͽ� ����
            return MakeItem(defaultItemPrefab, position, transform, useNoise);
        }
    }

    // ������ �������� �ڵ忡 ���� �������� �Լ�
    private GameObject GetItemPrefabByCode(ItemCode code)
    {
        return GameManager.Instance.ItemData[code].itemPrefab;
    }
}