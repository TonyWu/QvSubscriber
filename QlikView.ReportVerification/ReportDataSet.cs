using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace QlikView.ReportVerification
{
    public class ReportDataSet
    {
        public Dictionary<string,ReportDataTable> Tables { get; set; }

        public ReportDataSet()
        {
            this.Tables = new Dictionary<string, ReportDataTable>();
        }
    }
    
    public class ReportDataTable
    {
        public string Name { get; set; }

        public List<string> Columns
        {
            get
            {
                return this.columnMappings.Keys.ToList();
            }
        }
        public List<string> Rows
        {
            get
            {
                return this.rowMappings.Keys.ToList();
            }
        }

        public DataTable DataTable
        {
            get
            {
                return ConvertToDataTable(this._dataMatrix);
            }
        }

        private string[,] _dataMatrix;
        private Dictionary<string, int> columnMappings;
        private Dictionary<string, int> rowMappings;

        public ReportDataTable(string[,] dataMatrix, string name)
        {
            this.Name = name;
            this._dataMatrix = dataMatrix;

            this.columnMappings = new Dictionary<string, int>();
            for (int i = 0; i < this._dataMatrix.GetLength(1); i++)
            {
                this.columnMappings.Add(this._dataMatrix[0, i], i);
            }

            this.rowMappings = new Dictionary<string, int>();
            for (int i = 0; i < this._dataMatrix.GetLength(0); i++)
            {
                this.rowMappings.Add(this._dataMatrix[i, 0], i);
            }
        }

        public string this[string row, string column]
        {
            get
            {
                if (this.rowMappings.ContainsKey(row) && this.columnMappings.ContainsKey(column))
                {
                    int rowNumber = this.rowMappings[row];
                    int columnNumber = this.columnMappings[column];
                    return this._dataMatrix[rowNumber, columnNumber];
                }
                else
                {
                    return null;
                }
            }
        }

        private static DataTable ConvertToDataTable(string[,] arr)
        {

            DataTable dataSouce = new DataTable();
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                DataColumn newColumn = new DataColumn(i.ToString(), arr[0, 0].GetType());
                dataSouce.Columns.Add(newColumn);
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                DataRow newRow = dataSouce.NewRow();
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    newRow[j.ToString()] = arr[i, j];
                }
                dataSouce.Rows.Add(newRow);
            }
            return dataSouce;

        }
    }
}
