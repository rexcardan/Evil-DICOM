using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1591

namespace EvilDICOM.Anonymization.Settings
{
    public enum DateSettings
    {
        PRESERVE_AGE,
        NULL_AGE_ANON,
        NULL_AGE_PRESERVE,
        MAKE_89,
        RANDOMIZE,
        KEEP_ALL_DATES
    }
}
