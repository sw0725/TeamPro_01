using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletBase;

public class WeaponBase : ItemBase
{
    [Tooltip("최대 총알 수, 장탄수")]
    public int maxAmmo = 10;
    [Tooltip("연사력")]
    public int fireRate = 0;
    [Tooltip("무게")]
    public float weight = 0f;
    [Tooltip("가격")]
    public uint price = 0;
    [Tooltip("내구도")]
    public float durability = 0f;
    [Tooltip("반동. 기본적 수치.")]
    public float recoil = 0f;
    [Tooltip("조준 거리")]
    public uint sightingRange = 0;
    [Tooltip("탄종")]
    public BulletType ammunitionType;
    [Tooltip("데미지")]
    public float damage = 5.0f;
    [Tooltip("최대 공격력, 치뎀")]
    public float headDamage = 10.0f;
    [Tooltip("명중률")]
    public float accuracy = 0f;
    [Tooltip("치확")]
    public float critRate = 0f;
    [Tooltip("탄속")]
    public uint muzzleVelocity = 0;

    [Tooltip("생성할 총알 오브젝트")]
    public GameObject round;

    /// <summary>
    /// 실행 하면 해당 오브젝트의 맞는 기능들이 실현됨.
    /// </summary>
    public override void Use()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Player();
        }
    }
}
