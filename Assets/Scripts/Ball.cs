using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    float radius;
    Vector2 direction;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        radius = transform.localScale.x / 2;
        gameManager = Object.FindObjectOfType<GameManager>();
        Spawn();
    }

    public void Spawn() {
        transform.position = new Vector3(0,0,0);
        direction = Vector2.one.normalized;
        if (Random.Range(0,2) == 1) {
            direction = -direction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        
        // bounce off bottom
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0 ) {
            direction.y = -direction.y;
        }
        // bounce off top
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0) {
            direction.y = -direction.y;
        }
        // going off left
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0 ) {
            gameManager.RightWins();
            // enabled = false;
        }        
        // going off right
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0 ) {
            gameManager.LeftWins();
            // enabled = false;
        }        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Paddle") {
            GetComponent<AudioSource>().Play();
            bool isRight = other.GetComponent<Paddle>().isRight;
            if (isRight && direction.x > 0) {
                direction.x = -direction.x;
            }
            if (!isRight && direction.x < 0) {
                direction.x = -direction.x;
            }
        }
    }
}
