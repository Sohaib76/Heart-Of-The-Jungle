using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryModalController : MonoBehaviour
{
    private Button replayButton;
    private Button nextLevelButton;
    private Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        replayButton = transform.Find("Canvas/ReplayButton").GetComponent<Button>();
        nextLevelButton = transform.Find("Canvas/NextButton").GetComponent<Button>();
        mainMenuButton = transform.Find("Canvas/ReturnButton").GetComponent<Button>();

        replayButton.onClick.AddListener(ReplayButtonClicked);
        nextLevelButton.onClick.AddListener(NextLevelButtonClicked);
        mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
    }

    private void MainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void NextLevelButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ReplayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
