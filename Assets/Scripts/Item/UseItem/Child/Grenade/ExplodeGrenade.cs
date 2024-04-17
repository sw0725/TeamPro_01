using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ExplodeGrenade : GrenadeBase
{
    [Tooltip("������")]
    public float grenadeDamage = 100;
    [Tooltip("Ÿ������ ������ �� ����Ʈ")]
    private List<EnemyBace> targets = new List<EnemyBace>();
    [Tooltip("�����ϴµ� �ʿ��� ������ Ÿ��")]
    [SerializeField] private float targetsTrackDelay = 0.5f;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    protected override void Explode()
    {
        StartCoroutine(targetDamege());
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyBace enemy = other.GetComponent<EnemyBace>();
        if (enemy != null)
        {
            targets.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyBace enemy = other.GetComponent<EnemyBace>();
        if (enemy != null)
        {
            targets.Remove(enemy);
        }
    }

    /// <summary>
    /// ����.
    /// </summary>
    IEnumerator targetDamege()
    {
        yield return new WaitForSeconds(targetsTrackDelay);
        Instantiate(expoltionEffect, transform.position, Quaternion.identity);

        foreach (EnemyBace enemy in targets)
        {
            enemy.Demage(grenadeDamage);
        }

        Factory.Instance.GetNoise(NoiseRange, transform);

        Destroy(this.gameObject, 0.1f);
    }
}