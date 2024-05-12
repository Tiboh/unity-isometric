using UnityEngine;

public class CustomTile : MonoBehaviour
{
    public bool IsPickerable;
    // Store the tile position as Vector3Int
    public Vector3Int cartesianPosition;
    private SpriteRenderer spriteRenderer; // To hold the SpriteRenderer component

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer component found on this tile.");
        }
        IsPickerable = true;
    }

    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Color white50 = Color.white;
        white50.a = 0.5f;
        ChangeColor(white50);
        IsPickerable = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ChangeColor(Color.white);
        IsPickerable = true;
    }
}