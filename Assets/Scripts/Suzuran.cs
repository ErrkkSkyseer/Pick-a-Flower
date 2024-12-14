using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Suzuran inherit from the abstract flower class
// Suzuran can be attach to GameObject
// But the flower class can't
public class Suzuran : Flower
{
    // Suzuran need some information from GameManager
    GameManager gm;

    // An overrided Start function
    public override void Start()
    {
        // Call Flower's start function (So we don't need to rewrite the code)
        base.Start();
        Type = FlowerType.Suzuran; // Set flower type

        // Suzuran Behaviour setup
        gm = FindObjectOfType<GameManager>();
    }
    private void OnMouseDown()
    {
        int r = Random.Range(0, 3);
        if (r > 0)
            return;

        Vector2 begin = new Vector2(gm.LeftBound, gm.LowerBound);
        Vector2 end = new Vector2(gm.RightBound, gm.UpperBound);

        float randX = UnityEngine.Random.Range(begin.x, end.x);
        float randY = UnityEngine.Random.Range(begin.y, end.y);

        Vector3 pos = new Vector2(randX, randY);

        transform.position = pos;
    }
}
