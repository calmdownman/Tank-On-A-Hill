using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject cannon;
    public Transform firePos;
    private AudioClip fireSfx = null;
    private AudioSource sfx = null;
    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����
    private float lastFireTime; // ���� ���������� �߻��� ����
    // Start is called before the first frame update
    void Awake()
    {
        lastFireTime = 0;
        playerAudioPlayer = GetComponent<AudioSource>();
        cannon = (GameObject)Resources.Load("Cannon"); //��ź ������ ���ҽ��������� �ҷ�����
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // ���� ���� ���ҽ� �������� �ҷ���
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
        sfx.PlayOneShot(fireSfx, 1.0f); //�߻� ���� ����
        Instantiate(cannon, firePos.position, firePos.rotation); //��ź ����

    }

}
