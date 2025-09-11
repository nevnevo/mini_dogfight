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
        public static VirtualKey left { get; set; } = VirtualKey.Left;
        public static VirtualKey right { get; set; } = VirtualKey.Right;
        public static VirtualKey up { get; set; } = VirtualKey.Up;
        public static VirtualKey down { get; set; } = VirtualKey.Down;
    }
}
