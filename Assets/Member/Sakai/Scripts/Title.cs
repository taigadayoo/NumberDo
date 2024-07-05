
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

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private GameObject previousHoverStart = null;
    private GameObject previousHoverScenario = null;
    private GameObject previousHoverExit = null;
    [SerializeField]
    TitleAnim anim;

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

        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();

        SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
    }

  async  public void OnStart()
    {
        await anim.TitleAnimation();
        SampleSoundManager.Instance.StopBgm();
        sceneManagement.OnStart();
        
    }
   async public void OnScenario()
    {
        SampleSoundManager.Instance.StopBgm();
        Debug.Log("シナリオです。");
        await anim.TitleAnimation();
    }
   async public void Exit()
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
        await anim.TitleAnimation();
    }

   async void Update()
    {
        // Raycastの結果を保存するリスト
        List<RaycastResult> results = new List<RaycastResult>();

        // マウスの位置からRaycastを行う
        pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        raycaster.Raycast(pointerEventData, results);

        bool isHoveringStart = false;
        bool isHoveringScenario = false;
        bool isHoveringExit = false;

        // Raycast結果があった場合
        if (results.Count > 0)
        {
            foreach (var result in results)
            {
                GameObject hitObject = result.gameObject;

                // 各オブジェクトに対する処理
                if (hitObject == start)
                {
                    isHoveringStart = true;
                    if (hitObject != previousHoverStart)
                    {
                        ChangeStart();
                        previousHoverStart = hitObject;
                       
                    }
                }
                else if (hitObject == scenario)
                {
                    isHoveringScenario = true;
                    if (hitObject != previousHoverScenario)
                    {
                        ChangeScenario();
                        previousHoverScenario = hitObject;
                       
                    }
                }
                else if (hitObject == exit)
                {
                    isHoveringExit = true;
                    if (hitObject != previousHoverExit)
                    {
                        ChangeExit();
                        previousHoverExit = hitObject;
                       
                    }
                }
            }
        }
        if (!isHoveringStart && previousHoverStart != null)
        {
            DefaultStart();
            previousHoverStart = null;
        }
        if (!isHoveringScenario && previousHoverScenario != null)
        {
            DefaultScenario();
            previousHoverScenario = null;
        }
        if (!isHoveringExit && previousHoverExit != null)
        {
            DefaultExit();
            previousHoverExit = null;
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

