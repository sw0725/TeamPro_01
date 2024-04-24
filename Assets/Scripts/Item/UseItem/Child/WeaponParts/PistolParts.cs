using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolParts : PartsBase
{
    public override void Use()
    {
        // �ѱ� ������ �θ� ���� ������Ʈ���� �ѱ� ������Ʈ�� ã���ϴ�.
        Pistol pistol = GetComponentInParent<Pistol>();

        // �ѱ⸦ ã�� ���� ��쿡 ���� ���� ó��
        if (pistol == null)
        {
            Debug.LogWarning("Pistol component not found in parent objects.");
            return;
        }

        // �ѱ� ������ �ѱ��� �ڽ����� �����մϴ�.
        Transform pistolTransform = pistol.transform;
        Transform partsTransform = transform;

        // �ѱ� ������ �θ� �ѱ�� �����Ͽ� �ѱ� ������ �ѱ��� �ڽ����� ����ϴ�.
        partsTransform.SetParent(pistolTransform);
        // �ѱ� ������ ���� �����ǰ� ���� ȸ���� �ʱ�ȭ�Ͽ� �ѱ��� ��ġ�� ȸ���� ��������� �����մϴ�.
        partsTransform.localPosition = Vector3.zero;
        partsTransform.localRotation = Quaternion.identity;

        pistol.noiseVelocity += 3f;
    }
}
