using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blinkText;
    private bool flag = true;
    // Start is called before the first frame update
    void Awake()
    {
        blinkText.SetActive(false);
        StartCoroutine(ShowReady());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            flag = false;
            SceneManager.LoadScene(1);
        }
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    IEnumerator ShowReady()
    {
        while (flag)
        {
            blinkText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            blinkText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
