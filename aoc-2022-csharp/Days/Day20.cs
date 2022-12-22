using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day20 : Day
    {
        record Node
        {
            public BigInteger value;
            public Node? previous = null;
            public Node? next = null;

            public Node(BigInteger value) => this.value = value;
        }

        public override dynamic PartOne()
        {
            return DecryptList(false);
        }

        public override dynamic PartTwo()
        {
            return DecryptList(true);
        }

        private BigInteger DecryptList(bool p2)
        {
            List<Node> nodes = new List<Node>();
            foreach (string line in input.Split("\n"))
                nodes.Add(new Node(BigInteger.Parse(line) * (p2 ? 811589153 : 1)));

            for (int i = 0; i < nodes.Count - 1; i++)
                nodes[i].next = nodes[i + 1];

            for (int i = 1; i < nodes.Count; i++)
                nodes[i].previous = nodes[i - 1];

            nodes[0].previous = nodes[nodes.Count - 1];
            nodes[nodes.Count - 1].next = nodes[0];

            int maxIndex = nodes.Count - 1;

            Node? zeroNode = null;
            for (int mixCount = 0; mixCount < (p2 ? 10 : 1); mixCount++)
            {

                foreach (Node? node in nodes)
                {
                    if (node.value == 0)
                        zeroNode = node;

                    Node targetNode = node;

                    for (int i = 0; i < BigInteger.Abs(node.value) % maxIndex; i++)
                        targetNode = node.value > 0 ? targetNode.next! : targetNode.previous!;

                    if (node == targetNode)
                        continue;

                    if (node.value > 0)
                    {
                        node.next!.previous = node.previous;
                        node.previous!.next = node.next;
                        node.next = targetNode.next;
                        node.previous = targetNode;

                        targetNode.next!.previous = node;
                        targetNode.next = node;
                    }
                    else
                    {
                        node.previous!.next = node.next;
                        node.next!.previous = node.previous;
                        node.previous = targetNode.previous;
                        node.next = targetNode;

                        targetNode.previous!.next = node;
                        targetNode.previous = node;
                    }
                }
            }

            BigInteger ret = 0;
            for (int i = 1; i <= 3000; i++)
            {
                zeroNode = zeroNode!.next;

                if (i % 1000 == 0)
                    ret += zeroNode!.value;
            }

            return ret;
        }
    }
}
