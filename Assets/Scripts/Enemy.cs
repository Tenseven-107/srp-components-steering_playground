using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : SteeringVehicle
{
    [SerializeField] private float minMass = 25;
    [SerializeField] private float maxMass = 50;
    
    [SerializeField] private int damage = 1;
    [SerializeField] private int team = 1;

    private Entity _targetEntity;
    
    private void Awake()
    {
        var newMass = Random.Range(minMass, maxMass);
        Mass = newMass;
        
        onArrived.AddListener(DamagePlayer);
    }


    private void DamagePlayer()
    {
        if (_targetEntity != null)
        {
            _targetEntity.Damage(team, damage);
        }
    }



    public void SetEnemyTarget(GameObject player)
    {
        _targetEntity = player?.GetComponent<Entity>();
        
        SetTarget(player.transform);
    }
}
