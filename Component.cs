using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Component: MonoBehaviour
{
  public float health;
  public float mass;
  public bool isStatic;
  public bool destroyed = false;

  public float energy;
  
  public Car car;

  public abstract void Destroy();
  private void Start()
  {
    energy = mass / 2;
  }

  private void FixedUpdate()
  {
    if (!isStatic)
    {
      energy = (mass * (car.rigidbody.velocity.magnitude >= 1 ? car.rigidbody.velocity.magnitude : 1)) / 2;
    }
  }
}
