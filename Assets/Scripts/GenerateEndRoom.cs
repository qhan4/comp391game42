using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEndRoom : MonoBehaviour
{   
    Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();

        GameObject cafeteriaPrefab = Resources.Load<GameObject>("CafeteriaRoom");
        GameObject serverPrefab = Resources.Load<GameObject>("ServerRoom");
        GameObject storagePrefab = Resources.Load <GameObject>("StorageRoom");

        int i = Random.Range(1, 4);

        if (i == 1)
        {
            GameObject room = (GameObject)Instantiate(cafeteriaPrefab, tf);
        }
        else if (i == 2)
        {
            GameObject room = (GameObject)Instantiate(serverPrefab, tf);
        }
        else if (i == 3)
        {
            GameObject room = (GameObject)Instantiate(storagePrefab, tf);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
