using System;

namespace SimpleSFSample
{
    public static class TransSelector
    {
        /// <summary>
        /// y = x
        /// </summary>
        public static readonly Func<float, float> Linear = x => x;
        /// <summary>
        /// y = x ^ 2
        /// </summary>
        public static readonly Func<float, float> UpQuad = x => MathF.Pow(x, 2);
        /// <summary>
        /// y = -(x - 1) ^ 2 + 1
        /// </summary>
        public static readonly Func<float, float> DownQuad = x => -MathF.Pow(x - 1, 2) + 1;
        /// <summary>
        /// y = x ^ 2 + 0.5x for x &lt; 0.5
        /// <br></br>
        /// y = -x ^ 2 + 2.5x - 0.5 otherwise
        /// </summary>
        public static readonly Func<float, float> UpDownQuad = x => x < 0.5f ? MathF.Pow(x, 2) + 0.5f * x : -MathF.Pow(x, 2) + 2.5f * x - 0.5f;
    }
}