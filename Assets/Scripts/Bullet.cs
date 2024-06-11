using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _velocity = Vector2.right;
    
    [SerializeField] private float speed = 10;
    [SerializeField] private int damage = 1;
    [SerializeField] private int team = 0;


    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        _velocity = Vector2.right * speed;
        transform.Translate(_velocity * Time.deltaTime);                        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var entity = other?.GetComponent<Entity>();
        if (entity != null && entity.Team != team)
        {
            entity.Damage(team,damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
