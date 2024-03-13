using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] prefabs;
    public int rows = 5;
    public int columns = 4;
    // Start is called before the first frame update
    private Vector3 direction = Vector3.right;
    public float speed = 2.0f;

    public GameObject bullet;

    public int numOfEnemies;
    public delegate void GameStarted();
    public static event GameStarted OnGameStarted;

    public delegate void GameEnded();
    public static event GameEnded OnGameEnded;

    public bool gameStarted = false;

    public AudioSource audioSrc;
    public AudioClip shootSound;
    public AudioClip deathSound;
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Enemy.OnEnemyDied += EnemyOnEnemyDied;
        numOfEnemies = this.rows * this.columns;
        audioSrc = GetComponent<AudioSource>();
    }

    public void StartCredits() 
    {
        SceneManager.LoadScene("Credits");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return)) 
        {
            gameStarted = true;
           OnGameStarted.Invoke();
           for(int i = 0; i < this.rows; i++) 
           {
                float w = 5.0f * (this.columns - 1);
                float h = 2.0f * (this.rows - 1);
                Vector3 center = new Vector3(-w/2, -h/2, 0.0f);

                Vector3 rowPosition = new Vector3(center.x, center.y+ (i*2.0f) ,0.0f);
                for(int j = 0; j < this.columns; j++) 
                {
                   Enemy enemy = Instantiate(this.prefabs[i], this.transform);
                   Vector3 position = rowPosition;
                   position.x += j * 5.0f;
                   enemy.transform.localPosition = position;
                }
           }
        }

        if(gameStarted) 
        {
            this.transform.position += direction * this.speed * Time.deltaTime;

            foreach(Transform enemy in this.transform) 
            {
                if(enemy.position.x > 24.0f || enemy.position.x < -24.0f) 
                {
                    direction = -direction;
                    this.transform.position += Vector3.down * 0.5f;
                    break;
                }
            }
        }
    }

    void EnemyOnEnemyDied(int pointsWorth)
    {
        //audioSrc.PlayOneShot(deathSound, 1.0F);
        foreach(Transform enemy in this.transform) 
        {
            if(!enemy.gameObject.activeSelf) 
            {
                continue;
            }
            float x_chance = Random.value;
            Debug.Log(x_chance);
            if(x_chance < 1.0f ) 
            {
                GameObject enemyShot = Instantiate(bullet, enemy.position, Quaternion.identity);
                audioSrc.PlayOneShot(shootSound, 0.3F);
                
                
            }
        }
        int numleft = 0;
        foreach(Transform enemy in this.transform)
        {
            if(enemy.gameObject.activeSelf) 
            {
                numleft++;
            }
        }

        if(numleft < 15 && numleft > 10)
        {
            this.speed = 9.0f;
            Debug.Log("Speeding up!");
        }

        if(numleft < 10 && numleft > 5)
        {
            this.speed = 12.0f;
            Debug.Log("Speeding up!");
        }

        if(numleft < 5 && numleft > 2)
        {
            this.speed = 15.0f;
            Debug.Log("Speeding up!");
        }

        if(numleft == 1) {
            OnGameEnded.Invoke();
            StartCredits();
        }
       
       Invoke("doNothing", 0.3f);
    }

    void doNothing()
    {
         audioSrc.PlayOneShot(deathSound, 0.5F);
    }
}
