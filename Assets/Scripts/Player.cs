using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
  public GameObject bullet;

  public AudioSource audioSrc;

  public AudioClip shootSound;
  public AudioClip deathSound;
  public float speed = 5f;

  public Transform shottingOffset;

  public Sprite[] animationFrames;
  public float framesPerSecond = 1.0f;
  
  private SpriteRenderer spriteRenderer;
  private int currentFrameIndex = 0;
    // Update is called once per frame
  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }
  void Start()
  {
    Enemy.OnEnemyDied += EnemyOnEnemyDied;
    EnemySpawner.OnGameEnded += OnGameEnded;
    InvokeRepeating(nameof(NextFrame), this.framesPerSecond, this.framesPerSecond);
    audioSrc = GetComponent<AudioSource>();
  }

  public void StartCredits() 
  {
    SceneManager.LoadScene("Credits");
  }
  private void NextFrame()
  {
    currentFrameIndex++;
    if (currentFrameIndex >= 3)
    {
      currentFrameIndex = 0;
    }

    spriteRenderer.sprite = animationFrames[currentFrameIndex];
  }
  void OnDestroy()
  {
    Enemy.OnEnemyDied -= EnemyOnEnemyDied;
  }
  void EnemyOnEnemyDied(int pointsWorth)
  {
    Debug.Log($"I got one! It was worth ${pointsWorth}!");
  }

  void OnGameEnded()
  {
    Debug.Log("Game Over!");
    StartCredits();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    spriteRenderer.sprite = animationFrames[3];
    //wait for 1 second
    audioSrc.PlayOneShot(deathSound, 1.0f);
    Invoke("OnGameEnded", 1.0f);
    
    // if(collision.gameObject.tag == "EnemyBullet") 
    // {
    //   OnGameEnded();
    //   Destroy(gameObject);
    //   StartCredits();

    // }
    
  }

  void Update()
  {
    float direction = Input.GetAxis("Horizontal");
    Vector3 newPosition = transform.position + new Vector3(direction, 0, 0) * speed * Time.deltaTime;
    newPosition.x = Mathf.Clamp(newPosition.x, -24, 24);
    transform.position = newPosition;

    if (Input.GetKeyDown(KeyCode.Space))
    {
      GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
      //Debug.Log("Bang!");
      spriteRenderer.sprite = animationFrames[4];
      audioSrc.PlayOneShot(shootSound, 0.5f);

      Destroy(shot, 10f);

    }
  }
}
