using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAmmoPack : MonoBehaviour,IItem
{
    public int ammo = 3; // ������ �Ѿ� ��
    // Start is called before the first frame update
    public void Use(GameObject target)
    {
        // ���� ���� ���� ������Ʈ�κ��� PlayerShooter ������Ʈ�� �������� �õ�
        FireCannon fireCannon = target.GetComponent<FireCannon>();

        // PlayerShooter ������Ʈ�� ������, �� ������Ʈ�� �����ϸ�
        if (fireCannon != null)
        {
            // ���� ���� źȯ ���� ammo ��ŭ ���մϴ�.
            fireCannon.bigAmmoRemain +=ammo;
        }

        // ���Ǿ����Ƿ�, �ڽ��� �ı�
        Destroy(gameObject);
    }
}
