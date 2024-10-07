using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonEvent : MonoBehaviour
{

    public GameObject Blur;

    // Start is called before the first frame update
    bool Isblur;
    void Start()
    {
        Isblur = false;
 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CreatBlur()
    {
        if (!Isblur)
        {
            Blur.SetActive(true);
            Isblur = true;
        }
    }
    public void DeletBlur()
    {
        if (Isblur)
        {
            Blur.SetActive(false);
            Isblur = false;
        }
    }

}
