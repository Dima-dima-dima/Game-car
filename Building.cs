using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Component
{
    public float criticalValue;
    
    public override void Destroy()
    {
        GameObject destroyed = Instantiate(this.gameObject, transform.position, transform.rotation);
        destroyed.layer = 9;
        destroyed.AddComponent<Rigidbody>();
        Destroy(this.gameObject);
        Destroy(destroyed.gameObject, 5f);
    }

    public void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.layer == 8 && other.gameObject.GetComponent<Component>().energy > criticalValue) ||
            (other.gameObject.layer == 10 && other.gameObject.GetComponent<Bullet>().damage > criticalValue))
        {
            Destroy();
        }
    }
}
