using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public GameObject cannon;
    public Transform firePos;
    private AudioClip fireSfx = null;
    private AudioSource sfx = null;
    public int ammoRemain = 30; // ���� ��ü ź��
    public int magAmmo; // ���� źâ�� �����ִ� ź��
    public int bigAmmoRemain; // ���� ��ü ź��
    private bool isAmmoEmpty;
    private float lastFireTime; // ���� ���������� �߻��� ����
    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����
    public AudioClip reloadingClip; // ������ ���� �Ҹ�

    public GameObject[] enemyPrefabS; // ������ ���� ���� ������
    public Transform[] spawnPoints; //
    private int wave; // ���� ���̺�
    public static int enemiesCount; // ���� �� ����
    public static int killScore; // ���� ų Ƚ��
    private bool nextStageFlag;

    public enum State
    {
        Ready, // �߻� �غ��
        Empty, // źâ�� ��
        Reloading // ������ ��
    }
    public State state { get; private set; } // ���� ���� ����
    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        cannon = (GameObject)Resources.Load("Cannon"); //��ź ������ ���ҽ��������� �ҷ�����
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // ���� ���� ���ҽ� �������� �ҷ�����
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
        if(Input.GetMouseButtonDown(0) && state == State.Ready) //���콺 ���� ��ư�� ������ ��ź �߻�
        {
            Fire();
        }

        if(Input.GetKeyDown(KeyCode.Space) && enemiesCount <= 0)
        {
            UIManager.instance.UpdateNextStage(false);
            SpawnWave();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) //�޴��� �̵�
        {
            Time.timeScale = 0f;
            UIManager.instance.gameoverUI.SetActive(true);
        }

        if (Input.GetMouseButtonDown(1) && state == State.Ready && bigAmmoRemain>0) //���콺 ������ ��ư�� ������ ��ź �߻�
        {
            BigFire();
        }

        if (Input.GetKeyDown(KeyCode.R) && state == State.Empty && !isAmmoEmpty) //R ��ư�� ������ ������
        {
            playerAudioPlayer.PlayOneShot(reloadingClip);
            state = State.Reloading;
            isAmmoEmpty = false;
            magAmmo = 10;
            ammoRemain -= magAmmo;
            state = State.Ready;
        }

        // ���� ź�� UI�� ����
        UpdateUI();
        UIManager.instance.UpdateWaveText(wave, enemiesCount);
        UIManager.instance.UpdateKillText(killScore);
    }

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
            // UI �Ŵ����� ź�� �ؽ�Ʈ�� źâ�� ź��� ���� ��ü ź���� ǥ��
            UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);
            UIManager.instance.UpdateBigAmmoText(bigAmmoRemain);
        }
    }

    void Fire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + 1.0f)
        {
            // ������ �� �߻� ������ ����
            lastFireTime = Time.time;
            // ���� �߻� ó�� ����
            Shot();
        }
       
    }

    void BigFire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + 2.0f)
        {
            // ������ �� �߻� ������ ����
            lastFireTime = Time.time;
            // ���� �߻� ó�� ����
            MissileShot();
        }

    }

    void MissileShot()
    {
        sfx.PlayOneShot(fireSfx, 1.0f); //�߻� ���� ����
        Instantiate((GameObject)Resources.Load("BigCannon"), firePos.position, firePos.rotation); //��ź ����
        bigAmmoRemain--;
    }

    void Shot()
    {
        sfx.PlayOneShot(fireSfx, 1.0f); //�߻� ���� ����
        Instantiate(cannon, firePos.position, firePos.rotation); //��ź ����
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
