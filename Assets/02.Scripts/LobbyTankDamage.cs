using System.Collections;
using UnityEngine;

public class LobbyTankDamage : MonoBehaviour
{

    private MeshRenderer[] renderers; // 탱크의 투명처리를 위한 렌더러 오브젝트
    private GameObject expEffect = null; // 탱크가 죽었을 때 폭발 프리팹
    private int initHp = 100; //최초 체력
    private int currHp = 0; // 현재 체력
    bool isDamage;
   

    // Start is called before the first frame update
    void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        currHp = initHp; //현재 체력을 초기화
        expEffect = Resources.Load<GameObject>("Exploson2"); //폭발 프리팹 로드
    }

    private void OnTriggerEnter(Collider other)
    {
        //안죽은 상태에서 포탄과 부딪혔다면
        if (currHp > 0 && other.tag == "CANNON")
        {
            currHp -= 20;//체력을 20감소
            //현재 생명치 백분율 = (현재 체력) / (최대 체력)
            if (currHp <= 0) //죽었다면
            {
                   StartCoroutine(ExplosionTank()); //코루틴 함수 실행  
            }
        }
    }

    IEnumerator ExplosionTank()//폭발효과 생성 및 리스폰 코루틴 함수
    {
        GameObject effect = GameObject.Instantiate(expEffect, transform.position,
            Quaternion.identity); //폭발효과 프리팹 생성
        Destroy(effect, 3.0f); // 3초 뒤 파괴

        SetTankVisible(false); // 탱크 투명화
        yield return new WaitForSeconds(3.0f); //3초 뒤
        currHp = initHp; // 체력을 다시 100으로
        SetTankVisible(true); //탱크를 다시 보이게
    }

    void SetTankVisible(bool isVisible) //탱크의 메쉬렌더러를 활성/비활성화
    {
        foreach (MeshRenderer _renderer in renderers)
        {
            _renderer.enabled = isVisible;
        }
    }
}
