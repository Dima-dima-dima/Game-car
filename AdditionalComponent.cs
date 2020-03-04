using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalComponent : Component
{
    [Range(0,1)]
    public float percentDamageToHull;

    public Hull hull;
    
    public override void Destroy()
    {
        destroyed = true;
        GameObject destroyedObj = Instantiate(this.gameObject, transform.position, transform.rotation);
        destroyedObj.layer = 9;
        destroyedObj.GetComponent<MeshCollider>().isTrigger=false;
        destroyedObj.GetComponent<AdditionalComponent>().enabled=false;
        destroyedObj.AddComponent<Rigidbody>().mass = mass;
        Destroy(this.gameObject);
        Destroy(destroyedObj.gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!destroyed && car.inCollision)
        {
            if (other.gameObject.layer == 10)
            {
                health -= other.gameObject.GetComponent<Bullet>().damage;
                car.health -= other.gameObject.GetComponent<Bullet>().damage;
            }
            else
            {
                float damage = Mathf.Sqrt(hull.impulseMagnitude) * Mathf.Sqrt(energy) / 10;
                health -= damage;
                hull.health -= damage * percentDamageToHull;
                car.health -= damage + damage * percentDamageToHull;
            }

            car.inCollision = false;
            if(hull.health <= 0) hull.Destroy();
            else
            if (health <= 0) Destroy();
        }
    }
}
