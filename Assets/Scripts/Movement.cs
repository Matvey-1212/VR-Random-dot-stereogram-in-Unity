using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float speed;
    public XRNode inputSource;
    Camera camera;
    private Vector2 inputAxis;
    private XRRig rig;
    private CharacterController character;
    
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
        camera = Camera.main;
    }

    
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        camera = Camera.main;
    }

    private void FixedUpdate()
    {
        
        Quaternion headYaw = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Vector3 direction =  headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);
    }
}
