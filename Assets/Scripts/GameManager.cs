using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private MapManager mapManager;
    private RiderManager riderManager;
	// Use this for initialization
	void Start () {
        mapManager = GetComponent<MapManager>();
        riderManager = GetComponent<RiderManager>();

        mapManager.CreateMap();
        riderManager.CreateRider();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
