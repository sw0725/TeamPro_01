using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct ItemSpawnInfo
    {
        public ItemCode code;
        [Range(0, 1)]
        public float dropRatio;
        public uint maxDropCount;
    }
    public ItemSpawnInfo[] dropItems;

    Transform[] spawnPoints;

    const int ItemCount = 33;

    private void Awake()
    {
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++) 
        {
            spawnPoints[i] = transform.GetChild(i);
        }
    }

    private void Start()    //���⼭ ��������Ʈ ����
    {
        GameManager.Instance.OnGameEnding += Clear;
        GameManager.Instance.OnGameStartCompleted += Spawn;
    }

    void Spawn() //��������Ʈ �޾Ƽ� ����
    {
        foreach (Transform point in spawnPoints)
        {
            int dataIndex;
            do
            {
                dataIndex = Random.Range(0, ItemCount);
            }
            while (dropItems[dataIndex].dropRatio < Random.value);

            uint count = (uint)Random.Range(1, dropItems[dataIndex].maxDropCount);
            Factory.Instance.MakeItems(dropItems[dataIndex].code, count, point.position, point, true);
        }
    }

    void Clear() //��������Ʈ �޾Ƽ� ����//�ڱ� �ڽ� �� ����
    {
        foreach (Transform point in spawnPoints) 
        {
            while (point.childCount > 0)     // �ڽ��� ���������� ��� �ݺ�
            {
                Transform child = point.GetChild(0);    // ù��° �ڽ� ����
                child.SetParent(null);                      // �θ� ����(Destroy�� ��� ������� �ʱ� ������ �ʿ�)
                Destroy(child.gameObject);                  // �ڽ� ����
            }
        }
    }

#if UNITY_EDITOR
    public void Test_Spawn() 
    {
        Spawn();
    }
    public void Test_Clear() 
    {
        Clear();
    }
#endif
}
