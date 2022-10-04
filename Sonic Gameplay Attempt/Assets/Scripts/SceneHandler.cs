using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] RectTransform fade;

    private void Start()
    {
        fade.gameObject.SetActive(true);

        LeanTween.scale(fade, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fade, Vector3.zero, 0.5f).setOnComplete(() =>
        {
            fade.gameObject.SetActive(false);
        });
    }
    public void OpenMenuScene()
    {
        //There won't be a way to go back to the main menu so this is pretty much useless.
        fade.gameObject.SetActive(true);
        LeanTween.scale(fade, Vector3.zero, 0f);
        LeanTween.scale(fade, new Vector3(1, 1, 1), 1.0f).setOnComplete(() =>
        {
            SceneManager.LoadScene(1);
        });
    }

    public void OpenGameScene()
    {
        fade.gameObject.SetActive(true);
        LeanTween.scale(fade, Vector3.zero, 0f);
        LeanTween.scale(fade, new Vector3(1, 1, 1), 1.0f).setOnComplete(() =>
       {
           SceneManager.LoadScene(0);
       });
        
    }
}
