using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : Component
{
    public float impulseMagnitude;
    
    public GameObject destroyedCar;
    
    public override void Destroy()
    {
        destroyed = true;
        foreach (AdditionalComponent child in car.GetComponentsInChildren<AdditionalComponent>())
        {
            child.Destroy();
        }
        Instantiate(destroyedCar, car.transform.position, car.transform.rotation);
        Destroy(car.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!destroyed && car.inCollision)
        {
            if (other.gameObject.layer == 10)
            {
                health -= other.gameObject.GetComponent<Bullet>().damage;
                base.car.health -= other.gameObject.GetComponent<Bullet>().damage;
            }
            else
            {
                float damage = Mathf.Sqrt(impulseMagnitude) * Mathf.Sqrt(energy) / 10;
                health -= damage;
                base.car.health -= damage;
            }

            car.inCollision = false;
            if (health <= 0) Destroy();
        }
    }
}
