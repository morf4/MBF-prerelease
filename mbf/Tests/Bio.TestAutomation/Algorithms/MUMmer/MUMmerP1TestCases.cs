﻿/****************************************************************************
 * MUMmerP1TestCases.cs
 * 
 *   This file contains the MUMmer Priority one test cases
 * 
***************************************************************************/

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

using Bio.IO;
using Bio.Algorithms.MUMmer;
using Bio.Algorithms.SuffixTree;
using Bio.TestAutomation.Util;
using Bio.Util.Logging;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using Bio.SimilarityMatrices;
using Bio.IO.FastA;
using Bio.Algorithms.Alignment;
using Bio;

namespace Bio.TestAutomation.Algorithms.MUMmer
{
    /// <summary>
    /// MUMmer Priority One Test case implementation.
    /// </summary>
    [TestClass]
    public class MUMmerP1TestCases
    {
        #region Enums

        /// <summary>
        /// Ambiguity Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum PhaseOneAmbiguityParameters
        {
            Dna,
            Rna,
            Other
        };

        /// <summary>
        /// Parser Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum ParserParameters
        {
            FastA
        };

        /// <summary>
        /// Additional Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum AdditionalParameters
        {
            PerformSimilarityMatrixChange,
            PerformAlgorithmChange,
            Other
        };

        #endregion Enums

        #region Global Variables

