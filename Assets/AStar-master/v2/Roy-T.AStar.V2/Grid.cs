using System;
using System.Collections.Generic;

namespace Roy_T.AStar.V2
{
    public class Grid
    {
        private readonly INode[,,] Nodes;

        public Grid(int columns, int rows, int depths, float xDistance, float yDistance, float zDistance, Velocity defaultSpeed, Connections connections = Connections.LateralAndDiagonal)
        {
            if (columns < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(columns), $"Argument {nameof(columns)} is {columns} but should be >= 1");
            }

            if (rows < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(rows), $"Argument {nameof(rows)} is {rows} but should be >= 1");
            }

            if (depths < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(depths), $"Argument {nameof(depths)} is {depths} but should be >= 1");
            }

            if (defaultSpeed.MetersPerSecond <= 0.0f)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(defaultSpeed), $"Argument {nameof(defaultSpeed)} is {defaultSpeed} but should be > 0.0 m/s");
            }

            if (xDistance <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(xDistance), $"Argument {nameof(xDistance)} is {xDistance} but should be > 0");
            }

            if (yDistance <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(yDistance), $"Argument {nameof(yDistance)} is {yDistance} but should be > 0");
            }

            if (zDistance <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(zDistance), $"Argument {nameof(zDistance)} is {zDistance} but should be > 0");
            }

            this.Columns = columns;
            this.Rows = rows;
            this.Depths = depths;

            this.Nodes = new INode[columns, rows, depths];

            this.CreateNodes(xDistance, yDistance, zDistance);

            switch (connections)
            {
                case Connections.Lateral:
                    this.CreateLateralConnections(defaultSpeed);
                    break;
                case Connections.Diagonal:
                    this.CreateDiagonalConnections(defaultSpeed);
                    break;
                default:
                    this.CreateLateralConnections(defaultSpeed);
                    this.CreateDiagonalConnections(defaultSpeed);
                    break;
            }
        }

        public INode GetNode(int x, int y, int z) => this.Nodes[x, y, z];

        public IReadOnlyList<INode> GetAllNodes()
        {
            var list = new List<INode>(this.Columns * this.Rows * this.Depths);

            for (var x = 0; x < this.Columns; x++)
            {
                for (var y = 0; y < this.Rows; y++)
                {
                    for (int z = 0; z < this.Depths; z++)
                    {
                        list.Add(this.Nodes[x, y, z]);
                    }
                }
            }

            return list;
        }

        public void BlockNode(int x, int y, int z)
        {
            var node = this.Nodes[x, y, z];

            foreach (var outgoingEdge in node.Outgoing)
            {
                var opposite = outgoingEdge.End;
                opposite.Incoming.Remove(outgoingEdge);
            }

            node.Incoming.Clear();

            foreach (var incomingEdge in node.Incoming)
            {
                var opposite = incomingEdge.Start;
                opposite.Outgoing.Remove(incomingEdge);
            }

            node.Outgoing.Clear();
        }

        private void CreateNodes(float xDistance, float yDistance, float zDistance)
        {
            for (var x = 0; x < this.Columns; x++)
            {
                for (var y = 0; y < this.Rows; y++)
                {
                    for (int z = 0; z < this.Depths; z++)
                    {
                        this.Nodes[x, y, z] = new Node(x * xDistance, y * yDistance, z * zDistance);
                    }
                }
            }
        }

        private void CreateLateralConnections(Velocity defaultSpeed)
        {
            for (var x = 0; x < this.Columns; x++)
            {
                for (var y = 0; y < this.Rows; y++)
                {
                    for (int z = 0; z < this.Depths; z++)
                    {
                        var node = this.Nodes[x, y, z];

                        if (x < this.Columns - 1)
                        {
                            var eastNode = this.Nodes[x + 1, y, z];
                            node.Connect(eastNode, defaultSpeed);
                            eastNode.Connect(node, defaultSpeed);
                        }

                        if (y < this.Rows - 1)
                        {
                            var downNode = this.Nodes[x, y + 1, z];
                            node.Connect(downNode, defaultSpeed);
                            downNode.Connect(node, defaultSpeed);
                        }

                        if (z < this.Depths - 1)
                        {
                            var backNode = this.Nodes[x, y, z + 1];
                            node.Connect(backNode, defaultSpeed);
                            backNode.Connect(node, defaultSpeed);
                        }
                    }
                }
            }
        }

        private void CreateDiagonalConnections(Velocity defaultSpeed)
        {
            for (var x = 0; x < this.Columns; x++)
            {
                for (var y = 0; y < this.Rows; y++)
                {
                    for (int z = 0; z < this.Depths; z++)
                    {
                        var node = this.Nodes[x, y, z];

                        if (x < this.Columns - 1 && y < this.Rows - 1)
                        {
                            var southEastNode = this.Nodes[x + 1, y + 1, z];
                            node.Connect(southEastNode, defaultSpeed);
                            southEastNode.Connect(node, defaultSpeed);
                        }

                        if (x > 0 && y < this.Rows - 1)
                        {
                            var southWestNode = this.Nodes[x - 1, y + 1, z];
                            node.Connect(southWestNode, defaultSpeed);
                            southWestNode.Connect(node, defaultSpeed);
                        }

                        // TODO: keep going?
                    }
                }
            }
        }

        public int Columns { get; }
        public int Rows { get; }
        public int Depths { get; }
    }
}
