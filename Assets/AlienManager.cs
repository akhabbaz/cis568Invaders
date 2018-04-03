﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour {
	float xStart = -5;
    float xstep = 2.5f;
	int numberx = 6;
    float yStart = 2.0f;
    float ystep = 2.5f;
    float numbery = 2.0f;
    float stepPerUpdate = 0.1f;
    float minX = 0.0f;
    float maxX = 0.0f;
    public Alien prefabAlien;
    List<Alien> aliens;
    // Use this for initialization
    AlienManager()
        { }
    void Start() {
        aliens = new List<Alien>();

        for (float i = 0; i < numberx; ++i) {
            for (float j = 0; j < numbery; ++j)
            {
                Vector3 test = new Vector3(xStart + i * xstep,
                    yStart + j * ystep, 0);
                if (xStart + i * xstep < minX)
                {
                    minX = xStart + i * xstep;
                }
                if ((xStart + i * xstep) > maxX)
                {
                    maxX = xStart + i * xstep;
                }
                Alien oneAlien = Instantiate(prefabAlien, test, Quaternion.identity);
                oneAlien.UpdateManager(gameObject.GetComponent<AlienManager>());
                oneAlien.name = " " + i + " " + j;
                aliens.Add(oneAlien);
            }
        }
        aliens.Sort();
        minX -= 5.0f;
        maxX += 5.0f;

	}
	
    public void RemoveAlien(Alien deadAlien)
    {
        int index = aliens.BinarySearch(deadAlien);
        aliens.RemoveAt(index);
        Debug.Log("Alien Count : " + aliens.Count);

    }
	// Update is called once per frame:
	void Update () {
        float pos = aliens[0].transform.position[0];

        if (pos < minX)
        {
            stepPerUpdate *= -1;
        }
        int last = aliens.Count - 1;
        pos = aliens[last].transform.position[0];
        if (pos > maxX)
        {
            stepPerUpdate *= -1;
        }
        foreach (Alien al in aliens)
        {
            pos = al.transform.position.x;
            al.transform.Translate(stepPerUpdate, 0, 0);
        }
     
    }
}
