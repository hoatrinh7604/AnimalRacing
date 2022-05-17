using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawCar : MonoBehaviour
{
    [SerializeField] GameObject[] cars;
    [SerializeField] Transform[] gates;
    [SerializeField] Transform[] gatesEnd;
    [SerializeField] Transform[] gatesEndLine;

    private List<int> list;
    private int numberCar = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> Spaw()
    {
        List<GameObject> listCars = new List<GameObject>();
        GamePlayController.Instance.listRanking = new int[gates.Length];

        list = new List<int>();
        for (int i = 0; i< gates.Length; i++)
        {
            list.Add(0);
        }

        // Random gate
        for(int i = 0; i< gates.Length; i++)
        {
            List<int> temp = new List<int>();
            for (int j = 0; j< list.Count; j++)
            {
                if(list[j] == 0)
                {
                    temp.Add(j);
                }
            }

            int randomGate = temp[Random.Range(0, temp.Count)];
            list[randomGate] = 1;

            GameObject car = Instantiate(cars[numberCar++], Vector3.zero, Quaternion.identity, gates[randomGate]);
            car.transform.localPosition = Vector3.zero;
            car.GetComponent<CarController>().SetTarget(gatesEnd[randomGate], gatesEnd[randomGate]);
            listCars.Add(car);
            GamePlayController.Instance.listRanking[randomGate] = car.GetComponent<CarController>().GetCarID();
        }

        return listCars;
    }
}
