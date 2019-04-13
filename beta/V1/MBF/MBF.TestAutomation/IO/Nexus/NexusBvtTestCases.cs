﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

/****************************************************************************
 * NexusBvtTestCases.cs
 * 
 *   This file contains the Nexus - Parsers and Formatters Bvt test cases.
 * 
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

using MBF.Algorithms.Alignment;
using MBF.Encoding;
using MBF.IO.Nexus;
using MBF.TestAutomation.Util;
using MBF.Util.Logging;

using NUnit.Framework;

namespace MBF.TestAutomation.IO.Nexus
{
    /// <summary>
    /// Nexus Bvt parser and formatter Test case implementation.
    /// </summary>
    [TestFixture]
    public class NexusBvtTestCases
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
            ParseOneTextReaderReadOnly,
            ParseEncoding
        };

        #endregion Enums

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static NexusBvtTestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.automation.log");
            }

            Utility._xmlUtil = new XmlUtility(@"TestUtils\NexusTestsConfig.xml");
        }

        #endregion Constructor

        #region Nexus Parser BVT Test cases

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(file-name) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseFileName()
        {
            ParserGeneralTestCases(Constants.SmallSizeNexusNodeName,
                AdditionalParameters.Parse);
        }

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using ParseOne(file-name) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseOneFileName()
        {
            ParserGeneralTestCases(Constants.SmallSizeNexusNodeName,
                AdditionalParameters.ParseOne);
        }

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using ParseOne(file-name, read-only) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseOneFileNameReadOnly()
        {
            ParserGeneralTestCases(Constants.SmallSizeNexusNodeName,
                AdditionalParameters.ParseOneReadOnly);
        }

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(file-name, read-only) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseFileNameReadOnly()
        {
            ParserGeneralTestCases(Constants.SmallSizeNexusNodeName,
                AdditionalParameters.ParseReadOnly);
        }

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using ParseOne(text-reader) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseOneTextReader()
        {
            ParserGeneralTestCases(Constants.SmallSizeNexusNodeName,
                AdditionalParameters.ParseOneTextReader);
        }

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using ParseOne(text-reader, read-only) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseOneTextReaderReadOnly()
        {
            ParserGeneralTestCases(Constants.SmallSizeNexusNodeName,
                AdditionalParameters.ParseOneTextReaderReadOnly);
        }

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(text-reader) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseTextReader()
        {
            ParserGeneralTestCases(Constants.SmallSizeNexusNodeName,
                AdditionalParameters.ParseTextReader);
        }

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(text-reader, read-only) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseTextReaderReadOnly()
        {
            ParserGeneralTestCases(Constants.SmallSizeNexusNodeName,
                AdditionalParameters.ParseTextReaderReadOnly);
        }

        /// <summary>
        /// Parse a valida Nexus Parser object and validate its properties
        /// Input : Valide Object
        /// Output : Validatation of properties
        /// </summary>
        [Test]
        public void ValidateNexusParserProperties()
        {
            NexusParser parser = new NexusParser(Encodings.Ncbi4NA);
            Assert.AreEqual(
                Utility._xmlUtil.GetTextValue(Constants.NexusPropertyNode,
                Constants.NexusDescriptionNode),
                parser.Description);
            Assert.AreEqual(
                Utility._xmlUtil.GetTextValue(Constants.NexusPropertyNode,
                Constants.NexusNameNode),
                parser.Name);
            Assert.AreEqual(
                Utility._xmlUtil.GetTextValue(Constants.NexusPropertyNode,
                Constants.NexusFileTypesNode),
                parser.FileTypes);
            Assert.AreEqual(null, parser.Alphabet);
            Assert.AreEqual(
                Utility._xmlUtil.GetTextValue(Constants.NexusPropertyNode,
                Constants.NexusEncodingNode),
                parser.Encoding.ToString());
        }

        /// <summary>
        /// Parse a valid Nexus file (Small size sequence less than 35 kb) and 
        /// convert the same to one sequence using Parse(text-reader, read-only) method and 
        /// validate with the expected sequence.
        /// Input : Nexus File
        /// Validation: Sequence Alignment list
        /// </summary>
        [Test]
        public void NexusParserValidateParseEncoding()
        {
            ParserGeneralTestCases(Constants.SimpleNexusFileNode,
                AdditionalParameters.ParseEncoding);
        }

        /// <summary>
        /// Parse a valid Nexus file and validate char block
        /// Input : Valid File
        /// Output: Expected sequence alignment count
        /// </summary>
        [Test]
        public void NexusParserValidateCharBlock()
        {
            string filePath = Utility._xmlUtil.GetTextValue(
                    Constants.SimpleNexusCharBlockNode,
                    Constants.FilePathNode);
            NexusParser parser = new NexusParser();

            IList<ISequenceAlignment> alignment =
                parser.Parse(filePath, true);

            Assert.AreEqual(1, alignment.Count);
        }

        #endregion Nexus Parser BVT Test cases

        #region Supported Methods

        /// <summary>
        /// Parsers the Nexus file for different test cases based
        /// on Additional parameter
        /// </summary>
        /// <param name="nodeName">Xml Node name</param>
        /// <param name="addParam">Additional parameter</param>
        static void ParserGeneralTestCases(string nodeName,
            AdditionalParameters addParam)
        {
            // Gets the Filename
            string filePath = Utility._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);

            Assert.IsNotEmpty(filePath);
            ApplicationLog.WriteLine(string.Format(
                "Nexus Parser BVT: Reading the File from location '{0}'", filePath));
            Console.WriteLine(string.Format(
                "Nexus Parser BVT: Reading the File from location '{0}'", filePath));

            // Get the rangelist after parsing.
            NexusParser parserObj = new NexusParser();

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
                    sequenceAlignmentList = parserObj.Parse(
                        new StreamReader(filePath));
                    break;
                case AdditionalParameters.ParseOneTextReader:
                    sequenceAlignment = parserObj.ParseOne(
                        new StreamReader(filePath));
                    break;
                case AdditionalParameters.ParseOneTextReaderReadOnly:
                    sequenceAlignment = parserObj.ParseOne(
                        new StreamReader(filePath), false);
                    break;
                case AdditionalParameters.ParseTextReaderReadOnly:
                    sequenceAlignmentList = parserObj.Parse(
                        new StreamReader(filePath), false);
                    break;
                case AdditionalParameters.ParseReadOnly:
                    sequenceAlignmentList = parserObj.Parse(filePath,
                        false);
                    break;
                case AdditionalParameters.ParseOneReadOnly:
                    sequenceAlignment = parserObj.ParseOne(filePath,
                        false);
                    break;
                case AdditionalParameters.ParseEncoding:
                    NexusParser parser = new NexusParser(Encodings.Ncbi4NA);
                    sequenceAlignmentList = parser.Parse(
                        new StreamReader(filePath), false);
                    break;
                default:
                    break;
            }

            // Gets all the expected values from xml.
            IList<Dictionary<string, string>> expectedAlignmentList =
                new List<Dictionary<string, string>>();
            Dictionary<string, string> expectedAlignmentObj =
                new Dictionary<string, string>();

            XmlNode expectedAlignmentNodes = Utility._xmlUtil.GetNode(
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
                "Nexus Parser BVT: Successfully validated all the Alignment Sequences");
            Console.WriteLine(
                "Nexus Parser BVT: Successfully validated all the Alignment Sequences");
        }

        /// <summary>
        /// Compare the actual output with expected output
        /// </summary>
        /// <param name="actualOutput">Actual output</param>
        /// <param name="expectedOutput">Expected output</param>
        /// <returns>True, if comparison is successful</returns>
        static bool CompareOutput(
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