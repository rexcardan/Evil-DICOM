using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Helpers
{
    public static class WindowLevelPresets
    {
        public static readonly WindowLevelPreset Brain = new WindowLevelPreset(80, 40);
        public static readonly WindowLevelPreset Lungs = new WindowLevelPreset(1500, -600);
        public static readonly WindowLevelPreset Abdomen = new WindowLevelPreset(400, 50);
        public static readonly WindowLevelPreset Spine = new WindowLevelPreset(1800, 400);
        public static readonly WindowLevelPreset Bones = new WindowLevelPreset(2800, 600);
        public static readonly WindowLevelPreset HeadNeck = new WindowLevelPreset(375, 40);
        public static readonly WindowLevelPreset Liver = new WindowLevelPreset(150, 30);
    }
}
