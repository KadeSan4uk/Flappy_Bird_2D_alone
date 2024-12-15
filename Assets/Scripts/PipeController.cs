using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private Transform _spawntTransformPoint;
    [SerializeField] private Transform _destroyTransformPoint;
    [SerializeField] private Transform _topPipePrefab;
    [SerializeField] private Transform _bottomPipePrefab;
    [SerializeField] private float _movePipeSpeed = 2f;
    [SerializeField] private float _generationPipeDilay;
    [SerializeField] private float _maxHeightGeneration = 3f;
    [SerializeField] private float _minHeightGeneration = 0f;

    private float _nexGenerationPipesTime;

    private List<Transform> _pipes = new();

    private void Update()
    {
        if (_nexGenerationPipesTime < Time.time)
            GenerationPipes();

        MovePipes();
    }

    private void MovePipes()
    {
        for (int i = _pipes.Count - 1; i >= 0; i--)
        {
            Transform pipe = _pipes[i];

            pipe.Translate(Vector2.left * _movePipeSpeed * Time.deltaTime);

            if (pipe.position.x <= _destroyTransformPoint.position.x)
            {
                _pipes.RemoveAt(i);
                Destroy(pipe.gameObject);
            }
        }
    }

    private void GenerationPipes()
    {
        _nexGenerationPipesTime = Time.time + _generationPipeDilay;

        float checkChance = Random.Range(0, 101);

        if (checkChance < 20)
        {
            var randomY = Random.Range(_minHeightGeneration, _maxHeightGeneration);

            InstantiatePipe(_topPipePrefab, randomY);
        }
        else if (checkChance > 80)
        {
            var randomY = Random.Range(_minHeightGeneration, _maxHeightGeneration);

            InstantiatePipe(_bottomPipePrefab, randomY);
        }
        else
        {
            var randomY = Random.Range(_minHeightGeneration, _maxHeightGeneration);

            InstantiatePipe(_topPipePrefab, randomY);
            InstantiatePipe(_bottomPipePrefab, randomY);
        }
    }

    private void InstantiatePipe(Transform pipePrefab, float randomY)
    {
        var pipe = Instantiate(pipePrefab);
        pipe.position = _spawntTransformPoint.position;
        var pipePosition = pipe.position;
        pipePosition.y = randomY;
        pipe.position = pipePosition;
        _pipes.Add(pipe);
    }
}
