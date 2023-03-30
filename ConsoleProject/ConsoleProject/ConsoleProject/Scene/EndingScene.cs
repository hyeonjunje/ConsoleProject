using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Scene
{
    class EndingScene : BaseScene
    {
        public override void End()
        {
            Console.Clear();

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
