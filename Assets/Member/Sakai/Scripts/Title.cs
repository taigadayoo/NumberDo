using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    SceneManagement sceneManagement;

    public GameObject start;
    public GameObject scenario;
    public GameObject exit;

    public List<Sprite> touchImage = new List<Sprite>();

    public List<Sprite> defaultImage = new List<Sprite>();

    private Image startImage;
    private Image scenarioImage;
    private Image exitImage;
    // Start is called before the first frame update
    void Start()
    {
        startImage = start.GetComponent<Image>();
        scenarioImage = scenario.GetComponent<Image>();
        exitImage = exit.GetComponent<Image>();

        // 初期画像を設定する
        DefaultStart();
        DefaultScenario();
        DefaultExit();

        SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
    }

    // Update is called once per frame
  public  void OnStart()
    {
            SampleSoundManager.Instance.StopBgm();
            sceneManagement.OnStart();
    }
    public void OnScenario()
    {
        SampleSoundManager.Instance.StopBgm();
        Debug.Log("シナリオです。");
    }
    public void Exit()
    {
        {
            SampleSoundManager.Instance.StopBgm();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        // ビルドされたゲームで実行されるコード
        Application.Quit();
        #endif
        }
    }
    void Update()
    {
        // マウスの位置からRayを飛ばして衝突判定
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // 衝突したオブジェクトに応じて処理を行う
            if (hitObject == start)
            {
                ChangeStart();
            }
            else if (hitObject == scenario)
            {
                ChangeScenario();
            }
            else if (hitObject == exit)
            {
                ChangeExit();
            }
        }
        else
        {
            // Rayが何も当たらなかった場合の処理
            DefaultStart();
            DefaultScenario();
            DefaultExit();
        }
    }

    // 対象のGameObjectに対する処理
    private void ChangeStart()
    {
        if (startImage != null && touchImage.Count > 0)
        {
            startImage.sprite = touchImage[0]; // touchImageリストの最初の要素を設定
        }
    }

    private void ChangeScenario()
    {
        if (scenarioImage != null && touchImage.Count > 1)
        {
            scenarioImage.sprite = touchImage[1]; // touchImageリストの2番目の要素を設定
        }
    }

    private void ChangeExit()
    {
        if (exitImage != null && touchImage.Count > 2)
        {
            exitImage.sprite = touchImage[2]; // touchImageリストの3番目の要素を設定
        }
    }

    private void DefaultStart()
    {
        if (startImage != null && defaultImage.Count > 0)
        {
            startImage.sprite = defaultImage[0]; //defaultImageリストの最初の要素を設定
        }
    }

    private void DefaultScenario()
    {
        if (scenarioImage != null && defaultImage.Count > 1)
        {
            scenarioImage.sprite = defaultImage[1]; // defaultImageリストの2番目の要素を設定
        }
    }

    private void DefaultExit()
    {
        if (exitImage != null && defaultImage.Count > 2)
        {
            exitImage.sprite = defaultImage[2]; // defaultImageリストの3番目の要素を設定
        }
    }
}

