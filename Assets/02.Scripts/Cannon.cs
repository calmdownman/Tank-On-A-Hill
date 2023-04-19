using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float speed = 6000.0f;
    public GameObject expEffect; //폭발 효과 프리팹 연결 변수
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        //포탄이 앞쪽 방향으로 6000의 힘만큼 날라감
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        //3초가 지난 후 자동으로 폭발하는 코루틴 실행 (포탄이 지면에 맞든 안맞든)
        StartCoroutine(ExplosionCannon(3.0f));
    }
    private void OnTriggerEnter(Collider other)
    {
        //지면 또는 적 탱크에 충돌한 경우 즉시 폭발하도록 코루틴 실행
        StartCoroutine(ExplosionCannon(0.0f));
    }
    IEnumerator ExplosionCannon(float tm)
    {
        yield return new WaitForSeconds(tm); //호출되었을 때 지정된 시간만큼 대기 후
        _collider.enabled = false; //콜라이더 비활성화
        _rigidbody.isKinematic = true; //물리엔진의 영향을 받지 않음
        //폭발 효과 오브젝트 생성
        GameObject obj = (GameObject)Instantiate(expEffect, transform.position,
            Quaternion.identity);
        Destroy(obj, 1.0f); //폭발효과 재생을 1초간 기다린 뒤 파괴
        Destroy(this.gameObject, 1.0f); // Trail Renderer가 소멸될때까지 대기 후 파괴
    }
}
