using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Anonymization.Settings
{
    public enum DateSettings
    {
        PRESERVE_AGE,
        NULL_AGE,
        MAKE_89,
        RANDOMIZE,
        KEEP_ALL_DATES
    }
}
