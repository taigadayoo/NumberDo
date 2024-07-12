using ScenarioFlow;
using ScenarioFlow.Scripts.SFText;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleSFSample
{
    /// <summary>
    /// Provides a decoder for the 'CharacterObject' type.
    /// </summary>
    public class CharacterProvider : IReflectable
    {
        private readonly GameObject characterObjectParent;
        private readonly CharacterObject characterObjectPrefab;
        private readonly Dictionary<string, CharacterObject> characterDictionary = new Dictionary<string, CharacterObject>();

        public CharacterProvider(Settings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (settings.CharacterObjectParent == null)
                throw new ArgumentNullException(nameof(settings.CharacterObjectParent));
            if (settings.CharacterObjectPrefab == null)
                throw new ArgumentNullException(nameof(settings.CharacterObjectPrefab));

            characterObjectParent = settings.CharacterObjectParent;
            characterObjectPrefab = settings.CharacterObjectPrefab;
        }

        [CommandMethod("add character")]
        [Category("Character")]
        [Description("Add a new character to the scene.")]
        [Snippet("Add a new character {${1:name}}.")]
        public void AddCharacter(string name)
        {
            //Make sure that a character with the same name doesn't exist
            if (characterDictionary.ContainsKey(name))
            {
                throw new ArgumentException($"Character '{name}' exists already.");
            }
            //Create a new character
            var newObject = GameObject.Instantiate(characterObjectPrefab.gameObject, characterObjectParent.transform);
            newObject.name = name;
            characterDictionary.Add(name, newObject.GetComponent<CharacterObject>());
        }

        [CommandMethod("remove character")]
        [Category("Character")]
        [Description("Remove a character in the scene.")]
        [Snippet("Remove the character {${1:name}}.")]
        public void RemoveCharacter(string name)
        {
            //Make sure that the character exists
            if (!characterDictionary.ContainsKey(name))
            {
                throw new ArgumentException($"Character '{name}' does not exist.");
            }
            //Destroy the character
            GameObject.Destroy(characterDictionary[name].gameObject);
            characterDictionary.Remove(name);
        }

        [CommandMethod("remove all characters")]
        [Category("Character")]
        [Description("Remove all characters in the scene.")]
        [Snippet("Remove all the characters.")]
        public void RemoveCharacterAll()
        {
            foreach (var name in characterDictionary.Keys.ToArray())
            {
                RemoveCharacter(name);
            }
        }

        [DecoderMethod]
        [Description("A decoder for the 'CharacterObject' type.")]
        [Description("Returns a character object with the character name in the scene.")]
        public CharacterObject GetCharacterObject(string name)
        {
            //Make sure that the character exists
            if (!characterDictionary.ContainsKey(name))
            {
                throw new ArgumentException($"Character '{name}' does not exist.");
            }
            return characterDictionary[name];
        }

        [Serializable]
        public class Settings
        {
            public GameObject CharacterObjectParent;
            public CharacterObject CharacterObjectPrefab;
        }
    }
}