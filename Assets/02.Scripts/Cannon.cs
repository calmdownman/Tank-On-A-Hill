using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float speed = 6000.0f;
    public GameObject expEffect; //���� ȿ�� ������ ���� ����
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        //��ź�� ���� �������� 6000�� ����ŭ ����
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        //3�ʰ� ���� �� �ڵ����� �����ϴ� �ڷ�ƾ ���� (��ź�� ���鿡 �µ� �ȸµ�)
        StartCoroutine(ExplosionCannon(3.0f));
    }
    private void OnTriggerEnter(Collider other)
    {
        //���� �Ǵ� �� ��ũ�� �浹�� ��� ��� �����ϵ��� �ڷ�ƾ ����
        StartCoroutine(ExplosionCannon(0.0f));
    }
    IEnumerator ExplosionCannon(float tm)
    {
        yield return new WaitForSeconds(tm); //ȣ��Ǿ��� �� ������ �ð���ŭ ��� ��
        _collider.enabled = false; //�ݶ��̴� ��Ȱ��ȭ
        _rigidbody.isKinematic = true; //���������� ������ ���� ����
        //���� ȿ�� ������Ʈ ����
        GameObject obj = (GameObject)Instantiate(expEffect, transform.position,
            Quaternion.identity);
        Destroy(obj, 1.0f); //����ȿ�� ����� 1�ʰ� ��ٸ� �� �ı�
        Destroy(this.gameObject, 1.0f); // Trail Renderer�� �Ҹ�ɶ����� ��� �� �ı�
    }
}
