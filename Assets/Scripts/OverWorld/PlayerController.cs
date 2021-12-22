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

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        main = Camera.main;

        Debug.Log("Player is located at: " + transform.position);
        // If the player is spawning in the overworld after having left it, we should put them back at their last position.
        if (GameManager.GameManagerInstance.isPlayerInBuilding == false)
        {
            transform.position = GameManager.GameManagerInstance.lastOverWorldPosition;
        }
        Debug.Log("Player is located at: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Should only move when the dialogue window is not up, also should not move when interacting with UI elements
        if (Input.GetMouseButtonDown(0) & DialogueUI.activeSelf == false & !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
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
