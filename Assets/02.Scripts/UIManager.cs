using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // �̱����� �Ҵ�� ����

    public Text ammoText; // ź�� ǥ�ÿ� �ؽ�Ʈ
    public Text waveText; // �� ���̺� ǥ�ÿ� �ؽ�Ʈ
    public GameObject gameoverUI; // ���� ������ Ȱ��ȭ�� UI 
    public GameObject reloadUI; // 
    public GameObject getBallUI; // 
    public Text bigAmmoText; // ź�� ǥ�ÿ� �ؽ�Ʈ
    public Text lifeText; // ���� ǥ�ÿ� �ؽ�Ʈ
    public GameObject NextStageText; // ���� ��������
    public GameObject gameclearUI; // ���� ���� ��
    public Text gameclearText;
    public Text killScore;
    
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    public void UpdateBigAmmoText(int magAmmo)
    {
        bigAmmoText.text = magAmmo.ToString();
    }

    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }

    public void UpdateLifeText(int _lifes)
    {
        lifeText.text = "Life : " + _lifes;
    }

    public void UpdateKillText(int _kills)
    {
        killScore.text = _kills.ToString();
    }

    public void UpdateNextStage(bool _flag)
    {
        NextStageText.SetActive(_flag);
    }

    public void SetClearGame(int _kills)
    {
        gameclearText.text = "Game Clear" + "\nKillScore: " + _kills;
        gameclearUI.SetActive(true);
        reloadUI.SetActive(false);
        getBallUI.SetActive(false);
        NextStageText.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }

    public void GameExit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);   
    }


}
