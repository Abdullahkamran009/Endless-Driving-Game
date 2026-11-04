using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    CarHandler carHandler;

    private void Awake()
    {
        if (!CompareTag("Player"))
        {
            Destroy(this);
            return;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        carHandler.SetInput(input);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1.0f;
        }

    }
}
