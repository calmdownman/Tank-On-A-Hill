using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyFireCannon : MonoBehaviour
{
    public GameObject cannon;
    public Transform firePos;
    private AudioClip fireSfx = null;
    private AudioSource sfx = null;
    private float lastFireTime; // 총을 마지막으로 발사한 시점
    // Start is called before the first frame update
    void Start()
    {
        cannon = (GameObject)Resources.Load("Cannon"); //포탄 프리팹 리소스폴더에서 불러오기
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // 사운드 파일 리소스 폴더에서 불러오기
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼을 누르면 포탄 발사
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Time.time >= lastFireTime + 1.0f)
        {
            // 마지막 총 발사 시점을 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 실행
            Shot();
        }

    }

    void Shot()
    {
        sfx.PlayOneShot(fireSfx, 1.0f); //발사 사운드 실행
        Instantiate(cannon, firePos.position, firePos.rotation); //포탄 생성
    }
}
