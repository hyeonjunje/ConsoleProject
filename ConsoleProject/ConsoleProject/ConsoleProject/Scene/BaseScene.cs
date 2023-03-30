using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Scene
{
    abstract class BaseScene
    {
        public abstract void Start();
        public abstract void Update();
        public abstract void End();
    }
}
