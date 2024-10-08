using UnityEngine;

namespace SimpleSFSample
{
    /// <summary>
    /// An object of character in the scene.
    /// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
    public class CharacterObject : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}