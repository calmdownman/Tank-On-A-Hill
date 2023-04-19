using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTankDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject expEffect = null; // 탱크가 죽었을 때 폭발 프리팹
    private int initHp = 100; //최초 체력
    private int currHp = 0; // 현재 체력
    public Canvas hudCanvas; // 탱크의 HUD (활성화, 비활성화를 위한..)
    public Image hpBar;//탱크 체력에 따라 Hpbar 게이지가 달라짐
    void Awake()
    {
        currHp = initHp; //현재 체력을 초기
        expEffect = Resources.Load<GameObject>("Exploson10"); //폭발 프리팹 로드
        hpBar.color = Color.green; //초기 체력바는 녹색으로..
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (gameObject.tag == "ENEMY2" && other.tag == "TANK")
        {
            GameObject effect = GameObject.Instantiate(expEffect, transform.position,
          Quaternion.identity); //폭발효과 프리팹 생성
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
                currHp -= 50;//체력을 50감소
            }
            else { currHp -= 25; }//체력을 25감소

            ChangeHPBar();

            if (currHp <= 0) //죽었다면
            {
                GameObject effect = GameObject.Instantiate(expEffect, transform.position,
          Quaternion.identity); //폭발효과 프리팹 생성
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
                currHp -= 100;//체력을 50감소
            }
            else { currHp -= 50; }//체력을 25감소

            ChangeHPBar();
            //현재 생명치 백분율 = (현재 체력) / (최대 체력)
            if (currHp <= 0) //죽었다면
            {
                GameObject effect = GameObject.Instantiate(expEffect, transform.position,
          Quaternion.identity); //폭발효과 프리팹 생성
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
        //현재 생명치 백분율 = (현재 체력) / (최대 체력)
        hpBar.fillAmount = (float)currHp / (float)initHp;
        if (hpBar.fillAmount <= 0.4f) //40% 이하는 빨간색
            hpBar.color = Color.red;
        else if (hpBar.fillAmount <= 0.6f) // 60% 이하는 노란색
            hpBar.color = Color.yellow;
    }
}
