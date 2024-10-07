using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCart1 : MonoBehaviour
{
    //public AudioSource Audio;
    //public AudioClip DriftSound;
    // Start is called before the first frame update
    public GameObject PlayerCharacter;
    public GameObject SkidMarkOBJ;
    public Rigidbody CartRigidbody;
    float FCartRotation;
    public float moveSpeed;
    public int Speed;
    public float CountSteer;
    public float m_Acceleration;
    public float m_Steering;
    public int CarMaxSpeed;
    bool m_HopPressed;
    bool m_HopHeld;
    bool m_BoostPressed;
    bool m_FirePressed;
    bool m_FixedUpdateHappened;
    bool m_SteeringOn;
    bool CarInGround;
    bool b_Skid;
    bool b_InAir;
    public bool bDrift;

    public int GoalInCount;
    public GameObject Wheel1;
    public GameObject Wheel2;

    GameObject Skid1;
    GameObject Skid2;
    public int CurLocalNum;
    public int PerLocalNum;
    //VehicleParent VP;
    //Animator CartAnimator;
    new Transform transform;


    public float startRot;
    public float CurRot;
    public float PermoveSpeed;
    Animator CartAnimator;
    void Start()
    {
        CurLocalNum = 100;
        PerLocalNum = 100;
        GoalInCount = 0;
        transform = GetComponent<Transform>();
        CartRigidbody = GetComponent<Rigidbody>();
        CartAnimator = PlayerCharacter.GetComponent<Animator>();
        //CartAnimator = GetComponent<Animator>();
        FCartRotation = 0;
        CountSteer = 0;
        Speed = 0;
        bDrift = false;
        b_Skid = false;
        b_InAir = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!b_InAir)
        {
            if (bDrift)
            {

                CreatSkid();

            }
            if (!bDrift)
            {
                DetachSkid();
            }
        }
        if(GameMng.Ins.EndLocalNum != CurLocalNum)
        {
            if (CurLocalNum + 1 <= PerLocalNum)
            {
                //뒤로가는것
                if (!GameMng.Ins.StopBackImage.gameObject.activeSelf)
                {
                    GameMng.Ins.StopBackImage.gameObject.SetActive(true);
                }
            }
            if (CurLocalNum - 1 == PerLocalNum)
            {
                if (GameMng.Ins.StopBackImage.gameObject.activeSelf)
                {
                    GameMng.Ins.StopBackImage.gameObject.SetActive(false);
                }
                //정상적으로 가는것
            }
        }
        else
        {

        }

        if (GameMng.Ins.StartCount == 0)
        {
            if (Input.GetKey(KeyCode.P))
            {
                Vector3 CurObject = GameObject.Find("MapChackPoint" + CurLocalNum.ToString()).transform.position;
                transform.position = new Vector3(CurObject.x, CurObject.y + 0.3f, CurObject.z);
            }
            if (Input.GetKey(KeyCode.I))
            {
                if (m_Acceleration <= 1)
                {
                    m_Acceleration += 1f * Time.deltaTime;
                }
                if (!bDrift)
                {
                    Debug.Log("asdqwd");
                    if (CarMaxSpeed <= moveSpeed)
                    {

                    }
                    else
                    {
                        if (GameMng.Ins.PlayMode == 2)
                        {
                            if (moveSpeed < 10)
                            {
                                moveSpeed += 40 * Time.deltaTime;
                            }
                            else if (moveSpeed < 30)
                            {
                                moveSpeed += 20 * Time.deltaTime;
                            }
                        }
                        else
                        {
                            if (moveSpeed < 10)
                            {
                                moveSpeed += 10 * Time.deltaTime;
                            }
                            else if (moveSpeed < 20)
                            {
                                moveSpeed += 5 * Time.deltaTime;
                            }
                        }
                    }
                }
            }
            else if (Input.GetKey(KeyCode.K))
            {
                if (m_Acceleration >= -1)
                    m_Acceleration += -1f * Time.deltaTime;
            }
            else
            {
                if (m_Acceleration < 0.1f)
                {
                    m_Acceleration = 0;
                }
                m_Acceleration += -m_Acceleration * Time.deltaTime;
                if (moveSpeed > 1)
                {
                    moveSpeed -= 15 * Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L))
            {
                m_Steering = -1f;
                if (!m_SteeringOn)
                {
                    m_SteeringOn = true;
                    Wheel1.transform.Rotate(0, m_Steering * 30, 0);
                    Wheel2.transform.Rotate(0, m_Steering * 30, 0);

                    CartAnimator.SetInteger("ads", -1);
                }
            }
            else if (!Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.L))
            {
                m_Steering = 1f;
                if (!m_SteeringOn)
                {
                    m_SteeringOn = true;
                    Wheel1.transform.Rotate(0, m_Steering * 30, 0);
                    Wheel2.transform.Rotate(0, m_Steering * 30, 0);
                    CartAnimator.SetInteger("ads", 1);
                }
            }
            else
            {
                Wheel1.transform.localEulerAngles = Vector3.zero;
                Wheel2.transform.localEulerAngles = Vector3.zero;

                m_Steering = 0f;
                CountSteer = 0;
                m_SteeringOn = false;

                CartAnimator.SetInteger("ads", 0);
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (bDrift)
                {
                    PermoveSpeed = moveSpeed;
                }
                bDrift = true;
            }
            else
            {
                if (bDrift)
                {
                    if (PermoveSpeed - 3 <= moveSpeed)
                    {
                        moveSpeed = PermoveSpeed;
                    }
                    else
                    {
                        moveSpeed = PermoveSpeed - 3;
                    }
                }
                startRot = 0;
                bDrift = false;
            }

        }
        //CartAnimator.SetFloat("fAcceleration", m_Acceleration);
        //CartAnimator.SetFloat("fSteering", m_Steering);
        //Debug.Log("m_Acceleration = " + m_Acceleration);
        //Debug.Log("fSteering = " + m_Steering);

        if (m_FixedUpdateHappened)
        {
            m_FixedUpdateHappened = false;

            m_HopPressed = false;
            m_BoostPressed = false;
            m_FirePressed = false;
            //Debug.Log("alsdlasdkqwkd" + m_Acceleration);
            //CartRigidbody.AddForce(transform.rotation * new Vector3(0, 0, m_Acceleration));
            //CartRigidbody.AddForce(transform.forward * m_Acceleration * moveSpeed);

            if (bDrift)
            {
                if (startRot == 0)
                {
                    startRot = transform.rotation.z;
                }
                //Vector3.Lerp(transform.rotation * new Vector3(0, 0, m_Acceleration * moveSpeed / 1.25f), transform.forward * m_Acceleration * moveSpeed, Time.deltaTime);

                CurRot = transform.rotation.z;
                if (startRot + 20 >= CurRot || startRot - 20 <= CurRot)
                {
                    if (moveSpeed >= 15)
                    {
                        CartRigidbody.AddForce(transform.rotation * new Vector3(0, 0, m_Acceleration * moveSpeed / 1.25f));
                    }
                    else
                    {
                        CartRigidbody.AddForce(transform.rotation * new Vector3(0, 0, m_Acceleration * 20));
                    }
                    moveSpeed = Mathf.Abs(CartRigidbody.velocity.z) + 1;
                }

                //startRot;
                //CurRot;
                //CartRigidbody.velocity.Normalize();
                //CartRigidbody.velocity *= 0.01f;
            }
            else
            {
                Vector3 moveSpeedVec3 = transform.forward * m_Acceleration * moveSpeed;
                moveSpeedVec3.y = CartRigidbody.velocity.y;
                CartRigidbody.velocity = moveSpeedVec3;
            }


            //if (Speed > 0)
            //{
            //    Speed -= (int)(10 * (1 - Mathf.Abs(m_Acceleration)));
            //    if (Speed <= 0)
            //    {
            //        Speed = 0;
            //    }
            //}
            //if (Speed <= CarMaxSpeed)
            //{
            //    //CartRigidbody.AddForce(transform.rotation * new Vector3(0,0, m_Acceleration * moveSpeed));
            //    if (m_Acceleration != 0)
            //    {
            //        Speed += (int)(10 * (1 - Mathf.Abs(m_Acceleration)));
            //    }
            //}
            if (bDrift)
            {
                if (m_Steering == 1 || m_Steering == -1)
                {
                    if (m_Acceleration != 0)
                    {
                        if (CountSteer < 1 && CountSteer > -1)
                        {
                            if (m_Steering == -1)
                            {
                                CountSteer -= 0.5f;
                            }
                            else
                            {
                                CountSteer += 0.5f;
                            }
                        }
                        else
                        {
                            CountSteer += m_Steering * Time.deltaTime;
                        }
                        //Debug.Log("CountSteer : " + CountSteer);

                        CartRigidbody.MoveRotation(Quaternion.Euler(0, transform.eulerAngles.y + CountSteer, 0));
                        //    transform.Rotate(0, CountSteer, 0);
                    }
                }
            }
            else
            {
                if (m_Steering == 1 || m_Steering == -1)
                {
                    if (m_Acceleration != 0)
                    {
                        if (CountSteer < 1 && CountSteer > -1)
                        {
                            CountSteer += m_Steering * Time.deltaTime / 2;

                        }
                        transform.Rotate(0, CountSteer, 0);
                        if (Speed >= 170)
                        {
                            //CartRigidbody.AddForce(transform.rotation * new Vector3(0, 0, m_Acceleration * moveSpeed * -1.5f));
                        }
                    }
                }
            }
            //m_velocityZ = CartRigidbody.velocity.y;
        }

        m_HopPressed |= Input.GetKeyDown(KeyCode.Space);
        m_BoostPressed |= Input.GetKeyDown(KeyCode.RightShift);
        m_FirePressed |= Input.GetKeyDown(KeyCode.RightControl);
    }
    void FixedUpdate()
    {
        m_FixedUpdateHappened = true;

    }
    private void LateUpdate()
    {
        //PlayerCharacter.transform.position = transform.position + new Vector3(0, 0, 0);
    }
    void CreatSkid()
    {

        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
        {

            if (!b_Skid)
            {
                // Audio.clip = DriftSound;
                // Audio.Play();
                Skid1 = Instantiate(SkidMarkOBJ);
                //Skid.transform.SetParent(transform.transform);
                Skid1.transform.parent = transform.transform;

                Skid1.transform.localPosition = new Vector3(18.69f, 4.0f, -24.28f);

                Skid1.transform.localScale = Vector3.one;

                Skid2 = Instantiate(SkidMarkOBJ);
                //Skid.transform.SetParent(transform.transform);
                Skid2.transform.parent = transform.transform;

                Skid2.transform.localPosition = new Vector3(-19.0f, 4.0f, -24.28f);

                Skid2.transform.localScale = Vector3.one;

                b_Skid = true;
                //StartCoroutine(WaitForIt());
            }
        }




    }
    void DetachSkid()
    {

        if (b_Skid)
        {
            Skid1.transform.parent = null;
            Skid2.transform.parent = null;
            b_Skid = false;
            b_InAir = true;
            StartCoroutine(Wait());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "floor")
        {
            b_InAir = true;
            DetachSkid();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "floor")
        {
            b_InAir = false;

        }
    }
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(1.5f);
        DetachSkid();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        b_InAir = false;
    }
}
