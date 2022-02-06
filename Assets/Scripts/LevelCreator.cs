using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private JumpAmplifier _jumpAmplifier;
    [SerializeField] private int _humanTowerCount;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float roadLength = _pathCreator.path.length;
        float distanceBetweeenTower = roadLength / _humanTowerCount;

        float distanceTrabelled = 0;

        Vector3 spawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTrabelled += distanceBetweeenTower;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTrabelled, EndOfPathInstruction.Stop);
            var apmlifierSpawnPoint = _pathCreator.path.GetPointAtDistance(distanceTrabelled - 2f, EndOfPathInstruction.Stop);

            var amplifire = Instantiate(_jumpAmplifier, apmlifierSpawnPoint, Quaternion.FromToRotation(apmlifierSpawnPoint, spawnPoint));

            var tower = Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);

            amplifire.SetJumpModifire(tower.HumansCount());
        }

    }
}
