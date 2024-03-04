/// <summary>
/// Parameter to set in the MiniGame events.
/// </summary>
public class MiniGameEventArgs {
    
    public static readonly MiniGameEventArgs Empty;
    public MiniGameEventArgs() {}
        
    public MiniGameEventArgs(float time) { Time = time;}
    public float Time { get; }
}