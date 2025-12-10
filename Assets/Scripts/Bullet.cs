using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float power;
    public GameObject killScoreText;
    public Player playerScript;
    public GameObject blood;

    public GameObject explodeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * power);
        killScoreText = GameObject.FindGameObjectWithTag("KillScore");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("Player"))
        {
            return;
        }
        Instantiate(explodeEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        if (collision.collider.CompareTag("Zombie")) 
        {
            playerScript.AddScore();
            killScoreText.GetComponent<TextMeshProUGUI>().text = $"killScore : {playerScript.k}";
            Zombie z = collision.gameObject.GetComponent<Zombie>();
            z.Death();

            Destroy(collision.gameObject, 3f);
            Instantiate(blood, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            collision.rigidbody.isKinematic = true;
        }



        Destroy(this.gameObject, 0.1f);
    }
}
