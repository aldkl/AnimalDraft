using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    Vector3 CameraAVec3;
    void Start()
    {

    }
    
    void LateUpdate()
    {
        if(GameMng.Ins.playCart.bDrift)
        {
            //transform.position = GameMng.Ins.playCart.transform.position + new Vector3(10, 5, 0);
            if(CameraAVec3.x == -1 && CameraAVec3.y == -1 && CameraAVec3.z == -1)
            {
                CameraAVec3 = transform.eulerAngles;
                Debug.Log("asd");
            }
            transform.eulerAngles = CameraAVec3;
        }
        else
        {
            //transform.position = GameMng.Ins.playCart.transform.position + new Vector3(10, 5, 0);
            if (CameraAVec3.x != -1 && CameraAVec3.y != -1 && CameraAVec3.z != -1)
            {
                CameraAVec3 = new Vector3(-1,-1,-1);
            }
        }
    }
}
