using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class NoiseSystem : RecycleObject
{
    public float radius = 1.0f;     //���� �ݰ�
    public Action<Transform> onFind;
    public Transform? origin;       //ȣ���� ������Ʈ�� ��ġ

    WaitForSeconds existTime = new WaitForSeconds(0.5f);
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
                onFind += (origin) => entity.OnDetect(origin);
            }
            onFind?.Invoke(origin);
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
            int index = 0;
            while (enemies.Count > 0)
            {
                enemies.Remove(enemies[index]);
                index++;
            }
        
        }
    }

    public void DataSet(float radi, Transform target) 
    {
        Radius = radi;
        origin = target;
    }
}
