using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private GameObject chunkPrefab;
	[SerializeField] private int baseMapSize;
	[SerializeField] private float updateRate;
	private List<GameObject> chunks;
	private Vector3 lastChunkPosition;

	private void Start()
	{
		chunks = new List<GameObject>();

		lastChunkPosition = Vector3.zero;

		for (int i = 0; i < baseMapSize; i++)
		{
			CreateChunk(i);
			MoveChunk(i);
		}

		StartCoroutine(CheckForAvailableChunks());
	}

	private IEnumerator CheckForAvailableChunks()
	{
		while(true)
		{
			//bool isAvailable = chunks.Find(chunk => chunk.activeInHierarchy == false);
			GameObject temp = chunks.Find(chunk => chunk.activeInHierarchy == false);
			if (temp)
			{
				MoveChunk(temp);
				temp.SetActive(true);	
			}

			yield return new WaitForSeconds(updateRate);
		}
	}

	private void CreateChunk(int index)
	{
		GameObject chunk = Instantiate(chunkPrefab, transform);
		chunks.Add(chunk);
	}

	private void MoveChunk(int index)
	{
		chunks[index].transform.position = GetNewPosition(index);
		lastChunkPosition = chunks[index].transform.position;
	}

	private void MoveChunk(GameObject chunk)
	{
		chunk.transform.position = GetNewPosition();
		lastChunkPosition = chunk.transform.position;
	}

	private Vector3 GetNewPosition(int index)
	{
		return new Vector3(0, index * 20.4f);
	}

	private Vector3 GetNewPosition()
	{
		Vector3 newPosition = lastChunkPosition == Vector3.zero ? Vector3.zero : lastChunkPosition + new Vector3(0, 20.4f);
		return newPosition;
	}
}
