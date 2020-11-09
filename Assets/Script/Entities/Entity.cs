﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    [Header("Basic Stats")]
    public string UnitName = "Entity";

    public float MaxHealth = 20;
    public float Health = 20;
    public float Damage = 1;
    public float Knockback = 500;

    public int Team = 0;

    public float MovementSpeed = 10;

    [HideInInspector]
    public Collider2D collider;
    [HideInInspector]
    public Rigidbody2D rigidbody;
    [HideInInspector]
    public SpriteRenderer Renderer;
    public Color BaseColor;

    [HideInInspector]
    public bool IsDead = false;

    [HideInInspector]
    public bool FacingLeft;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        Renderer = GetComponentInChildren<SpriteRenderer>();
        BaseColor = Renderer.color;
    }

    public void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        StartCoroutine(Flash(new Color(1, .5f, .5f)));

        if (Health <= 0 && !IsDead)
        {
            IsDead = true;
            Die();
        }
    }

    private bool stillFlashing = false;
    public IEnumerator Flash(Color tint, float duration = .1f)
    {
        if (stillFlashing)
            yield break;
        stillFlashing = true;
        Color prevCol = Renderer.color;
        Renderer.color = tint;
        yield return new WaitForSeconds(duration);
        Renderer.color = prevCol;
        stillFlashing = false;
    }

    public void Heal(float healing)
    {
        Health = Mathf.Min(MaxHealth, Health + healing);
    }

    public void FixedUpdate()
    {
        if(!IsDead)
            Move();
    }

    abstract public void Move();
    abstract public void Die();

}
