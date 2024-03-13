using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public int points = 0;
  public int totalEnemies = 0;

  public Sprite[] animationFrames;
  public float framesPerSecond = 1.0f;
  
  private SpriteRenderer spriteRenderer;
  private int currentFrameIndex = 0;
  public delegate void EnemyDied(int pointWorth);
  public static event EnemyDied OnEnemyDied;
  
  private AudioSource audioSrc;
    // Start is called before the first frame update
  
  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }
  void Start()
  {
    InvokeRepeating(nameof(NextFrame), this.framesPerSecond, this.framesPerSecond);
    audioSrc = GetComponent<AudioSource>();
  }

  private void NextFrame()
  {
    currentFrameIndex++;
    if (currentFrameIndex >= 2)
    {
      currentFrameIndex = 0;
    }

    spriteRenderer.sprite = animationFrames[currentFrameIndex];
  }
  void OnCollisionEnter2D(Collision2D collision)
  {
    //Debug.Log("Ouch!");
    Destroy(collision.gameObject);
    //spriteRenderer.sprite = animationFrames[2];
    audioSrc.Play();
    //Invoke("doNothing", 0.3f);
    //OnEnemyDied.Invoke(points);
    if(collision.gameObject.tag == "Bullet") {
      //spriteRenderer.sprite = animationFrames[2];
      OnEnemyDied.Invoke(points);
      gameObject.SetActive(false);
    }
  }

  void doNothing() 
  {
    spriteRenderer.sprite = animationFrames[2];
    //gameObject.SetActive(false);
  }

}
