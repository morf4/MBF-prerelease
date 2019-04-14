﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

/****************************************************************************
 * ClustalWBvtTestCases.cs
 * 
 *   This file contains the ClustalW - Parsers and Formatters Bvt test cases.
 * 
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

using MBF.Algorithms.Alignment;
using MBF.IO.ClustalW;
using MBF.TestAutomation.Util;
using MBF.Util.Logging;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MBF.TestAutomation.IO.ClustalW
{
    /// <summary>
    /// ClustalW Bvt parser and formatter Test case implementation.
    /// </summary>
    [TestClass]
    public class ClustalWBvtTestCases
    {

        #region Enums

        /// <summary>
        /// Additional parameters to validate different scenarios.
        /// </summary>
        enum AdditionalParameters
        {
            Parse,
            ParseOne,
            ParseTextReader,
            ParseOneTextReader,
            ParseReadOnly,
            ParseTextReaderReadOnly,
            ParseOneReadOnly,
            ParseOneTextReaderReadOnly
        };

        #endregion Enums

        #region Global Variables

        Utility _utilityObj = new Utility(@"TestUtils\ClustalWTestsConfig.xml");

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static ClustalWBvtTestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.automation.log");
            }
        }

        #endregion Constructor

        #region ClustalW Parser BVT Test cases

        /// <summary>
        /// Parse a valid ClustalW file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(file-name) method and 
        /// validate with the expected sequence.
        /// Input : ClustalW File
        /// Validation: Sequence Alignment list
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ClustalWParserValidateParseFileName()
        {
            ParserGeneralTestCases(Constants.SmallSizeClustalWNodeName,
                AdditionalParameters.Parse);
        }

        /// <summary>
        /// Parse a valid ClustalW file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using ParseOne(file-name) method and 
        /// validate with the expected sequence.
        /// Input : ClustalW File
        /// Validation: Sequence Alignment list
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ClustalWParserValidateParseOneFileName()
        {
            ParserGeneralTestCases(Constants.SmallSizeClustalWNodeName,
                AdditionalParameters.ParseOne);
        }

        /// <summary>
        /// Parse a valid ClustalW file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using ParseOne(file-name, read-only) method and 
        /// validate with the expected sequence.
        /// Input : ClustalW File
        /// Validation: Sequence Alignment list
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ClustalWParserValidateParseOneFileNameReadOnly()
        {
            ParserGeneralTestCases(Constants.SmallSizeClustalWNodeName,
                AdditionalParameters.ParseOneReadOnly);
        }

        /// <summary>
        /// Parse a valid ClustalW file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(file-name, read-only) method and 
        /// validate with the expected sequence.
        /// Input : ClustalW File
        /// Validation: Sequence Alignment list
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ClustalWParserValidateParseFileNameReadOnly()
        {
            ParserGeneralTestCases(Constants.SmallSizeClustalWNodeName,
                AdditionalParameters.ParseReadOnly);
        }

        /// <summary>
        /// Parse a valid ClustalW file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using ParseOne(text-reader) method and 
        /// validate with the expected sequence.
        /// Input : ClustalW File
        /// Validation: Sequence Alignment list
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ClustalWParserValidateParseOneTextReader()
        {
            ParserGeneralTestCases(Constants.SmallSizeClustalWNodeName,
                AdditionalParameters.ParseOneTextReader);
        }

        /// <summary>
        /// Parse a valid ClustalW file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using ParseOne(text-reader, read-only) method and 
        /// validate with the expected sequence.
        /// Input : ClustalW File
        /// Validation: Sequence Alignment list
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ClustalWParserValidateParseOneTextReaderReadOnly()
        {
            ParserGeneralTestCases(Constants.SmallSizeClustalWNodeName,
                AdditionalParameters.ParseOneTextReaderReadOnly);
        }

        /// <summary>
        /// Parse a valid ClustalW file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(text-reader) method and 
        /// validate with the expected sequence.
        /// Input : ClustalW File
        /// Validation: Sequence Alignment list
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ClustalWParserValidateParseTextReader()
        {
            ParserGeneralTestCases(Constants.SmallSizeClustalWNodeName,
                AdditionalParameters.ParseTextReader);
        }

        /// <summary>
        /// Parse a valid ClustalW file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(text-reader, read-only) method and 
        /// validate with the expected sequence.
        /// Input : ClustalW File
        /// Validation: Sequence Alignment list
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ClustalWParserValidateParseTextReaderReadOnly()
        {
            ParserGeneralTestCases(Constants.SmallSizeClustalWNodeName,
                AdditionalParameters.ParseTextReaderReadOnly);
        }

        #endregion ClustalW Parser BVT Test cases

        #region Supported Methods

        /// <summary>
        /// Parsers the ClustalW file for different test cases based
        /// on Additional parameter
        /// </summary>
        /// <param name="nodeName">Xml Node name</param>
        /// <param name="addParam">Additional parameter</param>
        void ParserGeneralTestCases(string nodeName,
            AdditionalParameters addParam)
        {
            // Gets the Filename
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);

            Assert.IsFalse(string.IsNullOrEmpty(filePath));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "ClustalW Parser BVT: Reading the File from location '{0}'", filePath));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "ClustalW Parser BVT: Reading the File from location '{0}'", filePath));

            // Get the rangelist after parsing.
            ClustalWParser parserObj = new ClustalWParser();

            IList<ISequenceAlignment> sequenceAlignmentList = null;
            ISequenceAlignment sequenceAlignment = null;

            // Gets the SequenceAlignment list based on the parameters.
            switch (addParam)
            {
                case AdditionalParameters.Parse:
                    sequenceAlignmentList = parserObj.Parse(filePath);
                    break;
                case AdditionalParameters.ParseOne:
                    sequenceAlignment = parserObj.ParseOne(filePath);
                    break;
                case AdditionalParameters.ParseTextReader:
                    using (StreamReader strRdrObj = new StreamReader(filePath))
                    {
                        sequenceAlignmentList = parserObj.Parse(strRdrObj);
                    }
                    break;
                case AdditionalParameters.ParseOneTextReader:
                    using (StreamReader strRdrObj = new StreamReader(filePath))
                    {
                        sequenceAlignment = parserObj.ParseOne(strRdrObj);
                    }
                    break;
                case AdditionalParameters.ParseOneTextReaderReadOnly:
                    using (StreamReader strRdrObj = new StreamReader(filePath))
                    {
                        sequenceAlignment = parserObj.ParseOne(strRdrObj, false);
                    }
                    break;
                case AdditionalParameters.ParseTextReaderReadOnly:
                    using (StreamReader strRdrObj = new StreamReader(filePath))
                    {
                        sequenceAlignmentList = parserObj.Parse(strRdrObj, false);
                    }
                    break;
                case AdditionalParameters.ParseReadOnly:
                    sequenceAlignmentList = parserObj.Parse(filePath,
                        false);
                    break;
                case AdditionalParameters.ParseOneReadOnly:
                    sequenceAlignment = parserObj.ParseOne(filePath,
                        false);
                    break;
                default:
                    break;
            }

            // Gets all the expected values from xml.
            IList<Dictionary<string, string>> expectedAlignmentList =
                new List<Dictionary<string, string>>();
            Dictionary<string, string> expectedAlignmentObj =
                new Dictionary<string, string>();

            XmlNode expectedAlignmentNodes = _utilityObj._xmlUtil.GetNode(
                nodeName, Constants.ExpectedAlignmentNode);
            XmlNodeList alignNodes = expectedAlignmentNodes.ChildNodes;

            // Create a ISequenceAlignment List
            switch (addParam)
            {
                case AdditionalParameters.ParseOne:
                case AdditionalParameters.ParseOneTextReader:
                case AdditionalParameters.ParseOneTextReaderReadOnly:
                case AdditionalParameters.ParseOneReadOnly:
                    sequenceAlignmentList = new List<ISequenceAlignment>();
                    sequenceAlignmentList.Add(sequenceAlignment);
                    break;
                default:
                    break;
            }

            foreach (XmlNode expectedAlignment in alignNodes)
            {
                expectedAlignmentObj[expectedAlignment.Name] =
                    expectedAlignment.InnerText;
            }

            expectedAlignmentList.Add(expectedAlignmentObj);

            Assert.IsTrue(CompareOutput(sequenceAlignmentList, expectedAlignmentList));
            ApplicationLog.WriteLine(
                "ClustalW Parser BVT: Successfully validated all the Alignment Sequences");
            Console.WriteLine(
                "ClustalW Parser BVT: Successfully validated all the Alignment Sequences");
        }

        /// <summary>
        /// Compare the actual output with expected output
        /// </summary>
        /// <param name="actualOutput">Actual output</param>
        /// <param name="expectedOutput">Expected output</param>
        /// <returns>True, if comparison is successful</returns>
        private static bool CompareOutput(
             IList<ISequenceAlignment> actualOutput,
             IList<Dictionary<string, string>> expectedOutput)
        {
            if (expectedOutput.Count != actualOutput.Count)
            {
                return false;
            }

            int alignmentIndex = 0;

            // Validate each output alignment
            foreach (ISequenceAlignment alignment in actualOutput)
            {
                Dictionary<string, string> expectedAlignment =
                    expectedOutput[alignmentIndex];

                foreach (Sequence actualSequence in alignment.AlignedSequences[0].Sequences)
                {
                    if (0 != string.Compare(actualSequence.ToString(),
                            expectedAlignment[actualSequence.ID], true,
                            CultureInfo.CurrentCulture))
                    {
                        return false;
                    }
                }

                alignmentIndex++;
            }

            return true;
        }

        #endregion Supported Methods
    }
}
