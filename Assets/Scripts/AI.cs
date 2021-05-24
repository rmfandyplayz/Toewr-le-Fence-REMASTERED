using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    NavMeshAgent agent;
    Transform Destintation;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        Destintation = GameObject.Find("Waypoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(Destintation.position);
	}
}
