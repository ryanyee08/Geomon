using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    GameObject playerCharacter;

    Vector3 cameraOffset = new Vector3(0, 10, -10);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerCharacter.transform.position + cameraOffset;
    }
}
