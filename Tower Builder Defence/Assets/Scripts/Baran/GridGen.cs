using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGen : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] Transform lastGrid;
    void Start()
    {
        lastGrid = transform;
        GridCreator();
    }
    public void GridCreator()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject grid_ = Instantiate(grid, new Vector2(lastGrid.position.x + 1, lastGrid.position.y), Quaternion.identity);
            lastGrid = grid_.transform;
            lastGrid.name = "Grid";
            lastGrid.parent = this.gameObject.transform;
        }
    }
}
