using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    EnemyBehaviour mySelf;

    void Start()
    {
        target = GameManager.instance.hero;
        mySelf = GetComponent<EnemyBehaviour>();

        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    void Update()
    {
        if (!mySelf.IsDead()) //only update destination if you're alive
        {
            // Update destination if the target moves one unit
            if (Vector3.Distance(destination, target.position) > 1.0f)
            {
                destination = target.position;
                agent.destination = destination;
            }
        }
    }
}