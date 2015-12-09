using UnityEngine;
using System.Collections;

public class GoToPointSmooth : MonoBehaviour
{

    [SerializeField]
    private float maxSpeed = 5;

    /** hoe zwaarder het object, hoe slechter hij kan bijsturen */
    [SerializeField]
    private float mass = 20;

    /** boolean of het object de richting op kijkt waar we naar toe bewegen */
    [SerializeField]
    private bool followPath = false;

    /** Vector om onze huidige velocity bij te houden (x en y stap) */
    private Vector3 currentVelocity;
    /** Vector om onze huidige positie bij te houden (x en y positie) */
    private Vector3 currentPosition;
    /** Vector om de locatie bij te houden waar we heen willen */
    private Vector3 currentTarget;

    void Start()
    {
        // we starten zonder beweging (geen velocity)
        currentVelocity = new Vector3();
        // we nemen de huidige positie over in een eigen variabele
        currentPosition = transform.position;
    }

    // Elke frametick kijken we hoe we moeten sturen
    void Update()
    {
        Seek();
    }

    public void setTarget(Vector3 target)
    {
        currentTarget = target;
    }

    // van buitenaf kun je de huidige target uitlezen
    public Vector3 Target
    {
        get
        {
            return currentTarget;
        }
    }

    void Seek()
    {

        // we berekenen eerst de afstand/Vector tot de 'target' (in dit voorbeeld het mikpunt)		
        Vector3 desiredStep = currentTarget - currentPosition;

        // deze desiredStep mag niet groter zijn dan de maximale Speed
        //
        // als een vector ge'normalized' is .. dan houdt hij dezelfde richting
        // maar zijn lengte/magnitude is 1
        desiredStep.Normalize();

        // als je deze genormaliseerde vector weer vermenigvuldigt met de maximale snelheid dan
        // wordt de lengte van deze Vector maxSpeed (aangezien 1 x maxSpeed = maxSpeed)
        // de x en y van deze Vector wordt zo vanzelf omgerekend
        Vector3 desiredVelocity = desiredStep * maxSpeed;

        // bereken wat de Vector moet zijn om bij te sturen om bij de desiredVelocity te komen
        Vector3 steeringForce = desiredVelocity - currentVelocity;

        // uiteindelijk voegen we de steering force toe maar wel gedeeld door de 'mass'
        // hierdoor gaat hij niet in een rechte lijn naar de target
        // hoe zwaarder het object des te groter de bocht
        currentVelocity += steeringForce / mass;

        // Als laatste updaten we de positie door daar onze beweging (velocity) bij op te tellen
        currentPosition += currentVelocity * Time.deltaTime;
        transform.position = currentPosition;

        // roteer het object in de goede richting
        if (followPath)
        {
            float angle = Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
