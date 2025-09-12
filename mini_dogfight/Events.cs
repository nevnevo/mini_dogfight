using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace mini_dogfight
{
    public class Events
    {
        public Action<VirtualKey> OnKeyPress;
        public Action<VirtualKey> OnKeyLeave;
        public Action<DataObj> OnDataRecieve;
       


    }
}
