using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabS; // 생성할 좀비 원본 프리팹
    // Start is called before the first frame update
    
    public Transform[] spawnPoints; //
    private int wave; // 현재 웨이브
    public static int enemiesCount; // 현재 적 개수
    // Update is called once per frame

    void Awake()
    {
        enemiesCount = 0;
        wave = 0;
    }

    void Update()
    {
       if (UIManager.instance != null)
        {
            return;
        }

        // 좀비를 모두 물리친 경우 다음 스폰 실행
        if (enemiesCount <= 0)
        {
            SpawnWave();
        }

        // UI 갱신
        UpdateUI();
    }
    private void UpdateUI()
    {
        // 현재 웨이브와 남은 적 수 표시
        UIManager.instance.UpdateWaveText(wave, enemiesCount);
    }

    // 현재 웨이브에 맞춰 좀비들을 생성
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
        Instantiate(enemy, spawnPoint.position,Quaternion.identity);
    }
}
