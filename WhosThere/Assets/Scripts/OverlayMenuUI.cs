using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayMenuUI : MonoBehaviour
{
    HomeManager hm;

    void Start()
    {
        hm= GameObject.Find("HomeManager").GetComponent<HomeManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            gameObject.SetActive(false);
            hm.ResumeGame(); 
        }
    }
     
    public void GoBack() { 
       hm.ResumeGame();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
