/// <summary>
/// All mini games must implement this interface
/// </summary>
public interface IMiniGame {
    public delegate void MiniGameSuccessEvent(object sender, MiniGameEventArgs e);
    public event MiniGameSuccessEvent MiniGameSuccess;
}