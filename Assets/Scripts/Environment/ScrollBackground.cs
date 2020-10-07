using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Range(0.001F, 1F)]
    [SerializeField] float speed;
    float spriteSize = 1.28f;
    Vector3 startPosition;

    void Awake()
    {
        startPosition = transform.position;
        speed = 0.025F;
    }

    void Update()
    {
        var positionDelta = Mathf.Repeat(Time.time * speed, spriteSize) * new Vector3(4, 1);
        transform.position = startPosition + positionDelta;
    }
}
