using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Arrow : MonoBehaviour
{
    public enum EDirection
    {
        Right = 0,
        Up = 1,
        Left = 2,
        Down = 3,
        None = 4,
    }

    private EDirection _direction = EDirection.Right;
    public EDirection Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            UpdateDirection();
        } 
    }

    public void RandomizeDirection()
    {
        Direction = (EDirection)Random.Range(0, 4);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        RandomizeDirection();
    }
    
    void UpdateDirection()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * (float) Math.PI / 2f * (float) Direction);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }
}
