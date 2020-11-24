using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunk : MonoBehaviour
{
	private void OnBecameInvisible()
	{
		ReturnToPool();
	}

	private void ReturnToPool()
    {
        gameObject.SetActive(false);
	}

}
