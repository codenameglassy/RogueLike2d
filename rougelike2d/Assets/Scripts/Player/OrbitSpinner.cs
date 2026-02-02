using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbitSpinner : MonoBehaviour
{
    public float orbitSpeed = 90f; // degrees per second
    public float orbitRadius = 2f;

    private Vector3 startOffset;

    void Start()
    {
        // Set initial offset from player
        startOffset = transform.localPosition.normalized * orbitRadius;
        transform.localPosition = startOffset;
    }

    void Update()
    {
        // Rotate around the player
        transform.RotateAround(transform.parent.position, Vector3.forward, orbitSpeed * Time.deltaTime);
    }


}
