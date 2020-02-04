using System.Collections.Generic;

namespace Roy_T.AStar.V2
{
    public sealed class Node : INode
    {
        public Node(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Incoming = new List<IEdge>(0);
            this.Outgoing = new List<IEdge>(0);
        }

        public IList<IEdge> Incoming { get; }
        public IList<IEdge> Outgoing { get; }

        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public void Connect(INode node, Velocity traversalVelocity)
        {
            var edge = new Edge(this, node, traversalVelocity);
            this.Outgoing.Add(edge);
            node.Incoming.Add(edge);
        }

        public override string ToString() => $"({this.X:F2}, {this.Y:F2}, {this.Z:F2})";
    }
}
