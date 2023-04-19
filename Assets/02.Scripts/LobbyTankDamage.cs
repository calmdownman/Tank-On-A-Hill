using System.Collections;
using UnityEngine;

public class LobbyTankDamage : MonoBehaviour
{

    private MeshRenderer[] renderers; // ��ũ�� ����ó���� ���� ������ ������Ʈ
    private GameObject expEffect = null; // ��ũ�� �׾��� �� ���� ������
    private int initHp = 100; //���� ü��
    private int currHp = 0; // ���� ü��
    bool isDamage;
   

    // Start is called before the first frame update
    void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        currHp = initHp; //���� ü���� �ʱ�ȭ
        expEffect = Resources.Load<GameObject>("Exploson2"); //���� ������ �ε�
    }

    private void OnTriggerEnter(Collider other)
    {
        //������ ���¿��� ��ź�� �ε����ٸ�
        if (currHp > 0 && other.tag == "CANNON")
        {
            currHp -= 20;//ü���� 20����
            //���� ����ġ ����� = (���� ü��) / (�ִ� ü��)
            if (currHp <= 0) //�׾��ٸ�
            {
                   StartCoroutine(ExplosionTank()); //�ڷ�ƾ �Լ� ����  
            }
        }
    }

    IEnumerator ExplosionTank()//����ȿ�� ���� �� ������ �ڷ�ƾ �Լ�
    {
        GameObject effect = GameObject.Instantiate(expEffect, transform.position,
            Quaternion.identity); //����ȿ�� ������ ����
        Destroy(effect, 3.0f); // 3�� �� �ı�

        SetTankVisible(false); // ��ũ ����ȭ
        yield return new WaitForSeconds(3.0f); //3�� ��
        currHp = initHp; // ü���� �ٽ� 100����
        SetTankVisible(true); //��ũ�� �ٽ� ���̰�
    }

    void SetTankVisible(bool isVisible) //��ũ�� �޽��������� Ȱ��/��Ȱ��ȭ
    {
        foreach (MeshRenderer _renderer in renderers)
        {
            _renderer.enabled = isVisible;
        }
    }
}
