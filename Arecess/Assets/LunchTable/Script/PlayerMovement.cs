using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView pv;
    private CharacterController characterController;
    public float movementspeed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
       characterController= GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BasicMovement()
    {

    }
}
