using Cysharp.Threading.Tasks;
using ScenarioFlow;
using ScenarioFlow.Scripts.SFText;
using System;
using System.Threading;
using UnityEngine.UI;

namespace SimpleSFSample
{
    /// <summary>
    /// Provides animations of scene transitions.
    /// </summary>
    public class SceneTransitionAnimator : IReflectable
    {
        private readonly Image curtainImage;

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

        [Serializable]
        public class Settings
        {
            public Image CurtainImage;
        }
    }
}