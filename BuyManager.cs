using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
	public static BuyManager manager;

	void Awake()
	{
		if (manager != null)
		{
			Debug.LogError("More than one BuyManager in scene!");
			return;
		}
		manager = this;
	}

	private Transform allytospawn;

	public void SetAllyToSpawn(Transform ally)
	{
		allytospawn=ally;
	}

	public Transform GetAllyToSpawn()
	{
		return allytospawn;
	}
}
