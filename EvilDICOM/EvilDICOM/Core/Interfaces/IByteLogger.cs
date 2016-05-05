using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Core.Interfaces
{
    public interface IByteLogger
    {
        void Log(byte[] bytes);
        void Dump(string path);
    }
}
