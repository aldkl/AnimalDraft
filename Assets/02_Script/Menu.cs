using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Transform CameraTrans;
    public GameObject OptionScreen; 
    public AudioSource Audio;
    public AudioClip Clicksound;
    float speed = 100.0F;
    public Image LogoImgAnimal;
    public Image LogoImgDrift;
    float colorNum;
    float colorNum2;
    bool b_ColorUp;
    bool b_ColorUp2;
    // Start is called before the first frame update
    void Start()
    {
        colorNum = 0.0f;
        colorNum2 = 0.0f;
        b_ColorUp = true;
        b_ColorUp2 = true;
    }


    public void SetPlayMode(int paasd)
    {
        GameMng.Ins.PlayMode = paasd;
    }
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetAxis("Mouse X") >0)
        {
           
            CameraTrans.rotation = Quaternion.Lerp(CameraTrans.rotation, Quaternion.Euler(new Vector3(0, 6, 0)), 0.03f);
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            CameraTrans.rotation = Quaternion.Lerp(CameraTrans.rotation, Quaternion.Euler(new Vector3(0, -6, 0)), 0.03f);
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            CameraTrans.rotation = Quaternion.Lerp(CameraTrans.rotation, Quaternion.Euler(new Vector3(-6, 0, 0)), 0.03f);
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            CameraTrans.rotation = Quaternion.Lerp(CameraTrans.rotation, Quaternion.Euler(new Vector3(6, 0, 0)), 0.03f);
        }
        ///////Animallll
        if (b_ColorUp)
        {
            if (colorNum < 1.0f)
            {
                LogoImgAnimal.color = new Vector4(1, 0, colorNum, 1);
                StartCoroutine(colorTime());
                colorNum += 0.005f;
               
            }
            else
            {
                b_ColorUp = false;
            }
            
        }
        else
        {
            if (colorNum > 0.0f)
            {
                LogoImgAnimal.color = new Vector4(1, 0, colorNum, 1);
                StartCoroutine(colorTime());
                colorNum -= 0.005f;
            }
            else
            {
                b_ColorUp = true;
            }
        }
        /////Drift 
        if (b_ColorUp2)
        {
            if (colorNum2 < 0.7f)
            {
                LogoImgDrift.color = new Vector4(colorNum2, 0, 1, 1);
                StartCoroutine(colorTime2());
                colorNum2 += 0.005f;

            }
            else
            {
                b_ColorUp2 = false;
            }

        }
        else
        {
            if (colorNum2 > 0.0f)
            {
                LogoImgDrift.color = new Vector4(colorNum2, 0, 1, 1);
                StartCoroutine(colorTime2());
                colorNum2 -= 0.005f;
            }
            else
            {
                b_ColorUp2 = true;
            }
        }
    }
    IEnumerator colorTime()
    {
        yield return new WaitForSeconds(0.01f);
    }
    IEnumerator colorTime2()
    {
        yield return new WaitForSeconds(0.01f);
    }

    public void clicksound()
    {
        Audio.clip = Clicksound;
        Audio.Play();
    }
    public void SceneChange(int a)
    {

        StartCoroutine(time(a));
    }
    void Option()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OptionScreen.SetActive(true);
        }
    }
    IEnumerator time(int b)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(b);
    }

}
