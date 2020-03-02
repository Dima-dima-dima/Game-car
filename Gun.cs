using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject firePoint;
    public float fireRate;
    public Bullet bullet;

    private float _timeToFire = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _timeToFire)
        {
            _timeToFire = Time.time + 1 / fireRate;
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        Instantiate(bullet.gameObject, firePoint.transform.position, Quaternion.identity).transform.rotation =
            firePoint.transform.rotation;
    }
}
