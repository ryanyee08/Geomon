using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public LayerMask clickable;

    private NavMeshAgent agent;
    Camera main;

    // Used to check to see if a dialogue is currently being displayed
    [SerializeField]
    GameObject DialogueUI;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) & DialogueUI.activeSelf == false)
        {
            MoveToTarget();
        }
    }

    public void MoveToTarget()
    {
        RaycastHit hit;
        Debug.Log("Casting a ray");
        Debug.Log("Clickable layer is " + clickable.value);


        if (Physics.Raycast(main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            int collisionMask = (1 << hit.transform.gameObject.layer);

            if ((clickable.value & collisionMask) > 0)
            {
                agent.destination = hit.point;
                Debug.Log("layer collided is " + hit.transform.gameObject.layer);
            }
            else
                Debug.Log("Clicked a layer that has no navigation");
        }

    }
}
