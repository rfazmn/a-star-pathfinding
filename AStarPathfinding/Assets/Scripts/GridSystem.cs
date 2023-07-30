using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    Node[,] grid;
    [SerializeField] Vector2Int gridSize;
    [SerializeField] float nodeRadius = .5f;
    [SerializeField] LayerMask obstacleLayer;

    float nodeSize;
    public bool drawGrid;
    void Awake()
    {
        nodeSize = nodeRadius * 2f;
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            Vector2 offset = new Vector2(gridSize.x / 2f, gridSize.y / 2f) - Vector2.one * nodeRadius;
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 gridPos = new Vector2(x, y);
                Vector3 worldPos = gridPos - offset;
                RaycastHit2D hit = Physics2D.BoxCast(worldPos, Vector2.one * nodeSize, 0f, Vector3.forward, 1f, obstacleLayer);
                Node node = new Node(new Vector2Int(x, y), worldPos, hit.transform != null);
                grid[x, y] = node;
            }
        }
    }

    public Node GetNodeWithPosition(Vector3 position)
    {
        Vector2 offset = new Vector2(gridSize.x / 2f, gridSize.y / 2f) - Vector2.one * nodeRadius;
        int x = Mathf.RoundToInt(position.x + offset.x);
        int y = Mathf.RoundToInt(position.y + offset.y);

        if (x < 0 || x >= gridSize.x || y < 0 || y >= gridSize.y)
            return null;

            return grid[x, y];
    }

    public List<Node> GetNodeNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        Vector2Int gridPos = node.gridPosition;
        for (int x = Mathf.Max(0, gridPos.x - 1); x <= Mathf.Min(gridPos.x + 1, gridSize.x - 1); x++)
        {
            for (int y = Mathf.Max(0, gridPos.y - 1); y <= Mathf.Min(gridPos.y + 1, gridSize.y - 1); y++)
            {
                if (grid[x, y] == node)
                    continue;

                neighbours.Add(grid[x, y]);
            }
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        if (!drawGrid || grid == null)
            return;

        foreach (var node in grid)
        {
            Gizmos.color = node.obstacle ? Color.red : Color.white;
            Gizmos.DrawCube(node.worldPosition, Vector3.one * .9f);
        }
    }
}
