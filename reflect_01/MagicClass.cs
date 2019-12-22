using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflect_01
{
    public class MagicClass
    {
        private int magicBaseValue;

        public MagicClass()
        {
            magicBaseValue = 9;
        }

        public int ItsMagic(int preMagic)
        {
            return preMagic * magicBaseValue;
        }
    }
}
