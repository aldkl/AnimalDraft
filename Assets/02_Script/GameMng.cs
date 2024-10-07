using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameMng : MonoBehaviour
{
    #region SingleTon
    // Start is called before the first frame update
    private static GameMng Instance;
    public static GameMng Ins
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<GameMng>();
                if (Instance == null)
                {
                    GameObject MngGame = new GameObject();
                    Instance = MngGame.AddComponent<GameMng>();
                    MngGame.name = "GameMng";
                }
            }
            return Instance;
        }
    }
    #endregion
    public PlayCart playCart;
    public GameObject EnCart;
    public GameObject MinimapIcon1;
    public GameObject MinimapIcon2;
    public Path path;
    public GameObject[] LabNum1;
    public GameObject[] LabNum2;
    public GameObject[] PungChar;
    public GameObject[] rankings;
    public GameObject timeline;
    public float StartCount;
    public float StartCountTime;

    public Image[] CountImage;
    public Image StopBackImage;
    public float EndLocalNum;
    public int  PlayMode;
    public bool PlayPlayer2;
    public GameObject[] PlayObject;
    public GameObject[] PlayTimeObject;
    public float[] Times;
    public Camera Play1Cam;
    public Camera Play2Cam;
    PlayCart1 EnPlaycart;
    void Start()
    {
        StartCountTime = 0;
        StartCount = 3;
        if(PlayMode == 0)//솔로플레이
        {
            Play1Cam.rect = new Rect(0, 0, 1, 1);
            PlayObject[0].SetActive(false);
            PlayObject[1].SetActive(false);
            EnCart.gameObject.SetActive(false);
            
            PlayObject[3].SetActive(false);
            PlayObject[4].SetActive(false);
        }
        if (PlayMode == 1)//AI플레이
        {
            Play1Cam.rect = new Rect(0, 0, 1, 1);
            PlayObject[0].SetActive(false);

            PlayObject[1].SetActive(false);
            PlayObject[3].SetActive(false);
            PlayObject[4].SetActive(false);
        }
        if (PlayMode == 2)//멀티플레이
        {
            EnCart.GetComponent<PlayCart1>().enabled = true;
            EnPlaycart = EnCart.GetComponent<PlayCart1>();
            EnCart.GetComponent<EnCarEng>().enabled = false;

            PlayObject[1].SetActive(false);
            PlayObject[3].SetActive(false);
            PlayObject[4].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cheat();
        PungCharRole();
        if (StartCount == 0)
        {
            TimeCheck();
        }
        if (PlayMode == 1)//멀티플레이
        {
            MinimapIcon1.transform.position = playCart.transform.position;
            MinimapIcon2.transform.position = EnCart.transform.position;
        }
        else if (PlayMode == 2)//멀티플레이
        {
            MinimapIcon1.transform.position = playCart.transform.position;
            MinimapIcon2.transform.position = EnCart.transform.position;
            play1Lab();
            play2Lab();
            PlayVSPlay();
        }
    }

    public void Cheat()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(timeline.GetComponent<PlayableDirector>().time < 13)
            {
                timeline.GetComponent<PlayableDirector>().time = 13;
            }
        }
        if (Input.GetKey(KeyCode.F1) && PlayMode == 2)
        {
            EnPlaycart.GoalInCount = 2;
            EnPlaycart.CartRigidbody.velocity = Vector3.zero;
            EnPlaycart.transform.position = new Vector3(284.88f, -0.21f, 42.3f);
        }
        if (Input.GetKey(KeyCode.F2))
        {
            playCart.GoalInCount = 2;
            playCart.CartRigidbody.velocity = Vector3.zero;
            playCart.transform.position = new Vector3(284.88f, -0.21f, 42.3f);
        }
        //if (Input.GetKey(KeyCode.F3) && PlayMode == 1)
        //{
        //    playCart.GoalInCount = 2;
        //    playCart.CartRigidbody.velocity = Vector3.zero;
        //    playCart.transform.position = new Vector3(284.88f, -0.21f, 42.3f);
        //}
    }
    public void TimeCheck()
    {
        Times[0] += Time.deltaTime;
        if(Times[0] >= 1)
        {
            Times[0] -= 1;
            Times[1] += 1;
            if(Times[1] < 10)
            {
                PlayTimeObject[1].GetComponent<Text>().text = "0" + Times[1].ToString("N0");
            }
            else
            {
                PlayTimeObject[1].GetComponent<Text>().text = Times[1].ToString("N0");
            }
        }
        if (Times[1] >= 60)
        {
            Times[1] -= 60;
            Times[2] += 1;
            PlayTimeObject[2].GetComponent<Text>().text = Times[2].ToString("N0");
        }
        float asd = Times[0];
        asd *= 10;
        PlayTimeObject[0].GetComponent<Text>().text = asd.ToString("N1");
        asd = 0;
    }

    public void play1Lab()
    {
        if(playCart.GoalInCount == 0)
        {
            LabNum1[0].SetActive(true);
            LabNum1[1].SetActive(false);
            LabNum1[2].SetActive(false);
            LabNum1[3].SetActive(false);
        }
        else if(playCart.GoalInCount == 1)
        {
            LabNum1[0].SetActive(false);
            LabNum1[1].SetActive(true);
            LabNum1[2].SetActive(false);
            LabNum1[3].SetActive(false);
        }
        else if (playCart.GoalInCount == 2)
        {
            LabNum1[0].SetActive(false);
            LabNum1[1].SetActive(false);
            LabNum1[2].SetActive(true);
            LabNum1[3].SetActive(false);
        }
        else if (playCart.GoalInCount == 3)
        {
            LabNum1[0].SetActive(false);
            LabNum1[1].SetActive(false);
            LabNum1[2].SetActive(false);
            LabNum1[3].SetActive(true);


        }
    }
    public void play2Lab()
    {
        if (EnPlaycart.GoalInCount == 0)
        {
            LabNum2[0].SetActive(true);
            LabNum2[1].SetActive(false);
            LabNum2[2].SetActive(false);
            LabNum2[3].SetActive(false);
        }
        else if (EnPlaycart.GoalInCount == 1)
        {
            LabNum2[0].SetActive(false);
            LabNum2[1].SetActive(true);
            LabNum2[2].SetActive(false);
            LabNum2[3].SetActive(false);
        }
        else if (EnPlaycart.GoalInCount == 2)
        {
            LabNum2[0].SetActive(false);
            LabNum2[1].SetActive(false);
            LabNum2[2].SetActive(true);
            LabNum2[3].SetActive(false);
        }
        else if (EnPlaycart.GoalInCount == 3)
        {
            LabNum2[0].SetActive(false);
            LabNum2[1].SetActive(false);
            LabNum2[2].SetActive(false);
            LabNum2[3].SetActive(true);


        }
    }

    public void player1Fowrd()
    {
        rankings[0].SetActive(true);
        rankings[1].SetActive(false);
        rankings[2].SetActive(false);
        rankings[3].SetActive(true);
    }
    public void player2Fowrd()
    {
        rankings[0].SetActive(false);
        rankings[1].SetActive(true);
        rankings[2].SetActive(true);
        rankings[3].SetActive(false);
    }
    public void PlayVSPlay()
    {
        if (playCart.GoalInCount > EnPlaycart.GoalInCount)
        {//플레이어1이 더 앞에 있음
            player1Fowrd();
        }
        else if (playCart.GoalInCount < EnPlaycart.GoalInCount)
        {//플레이어2가 더 앞에 있음
            player2Fowrd();
        }
        else//플레이어1과 2가 같은 골카운트일때
        {
            if (playCart.CurLocalNum > EnPlaycart.CurLocalNum)
            {//플레이어1이 더 앞에 있음
                player1Fowrd();
            }
            else if (playCart.CurLocalNum < EnPlaycart.CurLocalNum)
            {//플레이어2가 더 앞에 있음
                player2Fowrd();
            }
            else//플레이어1과 2가 같은 넘버에있을때
            {
                if (Vector3.Distance(playCart.transform.position, path.nodes[playCart.CurLocalNum - 100].position) >
                    Vector3.Distance(EnPlaycart.transform.position, path.nodes[EnPlaycart.CurLocalNum - 100].position))
                {//플레이어1이 더 앞에 있음
                    player1Fowrd();
                }
                else
                {//플레이어2가 더 앞에 있음
                    player2Fowrd();
                }
            }
        }
    }

    public void PungCharRole()
    {
        for(int i = 0; i < PungChar.Length; i ++)
        {
            PungChar[i].transform.Rotate(Vector3.forward, 2);
        }
    }
    public void StartCouting3()
    {
        StartCount = 3;
        if (PlayMode == 0)//솔로플레이
        {
            PlayObject[3].SetActive(true);
            PlayObject[4].SetActive(true);
        }
        if (PlayMode == 1)//AI플레이
        {
            PlayObject[3].SetActive(true);
            PlayObject[4].SetActive(true);
        }
        if (PlayMode == 2)//멀티플레이
        {
            PlayObject[1].SetActive(true);
            PlayObject[3].SetActive(true);
            PlayObject[4].SetActive(true);
        }
        CountImage[0].gameObject.SetActive(true);
        CountImage[1].gameObject.SetActive(false);
        CountImage[2].gameObject.SetActive(false);
        CountImage[3].gameObject.SetActive(false);
    }
    public void StartCouting2()
    {
        StartCount = 2;
        CountImage[0].gameObject.SetActive(false);
        CountImage[1].gameObject.SetActive(true);
        CountImage[2].gameObject.SetActive(false);
        CountImage[3].gameObject.SetActive(false);
    }
    public void StartCouting1()
    {
        StartCount = 1;
        CountImage[0].gameObject.SetActive(false);
        CountImage[1].gameObject.SetActive(false);
        CountImage[2].gameObject.SetActive(true);
        CountImage[3].gameObject.SetActive(false);
    }
    public void StartCoutingGO()
    {
        StartCount = 0;
        CountImage[0].gameObject.SetActive(false);
        CountImage[1].gameObject.SetActive(false);
        CountImage[2].gameObject.SetActive(false);
        CountImage[3].gameObject.SetActive(true);

        StartCoroutine(GODel());

    }

    IEnumerator GODel()
    {
        yield return new WaitForSeconds(0.75f);
        CountImage[3].gameObject.SetActive(false);
    }
}
