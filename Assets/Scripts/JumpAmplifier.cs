using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAmplifier : MonoBehaviour
{
    [SerializeField] private int _jumpModifire;

    public void SetJumpModifire (int modifire)
    {
        _jumpModifire = modifire;
    }

    public int GetJumpModifire ()
    {
        return _jumpModifire;
    }
}
