using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tilemap : MonoBehaviour
{
    #region singleton
    public static Tilemap instance = null;
    void Awake()
    {
        instance = this;
    }
    #endregion 
    public TileType[] tileTypes;
    public GameObject selectedUnit;
    //Add all units in the scene to this array
    public List<GameObject> allUnits = new List<GameObject>();

    int[,] tiles;

    int mapSizeX;
    int mapSizeY;

    Node[,] graph;
    
    // Start is called before the first frame update
    void Start()
    {
        mapSizeX = Random.Range(9, 15);
        mapSizeY = Random.Range(9, 15);
        // set up selected unit variables
        foreach (GameObject unit in allUnits)
        {
            unit.GetComponent<Character>().tileX = (int)unit.transform.position.x;
            unit.GetComponent<Character>().tileY = (int)unit.transform.position.y;
            unit.GetComponent<Character>().map = this;
        }

        GenerateMap();
        GeneratePathfindingGraph();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap()
    {
        // creates map
        tiles = new int[mapSizeX, mapSizeY];

        // initializes map to grassland
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }

        // creates a U-shaped mountain range
        tiles[4, 4] = 4;
        tiles[5, 4] = 3;
        tiles[6, 4] = 3;
        tiles[7, 4] = 3;
        tiles[8, 4] = 4;
        
        tiles[4, 5] = 3;
        tiles[4, 6] = 3;
        tiles[8, 5] = 3;
        tiles[8, 6] = 3;

        for (int x = 3; x <= 5; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tiles[x, y] = 2;
            }
        }

        // spawn the map visuals (prefabs)
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tileToSpawn = tileTypes[tiles[x, y]];
                GameObject gameObj = (GameObject)Instantiate(tileToSpawn.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
                gameObj.tag = "Tile";

                ClickableTile clickTile = gameObj.GetComponent<ClickableTile>();
                clickTile.tileX = x;
                clickTile.tileY = y;
                clickTile.map = this;
            }
        }
    }

    public Vector3 TileToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tiles[targetX, targetY]];

        float cost = tt.movementCost;

        if(sourceX != targetX && sourceY != targetY)
        {
            // we are moving diagonally, fudge cost for tie-breaker
            cost += 0.001f;
            // purely aesthetic, so it doesnt look stupid
        }
        return cost;
    }

    void GeneratePathfindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }
         for (int x = 0; x < mapSizeX; x++)
                {
            for (int y = 0; y < mapSizeY; y++)
             {
                // try left
                if(x > 0)
                {
                    graph[x, y].tileEdges.Add(graph[x - 1, y]);
                    if (y > 0)
                    {
                        graph[x, y].tileEdges.Add(graph[x - 1, y - 1]);
                    }
                    if (y < mapSizeY - 1)
                    {
                        graph[x, y].tileEdges.Add(graph[x - 1, y + 1]);
                    }
                }

                //try right
                if(x < mapSizeX - 1)
                {
                    graph[x, y].tileEdges.Add(graph[x + 1, y]);
                    if (y > 0)
                    {
                        graph[x, y].tileEdges.Add(graph[x+1, y - 1]);
                    }
                    if (y < mapSizeY - 1)
                    {
                        graph[x, y].tileEdges.Add(graph[x+1, y + 1]);
                    }
                }

                // try straight up and down
                if (y > 0)
                {
                    graph[x, y].tileEdges.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].tileEdges.Add(graph[x, y + 1]);
                }

                //diagonal
                if(x < mapSizeX - 1 && y < mapSizeY - 1)
                {
                    graph[x, y].tileEdges.Add(graph[x + 1, y + 1]);
                }
                if(x != 0 && y != 0)
                {
                    graph[x, y].tileEdges.Add(graph[x - 1, y - 1]);
                }
            }
        }
    }

    // DIJKSTRA'S ALGORITHM
    public void MoveSelectedUnit(int x, int y)
    {
        selectedUnit.GetComponent<Character>().currentPath = null;
        //selectedUnit.transform.position = TileToWorldCoord(x, y);
        Dictionary<Node, float> distance = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();

        // the list of nodes that haven't been checked yet
        List<Node> unvisitedNodes = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Character>().tileX, selectedUnit.GetComponent<Character>().tileY];
        Node target = graph[x, y];
        distance[source] = 0;
        previous[source] = null;

        // initialize everything to have INFINITY distance, since
        // we dont know any better right now. Also, it's possible
        // that some nodes CANT be reached form the source,
        // which would make INFINITY a reasonable value.

        foreach (Node vertex in graph)
        {
            if(vertex != source)
            {
                distance[vertex] = Mathf.Infinity;
                previous[vertex] = null;
            }

            unvisitedNodes.Add(vertex);
        }

        while(unvisitedNodes.Count > 0)
        {
            // u = unvisited node with the smallest distance
            Node u = null;
            foreach (Node potentialU in unvisitedNodes)
            {
                if(u == null || distance[potentialU] < distance[u])
                {
                    u = potentialU;
                }
            }

            if(u == target)
            {
                break;      // exit the while loop
            }
            unvisitedNodes.Remove(u);

            foreach (Node v in u.tileEdges)
            {
                float tempDistance = distance[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if(tempDistance < distance[v])
                {
                    distance[v] = tempDistance;
                    previous[v] = u;
                }
            }
        }

        // if we get here, we either found the shortest route to the target
        // or there is no route at all

        if(previous[target] == null)
        {
            // no route to the target
            return;
        }

        // we have a route to the target
        // step through the "previous" chain and add it to our path
        List<Node> currentPath = new List<Node>();
        Node curr = target;

        while(curr != null){
            currentPath.Add(curr);
            curr = previous[curr];
        }

        // Right now, currentPath describe the route from the target to the source
        // So it needs to be inverted!
        currentPath.Reverse();

        selectedUnit.GetComponent<Character>().currentPath = currentPath;
    }






    // https://www.youtube.com/watch?v=au6_95iI_gE&index=6&list=PLbghT7MmckI55gwJLrDz0UtNfo9oC0K1Q
    // pick up at 29:26
}
