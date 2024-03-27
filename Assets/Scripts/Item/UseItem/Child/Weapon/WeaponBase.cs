using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletBase;

public class WeaponBase : ItemBase
{
    [Tooltip("�ִ� �Ѿ� ��, ��ź��")]
    public int maxAmmo = 10;
    [Tooltip("�����")]
    public int fireRate = 0;
    [Tooltip("����")]
    public float weight = 0f;
    [Tooltip("����")]
    public uint price = 0;
    [Tooltip("������")]
    public float durability = 0f;
    [Tooltip("�ݵ�. �⺻�� ��ġ.")]
    public float recoil = 0f;
    [Tooltip("���� �Ÿ�")]
    public uint sightingRange = 0;
    [Tooltip("ź��")]
    public BulletType ammunitionType;
    [Tooltip("������")]
    public float damage = 5.0f;
    [Tooltip("�ִ� ���ݷ�, ġ��")]
    public float headDamage = 10.0f;
    [Tooltip("���߷�")]
    public float accuracy = 0f;
    [Tooltip("ġȮ")]
    public float critRate = 0f;
    [Tooltip("ź��")]
    public uint muzzleVelocity = 0;

    [Tooltip("������ �Ѿ� ������Ʈ")]
    public GameObject round;

    /// <summary>
    /// ���� �ϸ� �ش� ������Ʈ�� �´� ��ɵ��� ������.
    /// </summary>
    public override void Use()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Player();
        }
    }
}
