﻿/****************************************************************************
 * SnpP1TestCases.cs
 * 
 *   This file contains the Snp - Parsers P1 test cases.
 * 
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using Bio.IO.Snp;
using Bio.TestAutomation.Util;
using Bio.Util.Logging;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Bio;

namespace Bio.TestAutomation.IO.Snp
{
    /// <summary>
    /// Snp P1 parser Test case implementation.
    /// </summary>
    [TestClass]
    public class SnpP1TestCases
    {

        #region Enums

        /// <summary>
        /// Additional Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum AdditionalParameters
        {
            AlphabetProperty,
            ParseAlleleOne
        };

        #endregion Enums

        #region Global Variables

        Utility utilityObj = new Utility(@"TestUtils\TestsConfig.xml");
        ASCIIEncoding encodingObj = new ASCIIEncoding();

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static SnpP1TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("Bio.automation.log");
            }
        }

        #endregion Constructor

        #region Test cases

        /// <summary>
        /// Parse a valid Snp file and convert the same to one sequence using Parse(file-name)
        /// method, Alphabet property and validate with the expected sequence.
        /// Input : Snp File
        /// Validation : Expected sequence, Chromosome position, Sequence Alphabet, Sequence ID.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SnpP1ParserValidateParseAlphabetProperty()
        {
            SnpParserGeneralTestCases(Constants.SimpleSnpNodeName,
                AdditionalParameters.AlphabetProperty);
        }

        /// <summary>
        /// Parse a valid Snp file and convert the same to one sequence using Parse(file-name)
        /// method, ParseAlleleOne property set to true and validate with the expected sequence.
        /// Input : Snp File
        /// Validation : Expected sequence, Chromosome position, Sequence Alphabet, Sequence ID.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SnpP1ParserValidateParseAlleleOne()
        {
            SnpParserGeneralTestCases(Constants.SimpleSnpNodeName,
                AdditionalParameters.ParseAlleleOne);
        }

        /// <summary>
        /// Parse a valid Snp file and convert the same to one sequence using Parse(file-name)
        /// method, ParseAlleleOne property set to true and validate with the expected sequence.
        /// Input : Snp File
        /// Validation : Expected sequence, Chromosome position, Sequence Alphabet, Sequence ID.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SnpP1ParserValidateParseMultiChromosomes()
        {
            SnpParserGeneralTestCases(Constants.MultiChromosomeSnpNodeName,
                AdditionalParameters.ParseAlleleOne);
        }

        #endregion Test cases

        #region Supporting Methods

        /// <summary>
        /// Snp parser generic method called by all the test cases to 
        /// validate the test case based on the parameters passed.
        /// </summary>
        /// <param name="nodename">Xml node Name.</param>
        /// <param name="additionalParam">Additional parameter based on which 
        /// the validation of  test case is done.</param>
        void SnpParserGeneralTestCases(string nodename,
            AdditionalParameters additionalParam)
        {
            // Gets the expected sequence from the Xml
            string filePath = utilityObj.xmlUtil.GetTextValue(nodename,
                Constants.FilePathNode);

            Assert.IsTrue(File.Exists(filePath));
            // Logs information to the log file
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Snp Parser P1: File Exists in the Path '{0}'.", filePath));

            IList<ISequence> seqList = null;
            SparseSequence sparseSeq = null;
            SimpleSnpParser parser = new SimpleSnpParser(filePath);


            switch (additionalParam)
            {
                case AdditionalParameters.ParseAlleleOne:
                    parser.ParseAlleleOne = true;
                    break;
                default:
                    break;
            }

            string noOfChromos = utilityObj.xmlUtil.GetTextValue(nodename,
                 Constants.NumberOfChromosomesNode);

            seqList = parser.Parse().ToList();
            sparseSeq = (SparseSequence)seqList[0];

            // Based on the number of chromosomes the validation is done reading from the xml
            if (0 == string.Compare(noOfChromos, "1", true, CultureInfo.CurrentCulture))
            {
                string expectedPosition = utilityObj.xmlUtil.GetTextValue(nodename,
               Constants.ExpectedPositionNode);

                string[] expectedPositions = expectedPosition.Split(',');
                string[] expectedCharacters = null;
                string expectedSequence = utilityObj.xmlUtil.GetTextValue(nodename,
                  Constants.ExpectedSequenceNode);

                expectedCharacters = expectedSequence.Split(',');

                Assert.IsNotNull(seqList);
                Assert.AreEqual(noOfChromos, seqList.Count.ToString((IFormatProvider)null));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "Snp Parser P1: Number of Sequences found are '{0}'.",
                    seqList.Count.ToString((IFormatProvider)null)));

                // Validation of sequences with positions and xml is done in this section.
                for (int i = 0; i < expectedPositions.Length; i++)
                {
                    byte item = sparseSeq[int.Parse(expectedPositions[i], (IFormatProvider)null)];

                    Assert.AreEqual(encodingObj.GetBytes(expectedCharacters[i])[0].ToString((IFormatProvider)null),
                        item.ToString((IFormatProvider)null));
                }

                string expSequenceID = utilityObj.xmlUtil.GetTextValue(nodename,
                Constants.SequenceIdNode);

                Assert.AreEqual(expSequenceID, sparseSeq.ID);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "Snp Parser P1: The Sequence ID is '{0}' and is as expected.", sparseSeq.ID));
                // Logs to the VSTest GUI (Console.Out) window
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Snp Parser P1: The Sequence ID is '{0}' and is as expected.", sparseSeq.ID));
            }
            else
            {
                string[] expectedPositions = utilityObj.xmlUtil.GetTextValues(nodename,
                    Constants.ExpectedPositionsNode);

                string[] expectedSequences = utilityObj.xmlUtil.GetTextValues(nodename,
                    Constants.ExpectedSequencesNode);

                string[] expectedSequenceIds = utilityObj.xmlUtil.GetTextValues(nodename,
                    Constants.SequenceIdsNode);

                // Validation of sequences with positions and xml is done in this section.
                for (int i = 0; i < int.Parse(noOfChromos, (IFormatProvider)null); i++)
                {
                    string[] expectedChromoPositions = expectedPositions[i].Split(',');
                    string[] expectedChromoSequences = expectedSequences[i].Split(',');

                    SparseSequence tempSparseSeq = (SparseSequence)seqList[i];

                    for (int j = 0; j < expectedChromoPositions.Length; j++)
                    {
                        byte item = tempSparseSeq[int.Parse(expectedChromoPositions[j], (IFormatProvider)null)];
                        Assert.AreEqual(encodingObj.GetBytes(expectedChromoSequences[j])[0].ToString((IFormatProvider)null),
                            item.ToString((IFormatProvider)null));
                    }

                    // Validation of Id are done in this section.
                    Assert.AreEqual(expectedSequenceIds[i], tempSparseSeq.ID);
                    ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                        "Snp Parser P1: The Sequence ID is '{0}' and is as expected.", tempSparseSeq.ID));
                    // Logs to the VSTest GUI (Console.Out) window
                    Console.WriteLine(string.Format((IFormatProvider)null,
                        "Snp Parser P1: The Sequence ID is '{0}' and is as expected.", tempSparseSeq.ID));
                }
            }

            ApplicationLog.WriteLine(
                "Snp Parser P1: The Snp sequence with position is validated successfully with Parse() method.");
            // Logs to the VSTest GUI (Console.Out) window
            Console.WriteLine(
                "Snp Parser P1: The Snp sequence with position is validated successfully with Parse() method.");

            Assert.IsNotNull(sparseSeq.Alphabet);
            Assert.IsTrue(sparseSeq.Alphabet.Name.ToLower(CultureInfo.CurrentCulture).Contains(utilityObj.xmlUtil.GetTextValue(nodename,
                Constants.AlphabetNameNode).ToLower(CultureInfo.CurrentCulture)));

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Snp Parser P1: The Sequence Alphabet is '{0}' and is as expected.",
                sparseSeq.Alphabet.Name));
        }

        #endregion Supporting Methods
    }
}
