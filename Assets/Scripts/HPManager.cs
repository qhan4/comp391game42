using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(int hp)
    {
        // wipe all previous hearts
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // create new hearts
        for (int i = 0; i < hp; i++)
        {
            GameObject newGO = new GameObject("heart");
            SpriteRenderer newHeart = newGO.AddComponent<SpriteRenderer>();
            newHeart.sprite = sprite;
            newGO.transform.parent = gameObject.transform;
            newGO.transform.localPosition = new Vector3(72 - (6 * i), 33, 0);
            newGO.transform.localScale = new Vector3(4, 4, 1);
            //Instantiate(newGO);
        }
    }
}
