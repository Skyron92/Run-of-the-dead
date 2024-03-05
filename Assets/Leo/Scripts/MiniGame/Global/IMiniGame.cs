using System.Reflection;

/// <summary>
/// All mini games must implement this interface
/// </summary>
public interface IMiniGame {
    public delegate void MiniGameWonEvent(object sender, MiniGameEventArgs e);
    public event MiniGameWonEvent MiniGameWon;
}