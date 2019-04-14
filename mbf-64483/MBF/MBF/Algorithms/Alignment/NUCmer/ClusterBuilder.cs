﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace MBF.Algorithms.Alignment
{
    /// <summary>
    /// Clustering is a process in which individual matches are grouped in larger
    /// set called Cluster. The matches in cluster are decided based on paramters 
    /// like fixed difference allowed, maximum difference allowed, minimum score
    /// and separation factor that should be satisfied.
    /// This class implements IClusterBuilder interface.
    /// </summary>
    public class ClusterBuilder : IClusterBuilder
    {
        /// <summary>
        /// Default fixed Separation
        /// </summary>
        internal const int DefaultFixedSeparation = 5;

        /// <summary>
        /// Default Maximum Separation
        /// </summary>
        internal const int DefaultMaximumSeparation = 1000;

        /// <summary>
        /// Default Minimum Output Score
        /// </summary>
        internal const int DefaultMinimumScore = 200;

        /// <summary>
        /// Default separation factor
        /// </summary>
        internal const float DefaultSeparationFactor = 0.05f;

        /// <summary>
        /// Property refering to Second sequence start in MUM
        /// </summary>
        private const string SecondSequenceStart = "SecondSequenceStart";

        /// <summary>
        /// Property refering to ID of Cluster
        /// </summary>
        private const string ClusterID = "ClusterID";

        /// <summary>
        /// This is a list of number which are used to generate the ID of cluster
        /// </summary>
        private IList<int> _unionFind;

        /// <summary>
        /// Initializes a new instance of the ClusterBuilder class
        /// </summary>
        public ClusterBuilder()
        {
            FixedSeparation = DefaultFixedSeparation;
            MaximumSeparation = DefaultMaximumSeparation;
            MinimumScore = DefaultMinimumScore;
            SeparationFactor = DefaultSeparationFactor;
        }

        /// <summary>
        /// Gets or sets maximum fixed diagonal difference
        /// </summary>
        public int FixedSeparation { get; set; }

        /// <summary>
        /// Gets or sets maximum separation between the adjacent matches in clusters
        /// </summary>
        public int MaximumSeparation { get; set; }

        /// <summary>
        /// Gets or sets minimum output score
        /// </summary>
        public int MinimumScore { get; set; }

        /// <summary>
        /// Gets or sets separation factor. Fraction equal to 
        /// (diagonal difference / match separation) where higher values
        /// increase the insertion or deletion (indel) tolerance
        /// </summary>
        public float SeparationFactor { get; set; }

        /// <summary>
        /// Get the Cluster from given inputs of matches.
        /// Steps are as follows:
        ///     1. Sort MUMs based on query sequence start.
        ///     2. Removing overlapping MUMs (in both sequences) and MUMs with same 
        ///         diagonal offset (usually adjacent)
        ///     3. Check for  separation between two MUMs
        ///     4. Check the diagonal separation
        ///     5. If MUMs passes above conditions merge them in one cluster.
        ///     6. Sort MUMs using cluster id
        ///     7. Process clusters (Joining clusters)</summary>
        /// <param name="matches">List of maximum unique matches</param>
        /// <returns>List of Cluster</returns>
        public IList<Cluster> BuildClusters(IList<MaxUniqueMatch> matches)
        {
            // Validate the input
            if (null == matches)
            {
                return null;
            }

            if (0 == matches.Count)
            {
                return null;
            }

            IList<MaxUniqueMatchExtension> matchExtensions = new List<MaxUniqueMatchExtension>();
            _unionFind = new List<int>();

            // Convert list of matches to list of matchextensions
            foreach (MaxUniqueMatch match in matches)
            {
                MaxUniqueMatchExtension matchExtension = new MaxUniqueMatchExtension(match);

                _unionFind.Add(0);
                matchExtensions.Add(matchExtension);
            }

            // Get the cluster and return it
            return GetClusters(matchExtensions);
        }

        /// <summary>
        /// Removes the duplicate and overlapping maximal unique matches.
        /// </summary>
        /// <param name="matches">List of matches</param>
        private static void FilterMatches(IList<MaxUniqueMatchExtension> matches)
        {
            int counter1, counter2;

            for (counter1 = 0; counter1 < matches.Count; counter1++)
            {
                matches[counter1].IsGood = true;
            }

            for (counter1 = 0; counter1 < matches.Count - 1; counter1++)
            {
                int diagonalIndex, endIndex;

                if (!matches[counter1].IsGood)
                {
                    continue;
                }

                diagonalIndex = matches[counter1].SecondSequenceStart
                        - matches[counter1].FirstSequenceStart;
                endIndex = matches[counter1].SecondSequenceStart
                        + matches[counter1].Length;

                for (counter2 = counter1 + 1;
                        counter2 < matches.Count &&
                            matches[counter2].SecondSequenceStart <= endIndex;
                        counter2++)
                {
                    int overlap;
                    int diagonalj;

                    if (matches[counter1].SecondSequenceStart <= matches[counter2].SecondSequenceStart)
                    {
                        if (!matches[counter2].IsGood)
                        {
                            continue;
                        }

                        diagonalj = matches[counter2].SecondSequenceStart
                                - matches[counter2].FirstSequenceStart;
                        if (diagonalIndex == diagonalj)
                        {
                            int extentj;

                            extentj = matches[counter2].Length
                                    + matches[counter2].SecondSequenceStart
                                    - matches[counter1].SecondSequenceStart;
                            if (extentj > matches[counter1].Length)
                            {
                                matches[counter1].Length = extentj;
                                endIndex = matches[counter1].SecondSequenceStart
                                        + extentj;
                            }

                            // match lies on the same diagonal, this match cannot be part of
                            // any cluster
                            matches[counter2].IsGood = false;
                        }
                        else if (matches[counter1].FirstSequenceStart == matches[counter2].FirstSequenceStart)
                        {
                            // look for overlaps in second(query) sequence
                            overlap = matches[counter1].SecondSequenceStart
                                    + matches[counter1].Length
                                    - matches[counter2].SecondSequenceStart;

                            if (matches[counter1].Length < matches[counter2].Length)
                            {
                                if (overlap >= matches[counter1].Length / 2)
                                {
                                    // match is overlapping, this match cannot be part of 
                                    // any cluster
                                    matches[counter1].IsGood = false;
                                    break;
                                }
                            }
                            else if (matches[counter2].Length < matches[counter1].Length)
                            {
                                if (overlap >= matches[counter2].Length / 2)
                                {
                                    // match is overlapping, this match cannot be part of 
                                    // any cluster
                                    matches[counter2].IsGood = false;
                                }
                            }
                            else
                            {
                                if (overlap >= matches[counter1].Length / 2)
                                {
                                    matches[counter2].IsTentative = true;
                                    if (matches[counter1].IsTentative)
                                    {
                                        // match is overlapping, this match cannot be part of 
                                        // any cluster
                                        matches[counter1].IsGood = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (matches[counter1].SecondSequenceStart == matches[counter2].SecondSequenceStart)
                        {
                            // look for overlaps in first(reference) sequence
                            overlap = matches[counter1].FirstSequenceStart
                                    + matches[counter1].Length
                                    - matches[counter2].FirstSequenceStart;

                            if (matches[counter1].Length < matches[counter2].Length)
                            {
                                if (overlap >= matches[counter1].Length / 2)
                                {
                                    // match is overlapping, this match cannot be part of 
                                    // any cluster
                                    matches[counter1].IsGood = false;
                                    break;
                                }
                            }
                            else if (matches[counter2].Length < matches[counter1].Length)
                            {
                                if (overlap >= matches[counter2].Length / 2)
                                {
                                    // match is overlapping, this match cannot be part of 
                                    // any cluster
                                    matches[counter2].IsGood = false;
                                }
                            }
                            else
                            {
                                if (overlap >= matches[counter1].Length / 2)
                                {
                                    matches[counter2].IsTentative = true;
                                    if (matches[counter1].IsTentative)
                                    {
                                        // match is overlapping, this match cannot be part of 
                                        // any cluster
                                        matches[counter1].IsGood = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Move all the matches that needs to be removed at the end of list
            for (counter1 = counter2 = 0; counter1 < matches.Count; counter1++)
            {
                if (matches[counter1].IsGood)
                {
                    if (counter1 != counter2)
                    {
                        matches[counter1].CopyTo(matches[counter2]);
                    }

                    counter2++;
                }
            }

            // Remove the matches that are not part of any cluster
            for (counter1 = matches.Count - 1; counter1 >= counter2; counter1--)
            {
                matches.RemoveAt(counter1);
            }

            for (counter1 = 0; counter1 < matches.Count; counter1++)
            {
                matches[counter1].IsGood = false;
            }
        }

        /// <summary>
        /// Sort by Cluster by specified column
        /// </summary>
        /// <param name="matches">List of matches</param>
        /// <param name="sortBy">Column to be sorted by</param>
        /// <returns>Sorted list of cluster</returns>
        private static IList<MaxUniqueMatchExtension> Sort(
                IList<MaxUniqueMatchExtension> matches,
                string sortBy)
        {
            IEnumerable<MaxUniqueMatchExtension> sortedMatches = null;

            switch (sortBy)
            {
                case SecondSequenceStart:
                    sortedMatches = from cluster in matches
                                    orderby cluster.SecondSequenceStart
                                    select cluster;
                    break;

                case ClusterID:
                    sortedMatches = from cluster in matches
                                    orderby cluster.ID
                                    orderby cluster.SecondSequenceStart
                                    orderby cluster.FirstSequenceStart
                                    select cluster;
                    break;

                default:
                    break;
            }

            return sortedMatches.ToList();
        }

        /// <summary>
        /// Process the matches and create clusters
        /// </summary>
        /// <param name="matches">List of matches</param>
        /// <returns>List of clusters</returns>
        private IList<Cluster> GetClusters(IList<MaxUniqueMatchExtension> matches)
        {
            int clusterSize, separation, firstMatchIndex, secondMatchIndex;
            int counter1, counter2;
            IList<Cluster> clusters = new List<Cluster>();

            // Create the union group based on distance
            for (counter1 = 0; counter1 < matches.Count; counter1++)
            {
                _unionFind[counter1] = -1;
            }

            // Sort matches by second sequence start
            matches = Sort(matches, SecondSequenceStart);

            // Remove overlapping and duplicate matches in cluster
            FilterMatches(matches);

            // Fnd the diagonal distance
            // If diagonal distance is less than user defined, they are clustered together
            for (counter1 = 0; counter1 < matches.Count - 1; counter1++)
            {
                int endIndex = matches[counter1].SecondSequenceStart
                        + matches[counter1].Length;
                int diagonalIndex = matches[counter1].SecondSequenceStart
                        - matches[counter1].FirstSequenceStart;

                for (counter2 = counter1 + 1; counter2 < matches.Count; counter2++)
                {
                    int diagonalDifference;

                    separation = matches[counter2].SecondSequenceStart - endIndex;
                    if (separation > MaximumSeparation)
                    {
                        break;
                    }

                    diagonalDifference = Math.Abs(
                            (matches[counter2].SecondSequenceStart - matches[counter2].FirstSequenceStart)
                            - diagonalIndex);
                    if (diagonalDifference <= Math.Max(FixedSeparation, SeparationFactor * separation))
                    {
                        firstMatchIndex = Find(counter1);
                        secondMatchIndex = Find(counter2);
                        if (firstMatchIndex != secondMatchIndex)
                        {
                            // add both the matches to the cluster
                            Union(firstMatchIndex, secondMatchIndex);
                        }
                    }
                }
            }

            // Set the cluster id of each match
            for (counter1 = 0; counter1 < matches.Count; counter1++)
            {
                matches[counter1].ID = Find(counter1);
            }

            // Sort the matches by cluster id
            matches = Sort(matches, ClusterID);

            for (counter1 = 0; counter1 < matches.Count; counter1 += clusterSize)
            {
                counter2 = counter1 + 1;
                while (counter2 < matches.Count
                        && matches[counter1].ID == matches[counter2].ID)
                {
                    counter2++;
                }

                clusterSize = counter2 - counter1;
                ProcessCluster(
                        clusters,
                        matches.Where((Match, Index) => Index >= counter1).ToList(),
                        clusterSize);
            }

            return clusters;
        }

        /// <summary>
        /// Return the id of the set containing "a" in Union-Find.
        /// </summary>
        /// <param name="matchIndex">Index of the maximal unique match in UnionFind</param>
        /// <returns>Cluster id</returns>
        private int Find(int matchIndex)
        {
            int clusterId, counter1, counter2;

            if (_unionFind[matchIndex] < 0)
            {
                return matchIndex;
            }

            for (clusterId = matchIndex; _unionFind[clusterId] > 0;)
            {
                clusterId = _unionFind[clusterId];
            }

            for (counter1 = matchIndex; _unionFind[counter1] != clusterId; counter1 = counter2)
            {
                counter2 = _unionFind[counter1];
                _unionFind[counter1] = clusterId;
            }

            return clusterId;
        }

        /// <summary>
        /// Group the matches in Union
        /// </summary>
        /// <param name="firstMatchIndex">Id of first cluster</param>
        /// <param name="secondMatchIndex">Id of second cluster</param>
        private void Union(int firstMatchIndex, int secondMatchIndex)
        {
            if (_unionFind[firstMatchIndex] < 0 && _unionFind[secondMatchIndex] < 0)
            {
                if (_unionFind[firstMatchIndex] < _unionFind[secondMatchIndex])
                {
                    _unionFind[firstMatchIndex] += _unionFind[secondMatchIndex];
                    _unionFind[secondMatchIndex] = firstMatchIndex;
                }
                else
                {
                    _unionFind[secondMatchIndex] += _unionFind[firstMatchIndex];
                    _unionFind[firstMatchIndex] = secondMatchIndex;
                }
            }
        }

        /// <summary>
        /// Process the clusters
        /// </summary>
        /// <param name="clusters">List of clusters</param>
        /// <param name="matches">List of matches</param>
        /// <param name="clusterSize">Size of cluster</param>
        private void ProcessCluster(
                IList<Cluster> clusters,
                IList<MaxUniqueMatchExtension> matches,
                int clusterSize)
        {
            IList<MaxUniqueMatchExtension> clusterMatches = null;
            int total, endIndex, startIndex, score, best;
            int counter1, counter2, counter3;

            do
            {
                // remove cluster overlaps
                for (counter1 = 0; counter1 < clusterSize; counter1++)
                {
                    matches[counter1].Score = matches[counter1].Length;
                    matches[counter1].Adjacent = 0;
                    matches[counter1].From = -1;

                    for (counter2 = 0; counter2 < counter1; counter2++)
                    {
                        int cost, overlap, overlap1, overlap2;

                        overlap1 = matches[counter2].FirstSequenceStart
                                + matches[counter2].Length
                                - matches[counter1].FirstSequenceStart;
                        overlap = Math.Max(0, overlap1);
                        overlap2 = matches[counter2].SecondSequenceStart
                                + matches[counter2].Length -
                                matches[counter1].SecondSequenceStart;
                        overlap = Math.Max(overlap, overlap2);

                        // cost matches which are not on same diagonal
                        cost = overlap
                                + Math.Abs((matches[counter1].SecondSequenceStart - matches[counter1].FirstSequenceStart)
                                - (matches[counter2].SecondSequenceStart - matches[counter2].FirstSequenceStart));

                        if (matches[counter2].Score + matches[counter1].Length - cost > matches[counter1].Score)
                        {
                            matches[counter1].From = counter2;
                            matches[counter1].Score = matches[counter2].Score
                                    + matches[counter1].Length
                                    - cost;
                            matches[counter1].Adjacent = overlap;
                        }
                    }
                }

                // Find the match which has highest score
                best = 0;
                for (counter1 = 1; counter1 < clusterSize; counter1++)
                {
                    if (matches[counter1].Score > matches[best].Score)
                    {
                        best = counter1;
                    }
                }

                total = 0;
                endIndex = int.MinValue;
                startIndex = int.MaxValue;
                for (counter1 = best; counter1 >= 0; counter1 = matches[counter1].From)
                {
                    matches[counter1].IsGood = true;
                    total += matches[counter1].Length;
                    if (matches[counter1].SecondSequenceStart + matches[counter1].Length > endIndex)
                    {
                        // Set the cluster end index
                        endIndex = matches[counter1].FirstSequenceStart + matches[counter1].Length;
                    }

                    if (matches[counter1].FirstSequenceStart < startIndex)
                    {
                        // Set the cluster start index
                        startIndex = matches[counter1].FirstSequenceStart;
                    }
                }

                score = endIndex - startIndex;

                // If the current score exceeds the minimum score
                // and the matches to cluster
                if (score >= MinimumScore)
                {
                    clusterMatches = new List<MaxUniqueMatchExtension>();

                    for (counter1 = 0; counter1 < clusterSize; counter1++)
                    {
                        if (matches[counter1].IsGood)
                        {
                            clusterMatches.Add(matches[counter1]);
                        }
                    }

                    // adding the cluster to list
                    if (0 < clusterMatches.Count)
                    {
                        clusters.Add(new Cluster(clusterMatches));
                    }
                }

                // Correcting the cluster indices
                for (counter1 = counter3 = 0; counter1 < clusterSize; counter1++)
                {
                    if (!matches[counter1].IsGood)
                    {
                        if (counter1 != counter3)
                        {
                            matches[counter3] = matches[counter1];
                        }

                        counter3++;
                    }
                }

                clusterSize = counter3;
            }
            while (clusterSize > 0);
        }
    }
}
