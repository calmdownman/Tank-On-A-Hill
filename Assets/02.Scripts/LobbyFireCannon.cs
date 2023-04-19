using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyFireCannon : MonoBehaviour
{
    public GameObject cannon;
    public Transform firePos;
    private AudioClip fireSfx = null;
    private AudioSource sfx = null;
    private float lastFireTime; // ���� ���������� �߻��� ����
    // Start is called before the first frame update
    void Start()
    {
        cannon = (GameObject)Resources.Load("Cannon"); //��ź ������ ���ҽ��������� �ҷ�����
        fireSfx = Resources.Load<AudioClip>("CannonFire"); // ���� ���� ���ҽ� �������� �ҷ�����
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��ư�� ������ ��ź �߻�
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Time.time >= lastFireTime + 1.0f)
        {
            // ������ �� �߻� ������ ����
            lastFireTime = Time.time;
            // ���� �߻� ó�� ����
            Shot();
        }

    }

    void Shot()
    {
        sfx.PlayOneShot(fireSfx, 1.0f); //�߻� ���� ����
        Instantiate(cannon, firePos.position, firePos.rotation); //��ź ����
    }
}
