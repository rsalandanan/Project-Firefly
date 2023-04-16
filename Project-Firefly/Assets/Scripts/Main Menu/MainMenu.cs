
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public GameObject htpPanel;

  public void PlayButton()
  {
    SceneManager.LoadScene("Main level");
  }

  public void Htp()
  {
    htpPanel.SetActive(true);
  }

  public void BackButton()
  {
    htpPanel.SetActive(false);
  }
}
