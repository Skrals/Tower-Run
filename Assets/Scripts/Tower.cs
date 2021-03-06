using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Human[] _human;
    [SerializeField] private Vector2Int _humanInTowerRange;

    [SerializeField] private List<Human> _humanInTower;

    private void Awake()
    {
        _humanInTower = new List<Human>();
        int humanInTowerCount = Random.Range(_humanInTowerRange.x, _humanInTowerRange.y);
        SpawnHumans(humanInTowerCount);
    }

    private void SpawnHumans(int humanCount)
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < humanCount; i++)
        {
            Human spawnedHuman = _human[Random.Range(0, _human.Length)];

            _humanInTower.Add(Instantiate(spawnedHuman, spawnPoint, Quaternion.Euler(transform.rotation.eulerAngles), transform));

            _humanInTower[i].transform.localPosition = new Vector3(0, _humanInTower[i].transform.localPosition.y, 0);

            spawnPoint = _humanInTower[i].FixationPoint.position;
        }
    }

    public List<Human> CollectHuman(Transform distanceChecker, float fixationMaxDistance)
    {
        for (int i = 0; i < _humanInTower.Count; i++)
        {
            float distanceBetweenPoints = CheckDistanceY(distanceChecker, _humanInTower[i].FixationPoint.transform);

            if (distanceBetweenPoints < fixationMaxDistance)
            {
                List<Human> collectedHumans = _humanInTower.GetRange(0, i + 1);
                _humanInTower.RemoveRange(0, i + 1);
                return collectedHumans;
            }
        }
        return null;
    }

    private float CheckDistanceY(Transform distanceChecker, Transform humanFixationPoint)
    {
        Vector3 distanceCheckerY = new Vector3(0, distanceChecker.position.y, 0);
        Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);

        return Vector3.Distance(distanceCheckerY, humanFixationPointY);
    }

    public void Break()
    {
        foreach (var human in _humanInTower)
        {
            human.transform.parent = null;
            var humanRigitbody = human.GetComponent<Rigidbody>();
            humanRigitbody.isKinematic = false;
            humanRigitbody.useGravity = true;
            humanRigitbody.AddExplosionForce(4, new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5)), 0,1,ForceMode.Impulse);
            human.DestroyHumanObj();
        }
    }

    public int HumansCount()
    {
        return _humanInTower.Count;
    }

}
