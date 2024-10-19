using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TicTacToe.Models;

namespace TicTacToe.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private CellStatus _currentPlayerStatus;
        public CellStatus CurrentPlayerStatus
        {
            get => _currentPlayerStatus;
            private set
            {
                _currentPlayerStatus = value;
                RaisePropertyChanged(nameof(CurrentPlayerStatus));
            }
        }

        public List<List<CellBtnVM>> AllCells { get; } = new List<List<CellBtnVM>>();

        private int _cellRowColumnCount = 3;

        public MainWindowViewModel()
        {
            CurrentPlayerStatus = CellStatus.Cross;

            for (int row = 0; row < _cellRowColumnCount; row++)
            {
                var newRow = new List<CellBtnVM>();
                for (int column = 0; column < _cellRowColumnCount; column++)
                {
                    var newCell = new CellBtnVM(row, column, CheckGAmeStatus);
                    newRow.Add(newCell);
                }
                AllCells.Add(newRow);
            }
        }

        private void CheckGAmeStatus(CellBtnVM lastClickBtn)
        {
            //first check #Horizontal chech
            var columnCheck = new List<CellBtnVM>();
            for (int i = 0; i <= _cellRowColumnCount / 2; i++)
            {
                int column = lastClickBtn.Column + 1 + i;
                if (column < _cellRowColumnCount)
                {
                    columnCheck.Add(AllCells[lastClickBtn.Row][column]);
                }
                else
                {
                    columnCheck.Add(AllCells[lastClickBtn.Row][column - _cellRowColumnCount]);
                }
            }
            if (columnCheck.All(x => x.Status == lastClickBtn.Status))
            {
                StopGame(lastClickBtn.Status);
                return;
            }
            //second check #Vertical chech
            var rowCheck = new List<CellBtnVM>();
            for (int i = 0; i <= _cellRowColumnCount / 2; i++)
            {
                int row = lastClickBtn.Row + 1 + i;
                if (row < _cellRowColumnCount)
                {
                    rowCheck.Add(AllCells[row][lastClickBtn.Column]);
                }
                else
                {
                    rowCheck.Add(AllCells[row - _cellRowColumnCount][lastClickBtn.Column]);
                }
            }
            if (rowCheck.All(x => x.Status == lastClickBtn.Status))
            {
                StopGame(lastClickBtn.Status);
                return;
            }
            //before diagonal check
            int checkCross = Math.Abs(lastClickBtn.Column - lastClickBtn.Row);
            if (checkCross != 0 && checkCross != _cellRowColumnCount - 1) return;

            //third check #Diagonal chech
            var crossMainCheck = new List<CellBtnVM>();
            for (int i = 0; i <= _cellRowColumnCount / 2; i++)
            {
                int column = lastClickBtn.Column + 1 + i;
                int row = lastClickBtn.Row + 1 + i;
                if (column < _cellRowColumnCount)
                {
                    crossMainCheck.Add(AllCells[row][column]);
                }
                else
                {
                    crossMainCheck.Add(AllCells[row - _cellRowColumnCount][column - _cellRowColumnCount]);
                }
            }
            if (crossMainCheck.All(x => x.Status == lastClickBtn.Status))
            {
                StopGame(lastClickBtn.Status);
                return;
            }
            //4th check #Diagonal chech
            var crossSecondCheck = new List<CellBtnVM>();
            for (int i = 0; i <= _cellRowColumnCount / 2; i++)
            {
                int column = lastClickBtn.Column + 1 + i;
                int row = lastClickBtn.Row + 1 - i;
                if (column < _cellRowColumnCount)
                {
                    crossSecondCheck.Add(AllCells[row][column]);
                }
                else
                {
                    crossSecondCheck.Add(AllCells[row + _cellRowColumnCount][column - _cellRowColumnCount]);
                }
            }
            if (crossSecondCheck.All(x => x.Status == lastClickBtn.Status))
            {
                StopGame(lastClickBtn.Status);
                return;
            }

            CurrentPlayerStatus = lastClickBtn.Status == CellStatus.Cross ? CellStatus.Circle : CellStatus.Cross;
        }

        private void StopGame(CellStatus winnerStatus)
        {
            MessageBox.Show($"Winner: {winnerStatus}");
            foreach (var row in AllCells)
            {
                foreach (var cell in row)
                {
                    cell.Status = CellStatus.Empty;
                }
            }

        }
    }
}
