using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stat
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass = null;
        
        public float GetHealth(CharacterClass character, int level)
        {
            foreach (ProgressionCharacterClass characterClass in characterClass)
            {
                if(characterClass.characterClass == character)
                {
                    return characterClass.health[level - 1];                    
                }
            }
            return 0;

        }

        [System.Serializable]         
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }
    }
}
