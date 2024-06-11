using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    private int _hp = 10;
    [SerializeField] private int maxHp = 10;
    [SerializeField] private int team = 0;
    public int Team => team;

    [SerializeField] private Slider hpBar;


    private void Awake()
    {
        _hp = maxHp;
        if (hpBar != null)
        {
            hpBar.maxValue = maxHp;
            hpBar.value = _hp;
        }
    }


    public void Damage(int damageTeam, int damage)
    {
        if (damageTeam != team)
        {
            _hp -= damage;

            if (hpBar != null) hpBar.value = _hp;

            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
