using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using MonoGraph;

public class Vertex : IComparable<Vertex>
{
    public GameObject MapTile { get; set; }
    public int Id { get; set; }
    public Vertex(GameObject mapTile, int id)
    {
        MapTile = mapTile;
        Id = id;
    }

    public int CompareTo(Vertex other)
    {
        if (other == null) return 1;
        return Id.CompareTo(other.Id);
    }

    public override string ToString()
    {
        return String.Format("Vertex: {0}", Id);
    }
}

public class MapManager : MonoBehaviour {
    public int mapSize;
    public GameObject mapTileSprite;
    // Holds references to all of the map tile objects
    private Vertex[,] mapTiles;
    private AdjacencyListGraph<Vertex, Edge<Vertex>> mapGraph;

    static System.Random random = new System.Random();

    // Use this for initialization
    void Awake () {
        mapGraph = new AdjacencyListGraph<Vertex, Edge<Vertex>>();
        mapTiles = new Vertex[mapSize, mapSize];
    }

    public void CreateMap()
    {
        int tileID = 0;
        foreach (int x in Enumerable.Range(0, mapSize))
        {
            foreach (int y in Enumerable.Range(0, mapSize))
            {
                GameObject tile = (GameObject)Instantiate(mapTileSprite, new Vector3(x, y, 0f), Quaternion.identity);
                var vertex = new Vertex(tile, tileID);
                mapTiles[x, y] = vertex;
                mapGraph.AddVertex(vertex);
                tileID++;
            }
        }

        foreach (int x in Enumerable.Range(0, mapSize))
        {
            foreach (int y in Enumerable.Range(0, mapSize))
            {
                if (x > 0) mapGraph.AddDirectedEdge(new Edge<Vertex>(mapTiles[x, y], mapTiles[x - 1, y]));
                if (y > 0) mapGraph.AddDirectedEdge(new Edge<Vertex>(mapTiles[x, y], mapTiles[x, y - 1]));
                if (x < mapSize - 1) mapGraph.AddDirectedEdge(new Edge<Vertex>(mapTiles[x, y], mapTiles[x + 1, y]));
                if (y < mapSize - 1) mapGraph.AddDirectedEdge(new Edge<Vertex>(mapTiles[x, y], mapTiles[x, y + 1]));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(RandomNeighbor(mapTiles[5, 5]));
	}

    public Vertex RandomNeighbor(Vertex startVertex)
    {
        var edges = mapGraph.EdgeList(startVertex);

        return edges[random.Next(edges.Count)].End;
    }

    public Vertex RandomVertex()
    {
        int randX = random.Next(mapSize);
        int randY = random.Next(mapSize);

        return mapTiles[randX, randY];
    }
}
