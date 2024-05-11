using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceMote : PickupableObject
{
    protected override void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GainExperience(1);
            Destroy(gameObject);
        }
    }
}
