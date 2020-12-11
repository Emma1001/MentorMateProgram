using System;
using System.Linq;

namespace Mentormate_assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensionsOfLayer = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = dimensionsOfLayer[0];
            int m = dimensionsOfLayer[1];

            //Checks if the dimensions of the layers are correct
            if(n > 100 || m>100 || n % 2 != 0 || m % 2 != 0 || n<=0 || m<=0)
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            //NewLogic newLogic = new NewLogic();
            //newLogic.initializeFirstLayer(n, m);
            //newLogic.createSecondLayer();

            Logic logic = new Logic();
            logic.initializeFirstLayer(n, m);
            logic.createSecondLayer();
        }
    }
}
