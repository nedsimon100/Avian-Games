using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControls : MonoBehaviour
{
    public Rigidbody rb;

    public float velocityMult;
    public float torque;


    private float Xinp;
    private float Yinp;

    void Update()
    {
        GetInputs();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBird();
    }
    private void GetInputs()
    {
        Xinp = Input.GetAxisRaw("Horizontal");
        Yinp = Input.GetAxisRaw("Vertical");
    }
    private void MoveBird()
    {
        this.transform.Rotate(0,0,- torque * Xinp);
        this.transform.Rotate( torque * Yinp,0,0);
        rb.velocity = transform.forward * velocityMult;

    }
}
