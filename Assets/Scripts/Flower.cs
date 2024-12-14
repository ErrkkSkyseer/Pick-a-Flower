using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class of the flower
// Since flower is abstract, This script cannot be attach to gameobject
// Because there are no real 'flower' gameObject
// Every flower will be in some type (i.e. Rose, Sunflower) 
// This is unnessery but it make the code safer to use
public abstract class Flower : MonoBehaviour
{
    [Header("Reference")]
    protected SpriteRenderer spriteRenderer;

    //Sprite of the flower
    [SerializeField] Sprite sprite;

    //Hit Point randomizer
    [SerializeField] MinMaxInt minMaxHP;
    [SerializeField] int maxHP;
    int currentHP;

    // Type of this flower
    FlowerType type;

    // Properties for type, provided with public getter and protected setter
    // Type of flower is an information that other class need to know
    // But should not be able to modifided
    public FlowerType Type { get => type; protected set => type = value; }
    // getter for MaxHP (Encapsulation)
    public int MaxHP { get => maxHP;}

    // Start function need to be overrided in derived class
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        //Random hp
        maxHP = minMaxHP.Roll();
        currentHP = maxHP;
    }
    
    // Flower need to be collected by player
    // return true if this flower is succesfully collected
    public bool TryCollect()
    {
        currentHP--;
        return currentHP <= 0;
    }
}
