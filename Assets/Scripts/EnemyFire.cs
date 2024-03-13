using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFire : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

  public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
      myRigidbody2D = GetComponent<Rigidbody2D>();
      Fire();
    }

    // Update is called once per frame
    private void Fire()
    {
      myRigidbody2D.velocity = Vector2.down * speed; 
      Debug.Log("Wwweeeeee");
    }

    void OnCollisionEnter(Collision col)
    {
      if(col.gameObject.tag == "Player")
      {
        Destroy(gameObject);
      }
    }

}
