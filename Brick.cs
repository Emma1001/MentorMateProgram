using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Mentormate_assignment
{
    public class Brick
    {
        int[,] brick;
        public int number;
        public bool isHorizontal;

        public Brick(bool isHorizontal, int number)
        {
            this.number = number;
            this.isHorizontal = isHorizontal;

            if (isHorizontal)
                brick = new int[1, 2];
            else
                brick = new int[2, 1];

            initializeWithNumber(number);
        }

        /// <summary>
        /// Inserts the numbers in the brick
        /// </summary>
        /// <param name="number">Number of the brick</param>
        public void initializeWithNumber(int number)
        {
            for (int i = 0; i < brick.GetLength(0); i++)
            {
                for (int j = 0; j < brick.GetLength(1); j++)
                {
                    brick[i, j] = number;
                }
            }
        }

        /// <summary>
        /// Rotates the brick
        /// </summary>
        public void rotateBrick()
        {
            if (isHorizontal)
            {
                brick = new int[2, 1];
                isHorizontal = false;
                initializeWithNumber(number);
            }
            else
            {
                brick = new int[1, 2];
                isHorizontal = true;
                initializeWithNumber(number);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(new String('-', brick.GetLength(1)*2+1));
            for (int i = 0; i < brick.GetLength(0); i++)
            {
                stringBuilder.Append('|');
                for (int j = 0; j < brick.GetLength(1); j++)
                {
                    if(j==brick.GetLength(1)-1)
                        stringBuilder.Append(brick[i, j].ToString());
                    else
                        stringBuilder.Append(brick[i, j].ToString() + " ");
                }
                stringBuilder.AppendLine("|");
            }
            stringBuilder.AppendLine(new String('-',brick.GetLength(1)*2+1));

            return stringBuilder.ToString();
        }
    }
}

