USE [Salesforce]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[[Object]_Save_p]
AS
-- =============================================
-- Author:		Tony.Wu
-- Create date: [@Date]
-- Description:	Archive [Object] Data
-- =============================================
BEGIN
	SET NOCOUNT ON


    MERGE Salesforce.dbo.[Object] AS t
	USING (SELECT
		   [Id],
		   [InsertPlaceHolderTarget]
		   [SystemModStamp]
    FROM Salesforce.dbo.w[Object]
		   ) 
		AS w		  
		ON (t.Salesforce_id = w.Id)
	WHEN MATCHED THEN
		UPDATE
		SET 
		   [UpdatePlaceHolder]
		   [SystemModStamp] = w.[SystemModStamp],
		   [UpdateDate] = getdate()
    WHEN NOT MATCHED  BY TARGET THEN
	   INSERT
		   (
		   [Salesforce_id],
		   [InsertPlaceHolderTarget]
		   [SystemModStamp],
		   [InsertDate],
		   [UpdateDate]
		   )
	   VALUES
		   (
		   w.[Id],
		   [InsertPlaceHolderSource]
		   w.[SystemModStamp],
		   getdate(),
		   getdate()
		   );

	--3. UPDATE Type DUE DATE
	DECLARE @dueDate datetime
	SELECT @dueDate = MAX([SystemModStamp]) FROM Salesforce.dbo.w[Object]

	UPDATE Salesforce.dbo.SalesForceServiceExecution
	SET DataDueDate = Salesforce.dbo.ToSalesforceDate_f(@dueDate)
	WHERE [TYPE] = '[Object]'

	--4. Clean up the working table
	TRUNCATE TABLE Salesforce.dbo.w[Object]
END

GO

GRANT execute on [dbo].[[Object]_Save_p] to SalesforceBaseRole

GO