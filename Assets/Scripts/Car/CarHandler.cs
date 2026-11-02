using System;
using System.IO.IsolatedStorage;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Transform gameModel;
    float accelerationMultiplier = 3;
    float brakesMultiplier = 15;
    float steeringMultiplier = 5;

    float maxSteervelocity = 2;
    float maxForwardveocity = 30;

    Vector2 input = Vector2.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate car mdoel when turning
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);
    }

    private void FixedUpdate()
    {
        //Applying Acceleration
        if (input.y > 0)
            Accelerate();
        else
            rb.linearDamping = 0.2f;

        //Apply Brakes
        if (input.y < 0)
            Brake();

        Steer();

        //Force the car not to go backwards
        if (rb.linearVelocity.z <= 0)
            rb.linearVelocity = Vector3.zero;
    }

    void Accelerate()
    {
        rb.linearDamping = 0;

        //Stay within the speed limit
        if (rb.linearVelocity.z >= maxForwardveocity)
            return;

        rb.AddForce(rb.transform.forward * accelerationMultiplier * input.y);
    }

    void Brake()
    {
        //Dont brake unless the car is moving forward
       if(rb.linearVelocity.z <= 0)
        {
            return;
        }
        rb.AddForce(rb.transform.forward * brakesMultiplier * input.y);

    }

    void Steer()
    {
        if (Mathf.Abs(input.x) > 0)
        {
            //Move the car sideways
            float speedBaseSteerLimit = rb.linearVelocity.z / 5.0f;
            speedBaseSteerLimit = Mathf.Clamp01(speedBaseSteerLimit);

            rb.AddForce(rb.transform.right * steeringMultiplier * input.x * speedBaseSteerLimit); //code for steeing + steering senstivity

            //normalize X velocity
            float normalizedX = rb.linearVelocity.x / maxSteervelocity;

            //Ensure that we dont allow it to get bigger than 1 in magnitued.
            normalizedX = Mathf.Clamp(normalizedX, -1.0f, 1.0f);

            rb.linearVelocity = new Vector3(normalizedX * maxSteervelocity, 0, rb.linearVelocity.z);
        }
        else
        {
            //auto centers car when button is released
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0, 0, rb.linearVelocity.z), Time.fixedDeltaTime * 3);
        }
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();

        input = inputVector;
    }
}
