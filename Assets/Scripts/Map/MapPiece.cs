using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MonoBehaviour
{
    public Action EndReached;
    private void OnTriggerEnter2D(Collider2D other)
    {
        EndReached?.Invoke();
    }
}