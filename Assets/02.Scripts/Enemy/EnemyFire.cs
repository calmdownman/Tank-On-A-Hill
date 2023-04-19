using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject cannon;
    public Transform firePos;
    private AudioClip fireSfx = null;
    private AudioSource sfx = null;
    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    private float lastFireTime; // 총을 마지막으로 발사한 시점
    // Start is called before the first frame update
    void Awake()
    {
        lastFireTime = 0;
        playerAudioPlayer = GetComponent<AudioSource>();
        cannon = (GameObject)Resources.Load("Cannon"); //포탄 프리팹 리소스폴더에서 불러오기
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // 사운드 파일 리소스 폴더에서 불러오
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastFireTime + 4.0f)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    void Shot()
    {
        sfx.PlayOneShot(fireSfx, 1.0f); //발사 사운드 실행
        Instantiate(cannon, firePos.position, firePos.rotation); //포탄 생성

    }

}
