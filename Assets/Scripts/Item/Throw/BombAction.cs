using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect);
        eff.transform.position = transform.position;    // bombEffect�� ������Ʈ�� ��ǥ�� �����ϰ� �����. 
        Destroy(gameObject);    // �ٸ� �ݶ��̴��� ������ ��ź ����
    }
}
