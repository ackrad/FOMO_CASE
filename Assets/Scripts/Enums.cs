public enum GridUnitType
{
    // empty is inside the grid
    Empty,
    
    Taken,
    // is outside the grid
    // this should not be returned but here as a reminder
    OutOfBounds
}