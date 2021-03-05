using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class NPCMovement : NetworkBehaviour
{
    public int xMovementRadius = 6;
    public int yMovementRadius = 6;
    public int movementUpdateInterval;


    private UnityEngine.AI.NavMeshAgent navmesh;
    private System.Random rand;
    private int frameCount;

    [SyncVar(hook = nameof(UpdateNavDestination))]
    public Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
         navmesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
         movementUpdateInterval = Random.Range(100,200);
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer) {
            SetNewPosition();
        }
       
    }


    private void SetNewPosition() {
        if(frameCount % movementUpdateInterval == 0) {
            Vector3 move = new Vector3(Random.Range(-xMovementRadius,xMovementRadius), 0,Random.Range(-yMovementRadius,yMovementRadius) );
            destination = transform.position + move;
            frameCount = 0;
            }
            frameCount++;
        }

    private void UpdateNavDestination(Vector3 oldVal, Vector3 newVal) {

        navmesh.destination = destination;

    }
}
