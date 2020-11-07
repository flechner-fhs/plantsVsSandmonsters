﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Watergun : MonoBehaviour
{
    [HideInInspector]
    private ParticleSystem ParticleSystem;

    public float PlantHeal = .1f;
    public float Damage = .1f;

    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.Stop();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (new string[] { "Enemy", "Obstacle", "Plant" }.Contains(other.tag))
        {
            switch (other.tag)
            {
                case "Enemy":
                    other.GetComponent<Entity>().TakeDamage(Damage);
                    break;
                case "Plant":
                    other.GetComponent<Entity>().Heal(PlantHeal);
                    break;
            }
        }
    }

    public void Activate() => ParticleSystem.Play();
    public void Deactivate() => ParticleSystem.Stop();
    public bool IsActive() => ParticleSystem.isPlaying;

}
