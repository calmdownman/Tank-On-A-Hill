using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabS; // ������ ���� ���� ������
    // Start is called before the first frame update
    
    public Transform[] spawnPoints; //
    private int wave; // ���� ���̺�
    public static int enemiesCount; // ���� �� ����
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

        // ���� ��� ����ģ ��� ���� ���� ����
        if (enemiesCount <= 0)
        {
            SpawnWave();
        }

        // UI ����
        UpdateUI();
    }
    private void UpdateUI()
    {
        // ���� ���̺�� ���� �� �� ǥ��
        UIManager.instance.UpdateWaveText(wave, enemiesCount);
    }

    // ���� ���̺꿡 ���� ������� ����
    private void SpawnWave()
    {
        // ���̺� 1 ����
        wave++;

        // ���� ���̺� * 1.5�� �ݿø� �� ���� ��ŭ ���� ����
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);
        enemiesCount = spawnCount;

        // spawnCount ��ŭ ���� ����
        for (int i = 0; i < spawnCount; i++)
        {
            // ���� ���� ó�� ����
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
