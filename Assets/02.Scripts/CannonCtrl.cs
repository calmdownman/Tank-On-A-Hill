using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCtrl : MonoBehaviour
{
    private Transform tr;
    public float rotSpeed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        float angleX = tr.localEulerAngles.x;
        //마우스 스크롤 휠을 이용하여 포신의 각도를 조절
        
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && (angleX > 335f || angleX < 16f) ) {
 
            tr.Rotate(-0.1f * Time.deltaTime * rotSpeed, 0, 0);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && (angleX > 334f || angleX < 15f))
        {

            tr.Rotate(0.1f * Time.deltaTime * rotSpeed, 0, 0);
        }


    }
}
