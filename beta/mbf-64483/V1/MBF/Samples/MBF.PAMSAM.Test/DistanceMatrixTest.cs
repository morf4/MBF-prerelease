﻿// -------------------------------------------------------------------------------------
// <copyright file="DistanceMatrixTest.cs" company="Microsoft">
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Contains test cases for Distance Matrix class.
// </summary>
// -------------------------------------------------------------------------------------

namespace MBF.Test
{
    using System;
    using System.Collections.Generic;
    using MBF.Algorithms;
    using MBF.Algorithms.Alignment;
    using MBF.Algorithms.Alignment.MultipleSequenceAlignment;
    using MBF.Util.Logging;
    using NUnit.Framework;

    /// <summary>
    /// Test for DistanceMatrix Class
    /// </summary>
    [TestFixture]
    public class DistanceMatrixTest
    {
        /// <summary>
        /// Test DistanceMatrix Class
        /// </summary>
        [Test]
        public void TestDistanceMatrix()
        {
            int dimension = 4;
            IDistanceMatrix distanceMatrix = new SymmetricDistanceMatrix(dimension);
            for (int i = 0; i < distanceMatrix.Dimension - 1; ++i)
            {
                for (int j = i + 1; j < distanceMatrix.Dimension; ++j)
                {
                    distanceMatrix[i, j] = i + j;
                    distanceMatrix[j, i] = i + j;
                }
            }

            Assert.AreEqual(dimension, distanceMatrix.Dimension);
            Assert.AreEqual(dimension, distanceMatrix.NearestNeighbors.Length);
            Assert.AreEqual(dimension, distanceMatrix.NearestDistances.Length);

            // Test elements
            for (int i = 0; i < distanceMatrix.Dimension - 1; ++i)
            {
                for (int j = i + 1; j < distanceMatrix.Dimension; ++j)
                {
                    Assert.AreEqual(i + j, distanceMatrix[i, j]);
                    Assert.AreEqual(i + j, distanceMatrix[j, i]);
                }
            }

            // Test NearestNeighbors
            Assert.AreEqual(1, distanceMatrix.NearestNeighbors[0]);
            Assert.AreEqual(0, distanceMatrix.NearestNeighbors[1]);
            Assert.AreEqual(0, distanceMatrix.NearestNeighbors[2]);
            Assert.AreEqual(0, distanceMatrix.NearestNeighbors[3]);

            Assert.AreEqual(1, distanceMatrix.NearestDistances[0]);
            Assert.AreEqual(1, distanceMatrix.NearestDistances[1]);
            Assert.AreEqual(2, distanceMatrix.NearestDistances[2]);
            Assert.AreEqual(3, distanceMatrix.NearestDistances[3]);
        }
    }
}