using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputInterpreter))] 
public class ThreeAxisVehicle : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    private float _steeringAngle;
    
    public InputInterpreter iM;
    public WheelCollider frontLeftWheel, frontRightWheel;
    public Transform frontLeftMesh, frontRightMesh;
    public WheelCollider rearLeftWheel1, rearRightWheel1;
    public Transform rearLeftMesh1, rearRightMesh1;
    public WheelCollider rearLeftWheel2, rearRightWheel2;
    public Transform rearLeftMesh2, rearRightMesh2;
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
        rearLeftWheel1.motorTorque = _verticalInput * enginePower;
        rearRightWheel1.motorTorque = _verticalInput * enginePower;
        rearLeftWheel2.motorTorque = _verticalInput * enginePower;
        rearRightWheel2.motorTorque = _verticalInput * enginePower;
    }

    private void UpdateWheels()
    {
        RotateWheels(frontLeftWheel, frontLeftMesh);
        RotateWheels(frontRightWheel, frontRightMesh);
        RotateWheels(rearLeftWheel1, rearLeftMesh1);
        RotateWheels(rearRightWheel1, rearRightMesh1);
        RotateWheels(rearLeftWheel2, rearLeftMesh2);
        RotateWheels(rearRightWheel2, rearRightMesh2);
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
