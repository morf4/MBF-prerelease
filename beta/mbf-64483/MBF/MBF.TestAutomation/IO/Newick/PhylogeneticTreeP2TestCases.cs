﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

/****************************************************************************
 * PhylogeneticTreeP2TestCases.cs
 * 
 *   This file contains the PhylogeneticTree - Parsers and Formatters P2 test cases.
 * 
***************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MBF.IO.Newick;
using MBF.Phylogenetics;
using MBF.TestAutomation.Util;
using MBF.Util.Logging;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MBF.TestAutomation.IO.Newick
{
    /// <summary>
    /// Phylogenetic Tree P2 parser and formatter Test case implementation.
    /// </summary>
    [TestClass]
    public class PhylogeneticTreeP2TestCases
    {

        #region Enums

        /// <summary>
        /// Additional Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum AdditionalParameters
        {
            TextReader,
            StringBuilder
        };

        #endregion Enums

        #region Global Variables

        Utility _utilityObj = new Utility(@"TestUtils\PhylogeneticTestsConfig.xml");

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static PhylogeneticTreeP2TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.automation.log");
            }
        }

        #endregion Constructor

        #region PhylogeneticTree Parser P2 Test cases

        /// <summary>
        /// Parse a invalid Phylogenetic File and invalidate the Parse() method.
        /// Input : Phylogenetic Tree
        /// Output : Expected Exception
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void PhylogeneticTreeP2ParserInvalidateParse()
        {
            PhylogeneticTreeParserGeneralTests(
                Constants.InvalidateNewickParseNode,
                AdditionalParameters.TextReader);
        }

        /// <summary>
        /// Parse a invalid Phylogenetic File and invalidate 
        /// the Peek() method.
        /// Input : Phylogenetic Tree
        /// Output : Expected Exception
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void PhylogeneticTreeP2ParserInvalidateParsePeek()
        {
            PhylogeneticTreeParserGeneralTests(
                Constants.InvalidateNewickParserPeekNode,
                AdditionalParameters.TextReader);
        }

        /// <summary>
        /// Parse a invalid StringBuilder and invalidate
        /// the Parse(treeBuilder) method.
        /// Input : null as treeBuilder
        /// Output : Expected Exception
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void PhylogeneticTreeP2ParserInvalidateParseBuilder()
        {
            try
            {
                using (NewickParser nwParserObj = new NewickParser())
                {
                    nwParserObj.Parse(null as StringBuilder);
                }
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                ApplicationLog.WriteLine(
                   "GFF Parser P2 : All the features validated successfully.");
                Console.WriteLine(
                    "GFF Parser P2 : All the features validated successfully.");
            }
        }

        /// <summary>
        /// Parse a invalid Phylogenetic File and invalidate
        /// the GetLeaf() method.
        /// Input : Phylogenetic Tree
        /// Output : Expected Exception
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void PhylogeneticTreeP2ParserInvalidateLeaf()
        {
            PhylogeneticTreeParserGeneralTests(
                Constants.InvalidateNewickLeafNode,
                AdditionalParameters.TextReader);
        }

        /// <summary>
        /// Parse a invalid Phylogenetic File and invalidate
        /// the GetBrach() method.
        /// Input : Phylogenetic Tree
        /// Output : Expected Exception
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void PhylogeneticTreeP2ParserInvalidateBranch()
        {
            PhylogeneticTreeParserGeneralTests(
                Constants.InvalidateNewickBranchNode,
                AdditionalParameters.TextReader);
        }

        #endregion PhylogeneticTree Parser P2 Test cases

        #region Helper Method

        /// <summary>
        /// General method to invalidate Newick Parser.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="method">Newick Parse method parameters</param>
        /// </summary>        
        void PhylogeneticTreeParserGeneralTests(
            string nodeName,
            AdditionalParameters method)
        {
            try
            {
                string filePath = _utilityObj._xmlUtil.GetTextValue(
                    nodeName,
                    Constants.FilePathNode);

                switch (method)
                {
                    case AdditionalParameters.TextReader:
                        using (StreamReader reader = File.OpenText(filePath))
                        {
                            using (NewickParser nwParserObj = new NewickParser())
                            {
                                nwParserObj.Parse(reader);
                            }
                        }
                        break;
                    case AdditionalParameters.StringBuilder:
                        using (StreamReader reader = File.OpenText(filePath))
                        {
                            StringBuilder strBuilderObj =
                                new StringBuilder(reader.ReadToEnd());
                            using (NewickParser nwParserObj = new NewickParser())
                            {
                                nwParserObj.Parse(strBuilderObj);
                            }
                        }
                        break;
                    default:
                        break;
                }

                Assert.Fail();
            }
            catch (FormatException)
            {
                ApplicationLog.WriteLine(
                    "GFF Parser P2 : All the features validated successfully.");
                Console.WriteLine(
                    "GFF Parser P2 : All the features validated successfully.");
            }
        }

        #endregion Helper Method
    }
}
