using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnCarEng : MonoBehaviour
{

    public bool PlayPlayer;
    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 80f;
    public float currentSpeed;
    public float maxSpeed = 100f;

    private float startTime;
    private List<Transform> nodes;
    public int currectNode = 0;

    private void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
        startTime = 0;
    }

    private void FixedUpdate()
    {
        if(GameMng.Ins.StartCount == 0)
        {

            ApplySteer();
            Drive();
            CheckWaypointDistance();
        }
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;

    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if(currentSpeed <= 300 && startTime <= 10)
        {
            startTime += Time.deltaTime;
            currentSpeed = 300;
        }

        if (currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void CheckWaypointDistance()
    {
        if (currectNode == nodes.Count - 1)
        {
            currectNode = 0;
        }
        else
        {
            if (Vector3.Distance(transform.position, nodes[currectNode].position) > Vector3.Distance(transform.position, nodes[currectNode + 1].position))
            {
                    currectNode++;
            }
        }
        if (Vector3.Distance(transform.position, nodes[currectNode].position) < 4)
        {
            if (currectNode == nodes.Count - 1)
            {
                currectNode = 0;
            }
            else
            {
                currectNode++;
            }
        }
    }
}
