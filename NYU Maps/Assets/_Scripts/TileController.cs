﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileController : MonoBehaviour 
{
	public float tileOffset;
	public GameObject tilePrefab;
	public GameObject tilesParent;
	public GameObject mapBackdrop;
	public Color defaultColor;
	public Color redColor;
	public Color blueColor;
	public Color greenColor;
	public List<Tile> tilesContainer;  //temp public

	Vector2 gridSize;
	bool canLightUp;
	GameObject localPlayerModelRef;
	List<Tile> litContainer;

	public void Setup()
	{
		canLightUp = false;
		litContainer = new List<Tile>();
	}

	void Update () 
	{
		//Debug.Log (localPlayerModelRef.transform.localPosition.x);
		//Debug.Log (Mathf.RoundToInt(localPlayerModelRef.transform.localPosition.x));
		if(canLightUp)
		{
			LightUpTile(GetTile (Mathf.RoundToInt(localPlayerModelRef.transform.localPosition.x), 
			                     Mathf.RoundToInt(localPlayerModelRef.transform.localPosition.z)));
		}
	}

	void LightUpTile(Tile inputTile)
	{
		for(int i = 0; i < litContainer.Count; i++)
			ResetTileColor(litContainer[i]);
		litContainer.Clear();
		inputTile.GetComponent<MeshRenderer>().material.color = greenColor;
		litContainer.Add (inputTile);
	}
	
	void ResetTileColor(Tile inputTile)
	{
		inputTile.gameObject.GetComponent<MeshRenderer>().material.color = defaultColor;
	}

	public void SetCanLightUpTile(bool val)
	{
		canLightUp = val;

		if(!val)
		{
			for(int i = 0; i < tilesContainer.Count; i++)
				ResetTileColor(tilesContainer[i]);
		}
	}

	public Vector3 ConvertLocationToPosition(Vector2 inputLoc, float height = 0f)
	{
		Vector3 pos = GetTile ((int)inputLoc.x, (int)inputLoc.y).gameObject.transform.localPosition;
		return new Vector3 (pos.x,height,pos.z);
	}

	public void SetLocalPlayerModelRef(GameObject playerModelReference)
	{
		localPlayerModelRef = playerModelReference;
	}

	public void CreateGrid(Vector2 inputGridSize)
	{
		gridSize = inputGridSize;
		for(int i = 0; i < gridSize.y; i++)
		{
			for(int j = 0; j < gridSize.x; j++)
			{
				GameObject tileInstance = Instantiate (tilePrefab);
				tileInstance.transform.parent = tilesParent.transform;
				tileInstance.transform.localPosition = new Vector3(j*(1 + tileOffset), 0, i*(1 + tileOffset));
				tileInstance.GetComponent<Tile>().TileSetup(j, i, "none", tileInstance);
				tilesContainer.Add(tileInstance.GetComponent<Tile>());
			}
		}
		mapBackdrop.transform.localScale = new Vector3 (gridSize.x * (1 + tileOffset), gridSize.y * (1 + tileOffset), 1);
		mapBackdrop.transform.localPosition = new Vector3 (gridSize.x/2 - .5f, mapBackdrop.transform.localPosition.y, gridSize.y/2 - .5f);
	}

	public Tile GetTile(int x, int y)
	{
		for(int i = 0; i < tilesContainer.Count; i++)
		{
			if(tilesContainer[i].location.x == x && tilesContainer[i].location.y == y)
				return tilesContainer[i];
		}
		return null;
	}

	public void SetGridFeatures()
	{
		//set building locations and stuff
	}


}
