using System;
using System.Collections.Generic;
using System.Text;

namespace Org.X2A.SpellChecker.Core
{
    public class WordTree
    {
        public const int DEFAULT_TOLERANCE = 3;

        class Node
        {
            public string word;
            public Dictionary<int, Node> children;

            public Node(string word)
            {
                this.word = word;
                children = new Dictionary<int, Node>();
            }
        }

        private Node root;

        public WordTree() { }

        public WordTree(IEnumerable<string> words)
        {
            foreach (string word in words)
            {
                Add(word);
            }
        }

        public void Add(string word)
        {
            if (root == null)
            {
                root = new Node(word);
            } else
            {
                AddInternal(new Node(word));
            }
        }

        private void AddInternal(Node newNode)
        {
            Node parent = root;
            int dist = WordUtils.EditDistance(newNode.word, parent.word);
            while (parent.children.ContainsKey(dist))
            {
                parent = parent.children[dist];
                dist = WordUtils.EditDistance(newNode.word, parent.word);
            }
            parent.children[dist] = newNode;
        }

        public List<string> GetBestMatch(string word, int tolerance = DEFAULT_TOLERANCE)
        {
            List<Node> corrections = GetBestMatchInternal(word, root, tolerance);
            List<string> results = new List<string>();
            foreach (Node n in corrections) //for whatever reason it doesn't seem like ConvertALl exists in this version of .NET
            {
                results.Add(n.word);
            }
            results.Sort((a, b) => WordUtils.EditDistance(a, word) - WordUtils.EditDistance(b, word));
            return results;
        }

        

        private List<Node> GetBestMatchInternal(string word, Node root, int tolerance)
        {
            List<Node> similar = new List<Node>();
            int dist = WordUtils.EditDistance(word, root.word);

            if (dist < tolerance)
            {
                similar.Add(root);
            }

            int min = Math.Max(1, dist - tolerance);
            int max = dist + tolerance;

            for (int i = min; i <= max; i++)
            {
                Node child;
                root.children.TryGetValue(i, out child);
                if (child != null)
                {
                    similar.AddRange(GetBestMatchInternal(word, child, tolerance));
                }
            }

            return similar;
        }
    }
}
