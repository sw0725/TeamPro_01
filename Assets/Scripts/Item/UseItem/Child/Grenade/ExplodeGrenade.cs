using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodeGrenade : GrenadeBase
{
    public GameObject target;
    [Tooltip("Ÿ������ ������ �� ����Ʈ")]
    public List<EnemyBace> targets = new List<EnemyBace>();
    public bool isExplode = false;

    public SphereCollider sightTrigger;

    private void Awake()
    {
        sightTrigger = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        sightTrigger.radius = range;     // ���� ���� ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�ϸ� �ٷ� ����. ���.
        Debug.Log("����");
        StartCoroutine(Explode());
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("������");

        if (!targets.Contains(other.gameObject.GetComponent<EnemyBace>()) && isExplode)     // ����Ʈ targets�� ����� ���� ���ٸ�
        {
            targets.Add(other.gameObject.GetComponent<EnemyBace>());
        }
    }

    /// <summary>
    /// ����.
    /// </summary>
    IEnumerator Explode()
    {
        isExplode = true;
        yield return new WaitForSeconds(0.3f);

        // Ÿ������ üũ �� ��� ������ �� ������ �Դ´�.
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].maxHP -= grenadeDamage;
        }
    }
}