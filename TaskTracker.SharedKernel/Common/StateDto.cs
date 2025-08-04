using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Shared.Common
{
    public class StateDto
    {
        public string Name { get; set; } = default!;
        public int Level { get; set; }
    }
}
