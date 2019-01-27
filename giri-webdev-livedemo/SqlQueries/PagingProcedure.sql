--Paging Procedure
CREATE PROCEDURE [dbo].[PagingProcedure]

@PageIndex int =1,
@PageSize int=10,
@RecordCount int output,
@SortOrder nvarchar(4),
@SortColumn nvarchar(25)

AS

SET NOCOUNT ON;

SELECT ROW_NUMBER() OVER(ORDER BY ProductID) As RowNumber, ProductID, ProductName INTO #TempTable FROM Products

SELECT @RecordCount=COUNT(*) FROM #TempTable

SELECT * FROM #TempTable WHERE RowNumber BETWEEN (@PageIndex-1) * @PageSize+1 AND (((@PageIndex-1) * @PageSize+1) + @PageSize)-1 ORDER BY
CASE WHEN @SortColumn ='ProductID' AND @SortOrder='ASC' THEN ProductID END ASC,
CASE WHEN @SortColumn ='ProductID' AND @SortOrder='DESC' THEN ProductID END DESC,
CASE WHEN @SortColumn ='ProductName' AND @SortOrder='ASC' THEN ProductName END ASC,
CASE WHEN @SortColumn ='ProductName' AND @SortOrder='DESC' THEN ProductName END DESC

DROP TABLE #TempTable

GO



----Execute the procedure
BEGIN
DECLARE @TotalRecords int
EXEC [PagingProcedure] @RecordCount=@TotalRecords output,@SortColumn='ProductName',@SortOrder='ASC',@PageIndex=2
PRINT @TotalRecords
END
