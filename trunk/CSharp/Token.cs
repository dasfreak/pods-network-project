using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Networking
{

    class Token
    {
        internal string ipCreator;
        internal string currentHolder;

        public Token(string ipCreator, string currentHolder)
        {
            this.ipCreator = ipCreator;
            this.currentHolder = currentHolder;
        }
    }

}
