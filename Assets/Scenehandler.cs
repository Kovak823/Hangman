using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenehandler : MonoBehaviour
{
    [SerializeField] RectTransform fader;


    private void Start()
    {
        SceneTrans();
    }


    public void SceneTrans() {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }


    public void OpenMenuScene() {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            SceneManager.LoadScene(0);
        });
    }

    public void OpenGameScene() {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            SceneManager.LoadScene(1);
            Invoke("LoadGame", 0.5f);
        });
    }

    private void LoadGame() {
        SceneManager.LoadScene(1);
    }

}
