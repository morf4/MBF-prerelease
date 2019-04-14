﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using MBF.Matrix;
using MBF.TestAutomation.Util;
using MBF.Util;
using MBF.Util.Logging;
using NUnit.Framework;

namespace MBF.TestAutomation.Matrix
{
    /// <summary>
    /// Bvt test cases to confirm the features of Dense Matrix
    /// </summary>
    [TestFixture]
    public class VectorViewBvtTestCases
    {

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static VectorViewBvtTestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.automation.log");
            }

            Utility._xmlUtil = new XmlUtility(@"TestUtils\MatrixTestsConfig.xml");
        }

        #endregion

        #region Test Cases

        #region RowView Test cases

        /// <summary>
        /// Creates a Dense Matrix with RowView.Add()
        /// Input : Valid values for VectorView
        /// Validation : Add method
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewAdd()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);

            rowView.Add("C0", 1);

            Assert.AreEqual("1", rowView["C0"].ToString());

            Console.WriteLine(
                "VectorView BVT : Validation of Add() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of Add() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with RowView.Add(Key value pair)
        /// Input : Valid values for VectorView
        /// Validation : Add method with Key value pair
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewAddKVP()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView =
                denseMatObj.RowView(0);
            KeyValuePair<string, double> keyValPairObj =
                new KeyValuePair<string, double>("C0", 1);
            rowView.Add(keyValPairObj);

            Assert.AreEqual("1", rowView["C0"].ToString());

            Console.WriteLine(
                "VectorView BVT : Validation of Add(Key value pair) method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of Add(Key value pair) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with RowView.Remove()
        /// Input : Valid values for VectorView
        /// Validation : Remove method
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewRemove()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);

            rowView.Remove("C0");

            try
            {
                double val = rowView["C0"];
                Assert.IsNotNull(val);
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(
                    "VectorView BVT : Validation of Remove() method successful");
                ApplicationLog.WriteLine(
                    "VectorView BVT : Validation of Remove() method successful");
            }
        }

        /// <summary>
        /// Creates a Dense Matrix with RowView.Remove(Key value pair)
        /// Input : Valid values for VectorView
        /// Validation : Remove method with Key value pair
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewRemoveKVP()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView =
                denseMatObj.RowView(0);
            KeyValuePair<string, double> keyValPairObj =
                new KeyValuePair<string, double>("C0", 1);
            rowView.Remove(keyValPairObj);

            try
            {
                double val = rowView["C0"];
                Assert.IsNotNull(val);
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(
                    "VectorView BVT : Validation of Remove(Key value pair) method successful");
                ApplicationLog.WriteLine(
                    "VectorView BVT : Validation of Remove(Key value pair) method successful");
            }
        }

        /// <summary>
        /// Creates a Dense Matrix with RowView.Clear()
        /// Input : Valid values for VectorView
        /// Validation : Clear method
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewClear()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);

            rowView.Clear();

            Assert.AreEqual(0, rowView.Count);

            Console.WriteLine(
                "VectorView BVT : Validation of Clear() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of Clear() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with RowView.Contains()
        /// Input : Valid values for VectorView
        /// Validation : Contains method
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewContains()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);
            KeyValuePair<string, double> keyValPairObj =
                new KeyValuePair<string, double>("C0", denseMatObj[0, 0]);

            Assert.IsTrue(rowView.Contains(keyValPairObj));

            Console.WriteLine(
                "VectorView BVT : Validation of Contains() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of Contains() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with RowView.ContainsKey()
        /// Input : Valid values for VectorView
        /// Validation : ContainsKey method
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewContainsKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);

            Assert.IsTrue(rowView.ContainsKey("C0"));

            Console.WriteLine(
                "VectorView BVT : Validation of ContainsKey() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of ContainsKey() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with RowView.CopyTo()
        /// Input : Valid values for VectorView
        /// Validation : CopyTo method
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewCopyTo()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);
            KeyValuePair<string, double>[] keyValPairObj =
                new KeyValuePair<string, double>[4];

            rowView.CopyTo(keyValPairObj, 0);

            Assert.AreEqual(keyValPairObj[0].Value, rowView["C0"]);

            Console.WriteLine(
                "VectorView BVT : Validation of CopyTo() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of CopyTo() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with RowView.TryGetValue()
        /// Input : Valid values for VectorView
        /// Validation : TryGetValue method
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewTryGetValue()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);

            double val = 0;
            Assert.IsTrue(rowView.TryGetValue("C0", out val));

            Assert.AreEqual(val, denseMatObj[0, 0]);

            Console.WriteLine(
                "VectorView BVT : Validation of TryGetValue() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of TryGetValue() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with all properties validation
        /// Input : Valid values for VectorView
        /// Validation : all properties
        /// </summary>
        [Test]
        public void ValidateVectorViewRowViewAllProperties()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> rowView = denseMatObj.RowView(0);

            Assert.AreEqual(denseMatObj.ColCount, rowView.Count);
            Assert.IsFalse(rowView.IsReadOnly);
            Assert.AreEqual(denseMatObj.ColCount, rowView.Keys.Count);
            Assert.AreEqual(denseMatObj.ColCount, rowView.Values.Count);

            Console.WriteLine(
                "VectorView BVT : Validation of all properties successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of all properties successful");
        }

        #endregion

        #region ColView Test cases

        /// <summary>
        /// Creates a Dense Matrix with ColView.Add()
        /// Input : Valid values for VectorView
        /// Validation : Add method
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewAdd()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);

            colView.Add("R0", 1);

            Assert.AreEqual("1", colView["R0"].ToString());

            Console.WriteLine(
                "VectorView BVT : Validation of Add() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of Add() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with ColView.Add(Key value pair)
        /// Input : Valid values for VectorView
        /// Validation : Add method with Key value pair
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewAddKVP()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView =
                denseMatObj.ColView(0);
            KeyValuePair<string, double> keyValPairObj =
                new KeyValuePair<string, double>("R0", 1);
            colView.Add(keyValPairObj);

            Assert.AreEqual("1", colView["R0"].ToString());

            Console.WriteLine(
                "VectorView BVT : Validation of Add(Key value pair) method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of Add(Key value pair) method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with ColView.Remove()
        /// Input : Valid values for VectorView
        /// Validation : Remove method
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewRemove()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);

            colView.Remove("R0");

            try
            {
                double val = colView["R0"];
                Assert.IsNotNull(val);
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(
                    "VectorView BVT : Validation of Remove() method successful");
                ApplicationLog.WriteLine(
                    "VectorView BVT : Validation of Remove() method successful");
            }
        }

        /// <summary>
        /// Creates a Dense Matrix with ColView.Remove(Key value pair)
        /// Input : Valid values for VectorView
        /// Validation : Remove method with Key value pair
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewRemoveKVP()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView =
                denseMatObj.ColView(0);
            KeyValuePair<string, double> keyValPairObj =
                new KeyValuePair<string, double>("R0", 1);
            colView.Remove(keyValPairObj);

            try
            {
                double val = colView["R0"];
                Assert.IsNotNull(val);
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(
                    "VectorView BVT : Validation of Remove(Key value pair) method successful");
                ApplicationLog.WriteLine(
                    "VectorView BVT : Validation of Remove(Key value pair) method successful");
            }
        }

        /// <summary>
        /// Creates a Dense Matrix with ColView.Clear()
        /// Input : Valid values for VectorView
        /// Validation : Clear method
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewClear()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);

            colView.Clear();

            Assert.AreEqual(0, colView.Count);

            Console.WriteLine(
                "VectorView BVT : Validation of Clear() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of Clear() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with ColView.Contains()
        /// Input : Valid values for VectorView
        /// Validation : Contains method
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewContains()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);
            KeyValuePair<string, double> keyValPairObj =
                new KeyValuePair<string, double>("R0", denseMatObj[0, 0]);

            Assert.IsTrue(colView.Contains(keyValPairObj));

            Console.WriteLine(
                "VectorView BVT : Validation of Contains() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of Contains() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with ColView.ContainsKey()
        /// Input : Valid values for VectorView
        /// Validation : ContainsKey method
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewContainsKey()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);

            Assert.IsTrue(colView.ContainsKey("R0"));

            Console.WriteLine(
                "VectorView BVT : Validation of ContainsKey() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of ContainsKey() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with ColView.CopyTo()
        /// Input : Valid values for VectorView
        /// Validation : CopyTo method
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewCopyTo()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);
            KeyValuePair<string, double>[] keyValPairObj =
                new KeyValuePair<string, double>[4];

            colView.CopyTo(keyValPairObj, 0);

            Assert.AreEqual(keyValPairObj[0].Value, colView["R0"]);

            Console.WriteLine(
                "VectorView BVT : Validation of CopyTo() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of CopyTo() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with ColView.TryGetValue()
        /// Input : Valid values for VectorView
        /// Validation : TryGetValue method
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewTryGetValue()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);

            double val = 0;
            Assert.IsTrue(colView.TryGetValue("R0", out val));

            Assert.AreEqual(val, denseMatObj[0, 0]);

            Console.WriteLine(
                "VectorView BVT : Validation of TryGetValue() method successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of TryGetValue() method successful");
        }

        /// <summary>
        /// Creates a Dense Matrix with all properties validation
        /// Input : Valid values for VectorView
        /// Validation : all properties
        /// </summary>
        [Test]
        public void ValidateVectorViewColViewAllProperties()
        {
            DenseMatrix<string, string, double> denseMatObj =
                GetDenseMatrix();

            IDictionary<string, double> colView = denseMatObj.ColView(0);

            Assert.AreEqual(denseMatObj.RowCount, colView.Count);
            Assert.IsFalse(colView.IsReadOnly);
            Assert.AreEqual(denseMatObj.RowCount, colView.Keys.Count);
            Assert.AreEqual(denseMatObj.RowCount, colView.Values.Count);

            Console.WriteLine(
                "VectorView BVT : Validation of all properties successful");
            ApplicationLog.WriteLine(
                "VectorView BVT : Validation of all properties successful");
        }

        #endregion

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the two D array from the xml
        /// </summary>
        /// <param name="nodeName">Node Name of the xml to be parsed</param>
        /// <param name="maxRows">Maximum rows</param>
        /// <param name="maxColumns">Maximum columns</param>
        /// <returns>2 D Array</returns>
        static double[,] GetTwoDArray(string nodeName, out int maxRows,
            out int maxColumns)
        {
            string[] rowArray = Utility._xmlUtil.GetTextValues(nodeName, Constants.RowsNode);

            // Gets the max number columns in the array
            maxColumns = 0;
            maxRows = rowArray.Length;
            for (int i = 0; i < maxRows; i++)
            {
                string[] colArray = rowArray[i].Split(',');
                if (maxColumns < colArray.Length)
                    maxColumns = colArray.Length;
            }

            // Creates a 2 D with max row and column length
            double[,] twoDArray = new double[maxRows, maxColumns];
            for (int i = 0; i < maxRows; i++)
            {
                string[] colArray = rowArray[i].Split(',');
                for (int j = 0; j < colArray.Length; j++)
                {
                    twoDArray[i, j] = double.Parse(colArray[j]);
                }
            }

            return twoDArray;
        }

        /// <summary>
        /// Gets the key sequence with the max length specified
        /// </summary>
        /// <param name="maxKey">Max length of the key sequence</param>
        /// <param name="isRow">If Row, append R else append C</param>
        /// <returns>Key Sequence Array</returns>
        static string[] GetKeySequence(int maxKey, bool isRow)
        {
            string[] keySeq = new string[maxKey];
            string tempSeq = string.Empty;

            if (isRow)
                tempSeq = "R";
            else
                tempSeq = "C";

            for (int i = 0; i < maxKey; i++)
            {
                keySeq[i] = tempSeq + i.ToString();
            }

            return keySeq;
        }

        /// <summary>
        /// Creates a DenseMatrix instance and returns the same.
        /// </summary>
        /// <returns>DenseMatrix Instance</returns>
        static DenseMatrix<string, string, double> GetDenseMatrix()
        {
            int maxRows = 0;
            int maxColumns = 0;
            double[,] twoDArray = GetTwoDArray(Constants.SimpleMatrixNodeName,
                out maxRows, out maxColumns);

            string[] rowKeySeq = GetKeySequence(maxRows, true);
            string[] colKeySeq = GetKeySequence(maxColumns, false);

            DenseMatrix<string, string, double> denseMatrixObj =
                new DenseMatrix<string, string, double>(twoDArray, rowKeySeq,
                    colKeySeq, double.NaN);

            return denseMatrixObj;
        }

        #endregion;
    }
}
