#region

using System;
using System.Collections.Generic;
using EvilDICOM.Core.Enums;

#endregion

namespace EvilDICOM.Core.Helpers
{
    public class SOPClassHelper
    {
        public static Dictionary<string, SOPClass> Dictionary { get; } = SOPClassDictionary.Initialize();

        public static SOPClass FromUID(string sopClassUid)
        {
            if (Dictionary.ContainsKey(sopClassUid))
                return Dictionary[sopClassUid];
            return SOPClass.Unknown;
        }
    }
}