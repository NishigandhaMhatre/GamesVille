using System;
using System.Collections.Generic;
using Game.Models;
using System.Linq;

namespace Game.Helpers
{
    /// <summary>
    /// Helper class that will be used to get special abilities from enum 
    /// </summary>
    public static class SpecialAbilityEnumHelper
    {
        /// <summary>
        /// Gets the list of special abilities in the form of a list of strings
        /// </summary>
        public static List<string> GetSpecialAbilityList
        {
            get
            {
                var myList = Enum.GetNames(typeof(SpecialAbilityEnum)).ToList();
                return myList;
            }
        }
    }
}
