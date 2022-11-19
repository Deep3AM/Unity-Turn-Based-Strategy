public class PathNode
{
    private GridPosition gridPosition;
    private int gCost;
    private int hCost;
    private int wCost;
    private PathNode cameFromPathNode;

    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public int GetGCost()
    {
        return gCost;
    }

    public int GetHCost()
    {
        return hCost;
    }

    public int GetWCost()
    {
        return wCost;
    }
}
