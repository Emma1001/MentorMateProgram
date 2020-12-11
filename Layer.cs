using System;
using System.Collections.Generic;
using System.Text;

namespace Mentormate_assignment
{
    public class Layer
    {
        public int[,] layerValues { get; set; }

        //All bricks in the layer
        public Queue<Brick> layerQueue { get; set; }

        public Layer(int rows, int cols)
        {
            layerValues = new int[rows, cols];
            layerQueue = new Queue<Brick>();
        }
    }
}
