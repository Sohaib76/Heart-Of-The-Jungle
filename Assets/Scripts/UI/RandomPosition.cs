using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{

    private Vector3 StartPos;
    private Vector3 randomPos;

    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private float interval = 0.75f;

    float TimeSinceRandomRefresh = 9999.0f;
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        setRandomPos(interval);
        RandomLerpPos(speed);
    }


    void RandomLerpPos(float speed)
    {
        Vector3 newPos = Vector3.Lerp(transform.position, randomPos, Time.deltaTime * speed);
        transform.position = newPos;
    }
    void setRandomPos(float interval)
    {
        if (TimeSinceRandomRefresh > interval)
        {
            randomPos = Random.insideUnitSphere;
            randomPos += StartPos;
            Debug.Log(randomPos);
            TimeSinceRandomRefresh = 0.0f;
        }
        else
        {
            TimeSinceRandomRefresh += Time.deltaTime;
        }
    }
}
