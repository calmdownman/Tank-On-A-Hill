using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTankDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject expEffect = null; // ��ũ�� �׾��� �� ���� ������
    private int initHp = 100; //���� ü��
    private int currHp = 0; // ���� ü��
    public Canvas hudCanvas; // ��ũ�� HUD (Ȱ��ȭ, ��Ȱ��ȭ�� ����..)
    public Image hpBar;//��ũ ü�¿� ���� Hpbar �������� �޶���
    void Awake()
    {
        currHp = initHp; //���� ü���� �ʱ�
        expEffect = Resources.Load<GameObject>("Exploson10"); //���� ������ �ε�
        hpBar.color = Color.green; //�ʱ� ü�¹ٴ� �������..
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (gameObject.tag == "ENEMY2" && other.tag == "TANK")
        {
            GameObject effect = GameObject.Instantiate(expEffect, transform.position,
          Quaternion.identity); //����ȿ�� ������ ����
            FireCannon.killScore++;
            FireCannon.enemiesCount--;
            if (FireCannon.enemiesCount <= 0)
            {
                if (FireCannon.killScore >= 9)
                {
                    UIManager.instance.SetClearGame(FireCannon.killScore);
                }
                else
                {
                    UIManager.instance.UpdateNextStage(true);
                }

            }

            Destroy(gameObject);
        }


        if (currHp > 0 && other.tag == "CANNON")
        {
            if (gameObject.tag == "ENEMY2")
            {
                currHp -= 50;//ü���� 50����
            }
            else { currHp -= 25; }//ü���� 25����

            ChangeHPBar();

            if (currHp <= 0) //�׾��ٸ�
            {
                GameObject effect = GameObject.Instantiate(expEffect, transform.position,
          Quaternion.identity); //����ȿ�� ������ ����
                FireCannon.killScore++;
                FireCannon.enemiesCount--;
                if(FireCannon.enemiesCount<=0)
                {
                    if(FireCannon.killScore >= 9)
                    {
                        UIManager.instance.SetClearGame(FireCannon.killScore);
                    } else
                    {
                        UIManager.instance.UpdateNextStage(true);
                    }
                  
                }

                Destroy(gameObject);
            }
        }

        if (currHp > 0 && other.tag == "BIGCANNON")
        {
            if (gameObject.tag == "ENEMY2")
            {
                currHp -= 100;//ü���� 50����
            }
            else { currHp -= 50; }//ü���� 25����

            ChangeHPBar();
            //���� ����ġ ����� = (���� ü��) / (�ִ� ü��)
            if (currHp <= 0) //�׾��ٸ�
            {
                GameObject effect = GameObject.Instantiate(expEffect, transform.position,
          Quaternion.identity); //����ȿ�� ������ ����
                FireCannon.killScore++;
                FireCannon.enemiesCount--;
                if (FireCannon.enemiesCount <= 0)
                {
                    if (FireCannon.killScore >= 9)
                    {
                        UIManager.instance.SetClearGame(FireCannon.killScore);
                    } else
                    {
                       UIManager.instance.UpdateNextStage(true);
                    }
                 
                }
                Destroy(gameObject);
            }
        }
    }

    void ChangeHPBar()
    {
        //���� ����ġ ����� = (���� ü��) / (�ִ� ü��)
        hpBar.fillAmount = (float)currHp / (float)initHp;
        if (hpBar.fillAmount <= 0.4f) //40% ���ϴ� ������
            hpBar.color = Color.red;
        else if (hpBar.fillAmount <= 0.6f) // 60% ���ϴ� �����
            hpBar.color = Color.yellow;
    }
}
