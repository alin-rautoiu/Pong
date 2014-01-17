using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    class Automata : List<Cell>
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
