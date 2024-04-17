using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class NoiseSystem : RecycleObject
{
    public float radius = 1.0f;     //���� �ݰ�
    public Action<Vector3> onFind;
    public Transform origin;       //ȣ���� ������Ʈ�� ��ġ

    WaitForSeconds existTime = new WaitForSeconds(0.1f);
    float Radius 
    {
        get => radius;
        set 
        {
            radius = value;
            sphere.radius = value;
        }
    }

    List<EnemyBace> enemies = new List<EnemyBace>();
    SphereCollider sphere;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        sphere.radius = radius;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(ExistTimer());
    }

    protected override void OnDisable()
    {
        onFind = null;
        base.OnDisable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (origin == null) origin = transform;
        EnemyBace enemy = other.GetComponent<EnemyBace>();
        if (enemy != null)
        {
            enemies.Add(enemy);
            
            foreach (EnemyBace entity in enemies)
            {
                onFind += (originPos) => entity.OnDetect(origin.position);
            }
            onFind?.Invoke(origin.position);
        }
    }

    IEnumerator ExistTimer() 
    {
        yield return existTime;
        Extinct();
        gameObject.SetActive(false);
    }

    void Extinct() 
    {
        if (enemies != null) 
        {
            //foreach (EnemyBace entity in enemies)     //����Ʈ ����ġ������ ����Ʈ ������ȭ ������ �ȵ�
            //{
            //    enemies.Remove(entity);
            //}
            while (enemies.Count > 0)
            {
                enemies.Remove(enemies[0]);
            }
        
        }
    }

    public void DataSet(float radi, Transform target) 
    {
        Radius = radi;
        origin = target;
    }
}
