using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;
using UnityEngine;

namespace SimpleSFSample
{
    public static class SpriteRendererTransitionExtensions
    {
        /// <summary>
        /// Change the alpha of the sprite renderer gradually.
        /// </summary>
        /// <param name="spriteRenderer">The target sprite renderer.</param>
        /// <param name="endAlpha">The alpha value to be set.</param>
        /// <param name="duration">How many seconds it will take to complete the task.</param>
        /// <param name="selector">How the alpha transitions.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static UniTask TransAlphaAsync(this SpriteRenderer spriteRenderer, float endAlpha, float duration, Func<float, float> selector, CancellationToken cancellationToken)
        {
            return TransitionEnumerable.TransitionRange(spriteRenderer.color.a, endAlpha, duration, selector, cancellationToken)
                .ForEachAsync(alpha =>
                {
                    spriteRenderer.TransAlpha(alpha);
                }, cancellationToken: cancellationToken);
        }

        public static void TransAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        }
    }
}