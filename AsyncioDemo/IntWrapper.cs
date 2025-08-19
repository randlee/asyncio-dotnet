namespace AsyncioDemo;

/// <summary>
/// A simple wrapper class that wraps an integer value.
/// This demonstrates reference type returns for Python.NET integration.
/// </summary>
public class IntWrapper
{
    public int Value { get; set; }
    
    public IntWrapper(int value)
    {
        Value = value;
    }
    
    public override string ToString()
    {
        return $"IntWrapper({Value})";
    }
    
    public override bool Equals(object? obj)
    {
        return obj is IntWrapper other && Value == other.Value;
    }
    
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
