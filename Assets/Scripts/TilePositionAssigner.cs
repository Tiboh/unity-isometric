using UnityEngine;
using System.Collections.Generic;
using System;

public class TilePositionAssigner : MonoBehaviour
{
    // Reference to the tile GameObjects
    private CustomTile[] _tiles;

    void Start()
    {
        _tiles = FindObjectsOfType<CustomTile>();
        AssignTilePositions();
        AreTilePositionsUnique(_tiles);
    }

    // Convert isometric position, to cartesian position
    // (0 -0.25 0) should equal (0 0 0)
    // (0.5 0 0) should equal (1 0 0)
    // (-0.5 -0.5 0) should equal (-1 0 0)
    // (-0.5 0 0) should equal (0 1 0)
    // (0.5 -0.5 0) should equal (0 -1 0)

    // (0 0 1) should equal (0 0 1)
    // (-0.5 0.25 1) should equal (0 1 1)
    // (0.5 0.25 1) should equal (1 0 1)
    // (-0.5 -0.25 1) should equal (-1 0 1)
    // (0.5 -0.25 1) should equal (0 -1 1)
    // (0 0.5 1) should equal (1 1 1)
    // (0 -0.5 1) should equal (-1 -1 1)
    // (-1 0 1) should equal (-1 1 1)
    // (1 0 1) should equal (1 -1 1)

    // (0.5 -2 0) should equal (-3 -4 0)
    // (0.5 -1.75 1) should equal (-3 -4 1)
    // (0 -2.25 0) should equal (-4 -4 0)
    // (0 -2 1) should equal (-4 -4 1)

    // (-0.5 0.5 2) should equal (0 1 2)
    // (-1 0.25 2) should equal (-1 1 2)
    // (0 0.75 2) should equal (1 1 2)
    private void AssignTilePositions()
    {
        foreach (CustomTile tile in _tiles)
        {
            Vector3 position = tile.transform.position;

            // {{1,2,-0.5},{-1,2,-0.5},{0,0,1}}
            // CeilToInt to avoid the -0.5 offset on x and y due to the grid
            int cartesianX = (int)Mathf.CeilToInt(1f * position.x + 2f * position.y - 0.5f * position.z);
            int cartesianY = (int)Mathf.CeilToInt(-1f * position.x + 2f * position.y - 0.5f * position.z);
            int cartesianZ = (int)Mathf.CeilToInt(position.z);  // Z remains unchanged

            tile.cartesianPosition = new Vector3Int(cartesianX, cartesianY, cartesianZ);
        }
    }

    // Check if each tile has a unique position
    public bool AreTilePositionsUnique(CustomTile[] tiles)
    {
        HashSet<Vector3Int> uniquePositions = new HashSet<Vector3Int>();

        foreach (CustomTile tile in tiles)
        {
            Vector3Int position = tile.GetComponent<CustomTile>().cartesianPosition;
            if (uniquePositions.Contains(position))
            {
                Debug.Log("Duplicate position found: " + position);
                return false;
            }
            uniquePositions.Add(position);
        }

        Debug.Log("All tiles have unique positions.");
        return true;
    }
}

