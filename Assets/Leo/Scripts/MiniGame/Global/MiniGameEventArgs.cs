/// <summary>
/// Parameter to set in the MiniGame events.
/// </summary>
public class MiniGameEventArgs {
    
    public static readonly MiniGameEventArgs Empty;
    public MiniGameEventArgs() {}

    public MiniGameEventArgs(float time, Bonus bonus)
    {
        Time = time;
        Bonus = bonus;
    }
    
    public MiniGameEventArgs(Bonus bonus)
    {
        Bonus = bonus;
    }
    
    public float Time { get; }
    
    public Bonus Bonus { get; }
}