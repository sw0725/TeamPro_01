using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RifleParts : PartsBase
{
    public override void Use()
    {
        Rifle rifle = GetComponentInParent<Rifle>();

        if (rifle == null)
        {
            Debug.LogWarning("Rifle component not found in parent objects.");
            return;
        }

        Transform rifleTransform = rifle.transform;
        Transform partsTransform = transform;

        partsTransform.SetParent(rifleTransform);

        partsTransform.localPosition = Vector3.zero;
        partsTransform.localRotation = Quaternion.identity;

        // ���� ���� �ڵ�
        //// ������ ��ǰ�� ���� �������� �Ӽ��� �����մϴ�.
        //if (GetComponent<>() != null)
        //{
        //    // ���ذ� ���� �� �ִ� ź�� ���� ������ŵ�ϴ�.
        //    rifle.maxAmmo += rifle.maxAmmoIncreaseAmount;
        //}
        //else if (GetComponent<>() != null)
        //{
        //    // ź�� ���� �� �ݵ��� ������ŵ�ϴ�.
        //    rifle.recoil += rifle.recoilIncreaseAmount;
        //}
        //else if (GetComponent<Stock>() != null)
        //{
        //    // ���Ӹ��� ���� �� ���߷��� ������ŵ�ϴ�.
        //    rifle.accuracy += rifle.accuracyIncreaseAmount;
        //}

    }
}
