using System.Collections.Generic;

public class GridData
{
    public int MoveLimit { get; set; }
    public int RowCount { get; set; }
    public int ColCount { get; set; }
    public List<CellInfo> CellInfo { get; set; }
    public List<MovableInfo> MovableInfo { get; set; }
    public List<ExitInfo> ExitInfo { get; set; }
}
public class CellInfo
{
    public int Row { get; set; }
    public int Col { get; set; }
}

public class ExitInfo
{
    public int Row { get; set; }
    public int Col { get; set; }
    public int Direction { get; set; }
    public int Colors { get; set; }
}

public class MovableInfo
{
    public int Row { get; set; }
    public int Col { get; set; }
    public List<int> Direction { get; set; }
    public int Length { get; set; }
    public int Colors { get; set; }
}