        Utility utilityObj = new Utility(@"TestUtils\MUMmerTestsConfig.xml");
        ASCIIEncoding encodingObj = new ASCIIEncoding();

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static MUMmerP1TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("bio.automation.log");
            }
        }

        #endregion Constructor

        #region Suffix Tree Test cases

        /// <summary>
        /// Validate BuildSuffixTree() method with Dna sequence 
        /// and validate the nodes, edges and the sequence
        /// Input : Dna sequence file
        /// Validation : Validate the nodes, edges and the sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeBuildSuffixTreeDnaSequence()
        {
            ValidateBuildSuffixTreeGeneralTestCases(Constants.DnaSequenceNodeName, true);
        }

        /// <summary>
        /// Validate BuildSuffixTree() method with Rna sequence 
        /// and validate the nodes, edges and the sequence
        /// Input : Rna sequence file
        /// Validation : Validate the nodes, edges and the sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeBuildSuffixTreeRnaSequence()
        {
            ValidateBuildSuffixTreeGeneralTestCases(Constants.RnaSequenceNodeName, true);
        }

        /// <summary>
        /// Validate BuildSuffixTree() method with Medium size sequence 
        /// and validate the nodes, edges and the sequence
        /// Input : Medium size sequence file
        /// Validation : Validate the nodes, edges and the sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeBuildSuffixTreeMediumSizeSequence()
        {
            ValidateBuildSuffixTreeGeneralTestCases(Constants.MediumSizeSequenceNodeName, true);
        }

        /// <summary>
        /// Validate BuildSuffixTree() method with ContinousRepeating Characters 
        /// and validate the nodes, edges and the sequence
        /// Input : Continuous Repeating Characters
        /// Validation : Validate the nodes, edges and the sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeBuildSuffixTreeContinousRepeatingCharacters()
        {
            ValidateBuildSuffixTreeGeneralTestCases(Constants.OneLineRepeatingCharactersNodeName, false);
        }

        /// <summary>
        /// Validate BuildSuffixTree() method with Alternate Repeating Characters 
        /// and validate the nodes, edges and the sequence
        /// Input : Alternate Repeating Characters
        /// Validation : Validate the nodes, edges and the sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeBuildSuffixTreeAlternateRepeatingCharacters()
        {
            ValidateBuildSuffixTreeGeneralTestCases(Constants.OneLineAlternateRepeatingCharactersNodeName,
                false);
        }

        /// <summary>
        /// Validate BuildSuffixTree() method with Fasta File sequence 
        /// and validate the nodes, edges and the sequence
        /// Input : Dna sequence Fasta file
        /// Validation : Validate the nodes, edges and the sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeBuildSuffixTreeFastaFileSequence()
        {
            ValidateBuildSuffixTreeGeneralTestCases(Constants.SimpleDnaFastaNodeName, true);
        }

        /// <summary>
        /// Validate BuildSuffixTree() method with Only Repeating Characters 
        /// and validate the nodes, edges and the sequence
        /// Input : Only Repeating Characters
        /// Validation : Validate the nodes, edges and the sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeBuildSuffixTreeOnlyRepeatingCharacters()
        {
            ValidateBuildSuffixTreeGeneralTestCases(
                Constants.OneLineOnlyRepeatingCharactersNodeName, false);
        }

        /// <summary>
        /// Validate FindMatches() method with valid MUM length 
        /// for reference and query parameter and validate
        /// the unique matches
        /// Input : Valid mum length with both reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesValidMumLengthSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.OneLineSequenceNodeName,
                false, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with valid Dna sequence 
        /// for reference and query parameter and validate
        /// the unique matches
        /// Input : Dna sequence for both reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesDnaSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.DnaSequenceNodeName,
                true, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with valid Rna sequence 
        /// for reference and query parameter and validate
        /// the unique matches
        /// Input : Rna sequence for both reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesRnaSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.RnaSequenceNodeName,
                true, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with valid Medium size sequence 
        /// for reference and query parameter and validate
        /// the unique matches
        /// Input : Medium Size sequence for both reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesMediumSizeSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.MediumSizeSequenceNodeName,
                true, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with continuous repeating sequence 
        /// for reference and valid Sequence for query parameter and validate
        /// the unique matches
        /// Input : continuous repeating sequence for reference and valid sequence for query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesContinousRepeatingSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.OneLineRepeatingCharactersNodeName,
                false, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with same sequence 
        /// for reference and query parameter and validate
        /// the unique matches
        /// Input : same sequence for reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesSameSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.OneLineSameCharactersNodeName,
                false, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with over lap sequence 
        /// for reference and query parameter and validate
        /// the unique matches
        /// Input : over lap sequence for reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesOverlapSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.OneLineOverlapSequenceNodeName,
                false, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with no match 
        /// for reference and query parameter and validate
        /// there are no unique matches
        /// Input : over lap sequence for reference and query parameter
        /// Validation : Validate there are no unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesNoMatchSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.OneLineNoMatchSequenceNodeName,
                false, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with cross over lap sequence 
        /// for reference and query parameter and validate
        /// the unique matches
        /// Input : cross over lap sequence for reference and query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesCrossoverlapSequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.SmallSizeSequenceNodeName,
                true, PhaseOneAmbiguityParameters.Other);
        }

        /// <summary>
        /// Validate FindMatches() method with Dna valid sequence 
        /// for reference and Ambiguity characters in sequence for query parameter
        /// and validate the unique matches
        /// Input : valid Dna sequence for reference and ambiguity characters for query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesDnaSearchAmbiguitySequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.DnaSearchAmbiguitySequenceNodeName,
                true, PhaseOneAmbiguityParameters.Dna);
        }

        /// <summary>
        /// Validate FindMatches() method with Rna valid sequence 
        /// for reference and Ambiguity characters in sequence for query parameter
        /// and validate the unique matches
        /// Input : valid Rna sequence for reference and ambiguity characters for query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesRnaSearchAmbiguitySequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.RnaSearchAmbiguitySequenceNodeName,
                true, PhaseOneAmbiguityParameters.Rna);
        }

        /// <summary>
        /// Validate FindMatches() method with Dna valid sequence 
        /// with Ambiguity characters for reference and valid sequence for query parameter
        /// and validate the unique matches
        /// Input : valid Dna sequence with ambiguity characters for reference 
        /// and valid sequence for query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesDnaQueryAmbiguitySequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.DnaQueryAmbiguitySequenceNodeName,
                true, PhaseOneAmbiguityParameters.Dna);
        }

        /// <summary>
        /// Validate FindMatches() method with Rna valid sequence 
        /// with Ambiguity characters for reference and valid sequence for query parameter
        /// and validate the unique matches
        /// Input : valid Rna sequence with ambiguity characters for reference 
        /// and valid sequence for query parameter
        /// Validation : Validate the unique matches
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void SuffixTreeFindMatchesRnaQueryAmbiguitySequence()
        {
            ValidateFindMatchSuffixGeneralTestCases(Constants.RnaQueryAmbiguitySequenceNodeName,
                true, PhaseOneAmbiguityParameters.Rna);
        }

        #endregion Suffix Tree Test cases

        #region MUMmer Align Test Cases

        /// <summary>
        /// Validate Align(reference, query) method with Dna sequence 
        /// and validate the aligned sequences
        /// Input : Dna sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>

        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignDnaSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.DnaSequenceNodeName, true, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with Rna sequence 
        /// and validate the aligned sequences
        /// Input : Rna sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignRnaSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.RnaSequenceNodeName, true, false);
        }

        /// <summary>
        /// Validate Align(list of sequences) method with valid sequence 
        /// and validate the aligned sequences
        /// Input : valid sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignListOneLineSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineSequenceNodeName, false, true);
        }

        /// <summary>
        /// Validate Align(list of sequences) method with valid 
        /// small size sequence and validate the aligned sequences
        /// Input : valid small size sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>

        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignListSmallSizeSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.SmallSizeSequenceNodeName, true, true);
        }

        /// <summary>
        /// Validate Align(list of sequences) method with valid 
        /// Dna sequence and validate the aligned sequences
        /// Input : valid Dna sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>

        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignListDnaSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.DnaSequenceNodeName, true, true);
        }

        /// <summary>
        /// Validate Align(list of sequences) method with valid 
        /// Rna sequence and validate the aligned sequences
        /// Input : valid Rna sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignListRnaSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.RnaSequenceNodeName, true, true);
        }

        /// <summary>
        /// Validate Align(reference, query) method with repeating character sequence 
        /// and validate the aligned sequences
        /// Input : repeating character sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignRepeatingCharactersSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineRepeatingCharactersNodeName, false, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with alternate repeating character sequence 
        /// and validate the aligned sequences
        /// Input : repeating character sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignAlternateRepeatingCharactersSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineAlternateRepeatingCharactersNodeName,
                false, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with fasta file sequence 
        /// and validate the aligned sequences
        /// Input : FastA file sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>

        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignFastAFileSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.SimpleDnaFastaNodeName,
                true, false, ParserParameters.FastA);
        }

        /// <summary>
        /// Validate Align(reference, query) method with only repeating character sequence 
        /// and validate the aligned sequences
        /// Input : only Repeating character sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignOnlyRepeatingCharactersSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineOnlyRepeatingCharactersNodeName,
                false, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with two query sequences 
        /// and validate the aligned sequences
        /// Input : Two Query sequences
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignTwoQuerySequences()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.DnaQueryDnaRnaSequenceNodeName,
                true, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with Dna sequence 
        /// and validate the aligned sequences with valid MUM length
        /// Input : Dna sequence with valid Mum length
        /// Validation : Validate the aligned sequences.
        /// </summary>

        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignValidMumLengthSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.DnaSequenceNodeName, true, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with same sequence 
        /// for both and validate the aligned sequences
        /// Input : Same sequence for both Query and Reference
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignSameCharactersSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineSameCharactersNodeName,
                false, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with overlap sequence 
        /// and validate the aligned sequences
        /// Input : Over lap sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignOverlapSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineOverlapSequenceNodeName,
                false, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with no matching sequence 
        /// and validate the aligned sequences
        /// Input : No matching sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignNoMatchSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineNoMatchSequenceNodeName,
                false, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with Cross overlap sequence 
        /// and validate the aligned sequences
        /// Input : Cross Over lap sequence
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignCrossOverlapSequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineMoreThanTwoMatchOverlapSequenceNodeName,
                false, false);
        }

        /// <summary>
        /// Validate Align(reference, query) method with similarity matrix
        /// as Blosum 50 and validate the aligned sequences
        /// Input : Sequence with Similarity matrix Blosum 50.
        /// Validation : Validate the aligned sequences.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void MUMmerAlignSimilarityMatrixBlosum50Sequence()
        {
            ValidateMUMmerAlignGeneralTestCases(Constants.OneLineMultipleSameLengthMatchOverlapSequenceNodeName,
                false, false, ParserParameters.FastA, AdditionalParameters.PerformSimilarityMatrixChange);
        }

        #endregion MUMmer Align Test Cases

        #region MUMs validation Test Cases

        /// <summary>
        /// Validate MUMs with One Line Multiple Match Overlap Sequences 
        /// and validate the MUMs up to LIS.
        /// Input : One Line Multiple Match Overlap Sequences
        /// Validation : Validate the MUMs Output.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMUMsUpToLISOneLineMultipleOverlapSeq()
        {
            ValidateMUMsGeneralTestCases(Constants.OneLineMultipleMatchOverlapSequenceUpToLISNode, false);
        }

        /// <summary>
        /// Validates SearchMatch() with One line sequence as input.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateGetMatchesWithOneLineSequence()
        {
            ValidateGetMatch(Constants.OneLineSequenceNodeName);
        }

        /// <summary>
        /// Validates MUMmer(ISuffixTree) constructor with One line sequence as input.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void ValidateMUMerConstructorWithSuffixTree()
        {
            ValidateConstructorWithSuffixTree(Constants.OneLineSequenceNodeName);
        }

        /// <summary>
        /// Validate MUMs with One Line Multiple Same Length MatchOverlap Sequence
        /// AfterLIS and validate the MUMs Up to LIS.
        /// Input : One Line MultipleMatchOverlap Sequences
        /// Validation : Validate the MUMs Output.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMUMsUpToLISOneLineMultipleOverlapSameLengthSeq()
        {
            ValidateMUMsGeneralTestCases(Constants.OneLineMultipleSameLengthMatchOverlapSequenceUptoLISNode, false);
        }

        /// <summary>
        /// Validate MUMs with One Line More Than Two Match Sequence
        /// and validate the MUMs Upto LIS.
        /// Input : One Line More Than Two Match Sequence
        /// Validation : Validate the MUMs Output.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMUMsUpToLISOneLineMoreThanTwoMatchSequence()
        {
            ValidateMUMsGeneralTestCases(Constants.OneLineMoreThanTwoMatchSequenceUpToLISNode,
                false);
        }

        #endregion MUMs valildation Test Cases

        #region Supported Methods

        /// <summary>
        /// Validates most of the find matches suffix tree test cases with varying parameters.
        /// </summary>
        /// <param name="nodeName">Node name which needs to be read for execution.</param>
        /// <param name="isFilePath">Is File Path?</param>
        void ValidateFindMatchSuffixGeneralTestCases(string nodeName,
            bool isFilePath, PhaseOneAmbiguityParameters isAmbiguousCharacter)
        {
            ValidateFindMatchSuffixGeneralTestCases(nodeName, isFilePath, false, isAmbiguousCharacter);
        }

        /// <summary>
        /// Validates most of the find matches suffix tree test cases with varying parameters.
        /// </summary>
        /// <param name="nodeName">Node name which needs to be read for execution.</param>
        /// <param name="isFilePath">Is File Path?</param>
        /// <param name="isMultiSequenceFile">Is Multi Sequence Search File?</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateFindMatchSuffixGeneralTestCases(string nodeName, bool isFilePath,
            bool isMultiSequenceSearchFile, PhaseOneAmbiguityParameters isAmbiguousCharacter)
        {
            ISequence referenceSeq = null;
            ISequence querySeq = null;
            string referenceSequence = string.Empty;
            string querySequence = string.Empty;
            IEnumerable<ISequence> referenceSeqs = null;
            IEnumerable<ISequence> querySeqs = null;

            if (isFilePath)
            {
                // Gets the reference sequence from the configurtion file
                string filePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.FilePathNode);

                Assert.IsNotNull(filePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the File Path '{0}'.", filePath));

                FastAParser parser = new FastAParser(filePath);
                switch (isAmbiguousCharacter)
                {
                    case PhaseOneAmbiguityParameters.Dna:
                        parser.Alphabet = AmbiguousDnaAlphabet.Instance;
                        break;
                    case PhaseOneAmbiguityParameters.Rna:
                        parser.Alphabet = AmbiguousRnaAlphabet.Instance;
                        break;
                    default:
                        break;
                }
                referenceSeqs = parser.Parse();
                referenceSeq = referenceSeqs.ElementAt(0);
                referenceSequence = new string(referenceSeq.Select(a => (char)a).ToArray());

                // Gets the reference sequence from the configurtion file
                string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceFilePathNode);

                Assert.IsNotNull(queryFilePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer BVT : Successfully validated the Search File Path '{0}'.", queryFilePath));

                FastAParser queryParser = new FastAParser(queryFilePath);
                switch (isAmbiguousCharacter)
                {
                    case PhaseOneAmbiguityParameters.Dna:
                        queryParser.Alphabet = AmbiguousDnaAlphabet.Instance;
                        break;
                    case PhaseOneAmbiguityParameters.Rna:
                        queryParser.Alphabet = AmbiguousRnaAlphabet.Instance;
                        break;
                    default:
                        break;
                }

                querySeqs = queryParser.Parse();
                querySeq = querySeqs.ElementAt(0);
                querySequence = new string(querySeq.Select(a => (char)a).ToArray());
            }
            else
            {
                // Gets the reference sequence from the configurtion file
                referenceSequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SequenceNode);

                string referenceAlphabet = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.AlphabetNameNode);

                referenceSeq = new Sequence(Utility.GetAlphabet(referenceAlphabet),
                   encodingObj.GetBytes(referenceSequence));

                querySequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceNode);

                referenceAlphabet = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceAlphabetNode);

                querySeq = new Sequence(Utility.GetAlphabet(referenceAlphabet),
                   encodingObj.GetBytes(querySequence));
            }

            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);

            // Builds the suffix for the reference sequence passed.            
            MultiWaySuffixTree suffixTreeBuilder = new MultiWaySuffixTree(referenceSeq as Sequence);
            suffixTreeBuilder.MinLengthOfMatch = long.Parse(mumLength, null);
            IEnumerable<Match> matches = null;
            matches = suffixTreeBuilder.SearchMatchesUniqueInReference(querySeq);

            // For multi sequence query file validate all the sequences with the reference sequence
            if (isMultiSequenceSearchFile)
            {
                matches = suffixTreeBuilder.SearchMatchesUniqueInReference(
                    querySeqs.ElementAt(0));
                Assert.IsTrue(ValidateUniqueMatches(matches, nodeName));
                matches = suffixTreeBuilder.SearchMatchesUniqueInReference(
                    querySeqs.ElementAt(1));
                Assert.IsTrue(ValidateUniqueMatches(matches, nodeName));
            }
            else
            {
                matches = suffixTreeBuilder.SearchMatchesUniqueInReference(querySeq);
                Assert.IsTrue(ValidateUniqueMatches(matches, nodeName));
            }

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer P1 : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
        }

        /// <summary>
        /// Validates most of the build suffix tree test cases with varying parameters.
        /// </summary>
        /// <param name="nodeName">Node name which needs to be read for execution.</param>
        /// <param name="isFilePath">Is file path?</param>
        /// <param name="parserParam">Parser parameter.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateBuildSuffixTreeGeneralTestCases(string nodeName, bool isFilePath)
        {
            ISequence referenceSeq = null;
            string referenceSequence = string.Empty;

            if (isFilePath)
            {
                IEnumerable<ISequence> referenceSeqs = null;
                // Gets the reference sequence from the configurtion file
                string filePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.FilePathNode);

                Assert.IsNotNull(filePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer P1 : Successfully validated the File Path '{0}'.", filePath));

                using (FastAParser fastaParserObj = new FastAParser(filePath))
                {
                    referenceSeqs = fastaParserObj.Parse();

                    referenceSeq = referenceSeqs.ElementAt(0);
                    referenceSequence = new string(referenceSeq.Select(a => (char)a).ToArray());
                }
            }
            else
            {
                // Gets the reference sequence from the configurtion file
                referenceSequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SequenceNode);

                referenceSeq = new Sequence(Utility.GetAlphabet(utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.AlphabetNameNode)), encodingObj.GetBytes(referenceSequence));
            }

            // Builds the suffix for the reference sequence passed.            
            MultiWaySuffixTree suffixTree = new MultiWaySuffixTree(referenceSeq as Sequence);

            Assert.AreEqual(new string(suffixTree.Sequence.Select(a => (char)a).ToArray()), referenceSequence);
            Console.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer P1 : Successfully validated the Suffix Tree properties for the sequence '{0}'.",
                referenceSequence));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer P1 : Successfully validated the Suffix Tree properties for the sequence '{0}'.",
                referenceSequence));
        }

        /// <summary>
        /// Validates the Mummer align method for several test cases for the parameters passed.
        /// </summary>
        /// <param name="nodeName">Node name to be read from xml</param>
        /// <param name="isFilePath">Is Sequence saved in File</param>
        /// <param name="isAlignList">Is align method to take list?</param>
        void ValidateMUMmerAlignGeneralTestCases(string nodeName, bool isFilePath,
            bool isAlignList)
        {
            ValidateMUMmerAlignGeneralTestCases(nodeName, isFilePath, isAlignList,
                ParserParameters.FastA, AdditionalParameters.Other);
        }

        /// <summary>
        /// Validates the Mummer align method for several test cases for the parameters passed.
        /// </summary>
        /// <param name="nodeName">Node name to be read from xml</param>
        /// <param name="isFilePath">Is Sequence saved in File</param>
        /// <param name="isAlignList">Is align method to take list?</param>
        /// <param name="parserParam">Parser Parameter</param>
        void ValidateMUMmerAlignGeneralTestCases(string nodeName, bool isFilePath,
            bool isAlignList, ParserParameters parserParam)
        {
            ValidateMUMmerAlignGeneralTestCases(nodeName, isFilePath,
                       isAlignList, parserParam, AdditionalParameters.Other);
        }

        /// <summary>
        /// Validates the Mummer align method for several test cases for the parameters passed.
        /// </summary>
        /// <param name="nodeName">Node name to be read from xml</param>
        /// <param name="isFilePath">Is Sequence saved in File</param>
        /// <param name="isAlignList">Is align method to take list?</param>
        /// <param name="parserParam">Parser Parameter</param>
        /// <param name="addParam">Additional parameter</param>
        /// Suppress the ParserParam variable CA1801 as this would be reused later.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "parserParam"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateMUMmerAlignGeneralTestCases(string nodeName, bool isFilePath,
            bool isAlignList, ParserParameters parserParam,
            AdditionalParameters addParam)
        {
            ISequence referenceSeq = null;
            ISequence querySeq = null;
            IList<ISequence> querySeqs = new List<ISequence>();
            string referenceSequence = string.Empty;
            string querySequence = string.Empty;
            List<ISequence> alignList = null;
            IEnumerable<ISequence> referenceSeqs = null;

            if (isFilePath)
            {
                // Gets the reference sequence from the configurtion file
                string filePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.FilePathNode);

                Assert.IsNotNull(filePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer P1 : Successfully validated the File Path '{0}'.",
                    filePath));

                FastAParser fastaParserObj = new FastAParser(filePath);
                referenceSeqs = fastaParserObj.Parse();

                referenceSeq = referenceSeqs.ElementAt(0);
                referenceSequence = referenceSeq.ToString();

                // Gets the reference sequence from the configurtion file
                string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceFilePathNode);

                Assert.IsNotNull(queryFilePath);
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "MUMmer P1 : Successfully validated the Search File Path '{0}'.",
                    queryFilePath));


                FastAParser queryParserObj = new FastAParser(queryFilePath);
                IEnumerable<ISequence> queryEnumSeqs = queryParserObj.Parse();
                foreach (ISequence seq in queryEnumSeqs)
                {
                    querySeqs.Add(seq);
                }

                querySeq = querySeqs[0];
                querySequence = new string(querySeq.Select(a => (char)a).ToArray());

                if (isAlignList)
                {
                    alignList = new List<ISequence>();
                    alignList.Add(referenceSeq);
                    alignList.Add(querySeq);
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
                    referenceSequence);

                querySequence = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceNode);

                referenceSeqAlphabet = utilityObj.xmlUtil.GetTextValue(nodeName,
                    Constants.SearchSequenceAlphabetNode);

                querySeq = new Sequence(Utility.GetAlphabet(referenceSeqAlphabet),
                    querySequence);
                querySeqs = new List<ISequence>();

                if (isAlignList)
                {
                    alignList = new List<ISequence>();
                    alignList.Add(referenceSeq);
                    alignList.Add(querySeq);
                }
                else
                    querySeqs.Add(querySeq);
            }

            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMAlignLengthNode);

            MUMmerAligner mumAlignObj = new MUMmerAligner();
            mumAlignObj.LengthOfMUM = long.Parse(mumLength, null);
            mumAlignObj.StoreMUMs = true;

            switch (addParam)
            {
                case AdditionalParameters.PerformSimilarityMatrixChange:
                    mumAlignObj.SimilarityMatrix =
                        new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
                    mumAlignObj.GapOpenCost =
                        int.Parse(utilityObj.xmlUtil.GetTextValue(nodeName, Constants.GapOpenCostNode),
                        (IFormatProvider)null);
                    break;
                default:
                    mumAlignObj.GapOpenCost =
                        int.Parse(utilityObj.xmlUtil.GetTextValue(nodeName, Constants.GapOpenCostNode),
                        (IFormatProvider)null);
                    break;
            }

            IList<IPairwiseSequenceAlignment> align = null;

            IEnumerable<ISequence> alignEnumSeqs = alignList;

            if (isAlignList)
                align = mumAlignObj.AlignSimple(alignEnumSeqs);
            else
                align = mumAlignObj.AlignSimple(referenceSeq, querySeqs);

            // Validate MUMs Properties
            Assert.IsNotNull(mumAlignObj.MUMs);

            string expectedScore = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.ScoreNodeName);

            string[] expectedSequences =
                utilityObj.xmlUtil.GetTextValues(nodeName, Constants.ExpectedSequencesNode);
            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();

            // Validate for two aligned sequences and single aligned sequences appropriately
            if (1 >= querySeqs.Count)
            {
                IPairwiseSequenceAlignment seqAlign = new PairwiseSequenceAlignment();
                PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
                alignedSeq.FirstSequence = new Sequence(referenceSeq.Alphabet, expectedSequences[0]);
                alignedSeq.SecondSequence = new Sequence(referenceSeq.Alphabet, expectedSequences[1]);
                alignedSeq.Score = Convert.ToInt32(expectedScore, (IFormatProvider)null);
                seqAlign.PairwiseAlignedSequences.Add(alignedSeq);
                expectedOutput.Add(seqAlign);
                Assert.IsTrue(CompareAlignment(align, expectedOutput));
            }
            else
            {
                string[] expectedScores = expectedScore.Split(',');
                IPairwiseSequenceAlignment seq1Align = new PairwiseSequenceAlignment();
                IPairwiseSequenceAlignment seq2Align = new PairwiseSequenceAlignment();

                // Get the first sequence for validation
                PairwiseAlignedSequence alignedSeq1 = new PairwiseAlignedSequence();
                alignedSeq1.FirstSequence = new Sequence(referenceSeq.Alphabet, expectedSequences[0]);
                alignedSeq1.SecondSequence = new Sequence(referenceSeq.Alphabet, expectedSequences[1]);
                alignedSeq1.Score = int.Parse(expectedScores[0], (IFormatProvider)null);
                seq1Align.PairwiseAlignedSequences.Add(alignedSeq1);
                expectedOutput.Add(seq1Align);

                // Get the second sequence for validation
                PairwiseAlignedSequence alignedSeq2 = new PairwiseAlignedSequence();
                alignedSeq2.FirstSequence = new Sequence(referenceSeq.Alphabet, expectedSequences[2]);
                alignedSeq2.SecondSequence = new Sequence(referenceSeq.Alphabet, expectedSequences[3]);
                alignedSeq2.Score = int.Parse(expectedScores[1], (IFormatProvider)null);
                seq2Align.PairwiseAlignedSequences.Add(alignedSeq2);
                expectedOutput.Add(seq2Align);
                Assert.IsTrue(CompareAlignment(align, expectedOutput));
            }
            Console.WriteLine("MUMmer P1 : Successfully validated the aligned sequences.");
            ApplicationLog.WriteLine("MUMmer P1 : Successfully validated the aligned sequences.");
        }

        /// <summary>
        /// Compare the alignment of mummer and defined alignment
        /// </summary>
        /// <param name="actualAlignment">actual alignment</param>
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
                    if (actualAlignment[resultCount].PairwiseAlignedSequences.Count ==
                        expectedAlignment[resultCount].PairwiseAlignedSequences.Count)
                    {
                        for (int alignSeqCount = 0; alignSeqCount <
                            actualAlignment[resultCount].PairwiseAlignedSequences.Count; alignSeqCount++)
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

        /// <summary>
        /// Validates the Unique Matches for the input provided.
        /// </summary>
        /// <param name="matches">Max Unique Match list</param>
        /// <param name="nodeName">Node name to be read from xml</param>
        /// <returns>True, if successfully validated</returns>
        bool ValidateUniqueMatches(IEnumerable<Match> matches,
            string nodeName)
        {
            // Gets all the unique matches properties to be validated as in xml.
            string[] firstSeqStart = null;
            string[] length = null;
            string[] secondSeqStart = null;

            firstSeqStart = utilityObj.xmlUtil.GetTextValue(nodeName,
                Constants.FirstSequenceStartNode).Split(',');
            length = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LengthNode).Split(',');
            secondSeqStart = utilityObj.xmlUtil.GetTextValue(nodeName,
                Constants.SecondSequenceStartNode).Split(',');

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
                        "MUMmer P1 : Unique match not matching at index '{0}'", i.ToString((IFormatProvider)null)));
                    ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                        "MUMmer P1 : Unique match not matching at index '{0}'", i.ToString((IFormatProvider)null)));
                    return false;
                }
                i++;
            }

            return true;
        }

        /// <summary>
        /// Validate the Mummer GetMatchesUniqueInReference method for different test cases.
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
                    "MUMmer P1 : Successfully validated the File Path '{0}'.", filePath));

                using (FastAParser parser = new FastAParser(filePath))
                {
                    referenceSeqs = parser.Parse();
                    referenceSeq = referenceSeqs.ElementAt(0);
                    referenceSequence = new string(referenceSeq.Select(a => (char)a).ToArray()); ;

                    // Gets the reference sequence from the configurtion file
                    string queryFilePath = utilityObj.xmlUtil.GetTextValue(nodeName,
                        Constants.SearchSequenceFilePathNode);

                    Assert.IsNotNull(queryFilePath);
                    ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                        "MUMmer P1 : Successfully validated the Search File Path '{0}'.", queryFilePath));

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
            }

            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);

            Bio.Algorithms.MUMmer.MUMmer mum = new Bio.Algorithms.MUMmer.MUMmer(referenceSeq as Sequence);
            mum.LengthOfMUM = long.Parse(mumLength, null);
            IEnumerable<Match> actualResult = null;

            actualResult = mum.GetMatchesUniqueInReference(querySeq);

            // Validate MUMs output.
            Assert.IsTrue(ValidateMums(nodeName, actualResult));

            Console.WriteLine("MUMmer P1 : Successfully validated the Mumms");
            ApplicationLog.WriteLine("MUMmer P1 : Successfully validated the Mumms.");
        }

        /// <summary>
        /// Validate the Mums output.
        /// </summary>
        /// <param name="result">Mumms Output</param>
        /// <param name="nodeName">Node name to be read from xml</param>
        bool ValidateMums(string nodeName, IEnumerable<Match> result)
        {
            string[] firstSeqStart =
                utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LisFirstSequenceStartNode).Split(',');
            string[] length =
                utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LisLengthNode).Split(',');
            string[] secondSeqStart =
                utilityObj.xmlUtil.GetTextValue(nodeName, Constants.LisSecondSequenceStartNode).Split(',');

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
        /// Validates GetMatches() with different inputs.
        /// </summary>
        /// <param name="nodeName">Parent Node from Xml.</param>
        void ValidateGetMatch(string nodeName)
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
            Sequence refSequence = new Sequence(Utility.GetAlphabet(seqAlp), referenceSequence);
            Bio.Algorithms.MUMmer.MUMmer mum = new Bio.Algorithms.MUMmer.MUMmer(refSequence);
            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);
            mum.LengthOfMUM = long.Parse(mumLength, null);
            IEnumerable<Match> matches = null;
            Sequence qrySequence = new Sequence(Utility.GetAlphabet(seqAlp), querySequence);
            matches = mum.GetMatches(qrySequence);

            // Validates the Unique Matches.
            ApplicationLog.WriteLine("MUMmer BVT : Validating the Unique Matches");
            Assert.IsTrue(ValidateUniqueMatches(matches, nodeName));

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
        }

        /// <summary>
        /// Validates Constructor of Mummer Class with a Suffix tree as parameter.
        /// </summary>
        /// <param name="nodeName">Parent Node from Xml.</param>
        void ValidateConstructorWithSuffixTree(string nodeName)
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

            Sequence refSequence = new Sequence(Utility.GetAlphabet(seqAlp), referenceSequence);

            MultiWaySuffixTree suffixTree = new MultiWaySuffixTree(refSequence);
            Bio.Algorithms.MUMmer.MUMmer mum = new Bio.Algorithms.MUMmer.MUMmer(suffixTree);
            string mumLength = utilityObj.xmlUtil.GetTextValue(nodeName, Constants.MUMLengthNode);
            mum.LengthOfMUM = long.Parse(mumLength, null);
            IEnumerable<Match> matches = null;
            Sequence sequence = new Sequence(Utility.GetAlphabet(seqAlp), querySequence);
            matches = mum.GetMatches(sequence);
            // Validates the Unique Matches.
            ApplicationLog.WriteLine(@"MUMmer BVT : Validating the Unique Matches for 
                                            implementation of customised MUMer Constructor");
            Assert.IsTrue(ValidateUniqueMatches(matches, nodeName));

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "MUMmer BVT : Successfully validated the all the unique matches for the sequence '{0}' and '{1}'.",
                referenceSequence, querySequence));
        }

        #endregion Supported Methods
    }
}
