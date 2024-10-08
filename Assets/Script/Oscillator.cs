using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 StartingPosition;
    [SerializeField] Vector3 MovementVector ;
    [Range(0,1)] float MovementFactor;
    [SerializeField] float Period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position ;
    }

    // Update is called once per frame
    void Update()
    {
        if(Period <= Mathf.Epsilon) {return;}
        float cycles = Time.time / Period; // continually growing the number over time
        const float Tau = Mathf.PI * 2; // constant value of 6.283
        float RawSinWave = Mathf.Sin(cycles * Tau);// going from -1 to 1

        MovementFactor = (RawSinWave + 1f)/2f; // recauculated to go from 0 to 1 so its cleaner
        Vector3 offset = MovementVector * MovementFactor ;
        transform.position = StartingPosition + offset ;
    }
}
