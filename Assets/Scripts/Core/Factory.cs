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
    public GameObject Spray_FatWhite;
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

    public NoiseSystem GetNoise(float radius, Transform target,  Vector3 position, float angle = 0.0f)
    {
        NoiseSystem noise = noisePool.GetObject(position, angle * Vector3.forward);
        noise.DataSet(radius, target);

        return noise;
    }

    // gpt �̿��� �ڵ�
    //�������� �����ϴ� �Լ�
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
    public GameObject[] MakeItems(GameObject prefab, uint count, Vector3 position, bool useNoise = false)
    {
        GameObject[] items = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            items[i] = MakeItem(prefab, position, useNoise);
        }
        return items;
    }

    // �������� �����ϴ� �Լ� (�������� ��ü�� �� �⺻ ������ �������� ���)
    public GameObject MakeItem(ItemCode code, Vector3 position, bool useNoise = false)
    {
        // �ڵ带 �̿��Ͽ� ������ �������� ������ (�� �κ��� �ڵ忡 ���� �ٸ��� ������ �� ����)
        GameObject prefab = GetItemPrefabByCode(code);

        // ������ ���������� ������ ����
        if (prefab != null)
        {
            return MakeItem(prefab, position, useNoise);
        }
        else
        {
            // �⺻ ������ �������� ����Ͽ� ����
            return MakeItem(defaultItemPrefab, position, useNoise);
        }
    }

    // ������ �������� �ڵ忡 ���� �������� �Լ� (�����Ǿ� ���� �����Ƿ� ������ ���� �ʿ�)
    private GameObject GetItemPrefabByCode(ItemCode code)
    {
        switch (code)
        {
            case ItemCode.MiddleHeal:
                return Spray_FatWhite;  
            //    or ���ҽ� ���� ���� �ϱ�
            //case ItemCode.SmallHeal:
            //    return Resources.Load<GameObject>("Prefabs/herbmix_red");


            default:
                Debug.LogWarning("Unhandled item code: " + code);

                return null;
        }
    }


}