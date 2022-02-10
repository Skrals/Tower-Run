using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceChecker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkCollider;

    private List<Human> _humans;

    public UnityAction<int> HumanAdded;

    private void Start()
    {
        _humans = new List<Human>();

        Vector3 spawnPoint = transform.position;

        _humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));
        _humans[0].Run();
        HumanAdded?.Invoke(_humans.Count);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();

            List<Human> collectedHumans = collisionTower.CollectHuman(_distanceChecker, _fixationMaxDistance);

            if (collectedHumans != null)
            {
                _humans[0].StopRun();
                for (int i = collectedHumans.Count - 1; i >= 0; i--)
                {
                    Human insertHuman = collectedHumans[i];
                    var humanRigitbody = insertHuman.GetComponent<Rigidbody>();
                    humanRigitbody.isKinematic = true;
                    humanRigitbody.freezeRotation = true;
                    humanRigitbody.angularVelocity = new Vector3(0, 0, 0);
                    insertHuman.GetComponent<BoxCollider>().enabled = false;
                    InsertHuman(insertHuman);
                    DisplaceChecker(insertHuman);
                }
                HumanAdded?.Invoke(_humans.Count);
                _humans[0].Run();
            }
            collisionTower.Break();
        }
    }

    private void InsertHuman(Human collectedHuman)
    {
        _humans.Insert(0, collectedHuman);
        SetHumanPosition(collectedHuman);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }

    private void DisplaceChecker(Human human)
    {
        float displaceScale = 1.5f;
        Vector3 distanceCheckerNewPosition = _distanceChecker.position;
        distanceCheckerNewPosition.y -= human.transform.localScale.y * displaceScale;
        _distanceChecker.position = distanceCheckerNewPosition;
        _checkCollider.center -= new Vector3(0, displaceScale,0) ;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Finish finish))
        {
            _humans[0].StopRun();
        }
    }
}
