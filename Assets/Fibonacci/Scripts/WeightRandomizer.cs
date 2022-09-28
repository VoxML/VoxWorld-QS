using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeightRandomizer : MonoBehaviour
{
    public List<int> weightValues;

    private System.Random rng = new System.Random();

    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void Start()
    {
        if (transform.childCount != weightValues.Count)
        {
            Debug.LogWarning("Weight counts do not match.");
        }

        Shuffle(weightValues);
        for (int i = 0; i < weightValues.Count; i++)
        {
            transform.GetChild(i).GetComponentInChildren<Block>().weight = weightValues[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
