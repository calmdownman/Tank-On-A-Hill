using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 10; // 충전할 총알 수
    // Start is called before the first frame update
    public void Use(GameObject target)
    {
        // 전달 받은 게임 오브젝트로부터 PlayerShooter 컴포넌트를 가져오기 시도
        FireCannon fireCannon = target.GetComponent<FireCannon>();

        // PlayerShooter 컴포넌트가 있으며, 총 오브젝트가 존재하면
        if (fireCannon != null)
        {
            // 총의 남은 탄환 수를 ammo 만큼 더합니다.
            fireCannon.ammoRemain += ammo;
        }

        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}
