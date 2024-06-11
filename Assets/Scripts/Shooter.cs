using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float cooldown = 0.6f;
    private float _last = 0;
    
    [SerializeField] private KeyCode shootButton = KeyCode.Space;


    private void Update()
    {
        Shoot();
    }


    private void Shoot()
    {
        if (Input.GetKey(shootButton))
        {
            if (Time.time - _last < cooldown)
            {
                return;
            }
            _last = Time.time;

            Instantiate(bullet, transform.position, transform.rotation, container);
        }
    }
}
