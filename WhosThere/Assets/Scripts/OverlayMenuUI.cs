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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.SetActive(false);
            hm.ResumeGame(); 
        }
    }
     
    public void GoBack() {
        Cursor.visible = false;
        gameObject.SetActive(false);
        hm.ResumeGame(); 
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
