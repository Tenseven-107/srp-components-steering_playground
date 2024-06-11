using System;
using UnityEngine;
using UnityEngine.Events;

public class SteeringVehicle : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 20;

    /** hoe zwaarder het object, hoe slechter hij kan bijsturen */
    [SerializeField] private float mass = 20;

    public float Mass
    {
        set { mass = value; }
        get { return mass; }
    }

    /** boolean of het object de richting op kijkt waar we naar toe bewegen */
    [SerializeField] private bool followPath = false;
    
    [SerializeField] private bool stopAtArrive = false;
    private bool _canMove = true;

    [SerializeField] private float arrivalDistance = 0.1f;

    private Vector2 _currentVelocity;
    private Vector2 _currentPosition;

    public UnityEvent onArrived;

    [SerializeField] private Transform targetTransform;

    void Start()
    {
        _currentVelocity = new Vector2(0, 0);
        _currentPosition = transform.position;
    }

    // Elke frametick kijken we hoe we moeten sturen
    void FixedUpdate()
    {
        if (targetTransform != null) { Seek();}
    }

    
    public void SetTarget(Transform newTarget)
    {
        targetTransform = newTarget;
    }

    
    void Seek()
    {
        if ((_canMove == true && stopAtArrive == true) || stopAtArrive == false) 
        {
            // we berekenen eerst de afstand/Vector tot de 'target'		
            var desiredStep = (Vector2)targetTransform.position - _currentPosition;

            // als een vector ge'normalized' is .. dan houdt hij dezelfde richting
            // maar zijn lengte/magnitude is 1
            var desiredStepNormalized = desiredStep.normalized;

            // vermenigvuldigen met de maximale snelheid dan
            // wordt de lengte van deze Vector maxSpeed (aangezien 1 x maxSpeed = maxSpeed)
            // de x en y van deze Vector wordt zo vanzelf omgerekend
            var desiredVelocity = desiredStepNormalized * maxSpeed;

            // bereken wat de Vector moet zijn om bij te sturen om bij de desiredVelocity te komen
            var steeringStep = desiredVelocity - _currentVelocity;

            // uiteindelijk voegen we de steering force toe maar wel gedeeld door de 'mass'
            // hierdoor gaat hij niet in een rechte lijn naar de target
            // hoe zwaarder het object des te groter de bocht
            var steeringForce = steeringStep / mass;
            _currentVelocity += steeringForce;

            // Als laatste updaten we de positie door daar onze beweging (velocity) bij op te tellen
            _currentPosition += _currentVelocity * Time.deltaTime;
            transform.position = _currentPosition;
        }
        
        if (followPath)
        {
            Vector2 usedVector = targetTransform.position - transform.position;
            float angle = Mathf.Atan2(usedVector.y, usedVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Vector2.Distance(targetTransform.position, _currentPosition) < arrivalDistance)
        {
            onArrived.Invoke();
            _canMove = false;
        }
        else
        {
            _canMove = true;
        }
    }
}
