using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace mini_dogfight
{
    public class Controls
    {
        public static VirtualKey left { get; set; } = VirtualKey.A;
        public static VirtualKey right { get; set; } = VirtualKey.D;
        public static VirtualKey up { get; set; } = VirtualKey.W;
        public static VirtualKey down { get; set; } = VirtualKey.S;
    }
}
