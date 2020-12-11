using System;
using System.Collections.Generic;
using System.Text;

namespace Mentormate_assignment
{
    interface ILogic
    {
        public void createSecondLayer();

        public void printSecondLayer();

        public void initializeFirstLayer(int rows, int cols);

        public int createFirstRowOfSecondLayer();

        public bool isThereVerticalBrick(int col);

        public bool isThereHorizontalBrick(int col);

        public void insertValueVertical(int number);

        public void insertValueHorizontal(int number);

        public bool isEmpty(int row, int col);

        public void fillNextRowOfSecondLayer(int rowToFill);

        public void fillSecondLayer();

        public void ValidateLayer(Layer layer);

    }
}
