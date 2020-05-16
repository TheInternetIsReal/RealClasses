using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealClasses.Experience
{
    public class Exp
    {
        protected int playerID;
        protected int level;
        protected int exp;
        protected int nextLevel;

        public Exp()
        {
            this.exp = 0;
            this.level = 1;
            nextLevel = 1;
        }

        public void ChangExp(int exp)
        {
            this.exp = this.exp + exp;
        }
    }
}
