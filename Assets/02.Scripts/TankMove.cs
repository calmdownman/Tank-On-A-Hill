using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float moveSpeed = 20.0f; //�̵� �ӵ�
    public float rotSpeed = 50.0f; // ȸ�� �ӵ�
    // ������ ������Ʈ ������
    private Rigidbody rbody; 
    private Transform tr;
    //Ű���� �Է� �� ����
    private float h, v;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        //��ũ�� ������ٵ� �����߽��� ���� ����
        rbody.centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);   
    }
    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal"); // �¿� ����Ű (ȸ��)
        v = Input.GetAxis("Vertical"); // ���Ʒ� ����Ű(�̵�)

        //ȸ���� �̵�ó��
        tr.Rotate(Vector3.up * rotSpeed * h * Time.deltaTime);
        tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
    }
}
