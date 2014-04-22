using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.ReportVerification
{
    public class VerificationPresenter
    {
        public IVerificationView View { get; set; }

        public void DoVerification()
        {
            ReportDataReader reader = new ReportDataReader();

            this.View.Model.SourceDataSet = reader.ReadDataSet(this.View.Model.SourceFile, this.View.Model.SourceHeaderRowNumber);
            this.View.Model.DestinationDataSet = reader.ReadDataSet(this.View.Model.DestinationFile, this.View.Model.DestinationHeaderRowNumber);

            this.ExecuteVerification(this.View.Model);

            this.View.RefreshUI();
        }

        private void ExecuteVerification(ReportVerificationModel reportVerificationModel)
        {

            reportVerificationModel.IsIdentity = true;

            foreach (var item in reportVerificationModel.DestinationDataSet.Tables.Keys)
            {
                ReportDataTable tableDes = reportVerificationModel.DestinationDataSet.Tables[item];
                string[,] resultData = new string[tableDes.Rows.Count, tableDes.Columns.Count];
                StringBuilder sbMsg = new StringBuilder();

                if (reportVerificationModel.SourceDataSet.Tables.ContainsKey(item))
                {
                    ReportDataTable tableSource = reportVerificationModel.SourceDataSet.Tables[item];

                    int countNew = 0;
                    int countN = 0;

                    for (int i = 0; i < tableDes.Rows.Count; i++)
                    {
                        for (int j = 0; j < tableDes.Columns.Count; j++)
                        {
                            string rowName = tableDes.Rows[i];
                            string columnName = tableDes.Columns[j];

                            if (j == 0 || i == 0)
                            {
                                resultData[i, j] = tableDes[rowName, columnName];
                            }
                            else
                            {

                                string valueDes = tableDes[rowName, columnName];
                                string valueSource = tableSource[rowName, columnName];
                                if (valueSource == null)
                                {
                                    reportVerificationModel.IsIdentity = false;
                                    resultData[i, j] = "New " + valueDes;
                                    countNew++ ;
                                }
                                else
                                {
                                    if (valueDes == valueSource)
                                    {
                                        resultData[i, j] = "Y";
                                    }
                                    else
                                    {
                                        reportVerificationModel.IsIdentity = false;
                                        resultData[i, j] = string.Format("N({0}/{1})", valueDes, valueSource);
                                        countN++;
                                    }
                                }
                            }
                        }
                    }

                    if(countN > 0)
                    {
                        sbMsg.Append(countN + " cells are not identity.");
                    }
                    else if(countNew >0)
                    {
                        sbMsg.Append(countNew + " cells are new.");
                    }
                    else
                    {
                        sbMsg.Append("All cells are identity.");
                    }
                    
                }
                else
                {
                    reportVerificationModel.IsIdentity = false;
                    for (int i = 0; i < tableDes.Rows.Count; i++)
                    {
                        for (int j = 0; j < tableDes.Columns.Count; j++)
                        {
                            string rowName = tableDes.Rows[i];
                            string columnName = tableDes.Columns[j];
                            string valueDes = tableDes[rowName, columnName];

                            if (j == 0 || i == 0)
                            {
                                resultData[i, j] = tableDes[rowName, columnName];
                            }
                            else
                            {
                               resultData[i, j] = "New";
                            }
                        }
                    }

                    sbMsg.Append("The sheet is new, it it not existing on the source file.");
                }

                reportVerificationModel.VerifiedResultMsg.Add(item, sbMsg.ToString());
                reportVerificationModel.VerifiedResultSet.Tables.Add(item, new ReportDataTable(resultData, item));
            }
        }
    }
}
            