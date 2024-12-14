using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public struct MinMaxInt //Struct for easily input a range of number for randomizing (int) 
{
    public int min, max;

    public int Roll() => min <= max ? UnityEngine.Random.Range(min, max) : 0;
    
}

[System.Serializable]
public struct MinMaxFloat //Struct for easily input a range of number for randomizing (float) 
{
    public float min, max;

    public float Roll() => min <= max ? UnityEngine.Random.Range(min, max) : 0;
}

public enum FlowerType // enum to spcific the type of the flower
{
    Rose, Violet, Sunflower, Suzuran
}

// Gamemanager class, manage most of the game logic
public class GameManager : MonoBehaviour
{
    // Main Collection of GameObject (that is flower)
    Dictionary<FlowerType, List<GameObject>> flowerDic = 
        new Dictionary<FlowerType, List<GameObject>>();

    [Header("Game Area")]
    [SerializeField] Vector2 gameArea;
    [SerializeField] Vector2 gameAreaOffset;

    // Frequency use virable declear as static
    public static int LEVEL = 1;
    public static int SCORE = 0;


    //Flower prefabs use for spawning new flowers
    [SerializeField] GameObject[] flowerPrefabs;

    //Some variable to keep track of some information
    int totalFlower;
    int totalCollectedFlower;
    [SerializeField] int flowerGoal;

    [Header("Spawning")]
    [SerializeField] MinMaxFloat spawnDelay;
    [SerializeField] MinMaxInt spawnBatch;

    [SerializeField] float delayMultipiler;
    [SerializeField] float batchMultipiler;

    bool onCooldown; // for coroutine use

    // Properties for external use (Encapsulation)
    // only provided access to nessery information
    public int RoseCounter { get => flowerDic[FlowerType.Rose].Count; }
    public int VioletCounter { get => flowerDic[FlowerType.Violet].Count; }
    public int SunflowerCounter { get => flowerDic[FlowerType.Sunflower].Count; }
    public int SuzuranCounter { get => flowerDic[FlowerType.Suzuran].Count; }
    public float LeftBound { get => gameAreaOffset.x; }
    public float RightBound { get => gameAreaOffset.x + gameArea.x; }
    public float LowerBound { get => gameAreaOffset.y; }
    public float UpperBound { get => gameAreaOffset.y + gameArea.y; }
    

    private void Start()
    {
        //Creating new FlowerType key that leads to new List of GameObject
        flowerDic[FlowerType.Rose] = new List<GameObject>();
        flowerDic[FlowerType.Violet] = new List<GameObject>();
        flowerDic[FlowerType.Sunflower] = new List<GameObject>();
        flowerDic[FlowerType.Suzuran] = new List<GameObject>();

        StartCoroutine(SpawnFlower(spawnDelay.Roll(),spawnBatch.Roll())); // spawn some flowers
    }

    private void Update()
    {
        //Spawn Flower Routine
        if (!onCooldown)
        {
            StartCoroutine(SpawnFlower(spawnDelay.Roll(), spawnBatch.Roll()));
        }

        // Calculate stuff when next level is reached
        if (totalCollectedFlower >= flowerGoal)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        LEVEL++;
        flowerGoal += flowerGoal + Mathf.RoundToInt(Mathf.Log10(LEVEL) * 10);

        spawnDelay.max -= delayMultipiler * Mathf.Log10(LEVEL);
        if (spawnDelay.max <= spawnDelay.min)
            spawnDelay.max = spawnDelay.min + 0.1f;

        spawnBatch.min += Mathf.RoundToInt(0.6f * LEVEL * batchMultipiler);
        spawnBatch.max += Mathf.RoundToInt(LEVEL * batchMultipiler);
    }

    // Spawn the flower from prefabs coroutine
    IEnumerator SpawnFlower(float time, int batch)
    {
        onCooldown = true;

        for (int i = 0; i < batch; i++)
            SpawnFlowerRandom();

        yield return new WaitForSeconds(time);
        onCooldown = false;
    }

    // Spawn random flower from prefabs
    private void SpawnFlowerRandom()
    {
        totalFlower++;

        Vector2 begin = gameAreaOffset;
        Vector2 end = gameArea + gameAreaOffset;

        float randX = UnityEngine.Random.Range(begin.x, end.x);
        float randY = UnityEngine.Random.Range(begin.y, end.y);

        Vector3 spawnPos = new Vector2(randX, randY);

        Quaternion randomRotation =
            Quaternion.Euler(
                0,
                0,
                UnityEngine.Random.Range(0f, 360f));

        int flowerType = UnityEngine.Random.Range(0, flowerPrefabs.Length - 1);

        int suzuranRandom = UnityEngine.Random.Range(0, 16);

        if (suzuranRandom == 8)
        {
            Instantiate(flowerPrefabs[^1], spawnPos, Quaternion.identity);
        }
        else
        {
            Instantiate(flowerPrefabs[flowerType], spawnPos, randomRotation);

        }
    }

    // For adding flower to collection (the big dictionary)
    // The function will be called by player and passing the flower into this function
    // In this way, the GameManager will get the flower that's collected via Player
    // And add it to the collection
    public void AddFlowerToCollection(Flower flower)
    {
        totalCollectedFlower++;

        //Calculate score here, passing the flower into the function and check its type.
        CalculateScore(flower);

        //Add flower into its respected list
        flowerDic[flower.Type].Add(flower.gameObject);
    }

    private void CalculateScore(Flower flower)
    {
        switch (flower.Type)
        {
            case FlowerType.Rose:
                SCORE += Mathf.RoundToInt(flower.MaxHP * LEVEL * 0.25f + 1);
                break;
            case FlowerType.Violet:
                SCORE += Mathf.RoundToInt(flower.MaxHP * LEVEL * 0.4f + 1);
                break;
            case FlowerType.Sunflower:
                SCORE += Mathf.RoundToInt(flower.MaxHP * LEVEL * 0.3f + 1);
                break;
            case FlowerType.Suzuran:
                SCORE += Mathf.RoundToInt(flower.MaxHP * LEVEL * 0.5f + 1);
                break;
               
        }
    }
    
    // Draw the yellow square
    private void OnDrawGizmos()
    {
        Vector2 center = new Vector2
            ((gameArea.x / 2) + gameAreaOffset.x,
            (gameArea.y / 2) + gameAreaOffset.y);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, gameArea);
    }
}
