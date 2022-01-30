using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent (typeof(Rigidbody))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PathCreator _pathCreator;

    private Rigidbody _rigitbody;
    private float _distanceTravelled;

    private void Start()
    {
        _rigitbody = GetComponent<Rigidbody>();

        _rigitbody.MovePosition(_pathCreator.path.GetPointAtDistance(_distanceTravelled));
    }

    private void Update()
    {
        _distanceTravelled += Time.deltaTime * _speed;

        Vector3 nextPoint = _pathCreator.path.GetPointAtDistance(_distanceTravelled, EndOfPathInstruction.Loop);
        nextPoint.y = transform.position.y;

        transform.LookAt(nextPoint);

        _rigitbody.MovePosition(nextPoint);
    }
}
