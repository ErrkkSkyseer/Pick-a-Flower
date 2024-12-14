using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Violet inherit from the abstract flower class
// Violet can be attach to GameObject
// But the flower class can't
public class Violet : Flower
{
    // Some Reference
    Rigidbody2D rb;

    // Variable for Violet behaviour
    [SerializeField] MinMaxFloat forceRandom;
    [SerializeField] MinMaxFloat dragRandom;

    // An overrided start function
    public override void Start()
    {
        // Call Flower's start function (So we don't need to rewrite the code)
        base.Start();
        Type = FlowerType.Violet; // Set flower type

        // Violet behaviour stuff
        rb = GetComponent<Rigidbody2D>();
        rb.drag = dragRandom.Roll();

        Physics2D.IgnoreLayerCollision(gameObject.layer, 6);
    }

    private void OnMouseDown()
    {
        float randX = Random.Range(-1.0f, 1.0f);
        float randY = Random.Range(-1.0f, 1.0f);
        Vector2 dir = new Vector2(randX, randY);

        rb.AddForce(dir * forceRandom.Roll(), ForceMode2D.Impulse);
    }
}
