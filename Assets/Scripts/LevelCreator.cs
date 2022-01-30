using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private int _humanTowerCount;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel ()
    {
        float roadLength = _pathCreator.path.length;
        float distanceBetweeenTower = roadLength / _humanTowerCount;

        float distanceTrabelled = 0;

        Vector3 spawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTrabelled += distanceBetweeenTower;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTrabelled, EndOfPathInstruction.Stop);

            Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);
        }
    }
}
