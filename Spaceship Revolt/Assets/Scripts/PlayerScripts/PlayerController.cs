using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player Stats")]
    [Tooltip("Base Speed of Player, number of units they move per second.")]
    [SerializeField]
    private float baseSpeed = 5f;
    [Tooltip("Speed of the Player after all other Adjustments")]
    [SerializeField]
    private float adjustedSpeed;
    [Tooltip("Multiplier Affecting Player Speed. 1f = 100%")]
    [SerializeField]
    private float speedAdjustments = 1f;
    [Tooltip("Amount of Momentum Loss everyframe. 0.05f = 5% every frame")]
    [SerializeField]
    private float momentumLoss = 0.05f;
    [Tooltip("Maximum Momentum Speed in any direction for the momentum. 10f in any direction.")]
    [SerializeField]
    private float momentumCap = 10f;

    [Tooltip("Time in seconds to Max Speed. 0.2f seconds")]
    [SerializeField]
    private float timeToMaxAcceleration = 0.2f;


    [Tooltip("Max Acceleration. 1.5x Speed at max.")]
    [SerializeField]
    private float maxAcceleration = 0.5f;

    [Tooltip("Maximum Cargo Space the Player has. 5 Units worth of Objects")]
    [SerializeField]
    private float maxCargo = 5f;



    [Header("Playtime Values")]
    [Tooltip("Current Acceleration. Calculated from timeToMaxAcceleration by Max Acceleration")]
    [SerializeField]
    private float currentAcceleration;

    [Tooltip("Current Cargo Capacity Taken by objects collected.")]
    [SerializeField]
    private float currentCargo;

    [Tooltip("List of the Objects collected by the Player")]
    [SerializeField]
    private List<GameObject> cargoList = new List<GameObject>();



    private Rigidbody2D playerReference;
    private bool canMove = true;
    private Vector2 movementVector;
    [SerializeField]
    private Vector2 momentumVector;
    public GameObject cargoDropPoint;

    void Start()
    {
        if (gameObject.GetComponent<Rigidbody2D>() != null)
        {
            playerReference = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            movementVector.x = Input.GetAxisRaw("Horizontal");
            movementVector.y = Input.GetAxisRaw("Vertical");
            calculateSpeed();
        }
        if (Input.GetMouseButtonDown(1))
        {
            DropCargo();
        }
    }

    private void calculateSpeed()
    {
        speedAdjustments = 1f;
        calculateAcceleration();
        adjustedSpeed = baseSpeed * speedAdjustments;
    }

    private void calculateAcceleration()
    {
        if (movementVector.magnitude != 0.0f)
        {
            currentAcceleration += maxAcceleration * timeToMaxAcceleration * Time.deltaTime;
   
        }
        else
        {
            currentAcceleration -= maxAcceleration * timeToMaxAcceleration * Time.deltaTime;
        }

        if(currentAcceleration >maxAcceleration)
        {
            currentAcceleration = maxAcceleration;
        }
        if(currentAcceleration < 0.0f)
        {
            currentAcceleration = 0.0f;
        }
        speedAdjustments += currentAcceleration;
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 moveValues = ((movementVector * adjustedSpeed)) * Time.fixedDeltaTime;
            playerReference.MovePosition(playerReference.position + moveValues);
            if(movementVector.magnitude != 0.0f)
            {
                momentumVector = moveValues;
            }
        }

        if(movementVector.magnitude == 0)
        {
            if (momentumVector.magnitude > 0.0f)
            {
                momentumVector = momentumVector * (1f - momentumLoss);
                CapMomentum();
            }

            if (momentumVector.magnitude < 0.01f)
            {
                momentumVector = Vector2.zero;
                playerReference.angularVelocity = 0f;
            }
            playerReference.MovePosition(playerReference.position + momentumVector);
        }


    }

    private void CapMomentum()
    {
        if(momentumVector.magnitude > momentumCap)
        {
            Vector2.ClampMagnitude(momentumVector, momentumCap);
        }
    }



    public bool TryAddCargo(GameObject objectReference)
    {
        CargoStat tempStat = objectReference.GetComponent<CargoStat>();
        if(currentCargo + tempStat.cargoWeight <= maxCargo)
        {
            currentCargo += tempStat.cargoWeight;
            cargoList.Add(objectReference);
            GameData.Instance.SetCargo(cargoList);
            return true;
        }
        return false;
    }

    public void DropCargo()
    {
        if(cargoList.Count != 0)
        {
            CargoStat tempStat = cargoList[0].GetComponent<CargoStat>();
            currentCargo -= tempStat.cargoWeight;
            //Call CargoList(0)s drop behaviour here.
            //Debug.Log((cargoDropPoint.transform.position - transform.position) * 10f);
            cargoList[0].GetComponent<DropBehaviour>().DroppedCargo(cargoDropPoint.transform.position,(cargoDropPoint.transform.position - transform.position)*(10f+adjustedSpeed));
            cargoList.RemoveAt(0);
            GameData.Instance.SetCargo(cargoList);
        }
    }

}
