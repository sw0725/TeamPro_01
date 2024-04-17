using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : ItemBase
{
    [Tooltip("�����ݰ�")]
    public float NoiseRange = 5.0f;
    public GameObject expoltionEffect;

    Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    protected virtual void Explode()
    {
        Factory.Instance.GetNoise(NoiseRange, transform);
    }

    public override void Use()
    {
        PlayerFire playerfire = GetComponentInParent<PlayerFire>();         //������ ����Ҷ��� ������ �ڽ����� �� ������
        Player player = GameManager.Instance.Player;
        Transform cam = player.transform.GetChild(0);

        transform.position = playerfire.firePosition.transform.position;
        rb.AddForce(cam.forward * playerfire.throwPower, ForceMode.Impulse);
    }
}