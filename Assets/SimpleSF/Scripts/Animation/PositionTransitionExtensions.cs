using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;
using UnityEngine;

namespace SimpleSFSample
{
    public static class PositionTransitionExtensions
    {
        /// <summary>
        /// Change the local position of the object gradually.
        /// </summary>
        /// <param name="transform">The target transform.</param>
        /// <param name="endLocalPos">The local position that the object moves to.</param>
        /// <param name="duration">How many seconds it will take to complete the task.</param>
        /// <param name="selector">How the local position transitions.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async static UniTask TransLocalPosAsync(this Transform transform, Vector3 endLocalPos, float duration, Func<float, float> selector, CancellationToken cancellationToken)
        {
            if (duration == 0.0f)
            {
                transform.localPosition = endLocalPos;
                return;
            }
            var startLocalPos = transform.localPosition;
            var diff = endLocalPos - startLocalPos;
            await TransitionEnumerable.TransitionRange(0.0f, 1.0f, duration, selector, cancellationToken)
                .ForEachAsync(rate => transform.localPosition = startLocalPos + diff * rate, cancellationToken: cancellationToken);
        }
    }
}