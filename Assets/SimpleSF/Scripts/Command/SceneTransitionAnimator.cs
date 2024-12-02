using Cysharp.Threading.Tasks;
using ScenarioFlow;
using ScenarioFlow.Scripts.SFText;
using SimpleSFSample;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SimpleSFSample
{
    /// <summary>
    /// Provides animations of scene transitions.
    /// </summary>
    public class SceneTransitionAnimator : IReflectable
    {
        private readonly Image curtainImage;

        public string nextSceneName;

        public SceneTransitionAnimator(Settings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            curtainImage = settings.CurtainImage ?? throw new ArgumentNullException(nameof(settings.CurtainImage));
        }

        [CommandMethod("enter scene transition async")]
        [Category("Scene Transition")]
        [Description("Close the curtain to prepare the next scene in the specified seconds.")]
        [Snippet("Close the curtain in {${1:n}} seconds.")]
        public async UniTask EnterSceneTransitionAsync(float duration, CancellationToken cancellationToken)
        {
            try
            {
                await curtainImage.TransAlphaAsync(1.0f, duration, TransSelector.Linear, cancellationToken);
                //if (string.IsNullOrEmpty(nextSceneName))
                //{
                //    Debug.LogError("Next scene name is not set.");
                //    return;
                //}

                //await LoadNextSceneAsync(nextSceneName, cancellationToken);

            }
            finally
            {
                curtainImage.TransAlpha(1.0f);
            }
        }

        [CommandMethod("exit scene transition async")]
        [Category("SceneTransition")]
        [Description("Open the curtain to show the next scene in the specified seconds.")]
        [Snippet("Open the curtain in {${2:n}} seconds.")]
        public async UniTask ExitSceneTransitionAsync(float duration, CancellationToken cancellationToken)
        {
            try
            {
                await curtainImage.TransAlphaAsync(0.0f, duration, TransSelector.Linear, cancellationToken);

            }
            finally
            {
                curtainImage.TransAlpha(0.0f);
            }
        }


        //private async UniTask LoadNextSceneAsync(string sceneName, CancellationToken cancellationToken)
        //{
        //    var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //    asyncOperation.allowSceneActivation = false;

        //    while (!asyncOperation.isDone)
        //    {
        //        if (asyncOperation.progress >= 0.9f && !cancellationToken.IsCancellationRequested)
        //        {
        //            asyncOperation.allowSceneActivation = true;
        //        }

        //        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        //    }
        //}

        [Serializable]
        public class Settings
        {
            public Image CurtainImage;
            public string nextSceneName;
        }
    }


    
}

//public class ExampleUsage : MonoBehaviour
//{
//    public Image curtainImage; // インスペクタで設定
//    public string nextSceneName; // インスペクタで設定

//    private SceneTransitionAnimator sceneTransitionAnimator;

//    private void Start()
//    {
//        var settings = new SceneTransitionAnimator.Settings
//        {
//            CurtainImage = curtainImage
//        };

//        sceneTransitionAnimator = new SceneTransitionAnimator(settings)
//        {
//            nextSceneName = nextSceneName
//        };
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            // スペースキーが押されたらシーン遷移を開始
//            var cancellationTokenSource = new CancellationTokenSource();
//            sceneTransitionAnimator.EnterSceneTransitionAsync(3.0f, cancellationTokenSource.Token).Forget();
//        }
//    }
//}
