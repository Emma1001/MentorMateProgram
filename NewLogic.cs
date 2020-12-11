using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mentormate_assignment
{
    /// <summary>
    /// This is the same class as "Logic" but the "CreateFirstRowOfSecondLayer" is modified in a way where
    /// 2 vertical bricks can not lay on one horizontal
    /// </summary>
    class NewLogic : ILogic
    {
        public Layer firstLayer { get; set; }
        public Layer secondLayer { get; set; }

        public NewLogic()
        {

        }

        public void createSecondLayer()
        {
            int number = createFirstRowOfSecondLayer();
            if (number == -1)
            {
                Console.WriteLine("-1");
                return;
            }

            fillNextRowOfSecondLayer(secondLayer.layerValues.GetLength(0) - 2);
            fillSecondLayer();
            printSecondLayer();
        }

        public void printSecondLayer()
        {
            Console.WriteLine(new String('-', secondLayer.layerValues.GetLength(1) * 3));
            for (int i = 0; i < secondLayer.layerValues.GetLength(0); i++)
            {
                for (int j = 0; j < secondLayer.layerValues.GetLength(1); j++)
                {
                    if (j < secondLayer.layerValues.GetLength(1) - 1)
                    {
                        if (secondLayer.layerValues[i, j] == secondLayer.layerValues[i, j + 1])
                        {
                            Console.Write("|" + secondLayer.layerValues[i, j] + " " + secondLayer.layerValues[i, j + 1] + "|");
                            j++;
                        }
                        else
                        {
                            Console.Write("|" + secondLayer.layerValues[i, j] + "|");
                        }
                    }
                    else
                    {
                        Console.Write("|" + secondLayer.layerValues[i, j] + "|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(new String('-', secondLayer.layerValues.GetLength(1) * 3));
        }

        public void initializeFirstLayer(int rows, int cols)
        {
            firstLayer = new Layer(rows, cols);
            secondLayer = new Layer(rows, cols);

            for (int i = 0; i < rows; i++)
            {
                int[] array = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                if (array.Length != cols)
                {
                    Console.WriteLine("Invalid input!");
                    Environment.Exit(0);
                }

                int k = 0;
                foreach (var item in array)
                {
                    firstLayer.layerValues[i, k] = item;
                    k++;
                }
            }

            for (int row = 0; row < firstLayer.layerValues.GetLength(0); row++)
            {
                for (int col = 0; col < firstLayer.layerValues.GetLength(1); col++)
                {
                    if (col < firstLayer.layerValues.GetLength(1) - 1)
                    {
                        if (firstLayer.layerValues[row, col] == firstLayer.layerValues[row, col + 1])
                        {
                            firstLayer.layerQueue.Enqueue(new Brick(true, firstLayer.layerValues[row, col]));
                            col++;
                        }
                    }
                    if (row < firstLayer.layerValues.GetLength(0) - 1)
                    {
                        if (firstLayer.layerValues[row, col] == firstLayer.layerValues[row + 1, col])
                        {
                            firstLayer.layerQueue.Enqueue(new Brick(false, firstLayer.layerValues[row, col]));
                        }
                    }

                }
            }

            ValidateLayer(firstLayer);
        }

        public void ValidateLayer(Layer layer)
        {
            int numberOfBricks = (layer.layerValues.GetLength(0) * layer.layerValues.GetLength(1)) / 2;
            if (layer.layerQueue.Count != numberOfBricks)
            {
                Console.WriteLine("Invalid input!");
                Environment.Exit(0);
            }
        }
       
        public int createFirstRowOfSecondLayer()
        {
            Brick brick = null;
            for (int j = 0; j < firstLayer.layerValues.GetLength(1); j++)
            {
                if (firstLayer.layerQueue.Count == 0)
                    break;

                if (isThereHorizontalBrick(j) && isThereVerticalBrick(j))
                {
                    return -1;
                }

                if(j == firstLayer.layerValues.GetLength(1) - 1)
                {
                    if (isThereVerticalBrick(j))
                    {
                        return -1;
                    }

                    brick = firstLayer.layerQueue.Dequeue();
                    if (brick.isHorizontal)
                        brick.rotateBrick();

                    insertValueVertical(brick.number);
                }

                if (isThereHorizontalBrick(j))
                {
                    brick = firstLayer.layerQueue.Dequeue();
                    if (brick.isHorizontal)
                        brick.rotateBrick();

                    insertValueVertical(brick.number);
                }
                else if (isThereVerticalBrick(j))
                {
                    brick = firstLayer.layerQueue.Dequeue();
                    if (!brick.isHorizontal)
                        brick.rotateBrick();

                    insertValueHorizontal(brick.number);
                    j++;
                }
                else
                {
                    brick = firstLayer.layerQueue.Dequeue();
                    if (!brick.isHorizontal)
                        brick.rotateBrick();

                    insertValueHorizontal(brick.number);
                    j++;
                }
            }

            return 1;
        }

        public bool isThereVerticalBrick(int col)
        {
            for (int i = 0; i < firstLayer.layerValues.GetLength(0) - 1; i++)
            {
                if (firstLayer.layerValues[i, col] == firstLayer.layerValues[i + 1, col])
                {
                    return true;
                }
            }

            return false;
        }

        public bool isThereHorizontalBrick(int col)
        {
            if (col == firstLayer.layerValues.GetLength(1) - 1)
                return false;

            for (int i = 0; i < firstLayer.layerValues.GetLength(0) - 1; i++)
            {
                if (firstLayer.layerValues[i, col] == firstLayer.layerValues[i, col + 1])
                {
                    return true;
                }

            }

            return false;
        }

        public void insertValueVertical(int number)
        {
            for (int i = secondLayer.layerValues.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < secondLayer.layerValues.GetLength(1); j++)
                {
                    if (isEmpty(i, j) && isEmpty(i - 1, j))
                    {
                        secondLayer.layerValues[i, j] = number;
                        secondLayer.layerValues[i - 1, j] = number;
                        return;
                    }
                }
            }
        }

        public void insertValueHorizontal(int number)
        {
            for (int i = secondLayer.layerValues.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < secondLayer.layerValues.GetLength(1); j++)
                {
                    if (isEmpty(i, j) && isEmpty(i, j + 1))
                    {
                        secondLayer.layerValues[i, j] = number;
                        secondLayer.layerValues[i, j + 1] = number;
                        return;
                    }
                }
            }
        }

        public bool isEmpty(int row, int col)
        {
            if (row >= secondLayer.layerValues.GetLength(0) || col >= secondLayer.layerValues.GetLength(1)
                || row < 0 || col < 0)
                return false;

            if (secondLayer.layerValues[row, col] == 0)
                return true;

            return false;
        }

        public void fillNextRowOfSecondLayer(int num)
        {
            int rowToFill = num;
            for (int i = 0; i < secondLayer.layerValues.GetLength(1) - 1; i++)
            {
                if (isEmpty(rowToFill, i) && isEmpty(rowToFill, i + 1))
                {
                    Brick brick = firstLayer.layerQueue.Dequeue();
                    if (!brick.isHorizontal)
                        brick.rotateBrick();

                    secondLayer.layerValues[rowToFill, i] = brick.number;
                    secondLayer.layerValues[rowToFill, i + 1] = brick.number;
                }
            }
        }

        public void fillSecondLayer()
        {
            for (int i = secondLayer.layerValues.GetLength(0) - 3; i >= 0; i -= 2)
            {
                for (int j = 0; j < secondLayer.layerValues.GetLength(1); j++)
                {
                    Brick brick = firstLayer.layerQueue.Dequeue();

                    int row = i + 2;
                    if (j < secondLayer.layerValues.GetLength(1) - 1)
                    {
                        if (secondLayer.layerValues[row, j] == secondLayer.layerValues[row, j + 1])
                        {
                            if (!brick.isHorizontal)
                                brick.rotateBrick();

                            secondLayer.layerValues[i, j] = brick.number;
                            secondLayer.layerValues[i, j + 1] = brick.number;
                            j++;
                        }
                        else
                        {
                            if (brick.isHorizontal)
                                brick.rotateBrick();

                            secondLayer.layerValues[i, j] = brick.number;
                            secondLayer.layerValues[i - 1, j] = brick.number;
                        }
                    }
                    else
                    {
                        if (brick.isHorizontal)
                            brick.rotateBrick();

                        secondLayer.layerValues[i, j] = brick.number;
                        secondLayer.layerValues[i - 1, j] = brick.number;
                    }
                }
                fillNextRowOfSecondLayer(i - 1);
            }
        }
    }
}