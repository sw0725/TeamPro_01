using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodeGrenade : GrenadeBase
{
    public GameObject target;
    [Tooltip("타깃으로 정해진 적 리스트")]
    public List<EnemyBace> targets = new List<EnemyBace>();
    public bool isExplode = false;

    public SphereCollider sightTrigger;

    private void Awake()
    {
        sightTrigger = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        sightTrigger.radius = range;     // 폭발 범위 지정
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌하면 바로 폭발. 충수.
        Debug.Log("폭발");
        StartCoroutine(Explode());
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("데미지");

        if (!targets.Contains(other.gameObject.GetComponent<EnemyBace>()) && isExplode)     // 리스트 targets에 저장된 적이 없다면
        {
            targets.Add(other.gameObject.GetComponent<EnemyBace>());
        }
    }

    /// <summary>
    /// 폭발.
    /// </summary>
    IEnumerator Explode()
    {
        isExplode = true;
        yield return new WaitForSeconds(0.3f);

        // 타깃으로 체크 된 모든 적들은 다 데미지 입는다.
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].maxHP -= grenadeDamage;
        }
    }
}