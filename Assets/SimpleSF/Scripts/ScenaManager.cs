using System.Threading;
using Cysharp.Threading.Tasks;
using ScenarioFlow;
using SimpleSFSample;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleSFSample
{
    public class SceneManager : IReflectable
    {
        public SceneTransitionAnimator sceneTransitionAnimator;
        public Image curtainImage;
        public string nextSceneName;

        private void Start()
        {
            // 初期化処理（例：最初のシーンを設定）
            if (sceneTransitionAnimator != null)
            {
                sceneTransitionAnimator.nextSceneName = nextSceneName;
            }
        }

        public async void StartSceneTransition(float duration)
        {
            // キャンセルトークンを作成
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            // シーン遷移を開始
            await sceneTransitionAnimator.ExitSceneTransitionAsync(duration, cancellationTokenSource.Token);
        }
    }
}


