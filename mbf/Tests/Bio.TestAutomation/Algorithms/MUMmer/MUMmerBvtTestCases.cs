﻿/****************************************************************************
 * MUMmerBvtTestCases.cs
 * 
 *   This file contains the MUMmer Bvt test cases
 * 
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Bio.Algorithms.Alignment;
using Bio.Algorithms.MUMmer.LIS;
using Bio.Algorithms.SuffixTree;
using Bio.IO.FastA;
using Bio.TestAutomation.Util;
using Bio.Util.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bio.TestAutomation.Algorithms.MUMmer
{
    /// <summary>
    /// MUMmer Bvt Test case implementation.
    /// </summary>
    [TestClass]
    public class MUMmerBvtTestCases
    {

        #region Global Variables

        Utility utilityObj = new Utility(@"TestUtils\MUMmerTestsConfig.xml");
        ASCIIEncoding encodingObj = new ASCIIEncoding();

        #endregion Global Variables

        # region Enum

        /// <summary>
        /// Lis Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum LISParameters
        {
            FindUniqueMatches,
            PerformLIS,
        };


        #endregion Enum

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static MUMmerBvtTestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("bio.automation.log");
            }
        }

        #endregion Constructor

        #region Suffix Tree Test Cases

        /// <summary>
        /// Validate FindMatches() method with one line sequence 
        /// for both reference and query parameter and validate
        /// the unique matches
        /// Input : One line sequence for both reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SuffixTreeFindMatchesOneLineSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.OneLineSequenceNodeName, false);
        }

        /// <summary>
        /// Validate FindMatches() method with small size (less than 35kb) sequence 
        /// for reference and query parameter and validate
        /// the unique matches
        /// Input : Small size sequence for both reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SuffixTreeFindMatchesSmallSizeSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.SmallSizeSequenceNodeName,
                true);
        }

        /// <summary>      
        /// Validates Edge count for a Suffix tree.
        /// Input:A Dna Sequence.
        /// Output:Edge Count.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateEdgeCount()
        {
            string dnaSequence = "ATGCA";
            Sequence sequence = new Sequence(Alphabets.DNA, dnaSequence);
            MultiWaySuffixTree dnaSuffixTree = new MultiWaySuffixTree(sequence);
            Assert.AreEqual(8, dnaSuffixTree.EdgesCount);
            ApplicationLog.WriteLine(@"MUMmer BVT : Validation of edge
                        count for a Dna sequence completed successfully");

            string ambiguousDnasequence = "RSVTW";

            sequence = new Sequence(AmbiguousDnaAlphabet.Instance, ambiguousDnasequence);
            MultiWaySuffixTree ambiguousDnaSuffixTree = new MultiWaySuffixTree(sequence);
            Assert.AreEqual(7, ambiguousDnaSuffixTree.EdgesCount);
            ApplicationLog.WriteLine(@"MUMmer BVT : Validation of edge
                        count for a Ambiguous Dna sequence completed successfully");
        }

        #endregion Suffix Tree Test Cases

        #region Edges Test cases

        /// <summary>      
        /// Validates the public properties for a leaf node.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateEdgesForALeaf()
        {
            MultiWaySuffixEdge rootEdge = new MultiWaySuffixEdge();
            Assert.AreEqual(rootEdge.IsLeaf, true);
            ApplicationLog.WriteLine("MUMmer BVT : Successfully Validated Is leaf property.");
            Assert.AreEqual(rootEdge.Children, null);
            ApplicationLog.WriteLine("MUMmer BVT : Successfully Validated Children property for a Leaf. ");
            Assert.AreEqual(rootEdge.StartIndex, 0);
            ApplicationLog.WriteLine("MUMmer BVT : Successfully Validated start index of a Leaf.");
        }

        # endregion Edges Test cases

        #region LIS test cases

        /// <summary>
        /// Validate GetLongestSequence() method with two match
        /// for reference and query parameter. 
        /// Input : One sequence for both reference and query parameter
        /// Validation : Validate longest sequence.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateLongestIncreasingSubsequenceTwoUniqueMatch()
        {
            ValidateLongestIncreasingSubsequenceTestCases(Constants.OneLineTwoMatchSequenceNodeName,
                false);
        }

        /// <summary>
        /// Validate GetLongestSequence() method with one match
        /// for reference and query parameter. 
        /// Input : One sequence for both reference and query parameter
        /// Validation : Validate longest sequence.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateLongestIncreasingSubsequenceOneUniqueMatch()
        {
            ValidateLongestIncreasingSubsequenceTestCases(Constants.OneLineSequenceNodeName,
                false);
        }

        #endregion LIS test cases

        #region MUMmer Align Test Cases

        /// <summary>
        /// Validate Align() method with one line sequence 
        /// and validate the aligned sequences
        /// Input : One line sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void MUMmerAlignOneLineSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineSequenceNodeName,
                false, false);
        }

        /// <summary>
        /// Validate Align() method with small size (less than 35kb) sequence 
        /// and validate the aligned sequences
        /// Input : small size sequence file
        /// Validation : Validate the aligned sequences.
        /// </summary>        
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void MUMmerAlignSmallSizeSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.SmallSizeSequenceNodeName,
                true, false);
        }

        /// <summary>
        /// Validate Align(QuerySeqList) method with one line sequence 
        /// and validate the aligned sequences
        /// Input : One line multiple sequences
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void MUMmerAlignQuerySeqList()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.QuerySeqeunceListNode, false, true);
        }

        #endregion MUMmer Align Test Cases

        #region MUMs validation Test Cases

        /// <summary>
        /// Validate GetMatches() method with one line sequence 
        /// and validate the Mumms.
        /// Input : One line sequence
        /// Validation : Validate the MUMs Output.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateMUMsWithOneLineSequence()
        {
            ValidateMUMsGeneralTestCases(Constants.OneLineSequenceNodeName,
                false);
        }

        /// <summary>
        /// Validate GetMumsWithMaxMatch() method with one line sequence 
        /// and validate the Mumms.
        /// Input : One line sequence
        /// Validation : Validate the MUMs Output.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateGetMumsWithMaxMatchWithOneLineSequence()
        {
            // Gets the reference sequence from the configurtion file
            string referenceSequence = utilityObj.xmlUtil.GetTextValue(Constants.OneLineSequenceNodeName,
                    Constants.SequenceNode);

            string referenceSeqAlphabet = utilityObj.xmlUtil.GetTextValue(Constants.OneLineSequenceNodeName,
                Constants.AlphabetNameNode);

            ISequence referenceSeq = new Sequence(Utility.GetAlphabet(referenceSeqAlphabet),
                encodingObj.GetBytes(referenceSequence));

            string querySequence = utilityObj.xmlUtil.GetTextValue(Constants.OneLineSequenceNodeName,
                Constants.SearchSequenceNode);

            referenceSeqAlphabet = utilityObj.xmlUtil.GetTextValue(Constants.OneLineSequenceNodeName,
                Constants.SearchSequenceAlphabetNode);

            ISequence querySeq = new Sequence(Utility.GetAlphabet(referenceSeqAlphabet),
                 encodingObj.GetBytes(querySequence));

            string mumLength = utilityObj.xmlUtil.GetTextValue(Constants.OneLineSequenceNodeName, Constants.MUMLengthNode);

            Bio.Algorithms.MUMmer.MUMmer mum = new Bio.Algorithms.MUMmer.MUMmer(referenceSeq as Sequence);
            mum.LengthOfMUM = long.Parse(mumLength, null);
            IEnumerable<Match> actualResult = null;
            actualResult = mum.GetMatches(querySeq);

            // Validate MUMs output.
            Assert.IsTrue(ValidateMums(Constants.OneLineSequenceNodeName, actualResult));

            Console.WriteLine("MUMmer BVT : Successfully validated the Mumms");
            ApplicationLog.WriteLine("MUMmer BVT : Successfully validated the Mumms.");
        }

        /// <summary>
        /// Validate GetMUMs() method with one line sequence 
        /// and validate the MUMs up to LIS.
        /// Input : One line sequence
        /// Validation : Validate the MUMs Output.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateMUMsUpToLISWithOneLineSequence()
        {
            ValidateMUMsGeneralTestCases(Constants.OneLineSequenceNodeName,
                false);
        }

        /// <summary>
        /// Validates SearchMatch() with One line sequence as input.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateSearchMatchesWithOneLineSequence()
        {
            ValidateSearchMatch(Constants.OneLineSequenceNodeName);
        }

        #endregion MUMs validation Test Cases

        #region Simple Suffix Tree Test Cases

        /// <summary>
        /// Validate SimpleSuffixTree Find Match() for one line sequence
        /// Input : One line sequence for both reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SimpleSuffixTreeFindMatchesOneLineSequence()
        {
            ValidateFindMatchSimpleSuffixGeneralTestCases(Constants.OneLineSequenceNodeName,
                false);
        }

        /// <summary>
        /// Validate SimpleSuffixTree Find Match() for small size sequences
        /// Input : Small size sequence for both reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SimpleSuffixTreeFindMatchesSmallSizeSequence()
        {
            ValidateFindMatchSimpleSuffixGeneralTestCases(Constants.SmallSizeSequenceNodeName,
                true);
        }

        #endregion  Simple Suffix Tree Test Cases

        #region Supported Methods

        /// <summary>
        /// Validates most of the find matches suffix tree test cases with varying parameters.
        /// </summary>
        /// <param name="nodeName">Node name which needs to be read for execution.</param>
        /// <param name="isFilePath">Is File Path?</param>
        /// <param name="LISActionType">LIS action type enum</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)")]
        void ValidateFindMatchSuffixGeneralTestCases(string nodeName, bool isFilePath)
        {
            ISequence referenceSeq = null;
            ISequence querySeq = null;
            string referenceSequence = string.Empty;
            string querySequence = string.Empty;
            IEnumerable<ISequence> referenceSeqs = null;

            if (isFilePath)
            {
                // Gets the reference sequence from the configurtion file
                string filePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.FilePathNode);

                Assert.IsNotNull(filePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the File Path '{0}'.", filePath));

                using (FastAParser parser = new FastAParser(filePath))
                {
                    referenceSeqs = parser.Parse();
                    referenceSeq = referenceSeqs.ElementAt(0);
                    referenceSequence = new string(referenceSeq.Select(a => (char)a).ToArray());
                }

                // Gets the reference sequence from the configurtion file
                string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceFilePathNode);

                Assert.IsNotNull(queryFilePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the Search File Path '{0}'.", queryFilePath));

                IEnumerable<ISequence> querySeqs = null;
                using (FastAParser queryParser = new FastAParser(queryFilePath))
                {
                    querySeqs = queryParser.Parse();
                    querySeq = querySeqs.ElementAt(0);
                    querySequence = new string(querySeq.Select(a => (char)a).ToArray());
                }
            }
            else
            {
                // Gets the reference sequence from the configurtion file
                referenceSequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SequenceNode);

                string seqAlp = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.AlphabetNameNode);

                referenceSeq = new Sequence(Utility.GetAlphabet(seqAlp),
                    encodingObj.GetBytes(referenceSequence));

                querySequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceNode);

                seqAlp = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceAlphabetNode);

                querySeq = new Sequence(Utility.GetAlphabet(seqAlp),
                    encodingObj.GetBytes(querySequence));
            }

            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);

            // Builds the suffix for the reference sequence passed.           
            MultiWaySuffixTree suffixTreeBuilder = new MultiWaySuffixTree(referenceSeq as Sequence);
            suffixTreeBuilder.MinLengthOfMatch = long.Parse(mumLength, null);
            IEnumerable<Match> matches = null;
            matches = suffixTreeBuilder.SearchMatchesUniqueInReference(querySeq);

            // Validates the Unique Matches.
            ApplicationLog.WriteLine("MUMmer BVT : Validating the Unique Matches");
            Assert.IsTrue(ValidateUniqueMatches(matches, nodeName, LISParameters.FindUniqueMatches));

            Console.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
        }

        /// <summary>
        /// Validates the Mummer align method for several test cases for the parameters passed.
        /// </summary>
        /// <param name="nodeName">Node name to be read from xml</param>
        /// <param name="isSeqList">Is MUMmer alignment with List of sequences</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateMUMmerAlignGeneralTestCases(string nodeName, bool isFilePath,
            bool isSeqList)
        {
            ISequence referenceSeq = null;
            ISequence querySeq = null;
            IList<ISequence> querySeqs = new List<ISequence>();
            string referenceSequence = string.Empty;
            string querySequence = string.Empty;
            IList<IPairwiseSequenceAlignment> align = null;
            IEnumerable<ISequence> referenceSeqs = null;

            if (isFilePath)
            {
                // Gets the reference sequence from the configurtion file
                string filePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.FilePathNode);

                Assert.IsNotNull(filePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the File Path '{0}'.", filePath));

                FastAParser parser = new FastAParser(filePath);
                referenceSeqs = parser.Parse();
                referenceSeq = referenceSeqs.ElementAt(0);
                referenceSequence = new string(referenceSeq.Select(a => (char)a).ToArray());

                // Gets the reference sequence from the configurtion file
                string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceFilePathNode);

                Assert.IsNotNull(queryFilePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the Search File Path '{0}'.", queryFilePath));

                FastAParser queryParserObj = new FastAParser(queryFilePath);
                IEnumerable<ISequence> queryEnumSeqs = queryParserObj.Parse();
                querySeq = queryEnumSeqs.ElementAt(0);
                foreach (ISequence seq in queryEnumSeqs)
                {
                    querySeqs.Add(seq);
                }
                querySequence = new string(querySeq.Select(a => (char)a).ToArray());

            }
            else
            {
                // Gets the reference sequence from the configurtion file
                referenceSequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SequenceNode);

                string referenceSeqAlphabet = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.AlphabetNameNode);

                referenceSeq = new Sequence(Utility.GetAlphabet(referenceSeqAlphabet),
                    referenceSequence);

                querySequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceNode);

                referenceSeqAlphabet = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceAlphabetNode);

                querySeq = new Sequence(Utility.GetAlphabet(referenceSeqAlphabet),
                    querySequence);
                querySeqs = new List<ISequence>();
                querySeqs.Add(querySeq);
            }

            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMAlignLengthNode);

            Bio.Algorithms.MUMmer.MUMmerAligner mumAlignObj = new Bio.Algorithms.MUMmer.MUMmerAligner();
            mumAlignObj.LengthOfMUM = long.Parse(mumLength, null);
            mumAlignObj.GapOpenCost = int.Parse(utilityObj.xmlUtil.GetTextValue(nodeName, Constants.GapOpenCostNode),
                (IFormatProvider)null);

            if (isSeqList)
            {
                querySeqs.Add(referenceSeq);
                align = mumAlignObj.Align(querySeqs);
            }
            else
            {
                align = mumAlignObj.AlignSimple(referenceSeq, querySeqs);
            }

            string expectedScore = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.ScoreNodeName);
            Assert.AreEqual(expectedScore,
                align[0].PairwiseAlignedSequences[0].Score.ToString((IFormatProvider)null));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the score for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the score for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));

            string[] expectedSequences = utilityObj.xmlUtil.GetTextValues(nodeName,
                Constants.ExpectedSequencesNode);

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();

            IPairwiseSequenceAlignment seqAlign = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(referenceSeq.Alphabet, expectedSequences[0]);
            alignedSeq.SecondSequence = new Sequence(referenceSeq.Alphabet, expectedSequences[1]);
            alignedSeq.Score = Convert.ToInt32(expectedScore, (IFormatProvider)null);
            seqAlign.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(seqAlign);
            Assert.IsTrue(CompareAlignment(align, expectedOutput));
            Console.WriteLine("MUMmer BVT : Successfully validated the aligned sequences.");
            ApplicationLog.WriteLine("MUMmer BVT : Successfully validated the aligned sequences.");
        }

        /// <summary>
        /// Validates the Unique Matches for the input provided.
        /// </summary>
        /// <param name="matches">Max Unique Match list</param>
        /// <param name="nodeName">Node name to be read from xml</param>
        /// <returns>True, if successfully validated</returns>        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)")]
        bool ValidateUniqueMatches(IEnumerable<Match> matches,
            string nodeName, LISParameters LISActionType)
        {
            // Gets all the unique matches properties to be validated as in xml.
            string[] firstSeqStart = null;
            string[] length = null;
            string[] secondSeqStart = null;

            switch (LISActionType)
            {
                case LISParameters.PerformLIS:
                    firstSeqStart = utilityObj.xmlUtil.GetTextValue(nodeName,
                        Constants.LisFirstSequenceStartNode).Split(',');
                    length = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LisLengthNode).Split(',');
                    secondSeqStart = utilityObj.xmlUtil.GetTextValue(nodeName,
                        Constants.LisSecondSequenceStartNode).Split(',');
                    break;
                case LISParameters.FindUniqueMatches:
                    firstSeqStart = utilityObj.xmlUtil.GetTextValue(nodeName,
                        Constants.FirstSequenceStartNode).Split(',');
                    length = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LengthNode).Split(',');
                    secondSeqStart = utilityObj.xmlUtil.GetTextValue(nodeName,
                        Constants.SecondSequenceStartNode).Split(',');
                    break;
                default:
                    break;
            }

            int i = 0;
            // Loops through all the matches and validates the same.            
            foreach (Match match in matches)
            {
                if ((0 != string.Compare(firstSeqStart[i],
                    match.ReferenceSequenceOffset.ToString((IFormatProvider)null), true, CultureInfo.CurrentCulture))
                    || (0 != string.Compare(length[i],
                    match.Length.ToString((IFormatProvider)null), true, CultureInfo.CurrentCulture))
                    || (0 != string.Compare(secondSeqStart[i],
                    match.QuerySequenceOffset.ToString((IFormatProvider)null), true, CultureInfo.CurrentCulture)))
                {
                    Console.WriteLine(string.Format((IFormatProvider)null,
                        "MUMmer BVT : Unique match not matching at index '{0}'", i.ToString((IFormatProvider)null)));
                    ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                        "MUMmer BVT : Unique match not matching at index '{0}'", i.ToString((IFormatProvider)null)));
                    return false;
                }
                i++;
            }

            return true;
        }

        /// <summary>
        /// Validate the Mummer GetMUMs method for different test cases.
        /// </summary>
        /// <param name="nodeName">Name of the XML node to be read.</param>
        /// <param name="isFilePath">Is Sequence saved in File</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateMUMsGeneralTestCases(string nodeName, bool isFilePath)
        {
            ISequence referenceSeq = null;
            ISequence querySeq = null;
            IEnumerable<ISequence> querySeqs = null;
            string referenceSequence = string.Empty;
            string querySequence = string.Empty;
            IEnumerable<ISequence> referenceSeqs = null;

            if (isFilePath)
            {
                // Gets the reference sequence from the configurtion file
                string filePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.FilePathNode);

                Assert.IsNotNull(filePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the File Path '{0}'.", filePath));

                using (FastAParser parser = new FastAParser(filePath))
                {
                    parser.Alphabet = Alphabets.DNA;
                    referenceSeqs = parser.Parse();
                    referenceSeq = referenceSeqs.ElementAt(0);
                    referenceSequence = new string(referenceSeq.Select(a => (char)a).ToArray());

                    // Gets the reference sequence from the configurtion file
                    string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                        Constants.SearchSequenceFilePathNode);

                    Assert.IsNotNull(queryFilePath);
                    ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                        "MUMmer BVT : Successfully validated the Search File Path '{0}'.", queryFilePath));

                    using (FastAParser parserQuery = new FastAParser(queryFilePath))
                    {
                        querySeqs = parser.Parse();
                        querySeq = querySeqs.ElementAt(0);
                        querySequence = new string(querySeq.Select(a => (char)a).ToArray());
                    }
                }
            }
            else
            {
                // Gets the reference sequence from the configurtion file
                referenceSequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SequenceNode);

                string referenceSeqAlphabet = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.AlphabetNameNode);

                referenceSeq = new Sequence(Utility.GetAlphabet(referenceSeqAlphabet),
                    encodingObj.GetBytes(referenceSequence));

                querySequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceNode);

                referenceSeqAlphabet = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceAlphabetNode);

                querySeq = new Sequence(Utility.GetAlphabet(referenceSeqAlphabet),
                    encodingObj.GetBytes(querySequence));

                querySeqs = new List<ISequence>() { querySeq };
            }

            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);

            Bio.Algorithms.MUMmer.MUMmer mum = new Bio.Algorithms.MUMmer.MUMmer(referenceSeq as Sequence);
            mum.LengthOfMUM = long.Parse(mumLength, null);
            IEnumerable<Match> actualResult = null;
            actualResult = mum.GetMatchesUniqueInReference(querySeqs.ElementAt(0));

            // Validate MUMs output.
            Assert.IsTrue(ValidateMums(nodeName, actualResult));

            Console.WriteLine("MUMmer BVT : Successfully validated the Mumms");
            ApplicationLog.WriteLine("MUMmer BVT : Successfully validated the Mumms.");
        }

        /// <summary>
        /// Validate the Mums output.
        /// </summary>
        /// <param name="result">Mumms Output</param>
        /// <param name="nodeName">Node name to be read from xml</param>       
        bool ValidateMums(string nodeName, IEnumerable<Match> result)
        {
            string[] firstSeqStart = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LisFirstSequenceStartNode).Split(',');
            string[] length = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LisLengthNode).Split(',');
            string[] secondSeqStart = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LisSecondSequenceStartNode).Split(',');

            var mums = result;
            for (int i = 0; i < mums.Count(); i++)
            {
                if ((0 != string.Compare(firstSeqStart[i], mums.ElementAt(i).ReferenceSequenceOffset.ToString((IFormatProvider)null), StringComparison.CurrentCulture))
                   || (0 != string.Compare(length[i], mums.ElementAt(i).Length.ToString((IFormatProvider)null), StringComparison.CurrentCulture))
                   || (0 != string.Compare(secondSeqStart[i], mums.ElementAt(i).QuerySequenceOffset.ToString((IFormatProvider)null), StringComparison.CurrentCulture)))
                {
                    Console.WriteLine(string.Format((IFormatProvider)null, "MUMmer P1 : There is no match at '{0}'", i.ToString((IFormatProvider)null)));
                    ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "MUMmer P1 : There is no match at '{0}'", i.ToString((IFormatProvider)null)));
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// Validates most of the find matches suffix tree test cases with varying parameters.
        /// </summary>
        /// <param name="nodeName">Node name which needs to be read for execution.</param>
        /// <param name="isFilePath">Is File Path?</param>
        /// <param name="LISActionType">LIS action type enum</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateFindMatchSimpleSuffixGeneralTestCases(string nodeName, bool isFilePath)
        {
            ISequence referenceSeq = null;
            ISequence querySeq = null;
            string referenceSequence = string.Empty;
            string querySequence = string.Empty;
            IEnumerable<ISequence> referenceSeqs = null;

            if (isFilePath)
            {
                // Gets the reference sequence from the configurtion file
                string filePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.FilePathNode);

                Assert.IsNotNull(filePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the File Path '{0}'.", filePath));

                using (FastAParser parser = new FastAParser(filePath))
                {
                    referenceSeqs = parser.Parse();
                    referenceSeq = referenceSeqs.ElementAt(0);
                    referenceSequence = new string(referenceSeq.Select(a => (char)a).ToArray());
                }

                // Gets the reference sequence from the configurtion file
                string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceFilePathNode);

                Assert.IsNotNull(queryFilePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the Search File Path '{0}'.", queryFilePath));

                IEnumerable<ISequence> querySeqs = null;
                using (FastAParser queryParser = new FastAParser(queryFilePath))
                {
                    querySeqs = queryParser.Parse();
                    querySeq = querySeqs.ElementAt(0);
                    querySequence = new string(querySeq.Select(a => (char)a).ToArray());
                }
            }
            else
            {
                // Gets the reference sequence from the configurtion file
                referenceSequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SequenceNode);

                string seqAlp = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.AlphabetNameNode);

                referenceSeq = new Sequence(Utility.GetAlphabet(seqAlp),
                    encodingObj.GetBytes(referenceSequence));

                querySequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceNode);

                seqAlp = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceAlphabetNode);

                querySeq = new Sequence(Utility.GetAlphabet(seqAlp),
                    encodingObj.GetBytes(querySequence));
            }

            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);

            // Builds the suffix for the reference sequence passed.

            MultiWaySuffixTree suffixTreeBuilder = new MultiWaySuffixTree(referenceSeq as Sequence);
            suffixTreeBuilder.MinLengthOfMatch = long.Parse(mumLength, null);
            IEnumerable<Match> matches = null;
            matches = suffixTreeBuilder.SearchMatchesUniqueInReference(querySeq);

            // Validates the Unique Matches.
            ApplicationLog.WriteLine("MUMmer BVT : Validating the Unique Matches");
            Assert.IsTrue(ValidateUniqueMatches(matches, nodeName, LISParameters.FindUniqueMatches));

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
        }

        /// <summary>
        /// Validates SearchMatch() with different inputs.
        /// </summary>
        /// <param name="nodeName">Parent Node from Xml.</param>
        void ValidateSearchMatch(string nodeName)
        {
            string referenceSequence = string.Empty;
            string querySequence = string.Empty;
            string seqAlp = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.AlphabetNameNode);

            // Gets the reference sequence from the configurtion file
            referenceSequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                Constants.SequenceNode);

            querySequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                Constants.SearchSequenceNode);

            seqAlp = utilityObj.xmlUtil.GetTextValue(nodeName,
                Constants.SearchSequenceAlphabetNode);

            IEnumerable<Match> matches = null;
            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);
            Sequence referenceSequenceForMatches = new Sequence(Utility.GetAlphabet(seqAlp), referenceSequence);
            MultiWaySuffixTree suffixTree = new MultiWaySuffixTree(referenceSequenceForMatches);
            suffixTree.MinLengthOfMatch = long.Parse(mumLength, null);
            Sequence querySequenceForMatches = new Sequence(Utility.GetAlphabet(seqAlp), querySequence);
            matches = suffixTree.SearchMatches(querySequenceForMatches);
            // Validates the Unique Matches.
            ApplicationLog.WriteLine("MUMmer BVT : Validating the Unique Matches");
            Assert.IsTrue(ValidateUniqueMatches(matches, nodeName, LISParameters.FindUniqueMatches));

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
        }

        /// <summary>
        /// Validates Longest Increasing sequences.
        /// </summary>
        /// <param name="nodeName">Node name which needs to be read for execution.</param>
        /// <param name="isFilePath">Is File Path?</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateLongestIncreasingSubsequenceTestCases(string nodeName, bool isFilePath)
        {

            ISequence referenceSeq = null;
            ISequence querySeq = null;
            string referenceSequence = string.Empty;
            string querySequence = string.Empty;
            IEnumerable<ISequence> referenceSeqs = null;

            if (isFilePath)
            {
                // Gets the reference sequence from the configurtion file
                string filePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.FilePathNode);

                Assert.IsNotNull(filePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the File Path '{0}'.", filePath));

                using (FastAParser parser = new FastAParser(filePath))
                {
                    referenceSeqs = parser.Parse();
                    referenceSeq = referenceSeqs.ElementAt(0);
                    referenceSequence = new string(referenceSeq.Select(a => (char)a).ToArray());
                }

                // Gets the reference sequence from the configurtion file
                string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceFilePathNode);

                Assert.IsNotNull(queryFilePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the Search File Path '{0}'.", queryFilePath));

                IEnumerable<ISequence> querySeqs = null;
                using (FastAParser queryParser = new FastAParser(queryFilePath))
                {
                    querySeqs = queryParser.Parse();
                    querySeq = querySeqs.ElementAt(0);
                    querySequence = new string(querySeq.Select(a => (char)a).ToArray());
                }
            }
            else
            {
                // Gets the reference sequence from the configurtion file
                referenceSequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SequenceNode);

                string seqAlp = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.AlphabetNameNode);

                referenceSeq = new Sequence(Utility.GetAlphabet(seqAlp),
                    referenceSequence);

                querySequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceNode);

                seqAlp = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceAlphabetNode);

                querySeq = new Sequence(Utility.GetAlphabet(seqAlp),
                    querySequence);
            }

            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);

            IEnumerable<Match> matches;
            Bio.Algorithms.MUMmer.MUMmer mum = new Bio.Algorithms.MUMmer.MUMmer(referenceSeq as Sequence);
            mum.LengthOfMUM = long.Parse(mumLength, (IFormatProvider)null);
            matches = mum.GetMatchesUniqueInReference(querySeq);

            // Validates the Unique Matches.
            ApplicationLog.WriteLine("MUMmer BVT : Validating the Unique Matches using LIS");
            LongestIncreasingSubsequence lisObj = new LongestIncreasingSubsequence();

            List<Match> listMatch = new List<Match>();

            foreach (Match mtch in matches)
            {
                listMatch.Add(mtch);
            }

            IList<Match> lisSorted = null, actualLis = null;
            lisSorted = lisObj.SortMum(listMatch);
            actualLis = lisObj.GetLongestSequence(lisSorted);
            Assert.IsTrue(ValidateUniqueMatches(actualLis, nodeName, LISParameters.PerformLIS));

            Console.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
        }

        /// <summary>
        /// Compare the alignment of mummer and defined alignment
        /// </summary>
        /// <param name="result">output of Aligners</param>
        /// <param name="expectedAlignment">expected output</param>
        /// <returns>Compare result of alignments</returns>
        private static bool CompareAlignment(IList<IPairwiseSequenceAlignment> actualAlignment,
             IList<IPairwiseSequenceAlignment> expectedAlignment)
        {
            bool output = true;

            if (actualAlignment.Count == expectedAlignment.Count)
            {
                for (int resultCount = 0; resultCount < actualAlignment.Count; resultCount++)
                {
                    if (actualAlignment[resultCount].PairwiseAlignedSequences.Count == expectedAlignment[resultCount].PairwiseAlignedSequences.Count)
                    {
                        for (int alignSeqCount = 0; alignSeqCount < actualAlignment[resultCount].PairwiseAlignedSequences.Count; alignSeqCount++)
                        {
                            // Validates the First Sequence, Second Sequence and Score
                            if (actualAlignment[resultCount].PairwiseAlignedSequences[alignSeqCount].FirstSequence.ToString().Equals(
                                    expectedAlignment[resultCount].PairwiseAlignedSequences[alignSeqCount].FirstSequence.ToString())
                                && actualAlignment[resultCount].PairwiseAlignedSequences[alignSeqCount].SecondSequence.ToString().Equals(
                                    expectedAlignment[resultCount].PairwiseAlignedSequences[alignSeqCount].SecondSequence.ToString())
                                && actualAlignment[resultCount].PairwiseAlignedSequences[alignSeqCount].Score ==
                                    expectedAlignment[resultCount].PairwiseAlignedSequences[alignSeqCount].Score)
                            {
                                output = true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return output;
        }

        #endregion Supported Methods
    }
}


