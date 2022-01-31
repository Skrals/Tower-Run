using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;

    public Transform FixationPoint => _fixationPoint;

    public void DestroyHumanObj ()
    {
        Destroy(gameObject, 5);
    }
}
