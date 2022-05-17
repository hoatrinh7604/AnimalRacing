using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float minTimeSwitch;
    [SerializeField] float maxTimeSwitch;

    private float speed;
    private bool isRunning;
    private Transform target;
    private Transform endLine;
    private float time;
    private float timeSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            time += Time.deltaTime;

            if(time > timeSwitch)
            {
                speed = Random.Range(minSpeed, maxSpeed);
                timeSwitch = Random.Range(minTimeSwitch, maxTimeSwitch);
                time = 0;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, endLine.position) < 0.01f)
            {
                // Reach endline
                GamePlayController.Instance.ReachEnd(id);
            }

            if(Vector3.Distance(transform.position, target.position) < 0.01f)
            {
                //Reach end
                isRunning = false;
            }
        }
    }

    public void SetTarget(Transform target, Transform endLine)
    {
        this.endLine = endLine;
        this.target = target;
    }

    public void StartRunning(bool value)
    {
        isRunning = value;
    }

    public int GetCarID()
    {
        return id;
    }
}
