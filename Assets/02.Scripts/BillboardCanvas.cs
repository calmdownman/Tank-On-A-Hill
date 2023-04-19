using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    private Transform tr; // 탱크HUD의 트랜스폼
    private Transform mainCameraTr; //카메라의 트랜스폼
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        mainCameraTr = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //지속적으로 탱크 HUD 캔버스를 메인카메라 방향으로 바라볼수 있게..
        tr.LookAt(mainCameraTr);
    }
}
