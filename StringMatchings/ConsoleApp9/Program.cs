using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection("SERVER = BROWN11; database = InternWorkspace; Integrated Security = SSPI"))
            {
                using (SqlConnection group = new SqlConnection("SERVER = BROWN11; database = InternWorkspace; Integrated Security = SSPI"))
                {
                    group.Open();
                    connection.Open();
                    SqlCommand makValues = new SqlCommand("SELECT DISTINCT [mak] FROM [InternWorkspace].[dbo].[MEK7]", connection);
                    SqlCommand makGroup;
                    using (SqlDataReader read = makValues.ExecuteReader())
                    {
                        using (SqlConnection connection2 = new SqlConnection("SERVER=BROWN11;database=InternWorkspace;Integrated Security=SSPI"))
                        {
                            connection2.Open();
                            SqlCommand insertData;
                            Int64 MAK;
                            List<Int64> mek = new List<Int64>();
                            List<String> names = new List<String>();
                            while (read.Read())
                            {
                                MAK = (Int64)read.GetValue(0);
                                makGroup = new SqlCommand($"SELECT [mek],[name],[mak] FROM [InternWorkspace].[dbo].[MEK7] WHERE mak = {MAK}", group);
                                using (SqlDataReader groupReader = makGroup.ExecuteReader())
                                {
                                    while (groupReader.Read())
                                    {
                                        names.Add(groupReader.GetValue(1).ToString());
                                        mek.Add((Int64)groupReader.GetValue(0));
                                    }
                                    for (int i = 0; i < names.Count - 1; i++)
                                    {
                                        for (int j = i + 1; j < names.Count; j++)
                                        {
                                            insertData = new SqlCommand($"INSERT INTO [dbo].[MEK7ResultsJaro]([mek1],[mek2],[name1],[name2],[match],[mak]) VALUES ({mek[i]},{mek[j]},'{names[i].Replace("'", "")}','{names[j].Replace("'", " ")}',{JaroDistance(names[i], names[j])},{MAK})", connection2);
                                            insertData.ExecuteNonQuery();
                                        }
                                    }
                                    names.Clear();
                                    mek.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }
      
        public static double JaccardDistance(string source, string target)
        {
            return 1 - JaccardIndex(target, source);
        }

        public static double JaccardIndex(string source, string target)
        {
            return (Convert.ToDouble(source.Intersect(target).Count())) / (Convert.ToDouble(source.Union(target).Count()));
        }
        public static  double JaroDistance(string source, string target)
        {
            int m = source.Intersect(target).Count();

            if (m == 0) { return 0; }
            else
            {
                string sourceTargetIntersetAsString = "";
                string targetSourceIntersetAsString = "";
                IEnumerable<char> sourceIntersectTarget = source.Intersect(target);
                IEnumerable<char> targetIntersectSource = target.Intersect(source);
                foreach (char character in sourceIntersectTarget) { sourceTargetIntersetAsString += character; }
                foreach (char character in targetIntersectSource) { targetSourceIntersetAsString += character; }
                double t = LevenshteinDistance(targetSourceIntersetAsString,sourceTargetIntersetAsString) / 2;
                return ((m / source.Length) + (m / target.Length) + ((m - t) / m)) / 3;
            }
        }
        public static int LevenshteinDistance(string source, string target)
        {
            if (source.Length == 0) { return target.Length; }
            if (target.Length == 0) { return source.Length; }

            int distance = 0;

            if (source[source.Length - 1] == target[target.Length - 1]) { distance = 0; }
            else { distance = 1; }

            return Math.Min(Math.Min(LevenshteinDistance(source.Substring(0, source.Length - 1), target) + 1,
                                     LevenshteinDistance(source, target.Substring(0, target.Length - 1))) + 1,
                                     LevenshteinDistance(source.Substring(0, source.Length - 1), target.Substring(0, target.Length - 1)) + distance);
        }
        public static double Distance(string s1, string s2)
        {
            if (s1 == null)
            {
                throw new ArgumentNullException(nameof(s1));
            }

            if (s2 == null)
            {
                throw new ArgumentNullException(nameof(s2));
            }

            if (s1.Equals(s2))
            {
                return 0;
            }

            int n = s1.Length, m = s2.Length;

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Create the distance matrix H[0 .. s1.length+1][0 .. s2.length+1]
            int[,] d = new int[n + 2, m + 2];

            //initialize top row and leftmost column
            for (int i = 0; i <= n; i++)
            {
                d[i, 0] = i;
            }
            for (int j = 0; j <= m; j++)
            {
                d[0, j] = j;
            }

            //fill the distance matrix
            int cost;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    //if s1[i - 1] = s2[j - 1] then cost = 0, else cost = 1
                    cost = 1;

                    if (s1[i - 1] == s2[j - 1])
                    {
                        cost = 0;
                    }

                    d[i, j] = Min(
                            d[i - 1, j - 1] + cost, // substitution
                            d[i, j - 1] + 1,        // insertion
                            d[i - 1, j] + 1         // deletion
                    );

                    //transposition check
                    if (i > 1 && j > 1
                            && s1[i - 1] == s2[j - 2]
                            && s1[i - 2] == s2[j - 1]
                        )
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                    }
                }
            }

            return d[n, m];
        }

        private static int Min(int a, int b, int c)
            => Math.Min(a, Math.Min(b, c));
        public static double TanimotoCoefficient(string source, string target)
        {
            double Na = source.Length;
            double Nb = target.Length;
            double Nc = source.Intersect(target).Count();

            return Nc / (Na + Nb - Nc);
        }
        public static double RatcliffObershelpSimilarity(string source, string target)
        {
            return (2 * Convert.ToDouble(source.Intersect(target).Count())) / (Convert.ToDouble(source.Length + target.Length));
        }
        public static double OverlapCoefficient(string source, string target)
        {
            return (Convert.ToDouble(source.Intersect(target).Count())) / Convert.ToDouble(Math.Min(source.Length, target.Length));
        }
        public static double DiceCoefficient(string strA, string strB)
        {
            HashSet<string> setA = new HashSet<string>();
            HashSet<string> setB = new HashSet<string>();

            for (int i = 0; i < strA.Length - 1; ++i)
                setA.Add(strA.Substring(i, 2));

            for (int i = 0; i < strB.Length - 1; ++i)
                setB.Add(strB.Substring(i, 2));

            HashSet<string> intersection = new HashSet<string>(setA);
            intersection.IntersectWith(setB);

            double temp = (2.0 * intersection.Count) / ((1.0 * setA.Count) + (1.0 * setB.Count));
            return temp;
        }
    }
}
