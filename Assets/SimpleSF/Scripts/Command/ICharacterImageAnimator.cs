using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace SimpleSFSample
{
	/// <summary>
	/// Provides animations to change character images gradually.
	/// </summary>
	public interface ICharacterImageAnimator
	{
		/// <summary>
		/// Change the character's image gradually.
		/// </summary>
		/// <param name="characterObject">The target character.</param>
		/// <param name="sprite">The image by which the current image will be replaced.</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		UniTask ChangeCharacterImageAsync(CharacterObject characterObject, Sprite sprite, CancellationToken cancellationToken);
	}
}