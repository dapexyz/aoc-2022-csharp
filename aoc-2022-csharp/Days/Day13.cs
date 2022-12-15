using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace aoc_2022_csharp.Days
{
    internal class Day13 : Day
    {
        public override dynamic PartOne()
        {
            int sum = 0;
            int index = 1;
            foreach (var chunk in (from e in input.Split("\n") where e != "" select JsonNode.Parse(e)).Chunk(2))
            {
                if (Check(chunk[0], chunk[1]) < 0)
                    sum += index;

                index++;
            }
            return sum;
        }

        public override dynamic PartTwo()
        {
            List<JsonNode> packets = new();
            foreach (var chunk in (from e in input.Split("\n") where e != "" select JsonNode.Parse(e)))
                packets.Add(chunk);

            JsonNode two = JsonNode.Parse("[[2]]");
            JsonNode six = JsonNode.Parse("[[6]]");
            packets.Add(two);
            packets.Add(six);

            packets.Sort(Check);

            return (packets.IndexOf(two) + 1) * (packets.IndexOf(six) + 1);
        }

        private int Check(JsonNode j1, JsonNode j2)
        {
            if (j1 is JsonValue && j2 is JsonValue)
                return (int)j1 - (int)j2;

            JsonArray ja1 = j1 as JsonArray;
            JsonArray ja2 = j2 as JsonArray;

            if (ja1 == null)
                ja1 = new((int)j1);

            if (ja2 == null)
                ja2 = new((int)j2);

            foreach((JsonNode jn1, JsonNode jn2) in ja1.Zip(ja2))
            {
                int comp = Check(jn1, jn2);
                if (comp != 0)
                    return comp;
            }

            return ja1.Count - ja2.Count;
        }
    }
}
