using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.tag == "Enemy Laser" && collision.tag == "Player" || 
           this.tag == "Enemy Laser" && collision.tag == "Player Laser" ||
           this.tag == "Player Laser" && collision.tag == "Enemy" ||
           this.tag == "Player Laser" && collision.tag == "Enemy Laser")
        {
            Destroy(gameObject);
        }
    }
}
