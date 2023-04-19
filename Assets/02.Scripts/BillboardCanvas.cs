using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    private Transform tr; // ��ũHUD�� Ʈ������
    private Transform mainCameraTr; //ī�޶��� Ʈ������
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        mainCameraTr = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //���������� ��ũ HUD ĵ������ ����ī�޶� �������� �ٶ󺼼� �ְ�..
        tr.LookAt(mainCameraTr);
    }
}
