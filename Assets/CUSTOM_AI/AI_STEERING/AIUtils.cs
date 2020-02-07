using UnityEngine;

public enum WalksOn
{
    all,
    groundOnly,
    surfaceOnly,
    airOnly
}

public enum PathfindsThrough
{
    both,
    ground,
    surface
}

public static class Vector3Diagonal
{
    private static float p {get{return  1;}}
    private static float n {get{return -1;}}
    
    public static Vector3
    rghtUpForw = new Vector3(p, p, p), rghtDnForw = new Vector3(p, n, p),
    leftUpForw = new Vector3(n, p, p), leftDnForw = new Vector3(n, n, p),
    rghtUpBack = new Vector3(p, p, n), rghtDnBack = new Vector3(p, n, n),
    leftUpBack = new Vector3(n, p, n), leftDnBack = new Vector3(n, n, n),

    rghtForw = new Vector3(p, 0, p), rghtBack = new Vector3(p, 0, n),
    leftForw = new Vector3(n, 0, p), leftBack = new Vector3(n, 0, n),

    rghtUp   = new Vector3(p, p, 0), rghtDn   = new Vector3(p, n, 0),
    leftUp   = new Vector3(n, p, 0), leftDn   = new Vector3(n, n, 0),
    
    forwUp   = new Vector3(0, p, p), forwDn   = new Vector3(0, n, p),
    backUp   = new Vector3(0, p, n), backDn   = new Vector3(0, n, n);
}

public class Status
{
    public const int 
        AIR     = 0, 
        SOLID   = 9, 
        GROUND  = 2, 
        SURFACE = 3,
        BORDER  = 8;
}

public enum RangeType
{
    sight,
    detect,
    custom
}