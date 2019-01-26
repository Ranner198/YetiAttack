﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour {

    public int Health = 100;
    public int flashCount = 6;
    public float interval = 0.4f;
    public Color PlayerColor;
    public Color EnemyColor;
    public Color[] defaultTint;

    public HealthBar hb;

    public void TakeDamage(int val, GameObject tag) {

        Health -= val;
        hb.SetHealth(Health);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(Flash(flashCount, tag));
    }

    public int GetHealth()
    {
        return this.Health;
    }

    void Start() {
        defaultTint = new Color[gameObject.GetComponent<Renderer>().materials.Length];
        var count = 0;
        foreach (Material mat in gameObject.GetComponent<Renderer>().materials)
        {
            defaultTint[count] = mat.color;
            count++;
        }
        hb.SetHealth(Health);
    }

    IEnumerator Flash(int flashCount, GameObject tag)
    {
        int flashCounter = 0;
        while (flashCounter < flashCount)
        {
            if (tag.tag == "Player")
            {        
                foreach (Material mat in gameObject.GetComponent<Renderer>().materials)
                {
                    mat.color = PlayerColor;
                }

                yield return new WaitForSeconds(interval);

                var count = 0;
                foreach (Material mat in gameObject.GetComponent<Renderer>().materials)
                {
                    mat.color = defaultTint[count];
                    count++;
                }

                yield return new WaitForSeconds(interval);

                flashCounter++;
            }
            else
            {
                foreach (Material mat in gameObject.GetComponent<Renderer>().materials)
                {
                    mat.color = EnemyColor;
                }

                yield return new WaitForSeconds(interval);

                var count = 0;
                foreach (Material mat in gameObject.GetComponent<Renderer>().materials)
                {
                    mat.color = defaultTint[count];
                    count++;
                }

                yield return new WaitForSeconds(interval);
                flashCounter++;
            }        
        }
    }
}
