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

    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄약 표시용 텍스트
    public Text waveText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI 
    public GameObject reloadUI; // 
    public GameObject getBallUI; // 
    public Text bigAmmoText; // 탄약 표시용 텍스트
    public Text lifeText; // 생명 표시용 텍스트
    public GameObject NextStageText; // 다음 스테이지
    public GameObject gameclearUI; // 게임 성공 시
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
