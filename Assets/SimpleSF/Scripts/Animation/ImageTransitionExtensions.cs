using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleSFSample
{
    public static class ImageTransitionExtensions
    {
        /// <summary>
        /// Change the alpha of the image gradually.
        /// </summary>
        /// <param name="image">The target image.</param>
        /// <param name="endAlpha">The alpha value to be set.</param>
        /// <param name="duration">How many seconds it will take to complete the task.</param>
        /// <param name="selector">How the alpha transitions.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async UniTask TransAlphaAsync(this Image image, float endAlpha, float duration, Func<float, float> selector, CancellationToken cancellationToken)
        {
            await TransitionEnumerable.TransitionRange(image.color.a, endAlpha, duration, selector, cancellationToken)
                .ForEachAsync(alpha =>
                {
                    image.TransAlpha(alpha);
                }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Set the alpha of the image.
        /// </summary>
        /// <param name="image">The target image.</param>
        /// <param name="alpha">Alpha value to be set to the image.</param>
        public static void TransAlpha(this Image image, float alpha)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }
}