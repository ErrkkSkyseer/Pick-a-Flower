using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Sunflower inherit from the abstract flower class
// Sunflower can be attach to GameObject
// But the flower class can't
public class Sunflower : Flower
{
    // Sunflower need some information from GameManager
    GameManager gm;
    Rigidbody2D rb;

    // Sunflower behaviour
    [SerializeField] MinMaxFloat velocityRandom;
    Vector2 v;
    Vector2 direction;


    // An overided start function
    public override void Start()
    {
        // Call Flower's start function (So we don't need to rewrite the code)
        base.Start();
        Type = FlowerType.Sunflower; // Set flower type

        // Sunflower behavior stuff
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        float randX = Random.Range(-1.0f, 1.0f);
        float randY = Random.Range(-1.0f, 1.0f);
        direction = new Vector2(randX, randY);
        direction = direction.normalized;

        v = direction * velocityRandom.Roll();

        rb.velocity = v;

    }

    private void Update()
    {
        if (transform.position.x >= gm.RightBound)
            v = new Vector2(Mathf.Abs(v.x) * -1, v.y);
        else if (transform.position.x <= gm.LeftBound)
            v = new Vector2(Mathf.Abs(v.x), v.y);
        else if (transform.position.y >= gm.UpperBound)
            v = new Vector2(v.x, Mathf.Abs(v.y) * -1);
        else if (transform.position.y <= gm.LowerBound)
            v = new Vector2(v.x, Mathf.Abs(v.y));
    }
}
