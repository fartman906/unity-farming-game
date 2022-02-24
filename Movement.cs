using System.Collections;
using System.Collections.Generic;
using System;
using Random = System.Random;
using UnityEngine;
using TMPro;

// Player movement and code

// i mean not really movement anymore 

public class Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb2D;
    public int hit;

    public GameObject cashObj;
    public TextMeshProUGUI cash;

    public GameObject plantprefab;
    public Random rand = new Random();

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        speed = 10f;
        // basically the currency
        hit = 0;
        //snewPlant = GameObject.Find("NewPlant");

        cashObj = GameObject.Find("/Canvas/Cash");
        cash = cashObj.GetComponent<TextMeshProUGUI>();
    }
 
    Vector2 movement = Vector3.zero;

    //Grow the plant and make it mature and ready to harvest
    IEnumerator growplant(GameObject huh) 
    {

        
        yield return new WaitForSeconds(5);
        huh.GetComponent<SpriteRenderer>().color = new Color(38,253,0,255);
        // change name so player can consume it
        huh.name = "Plant";


    }

    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.E)) { // Planting
            if (hit > 10) {

                Debug.Log("AAAAA");


                Vector3 vSpawnPos = transform.position;
    
                vSpawnPos.x += rand.Next(1,5);
                vSpawnPos.y += rand.Next(1,5);
                
                GameObject newplnt = Instantiate(plantprefab, vSpawnPos, transform.rotation) as GameObject;

                StartCoroutine(growplant(newplnt));

                
                hit = hit - 10;
                cash.text = "Cash: $" + hit.ToString();


            }
        }
    }
 
    void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Harvesting
        if (collision.gameObject.name == "Plant")
        {
            Debug.Log("Hit!");

            hit = hit + rand.Next(5,50);
            cash.text = "Cash: $" + hit.ToString();

            // Destroy the plant
            Destroy(collision.gameObject);

        }
    }
}
