﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public int desiredNumberOfWorkers = 50;
    public int currentlyPlacedWorkers = 0;
    public float rareGrouping = 0.2f; //Five Workers
    public float uncommonGrouping = 0.3f; //3 Workers
    public int commonGrouping = 5; //2 Workers

    public GameObject WorkerPrefab;



    private Transform roomReference;
    // Start is called before the first frame update
    void Start()
    {
        roomReference = gameObject.transform.Find("RoomZones");
        ScatterWorkers();
    }

    public void ScatterWorkers()
    {
        while (currentlyPlacedWorkers < desiredNumberOfWorkers)
        {
            //Work Out How many workers to place
            float randomGrouping = Random.Range(0.0f, 1f);
            if (randomGrouping <= rareGrouping)
            {
                PlaceWorkers(2f, 5);
            }
            else if (randomGrouping <= rareGrouping + uncommonGrouping)
            {
                PlaceWorkers(1.5f, 3);
            }
            else
            {
                PlaceWorkers(1f, 2);
            }
        }
    }

    public void PlaceWorkers(float grouping, int workers)
    {
        int randomRoom = Random.Range(0, roomReference.childCount);
        Transform room = roomReference.GetChild(randomRoom);
        //Debug.Log(roomReference.GetChild(0).GetComponent<RectTransform>().sizeDelta.x);
        RectTransform tempRect = room.GetComponent<RectTransform>();

        //Generate Random Point in this room with room for circle
        //First find the bounds. Then
        float roomWidth = tempRect.sizeDelta.x;
        float roomLength = tempRect.sizeDelta.y;

        Vector2 randomPoint = new Vector2(0f, 0f);
        randomPoint.x = Random.Range(0.0f + grouping, roomWidth - grouping);
        randomPoint.y = Random.Range(0.0f + grouping, roomLength - grouping);
        for (int i = 0; i < workers; i++)
        {
            Vector2 centrePoint = randomPoint + Random.insideUnitCircle * grouping;
            GameObject tempWorker = Instantiate(WorkerPrefab, (Vector2)room.transform.TransformPoint(Vector2.zero + centrePoint), Quaternion.identity);
            currentlyPlacedWorkers++;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
