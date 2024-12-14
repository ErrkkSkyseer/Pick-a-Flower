using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Class control over player's action
// Player can click at the flower and collect it
// In this game, player don't exist as a GameObject
// But rather as an input (from mouse click)
// Because player is the one who collect flower
// It's not flower job to collect itself anyways
public class Player : MonoBehaviour
{
    // Player need to know GameManager
    GameManager gm;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Tempolarly Flower local variable
            Flower f;

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert mouse position to world space
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero); // Shoot a raycast

            // if raycast hit nothing, return
            if (hit.collider == null) return;
            
            // if raycast hit flower (GameObject have Component of type "Flower")
            // Then do the following
            // (Ploymorphism)
            if (hit.collider.TryGetComponent<Flower>(out f)) // if true, f now store the hit flower
            {
                // (Abstraction) Player try to collect the flower, dont need to know how it process work.
                if (f.TryCollect())
                {
                    // If sucessful, add flower to the collection
                    // then deactivate the gameobject
                    gm.AddFlowerToCollection(f); // (Abstraction) Player don't need to know the compilication of adding to list in GM
                    f.gameObject.SetActive(false);
                }
            }   
        }
    }
}
