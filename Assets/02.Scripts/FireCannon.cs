using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public GameObject cannon;
    public Transform firePos;
    private AudioClip fireSfx = null;
    private AudioSource sfx = null;
    public int ammoRemain = 30; // 남은 전체 탄약
    public int magAmmo; // 현재 탄창에 남아있는 탄약
    public int bigAmmoRemain; // 남은 전체 탄약
    private bool isAmmoEmpty;
    private float lastFireTime; // 총을 마지막으로 발사한 시점
    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    public AudioClip reloadingClip; // 아이템 습득 소리

    public GameObject[] enemyPrefabS; // 생성할 좀비 원본 프리팹
    public Transform[] spawnPoints; //
    private int wave; // 현재 웨이브
    public static int enemiesCount; // 현재 적 개수
    public static int killScore; // 현재 킬 횟수
    private bool nextStageFlag;

    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }
    public State state { get; private set; } // 현재 총의 상태
    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        cannon = (GameObject)Resources.Load("Cannon"); //포탄 프리팹 리소스폴더에서 불러오기
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // 사운드 파일 리소스 폴더에서 불러오기
        sfx = GetComponent<AudioSource>();
        isAmmoEmpty = false;
        enemiesCount = 0;
        wave = 0;
        killScore = 0;
        nextStageFlag = true;
    }

    private void OnEnable()
    {
        ammoRemain = 30;
        magAmmo = 10;
        bigAmmoRemain = 0;
        isAmmoEmpty = false;
        state = State.Ready;
        lastFireTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && state == State.Ready) //마우스 왼쪽 버튼을 누르면 포탄 발사
        {
            Fire();
        }

        if(Input.GetKeyDown(KeyCode.Space) && enemiesCount <= 0)
        {
            UIManager.instance.UpdateNextStage(false);
            SpawnWave();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) //메뉴로 이동
        {
            Time.timeScale = 0f;
            UIManager.instance.gameoverUI.SetActive(true);
        }

        if (Input.GetMouseButtonDown(1) && state == State.Ready && bigAmmoRemain>0) //마우스 오른쪽 버튼을 누르면 포탄 발사
        {
            BigFire();
        }

        if (Input.GetKeyDown(KeyCode.R) && state == State.Empty && !isAmmoEmpty) //R 버튼을 누르면 재장전
        {
            playerAudioPlayer.PlayOneShot(reloadingClip);
            state = State.Reloading;
            isAmmoEmpty = false;
            magAmmo = 10;
            ammoRemain -= magAmmo;
            state = State.Ready;
        }

        // 남은 탄약 UI를 갱신
        UpdateUI();
        UIManager.instance.UpdateWaveText(wave, enemiesCount);
        UIManager.instance.UpdateKillText(killScore);
    }

    private void SpawnWave()
    {
        // 웨이브 1 증가
        wave++;

        // 현재 웨이브 * 1.5에 반올림 한 개수 만큼 좀비를 생성
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);
        enemiesCount = spawnCount;

        // spawnCount 만큼 좀비를 생성
        for (int i = 0; i < spawnCount; i++)
        {
            // 좀비 생성 처리 실행
            CreateTank();
        }
    }
     void CreateTank()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = enemyPrefabS[Random.Range(0, enemyPrefabS.Length)];
        Instantiate(enemy, spawnPoint.position, Quaternion.identity); 
    }

    public void SetClearGame()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = enemyPrefabS[Random.Range(0, enemyPrefabS.Length)];
        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    }

    private void UpdateUI()
    {
        if (UIManager.instance != null)
        {
            // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
            UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);
            UIManager.instance.UpdateBigAmmoText(bigAmmoRemain);
        }
    }

    void Fire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + 1.0f)
        {
            // 마지막 총 발사 시점을 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 실행
            Shot();
        }
       
    }

    void BigFire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + 2.0f)
        {
            // 마지막 총 발사 시점을 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 실행
            MissileShot();
        }

    }

    void MissileShot()
    {
        sfx.PlayOneShot(fireSfx, 1.0f); //발사 사운드 실행
        Instantiate((GameObject)Resources.Load("BigCannon"), firePos.position, firePos.rotation); //포탄 생성
        bigAmmoRemain--;
    }

    void Shot()
    {
        sfx.PlayOneShot(fireSfx, 1.0f); //발사 사운드 실행
        Instantiate(cannon, firePos.position, firePos.rotation); //포탄 생성
        magAmmo--;
        if (magAmmo <= 0)
        {
            if(ammoRemain > 0)
            {
                state = State.Empty;
                StartCoroutine(ShowReload());
            } else
            {
                isAmmoEmpty = true;
                state = State.Empty;
                StartCoroutine(ShowGetBall());
            }
        }
    }

    IEnumerator ShowReload()
    {
        while (state == State.Empty)
        {
            UIManager.instance.reloadUI.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            UIManager.instance.reloadUI.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator ShowGetBall()
    {
        while (isAmmoEmpty)
        {
            UIManager.instance.getBallUI.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            UIManager.instance.getBallUI.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
