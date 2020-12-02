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
        GameObject room;

        if (i == 1)
        {
            room = (GameObject)Instantiate(cafeteriaPrefab, tf);
        }
        else if (i == 2)
        {
            room = (GameObject)Instantiate(serverPrefab, tf);
        }
        else
        {
            room = (GameObject)Instantiate(storagePrefab, tf);
        }

        // un-rotate the monsters
        foreach (Transform child in room.transform)
        {
            if (child.gameObject.layer == 10)
            {
                child.Rotate(new Vector3(0f, 0f, -tf.rotation.eulerAngles.z));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
