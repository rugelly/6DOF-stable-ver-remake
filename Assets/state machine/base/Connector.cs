namespace PluggableFSM
{
[System.Serializable]
public class Connector
{
    public Connection connection;
    public enum WantedResult {isTrue, isFalse}
    public WantedResult wantedResult;
    public State next;
}
}