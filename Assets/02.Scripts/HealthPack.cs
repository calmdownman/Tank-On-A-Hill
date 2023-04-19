using UnityEngine;

// 체력을 회복하는 아이템
public class HealthPack : MonoBehaviour, IItem {
    public int health = 40; // 체력을 회복할 수치

    public void Use(GameObject target) {
        // 전달받은 게임 오브젝트로부터 LivingEntity 컴포넌트 가져오기 시도
        TankDamage life = target.GetComponent<TankDamage>();
        if (life != null)
        {
            // 체력 회복 실행
            life.currHp += health;
            if (life.currHp > 100)
            {
                life.currHp = 100;
            }
        }
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}