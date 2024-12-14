using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rose inherit from the abstract flower class
// Rose can be attach to GameObject
// But the flower class can't
public class Rose : Flower
{
    // Variables for rose's behaviour
    [SerializeField] MinMaxFloat rotationSpeedRandom;
    float rotationSpeed;

    // An overided start function
    public override void Start()
    {
        // Call Flower's start function (So we don't need to rewrite the code)
        base.Start();
        Type = FlowerType.Rose; // Set flower type

        // random rose spin speed
        rotationSpeed = rotationSpeedRandom.Roll();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
