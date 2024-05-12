using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePickerGameObject : MonoBehaviour
{
    public CustomTile SelectedCustomTile;
    public bool IsDisabled;
    void Start()
    {
        IsDisabled = false;
    }

    void Update()
    {
        if (!IsDisabled)
        {
            Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            List<Collider2D> hitColliders = Physics2D.OverlapPointAll(mousePosition2D).Reverse().ToList();
            if (hitColliders.Count > 0)
            {
                CustomTile newSelectedTile = hitColliders[0].GetComponent<CustomTile>();
                if (newSelectedTile != null && newSelectedTile != SelectedCustomTile)
                {
                    UpdateTilePickerData(newSelectedTile);
                }
            }
        }
    }

    private void UpdateTilePickerData(CustomTile newCustomTile)
    {
        if (newCustomTile != SelectedCustomTile)
        {
            if (SelectedCustomTile != null)
            {
                SelectedCustomTile.ChangeColor(Color.white);
            }
            SelectedCustomTile = newCustomTile;
            SelectedCustomTile.ChangeColor(Color.red);
        }
    }
}
