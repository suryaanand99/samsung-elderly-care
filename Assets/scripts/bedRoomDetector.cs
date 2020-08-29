using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class bedRoomDetector : MonoBehaviour
{
    private bool triggered = false;
    private Vector3 currentPOs;
    private string[] validator = new string[5];

    [SerializeField]
    public GameObject player;
    public float delay = 5f;
    public Text gameClock;

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
                    new string[]
                    {
                        tag.ToString(),
                        "Actor is moving",
                        gameClock.text
                    }
                );
                currentPOs = player.transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //this for the validation report
        validator[0] = tag;
        validator[1] = gameClock.text;
        validator[3] = System.DateTime.Now.ToString();

        //this for generation of dataset
        if(other.tag == "MainCamera")
        {
            triggered = true;
            currentPOs = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //this for the validation report
        validator[2] = gameClock.text;
        validator[4] = System.DateTime.Now.ToString();
        validationExporter.AppendToReport(validator);

        //this for generation of dataset
        if (other.tag == "MainCamera")
        {
            triggered = false;
        }
    }
}
