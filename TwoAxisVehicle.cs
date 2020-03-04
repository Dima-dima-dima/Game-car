using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputInterpreter))] 
public class TwoAxisVehicle : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    private float _steeringAngle;

    public bool fWD;
    public InputInterpreter iM;
    public WheelCollider frontLeftWheel, frontRightWheel;
    public Transform frontLeftMesh, frontRightMesh;
    public WheelCollider rearLeftWheel, rearRightWheel;
    public Transform rearLeftMesh, rearRightMesh;
    public float maxSteerAngle = 30;
    public float enginePower = 50;

    public void GetInput()
    {
        _horizontalInput = iM.steer;
        _verticalInput = iM.throttle;
    }

    private void Steer()
    {
        _steeringAngle = maxSteerAngle * _horizontalInput;
        frontLeftWheel.steerAngle = _steeringAngle;
        frontRightWheel.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        rearLeftWheel.motorTorque = _verticalInput * enginePower;
        rearRightWheel.motorTorque = _verticalInput * enginePower;
        if (fWD)
        {
            frontLeftWheel.motorTorque = _verticalInput * enginePower;
            frontRightWheel.motorTorque = _verticalInput * enginePower;
        }
    }

    private void UpdateWheels()
    {
        RotateWheels(frontLeftWheel, frontLeftMesh);
        RotateWheels(frontRightWheel, frontRightMesh);
        RotateWheels(rearLeftWheel, rearLeftMesh);
        RotateWheels(rearRightWheel, rearRightMesh);
    }

    private void RotateWheels(WheelCollider rwwheel, Transform rwtransform)
    {
        Vector3 pos = rwtransform.position;
        Quaternion quat = rwtransform.rotation;

        rwwheel.GetWorldPose(out pos, out quat);

        rwtransform.position = pos;
        rwtransform.rotation = quat;
    }
    
    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheels();
    }
}
