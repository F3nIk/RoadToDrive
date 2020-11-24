using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBuilder : MonoBehaviour
{
	[SerializeField] private List<GameObject> objects;
	[Range(0f, 0.5f)]
	[SerializeField] private float chance;
    private List<Transform> spawnPoints;

	private void Awake()
	{
		Initialize();
	}

	private void OnEnable()
	{
		Build();
	}

	private void Initialize()
	{
		spawnPoints = new List<Transform>();

		foreach (Transform child in transform)
		{
			spawnPoints.Add(child);	
		}	
	}

	private void Build()
	{
		foreach (var point in spawnPoints)
		{
			if(Random.value <= chance)
			{
				BuildInPoint(point);	
			}
		}
	}

	private void BuildInPoint(Transform point)
	{
		GameObject obj = Instantiate(GetRandomObject(), transform);
		obj.transform.position = point.position;
	}

	private GameObject GetRandomObject()
	{
		return objects[Random.Range(0, objects.Count)];
	}
}
