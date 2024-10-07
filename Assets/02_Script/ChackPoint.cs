using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChackPoint : MonoBehaviour
{
    // Start is called before the first frame update
    Transform mytransform;
    public int LocalNum;
    public bool StartLine;
    void Start()
    {
        LocalNum = 0;
        for (int i = 100; i < 151; i++)
        {
            if (gameObject.name == "MapChackPoint" + i.ToString())
            {
                LocalNum = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("asdsadasdasdasd");
        if (other.gameObject.CompareTag("Car"))
        {
            if (StartLine)
            {
                if (other.gameObject.GetComponent<PlayCart>().CurLocalNum == GameMng.Ins.EndLocalNum)
                    other.GetComponent<PlayCart>().GoalInCount++;
                other.gameObject.GetComponent<PlayCart>().PerLocalNum = 0;
                other.gameObject.GetComponent<PlayCart>().CurLocalNum = LocalNum;
            }
            else
            {
                other.gameObject.GetComponent<PlayCart>().PerLocalNum = other.gameObject.GetComponent<PlayCart>().CurLocalNum;
                other.gameObject.GetComponent<PlayCart>().CurLocalNum = LocalNum;
            }
        }
        if(GameMng.Ins.PlayMode == 2)
        {
            if (other.gameObject.CompareTag("EnCar"))
            {
                if (StartLine)
                {
                    if (other.gameObject.GetComponent<PlayCart1>().CurLocalNum == GameMng.Ins.EndLocalNum)
                        other.GetComponent<PlayCart1>().GoalInCount++;
                    other.gameObject.GetComponent<PlayCart1>().PerLocalNum = 0;
                    other.gameObject.GetComponent<PlayCart1>().CurLocalNum = LocalNum;
                }
                else
                {
                    other.gameObject.GetComponent<PlayCart1>().PerLocalNum = other.gameObject.GetComponent<PlayCart1>().CurLocalNum;
                    other.gameObject.GetComponent<PlayCart1>().CurLocalNum = LocalNum;
                }

            }
        }
    }
}
