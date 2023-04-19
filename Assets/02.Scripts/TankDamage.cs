using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankDamage : MonoBehaviour
{

    private MeshRenderer[] renderers; // 탱크의 투명처리를 위한 렌더러 오브젝트
    private GameObject expEffect = null; // 탱크가 죽었을 때 폭발 프리팹
    private int initHp = 100; //최초 체력
    public int currHp = 0; // 현재 체력
    private int initLife = 3; // 현재 체력
    private int currLife = 0; // 현재 체력
    public Canvas hudCanvas; // 탱크의 HUD (활성화, 비활성화를 위한..)
    public Image hpBar;//탱크 체력에 따라 Hpbar 게이지가 달라짐
    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    public AudioClip itemPickupClip; // 아이템 습득 소리


    // Start is called before the first frame update
    void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        currHp = initHp; //현재 체력을 초기화
        currLife = initLife; //현재 생명 초기화
        expEffect = Resources.Load<GameObject>("Exploson10"); //폭발 프리팹 로드
        hpBar.color = Color.green; //초기 체력바는 녹색으로..
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 상대방으로 부터 Item 컴포넌트를 가져오기 시도
        IItem item = other.GetComponent<IItem>();


        // 충돌한 상대방으로부터 Item 컴포넌트가 가져오는데 성공했다면
        if (item != null)
        {
            // Use 메서드를 실행하여 아이템 사용
            item.Use(gameObject);

            hpBar.fillAmount = (float)currHp / (float)initHp;
            if (hpBar.fillAmount >= 0.6f) 
                hpBar.color = Color.green;
            else if (hpBar.fillAmount >= 0.4f) 
                hpBar.color = Color.yellow;

            // 아이템 습득 소리 재생
            playerAudioPlayer.PlayOneShot(itemPickupClip);
        }

        //안죽은 상태에서 포탄과 부딪혔다면
        if (currHp > 0 && other.tag == "CANNON")
        {
            currHp -= 20;//체력을 20감소

            ChangeHPBar();

            if (currHp <= 0) //죽었다면
            {
                DeclineLife();

            }
        }

        if (currHp > 0 && other.tag == "BIGCANNON")
        {
            currHp -= 50;//체력을 50감소

            ChangeHPBar();
            //현재 생명치 백분율 = (현재 체력) / (최대 체력)
            if (currHp <= 0) //죽었다면
            {
                DeclineLife();

            }
        }

        if (currHp > 0 && other.tag == "ENEMY2")
        {
            currHp -= 50;//체력을 50감소

            ChangeHPBar();
            //현재 생명치 백분율 = (현재 체력) / (최대 체력)
            if (currHp <= 0) //죽었다면
            {
                DeclineLife();
            }
        }
    }

    void DeclineLife()
    {
        currLife--;
        UIManager.instance.UpdateLifeText(currLife);
        if (currLife >= 1)
        {
            StartCoroutine(ExplosionTank()); //코루틴 함수 실행
        }
        else
        {
            Time.timeScale = 0f;
            UIManager.instance.gameoverUI.SetActive(true);
        }
    }

    public void ChangeHPBar()
    {
        //현재 생명치 백분율 = (현재 체력) / (최대 체력)
        hpBar.fillAmount = (float)currHp / (float)initHp;
        if (hpBar.fillAmount <= 0.4f) //40% 이하는 빨간색
            hpBar.color = Color.red;
        else if (hpBar.fillAmount <= 0.6f) // 60% 이하는 노란색
            hpBar.color = Color.yellow;
    }

    IEnumerator ExplosionTank()//폭발효과 생성 및 리스폰 코루틴 함수
    {
        GameObject effect = GameObject.Instantiate(expEffect, transform.position,
            Quaternion.identity); //폭발효과 프리팹 생성
        Destroy(effect, 3.0f); // 3초 뒤 파괴

        hudCanvas.enabled = false; // 탱크 HUD 비활성화
        SetTankVisible(false); // 탱크 투명화
        yield return new WaitForSeconds(3.0f); //3초
        hpBar.fillAmount = 1.0f;  // hpBar.fillAmount = 1.0f; //체력바를 100%로 다시 채움
        hpBar.color = Color.green; //색을 다시 녹색으로..
        hudCanvas.enabled = true;//  탱크 HUD 활성화
        currHp = initHp; // 체력을 다시 100으로
        SetTankVisible(true); //탱크를 다시 보이게
    }

    void SetTankVisible(bool isVisible) //탱크의 메쉬렌더러를 활성/비활성화
    {
        foreach (MeshRenderer _renderer in renderers)
        {
            _renderer.enabled = isVisible;
        }
    }
}
