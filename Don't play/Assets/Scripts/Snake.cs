using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] LineRenderer line;
    [SerializeField] SpriteRenderer sprite;
    float horizontal = 0f;

    private void Start()
    {
        sprite.color = line.startColor;
    }

    void Update() {

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                horizontal = -1;
            }
            else if (touch.position.x > Screen.width / 2)
            {
                horizontal = 1;
            }
        }
        else
        {
            horizontal = 0;
        }

        if(!CheckDevice.instance.isMobile())
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate() {
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tail")
        {
            speed = 0;
            rotationSpeed = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
