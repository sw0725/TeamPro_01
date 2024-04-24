using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletBase;

public class WeaponBase : ItemBase
{
    [Tooltip("�ִ� �Ѿ� ��, ��ź��")]
    public int maxAmmo = 10;
    [Tooltip("�����")]
    public float fireRate = 0.1f;
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
    [Tooltip("����")]
    public float noiseVelocity = 7.0f;

    int currentAmmo = 0;
    float coolTime = 0f;
    public bool canFire => coolTime < fireRate && currentAmmo > 0;
    public Action<ItemCode, int> onReload;    //���â�� �����ɶ� �κ��丮�� ���ε� �Լ��� ���� 

    private void Update()
    {
        coolTime -= Time.deltaTime;
    }

    public override void Use() //���ε�
    {
        int needAmmor = maxAmmo - currentAmmo;
        ItemCode needType = ItemCode.PistolBullet;
        switch (ammunitionType) 
        {
            case BulletType.Pistolbullet:
                needType = ItemCode.PistolBullet;
                break;
            case BulletType.Riflebullet:
                needType = ItemCode.RifleBullet;
                break;
            case BulletType.Sniperbullet:
                needType = ItemCode.SniperBullet;
                break;
            case BulletType.Shotgunbullet:
                needType = ItemCode.ShotgunBullet;
                break;
        }
        onReload?.Invoke(needType, needAmmor);
    }

    public void ReLoad(int ammo)
    {
        currentAmmo += ammo;
    }

    public virtual void Fire() 
    {
        if (canFire) 
        {
            currentAmmo--;
            coolTime = fireRate;
        }
    }

}
