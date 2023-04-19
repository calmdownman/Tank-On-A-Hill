using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankDamage : MonoBehaviour
{

    private MeshRenderer[] renderers; // ��ũ�� ����ó���� ���� ������ ������Ʈ
    private GameObject expEffect = null; // ��ũ�� �׾��� �� ���� ������
    private int initHp = 100; //���� ü��
    public int currHp = 0; // ���� ü��
    private int initLife = 3; // ���� ü��
    private int currLife = 0; // ���� ü��
    public Canvas hudCanvas; // ��ũ�� HUD (Ȱ��ȭ, ��Ȱ��ȭ�� ����..)
    public Image hpBar;//��ũ ü�¿� ���� Hpbar �������� �޶���
    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����
    public AudioClip itemPickupClip; // ������ ���� �Ҹ�


    // Start is called before the first frame update
    void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        currHp = initHp; //���� ü���� �ʱ�ȭ
        currLife = initLife; //���� ���� �ʱ�ȭ
        expEffect = Resources.Load<GameObject>("Exploson10"); //���� ������ �ε�
        hpBar.color = Color.green; //�ʱ� ü�¹ٴ� �������..
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� �������� ���� Item ������Ʈ�� �������� �õ�
        IItem item = other.GetComponent<IItem>();


        // �浹�� �������κ��� Item ������Ʈ�� �������µ� �����ߴٸ�
        if (item != null)
        {
            // Use �޼��带 �����Ͽ� ������ ���
            item.Use(gameObject);

            hpBar.fillAmount = (float)currHp / (float)initHp;
            if (hpBar.fillAmount >= 0.6f) 
                hpBar.color = Color.green;
            else if (hpBar.fillAmount >= 0.4f) 
                hpBar.color = Color.yellow;

            // ������ ���� �Ҹ� ���
            playerAudioPlayer.PlayOneShot(itemPickupClip);
        }

        //������ ���¿��� ��ź�� �ε����ٸ�
        if (currHp > 0 && other.tag == "CANNON")
        {
            currHp -= 20;//ü���� 20����

            ChangeHPBar();

            if (currHp <= 0) //�׾��ٸ�
            {
                DeclineLife();

            }
        }

        if (currHp > 0 && other.tag == "BIGCANNON")
        {
            currHp -= 50;//ü���� 50����

            ChangeHPBar();
            //���� ����ġ ����� = (���� ü��) / (�ִ� ü��)
            if (currHp <= 0) //�׾��ٸ�
            {
                DeclineLife();

            }
        }

        if (currHp > 0 && other.tag == "ENEMY2")
        {
            currHp -= 50;//ü���� 50����

            ChangeHPBar();
            //���� ����ġ ����� = (���� ü��) / (�ִ� ü��)
            if (currHp <= 0) //�׾��ٸ�
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
            StartCoroutine(ExplosionTank()); //�ڷ�ƾ �Լ� ����
        }
        else
        {
            Time.timeScale = 0f;
            UIManager.instance.gameoverUI.SetActive(true);
        }
    }

    public void ChangeHPBar()
    {
        //���� ����ġ ����� = (���� ü��) / (�ִ� ü��)
        hpBar.fillAmount = (float)currHp / (float)initHp;
        if (hpBar.fillAmount <= 0.4f) //40% ���ϴ� ������
            hpBar.color = Color.red;
        else if (hpBar.fillAmount <= 0.6f) // 60% ���ϴ� �����
            hpBar.color = Color.yellow;
    }

    IEnumerator ExplosionTank()//����ȿ�� ���� �� ������ �ڷ�ƾ �Լ�
    {
        GameObject effect = GameObject.Instantiate(expEffect, transform.position,
            Quaternion.identity); //����ȿ�� ������ ����
        Destroy(effect, 3.0f); // 3�� �� �ı�

        hudCanvas.enabled = false; // ��ũ HUD ��Ȱ��ȭ
        SetTankVisible(false); // ��ũ ����ȭ
        yield return new WaitForSeconds(3.0f); //3��
        hpBar.fillAmount = 1.0f;  // hpBar.fillAmount = 1.0f; //ü�¹ٸ� 100%�� �ٽ� ä��
        hpBar.color = Color.green; //���� �ٽ� �������..
        hudCanvas.enabled = true;//  ��ũ HUD Ȱ��ȭ
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
