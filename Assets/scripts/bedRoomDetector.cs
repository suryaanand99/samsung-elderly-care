using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bedRoomDetector : MonoBehaviour
{
    private bool triggered = false;
    private Vector3 currentPOs;

    [SerializeField]
    public GameObject player;
    public float delay = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("UpdateSensor", 0.5f, delay);
    }

    // Update is called once per frame
    void UpdateSensor()
    {
        if(triggered)
        {
            float dist = Vector3.Distance(currentPOs, player.transform.position);
            if(dist > 1)
            {
                print(tag + ":Actor is moving");
                CSVExporter.AppendToReport(
                    new string[2]
                    {
                        tag.ToString(),
                        "Actor is moving"
                    }
                );
                currentPOs = player.transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            triggered = true;
           currentPOs = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            triggered = false;
        }
    }
}
