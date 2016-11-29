using UnityEngine;
using System.Collections;

public class RiderManager : MonoBehaviour {

    public GameObject riderSprite;
    private MapManager mapManager;
    private float timeLeft;
    public float timerLength = 1;
    private GameObject rider;

    private Vertex currentVertex;

	// Use this for initialization
	void Awake () {
        mapManager = GetComponent<MapManager>();
        timeLeft = timerLength;
	}

    public void CreateRider()
    {
        var randomVertex = mapManager.RandomVertex();
        currentVertex = randomVertex;
        var mapTile = randomVertex.MapTile;

        rider = Instantiate(riderSprite, mapTile.transform.position, Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            var newVertex = mapManager.RandomNeighbor(currentVertex);
            MoveRider(newVertex);
            timeLeft = timerLength;
            
        }
    }

    public void MoveRider(Vertex newVertex)
    {
        currentVertex = newVertex;
        rider.transform.position = currentVertex.MapTile.transform.position;
    }
}
