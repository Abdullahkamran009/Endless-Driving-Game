using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI distanceTravelledText;

    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    CanvasGroup gameOverCanvasGroup;
    //Reference
    CarHandler playerCarHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverCanvasGroup.interactable = false;
        gameOverCanvasGroup.alpha = 0;

        playerCarHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<CarHandler>();

        playerCarHandler.OnplayerCrashed += playerCarHandler_OnPlayerCrashed;
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelledText.text = playerCarHandler.DistanceTravelled.ToString("000000");
    }

    IEnumerator StartGameOverAnimationCO()
    {
        yield return new WaitForSecondsRealtime(3.0f);

        gameOverCanvasGroup.interactable = true;

        while (gameOverCanvasGroup.alpha < 0.8f)
        {
            gameOverCanvasGroup.alpha = Mathf.MoveTowards(gameOverCanvasGroup.alpha, 1.0f, Time.deltaTime * 2);

            yield return null;
        }
    }

    //Events
    void playerCarHandler_OnPlayerCrashed(CarHandler obj)
    {
        gameOverText.text = $"DISTANCE {distanceTravelledText.text}";

        StartCoroutine(StartGameOverAnimationCO());
    }

    public void OnRestartClicked()
    {
        //Restore tie scale
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
