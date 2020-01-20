public enum WalksOn
{
    all,
    groundOnly,
    surfaceOnly,
    airOnly
}

public static class Vector3Diagonal
{
    private static float p {get{return  1;}}
    private static float n {get{return -1;}}
    
    public static UnityEngine.Vector3
    rghtUpForw = new UnityEngine.Vector3(p, p, p), rghtDnForw = new UnityEngine.Vector3(p, n, p),
    leftUpForw = new UnityEngine.Vector3(n, p, p), leftDnForw = new UnityEngine.Vector3(n, n, p),
    rghtUpBack = new UnityEngine.Vector3(p, p, n), rghtDnBack = new UnityEngine.Vector3(p, n, n),
    leftUpBack = new UnityEngine.Vector3(n, p, n), leftDnBack = new UnityEngine.Vector3(n, n, n),

    rghtForw = new UnityEngine.Vector3(p, 0, p), rghtBack = new UnityEngine.Vector3(p, 0, n),
    leftForw = new UnityEngine.Vector3(n, 0, p), leftBack = new UnityEngine.Vector3(n, 0, n),

    rghtUp   = new UnityEngine.Vector3(p, p, 0), rghtDn   = new UnityEngine.Vector3(p, n, 0),
    leftUp   = new UnityEngine.Vector3(n, p, 0), leftDn   = new UnityEngine.Vector3(n, n, 0),
    
    forwUp   = new UnityEngine.Vector3(0, p, p), forwDn   = new UnityEngine.Vector3(0, n, p),
    backUp   = new UnityEngine.Vector3(0, p, n), backDn   = new UnityEngine.Vector3(0, n, n);
}