﻿/****************************************************************************
 * QualitativeSequenceP1TestCases.cs
 * 
 * This file contains the Qualitative Sequence P1 test cases.
 * 
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Bio.IO.FastQ;
using Bio.TestAutomation.Util;
using Bio.Util.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bio;

#if (SILVERLIGHT == false)
    namespace Bio.TestAutomation
#else
    namespace Bio.Silverlight.TestAutomation
#endif
{
    /// <summary>
    /// Test Automation code for Bio Qualitative sequence validations.
    /// </summary>
    [TestClass]
    public class QualitativeSequenceP1TestCases
    {
        #region Enums

        /// <summary>
        /// Qualitative Sequence method Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum QualitativeSequenceParameters
        {
            SequenceByte,
            Sequence,
            Score,
            ByteArray,
            IndexOf,
            IndexOfNonGap,
            IndexOfNonGapWithParam,
            LastIndexOf,
            LastIndexOfWithPam,
            DefaultScoreWithAlphabets,
            DefaultScoreWithSequence,
            MaxDefaultScore,
            MinDefaultScore,
            GetEnumerator,
            GetObjectData,
            Default
        };

        #endregion Enums

        #region Global Variables

        Utility utilityObj = new Utility(@"TestUtils\QualitativeTestsConfig.xml");        

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static QualitativeSequenceP1TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("bio.automation.log");
            }
        }

        #endregion Constructor

        #region Qualitative P1 TestCases

        /// <summary>
        /// Validate creation of Qualitative Sequence for Rna Sequence
        /// with Sanger FastQFormat and specified score.
        /// Input Data : Rna Alphabet,Rna Sequence,"Sanger" FastQFormat.
        /// and Score "120" 
        /// Output Data : Validation of Created Qualitative sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSangerFormatTypeRnaQualitativeSequenceWithScore()
        {
            GeneralQualitativeSequence(Constants.SimpleRnaSangerNode,
                QualitativeSequenceParameters.Score);
        }

        /// <summary>
        /// Validate creation of Qualitative Sequence for Rna Sequence
        /// with Solexa FastQFormat and specified score.
        /// Input Data : Rna Alphabet,Rna Sequence,"Solexa" FastQFormat.
        /// and Score "104" 
        /// Output Data : Validation of Created Qualitative sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSolexaFormatTypeRnaQualitativeSequenceWithScore()
        {
            GeneralQualitativeSequence(Constants.SimpleRnaSolexaNode,
                QualitativeSequenceParameters.Score);
        }

        /// <summary>
        /// Validate creation of Qualitative Sequence for Rna Sequence
        /// with Illumina FastQFormat and specified score.
        /// Input Data : Rna Alphabet,Rna Sequence,"Illumina" FastQFormat.
        /// and Score "104" 
        /// Output Data : Validation of Created Qualitative sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIlluminaFormatTypeRnaQualitativeSequenceWithScore()
        {
            GeneralQualitativeSequence(Constants.SimpleRnaIlluminaNode,
                QualitativeSequenceParameters.Score);
        }

        /// <summary>
        /// Validate creation of Qualitative Sequence for Protein Sequence
        /// with Sanger FastQFormat and specified score.
        /// Input Data : Protein Alphabet,Protein Sequence,"Sanger" FastQFormat.
        /// and Score "120" 
        /// Output Data : Validation of Created Qualitative sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSangerFormatTypeProteinQualitativeSequenceWithScore()
        {
            GeneralQualitativeSequence(Constants.SimpleProteinSangerNode,
                QualitativeSequenceParameters.Score);
        }

        /// <summary>
        /// Validate creation of Qualitative Sequence for Protein Sequence
        /// with Solexa FastQFormat and specified score.
        /// Input Data : Protein Alphabet,Protein Sequence,"Solexa" FastQFormat.
        /// and Score "104" 
        /// Output Data : Validation of Created Qualitative sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSolexaFormatTypeProteinQualitativeSequenceWithScore()
        {
            GeneralQualitativeSequence(Constants.SimpleProteinSolexaNode,
                QualitativeSequenceParameters.Score);
        }

        /// <summary>
        /// Validate creation of Qualitative Sequence for Protein Sequence
        /// with Illumina FastQFormat and specified score.
        /// Input Data : Protein Alphabet,Protein Sequence,"Illumina" FastQFormat.
        /// and Score "104" 
        /// Output Data : Validation of Created Qualitative sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIlluminaFormatTypeProteinQualitativeSequenceWithScore()
        {
            GeneralQualitativeSequence(Constants.SimpleProteinIlluminaNode,
                QualitativeSequenceParameters.Score);
        }

        /// <summary>
        /// Validate creation of Qualitative Sequence for Dna Sequence
        /// with Illumina FastQFormat and Byte array.
        /// Input Data : Dna Alphabet,Dna Sequence,"Illumina" FastQFormat.
        /// Output Data : Validation of Created Qualitative sequence with score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateIlluminaFormatTypeDnaQualitativeSequenceWithByteArray()
        {
            GeneralQualitativeSequence(Constants.SimpleDNAIlluminaByteArrayNode,
                QualitativeSequenceParameters.ByteArray);
        }

        /// <summary>
        /// Validate IndexOf Qualitative Sequence Items.
        /// Input Data : Dna Sequence and score.
        /// Output Data : Validate qualitative sequence item indices.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateQualitativeSeqItemIndexes()
        {
            ValidateGeneralQualitativeSeqItemIndices(
                Constants.SimpleDnaSangerNode, QualitativeSequenceParameters.IndexOf);
        }

        /// <summary>
        /// Validate IndexOf Non Gap characters present in Qualitative Sequence Items.
        /// Input Data : Dna Sequence and score.
        /// Output Data : Validate IndexOf Non Gap characters present in Qualitative Sequence Items.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateQualitativeSeqItemIndexOfNonGapChars()
        {
            ValidateGeneralQualitativeSeqItemIndices(
                Constants.SimpleDnaSangerNode, QualitativeSequenceParameters.IndexOfNonGap);
        }

        /// <summary>
        /// Validate IndexOf Non Gap characters present in Qualitative Sequence
        /// Items by passing Sequence Item position to IndexOfNonGap() method.
        /// Input Data : Dna Sequence and score.
        /// Output Data : Validate IndexOf Non Gap characters present in Qualitative Sequence Items.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateQualitativeSeqItemIndexOfNonGapCharsUsingPam()
        {
            ValidateGeneralQualitativeSeqItemIndices(
                Constants.SimpleDnaSangerNode, QualitativeSequenceParameters.IndexOfNonGapWithParam);
        }

        /// <summary>
        /// Validate Last IndexOf Non Gap characters present in Qualitative Sequence Items.
        /// Input Data : Dna Sequence and score.
        /// Output Data : Validate Last IndexOf Non Gap characters present in Qualitative Sequence Items.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateQualitativeSeqItemLastIndexOfNonGapChars()
        {
            ValidateGeneralQualitativeSeqItemIndices(
                Constants.SimpleDnaSangerNode, QualitativeSequenceParameters.LastIndexOf);
        }

        /// <summary>
        /// Validate LastIndexOf Non Gap characters present in Qualitative Sequence
        /// Items by passing Sequence Item position to LastIndexOfNonGap() method.
        /// Input Data : Dna Sequence and score.
        /// Output Data : Validate IndexOf Non Gap characters present in Qualitative Sequence Items.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateQualitativeSeqItemLastIndexOfNonGapCharsUsingPam()
        {
            ValidateGeneralQualitativeSeqItemIndices(
                Constants.SimpleDnaSangerNode, QualitativeSequenceParameters.LastIndexOfWithPam);
        }

        /// <summary>
        /// Validate default score for Dna solexa FastQ sequence.
        /// Input Data :Dna Alphabet,Solexa FastQ format.
        /// Output Data : Validate FastQ Sanger format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForDnaSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaSolexaNode, QualitativeSequenceParameters.DefaultScoreWithAlphabets);
        }

        /// <summary>
        /// Validate FastQ Sanger format type default score.
        /// Input Data :Dna Alphabet, Sanger FastQ format.
        /// Output Data : Validate FastQ Sanger format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForDnaSanger()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaSangerNode, QualitativeSequenceParameters.DefaultScoreWithAlphabets);
        }

        /// <summary>
        /// Validate FastQ Sanger format type default score.
        /// Input Data :Protein Alphabet, Sanger FastQ format.
        /// Output Data : Validate FastQ Sanger format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForProteinSanger()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinSangerNode, QualitativeSequenceParameters.DefaultScoreWithAlphabets);
        }

        /// <summary>
        /// Validate FastQ Illumina format type default score.
        /// Input Data :Dna Alphabet, Illumina FastQ format.
        /// Output Data : Validate FastQ Illumina format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForDnaIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaIlluminaNode, QualitativeSequenceParameters.DefaultScoreWithAlphabets);
        }

        /// <summary>
        /// Validate FastQ Illumina format type default score.
        /// Input Data :Rna Alphabet, Illumina FastQ format.
        /// Output Data : Validate FastQ Illumina format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForRnaIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleRnaIlluminaNode, QualitativeSequenceParameters.DefaultScoreWithAlphabets);
        }

        /// <summary>
        /// Validate FastQ Illumina format type default score.
        /// Input Data :Protein Alphabet, Illumina FastQ format.
        /// Output Data : Validate FastQ Illumina format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForProteinIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinIlluminaNode, QualitativeSequenceParameters.DefaultScoreWithAlphabets);
        }

        /// <summary>
        /// Validate FastQ Solexa format type default score.
        /// Input Data :Protein Sequence, Solexa FastQ format.
        /// Output Data : Validate FastQ Solexa format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForProteinSequenceSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinSolexaNode, QualitativeSequenceParameters.DefaultScoreWithSequence);
        }

        /// <summary>
        /// Validate FastQ Solexa format type default score.
        /// Input Data :Dna Sequence, Solexa FastQ format.
        /// Output Data : Validate FastQ Solexa format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForDnaSequenceSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaSolexaNode, QualitativeSequenceParameters.DefaultScoreWithSequence);
        }

        /// <summary>
        /// Validate FastQ Solexa format type default score.
        /// Input Data :Rna Sequence, Solexa FastQ format.
        /// Output Data : Validate FastQ Solexa format type default score.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDefaultQualScoreForRnaSequenceSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleRnaSolexaNode, QualitativeSequenceParameters.DefaultScoreWithSequence);
        }

        /// <summary>
        /// Validate Maximum score for Dna Sanger FastQ.
        /// Input Data :Dna Sequence, Sanger FastQ format.
        /// Output Data : Validate Maximum score for Dna Sanger FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForDnaSanger()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaSangerNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Maximum score for Rna Sanger FastQ.
        /// Input Data :Rna Sequence, Sanger FastQ format.
        /// Output Data : Validate Maximum score for Rna Sanger FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForRnaSanger()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleRnaSangerNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Maximum score for Protein Sanger FastQ.
        /// Input Data :Protein Sequence,Sanger FastQ format.
        /// Output Data : Validate Maximum score for Protein Sanger FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForProteinSanger()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinSangerNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Maximum score for Dna Illumina FastQ.
        /// Input Data :Dna Sequence,Illumina FastQ format.
        /// Output Data : Validate Maximum score for Dna Illumina FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForDnaIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaIlluminaNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Maximum score for Rna Illumina FastQ.
        /// Input Data :Rna Sequence,Illumina FastQ format.
        /// Output Data : Validate Maximum score for Rna Illumina FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForRnaIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleRnaIlluminaNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Maximum score for Protein Illumina FastQ.
        /// Input Data :Protein Sequence,Illumina FastQ format.
        /// Output Data : Validate Maximum score for Protein Illumina FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForProteinIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinIlluminaNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Maximum score for Dna Solexa FastQ.
        /// Input Data :Dna Sequence,Solexa FastQ format.
        /// Output Data : Validate Maximum score for Dna Solexa FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForDnaSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaSolexaNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Maximum score for Rna Solexa FastQ.
        /// Input Data :Rna Sequence,Solexa FastQ format.
        /// Output Data : Validate Maximum score for Rna Solexa FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForRnaSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleRnaSolexaNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Maximum score for Protein Solexa FastQ.
        /// Input Data :Protein Sequence,Solexa FastQ format.
        /// Output Data : Validate Maximum score for Protein Solexa FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMaxQualScoreForProteinSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinSolexaNode, QualitativeSequenceParameters.MaxDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Dna Sanger FastQ.
        /// Input Data :Dna Sequence, Sanger FastQ format.
        /// Output Data : Validate Minimum score for Dna Sanger FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForDnaSanger()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaSangerNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Rna Sanger FastQ.
        /// Input Data :Rna Sequence, Sanger FastQ format.
        /// Output Data : Validate Minimum score for Rna Sanger FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForRnaSanger()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleRnaSangerNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Protein Sanger FastQ.
        /// Input Data :Protein Sequence,Sanger FastQ format.
        /// Output Data : Validate Minimum score for Protein Sanger FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForProteinSanger()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinSangerNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Dna Illumina FastQ.
        /// Input Data :Dna Sequence,Illumina FastQ format.
        /// Output Data : Validate Minimum score for Dna Illumina FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForDnaIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaIlluminaNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Rna Illumina FastQ.
        /// Input Data :Rna Sequence,Illumina FastQ format.
        /// Output Data : Validate Minimum score for Rna Illumina FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForRnaIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleRnaIlluminaNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Protein Illumina FastQ.
        /// Input Data :Protein Sequence,Illumina FastQ format.
        /// Output Data : Validate Minimum score for Protein Illumina FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForProteinIllumina()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinIlluminaNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Dna Solexa FastQ.
        /// Input Data :Dna Sequence,Solexa FastQ format.
        /// Output Data : Validate Minimum score for Dna Solexa FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForDnaSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleDnaSolexaNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Rna Solexa FastQ.
        /// Input Data :Rna Sequence,Solexa FastQ format.
        /// Output Data : Validate Minimum score for Rna Solexa FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForRnaSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleRnaSolexaNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        /// <summary>
        /// Validate Minimum score for Protein Solexa FastQ.
        /// Input Data :Protein Sequence,Solexa FastQ format.
        /// Output Data : Validate Minimum score for Protein Solexa FastQ.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMinimumQualScoreForProteinSolexa()
        {
            ValidateFastQDefaultScores(
                Constants.SimpleProteinSolexaNode, QualitativeSequenceParameters.MinDefaultScore);
        }

        #endregion Qualitative P1 TestCases

        #region Supporting Methods

        /// <summary>
        /// General method to validate creation of Qualitative sequence.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="parameters">Different Qualitative Sequence parameters.</param>
        /// </summary>
        void GeneralQualitativeSequence(
            string nodeName, QualitativeSequenceParameters parameters)
        {
            // Gets the actual sequence and the alphabet from the Xml
            IAlphabet alphabet = Utility.GetAlphabet(utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.AlphabetNameNode));
            FastQFormatType expectedFormatType = Utility.GetFastQFormatType(
                utilityObj.xmlUtil.GetTextValue(nodeName, Constants.FastQFormatType));
            QualitativeSequence createdQualitativeSequence = null;
            string inputSequence = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.inputSequenceNode);
            string expectedSequence = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.ExpectedSequenceNode);
            string expectedSequenceCount = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.QSequenceCount);
            string inputScoreforIUPAC = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.MaxScoreNode);
            string expectedOuptutScore = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.InputScoreNode);
            string inputQuality = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.InputByteArrayNode);
            byte[] byteArray = UTF8Encoding.UTF8.GetBytes(inputQuality);
            int index = 0;

            // Create and validate Qualitative Sequence.
            switch (parameters)
            {
                case QualitativeSequenceParameters.Score:
                    createdQualitativeSequence = new QualitativeSequence(alphabet, expectedFormatType,
                        inputSequence, ((char)QualitativeSequence.GetDefaultQualScore(expectedFormatType)).ToString());
                    // Validate score
                    foreach (byte qualScore in createdQualitativeSequence.QualityScores)
                    {
                        Assert.AreEqual(qualScore, Convert.ToInt32(inputScoreforIUPAC, (IFormatProvider)null));
                    }
                    break;
                case QualitativeSequenceParameters.ByteArray:
                    createdQualitativeSequence = new QualitativeSequence(alphabet, expectedFormatType,
                       UTF8Encoding.UTF8.GetBytes(inputSequence), new byte[] { byte.Parse(expectedOuptutScore, (IFormatProvider)null) });

                    // Validate score
                    foreach (byte qualScore in createdQualitativeSequence.QualityScores)
                    {
                        Assert.AreEqual(qualScore, Convert.ToInt32(byteArray[index], (IFormatProvider)null));
                        index++;
                    }
                    break;
                default:
                    break;
            }

            // Validate createdSequence qualitative sequence.
            Assert.IsNotNull(createdQualitativeSequence);
            Assert.AreEqual(createdQualitativeSequence.Alphabet, alphabet);
            Assert.AreEqual(new string(createdQualitativeSequence.Select(a => (char)a).ToArray()), expectedSequence);
            Assert.AreEqual(createdQualitativeSequence.Count.ToString((IFormatProvider)null), expectedSequenceCount);
            Assert.AreEqual(createdQualitativeSequence.FormatType, expectedFormatType);

            // Logs to the VSTest GUI (Console.Out) window
            Console.WriteLine(string.Format((IFormatProvider)null,
                "Qualitative Sequence P1:Qualitative Sequence {0} is as expected.",
                createdQualitativeSequence.ToString()));

            Console.WriteLine(string.Format((IFormatProvider)null,
                "Qualitative Sequence P1:Qualitative Sequence Score {0} is as expected.",
                createdQualitativeSequence.QualityScores.ToString()));

            Console.WriteLine(string.Format((IFormatProvider)null,
                "Qualitative Sequence P1:Qualitative format type {0} is as expected.",
                createdQualitativeSequence.FormatType));
        }

        /// <summary>
        /// General method to validate Index of Qualitative Sequence Items.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="indexParam">Different Qualitative Sequence parameters.</param>
        /// </summary>
        void ValidateGeneralQualitativeSeqItemIndices(
            string nodeName, QualitativeSequenceParameters indexParam)
        {
            // Gets the actual sequence and the alphabet from the Xml
            IAlphabet alphabet = Utility.GetAlphabet(utilityObj.xmlUtil.GetTextValue(
               nodeName, Constants.AlphabetNameNode));
            FastQFormatType expectedFormatType = Utility.GetFastQFormatType(
                utilityObj.xmlUtil.GetTextValue(nodeName, Constants.FastQFormatType));
            QualitativeSequence createdQualitativeSequence = null;
            string inputSequence = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.inputSequenceNode);
            string expectedFirstItemIdex = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.FirstItemIndex);
            string expectedLastItemIdex = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.LastItemIndex);
            string expectedGapIndex = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.IndexOfGap);
            long lastItemIndex;
            long index;

            // Create a qualitative Sequence.
            createdQualitativeSequence = new QualitativeSequence(
                alphabet, expectedFormatType, inputSequence,
                ((char)QualitativeSequence.GetDefaultQualScore(expectedFormatType)).ToString());

            // Get a Index of qualitative sequence items
            switch (indexParam)
            {
                case QualitativeSequenceParameters.IndexOfNonGap:
                    index = createdQualitativeSequence.IndexOfNonGap();

                    // Validate Qualitative sequence item indices.
                    Assert.AreEqual(index, Convert.ToInt32(expectedFirstItemIdex, (IFormatProvider)null));
                    break;
                case QualitativeSequenceParameters.IndexOfNonGapWithParam:
                    index = createdQualitativeSequence.IndexOfNonGap(5);

                    // Validate Qualitative sequence item indices.
                    Assert.AreEqual(index, Convert.ToInt32(expectedGapIndex, (IFormatProvider)null));
                    break;
                case QualitativeSequenceParameters.LastIndexOf:
                    lastItemIndex = createdQualitativeSequence.LastIndexOfNonGap();

                    // Validate Qualitative sequence item indices.
                    Assert.AreEqual(lastItemIndex, Convert.ToInt32(expectedLastItemIdex, (IFormatProvider)null));
                    break;
                case QualitativeSequenceParameters.LastIndexOfWithPam:
                    lastItemIndex = createdQualitativeSequence.LastIndexOfNonGap(5);

                    // Validate Qualitative sequence item indices.
                    Assert.AreEqual(lastItemIndex, Convert.ToInt32(expectedGapIndex, (IFormatProvider)null));
                    break;
                default:
                    break;
            }

            // Logs to the VSTest GUI (Console.Out) window
            Console.WriteLine("Qualitative Sequence P1 : Qualitative SequenceItems indices validation completed successfully.");
        }

        /// <summary>
        /// General method to validate default score for different FastQ 
        /// format with different sequence.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="parameters">Different Qualitative Score method parameter.</param>
        /// </summary>
        void ValidateFastQDefaultScores(
            string nodeName, QualitativeSequenceParameters parameters)
        {
            // Gets the actual sequence and the alphabet from the Xml
            IAlphabet alphabet = Utility.GetAlphabet(utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.AlphabetNameNode));
            FastQFormatType expectedFormatType = Utility.GetFastQFormatType(
                utilityObj.xmlUtil.GetTextValue(nodeName, Constants.FastQFormatType));
            string inputSequence = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.inputSequenceNode);
            string expectedMaxScore = utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.DefualtMaxScore);
            string expectedMinScore = utilityObj.xmlUtil.GetTextValue(
                 nodeName, Constants.DefaultMinScore);

            QualitativeSequence createdQualitativeSequence = null;

            switch (parameters)
            {
                case QualitativeSequenceParameters.DefaultScoreWithAlphabets:
                    createdQualitativeSequence = new QualitativeSequence(
                        alphabet, expectedFormatType, inputSequence,
                        ((char)QualitativeSequence.GetDefaultQualScore(expectedFormatType)).ToString());

                    // Validate default score.
                    foreach (byte qualitativeScore in createdQualitativeSequence.QualityScores)
                    {
                        Assert.AreEqual(qualitativeScore,
                            QualitativeSequence.GetDefaultQualScore(expectedFormatType));
                    }

                    // Log VSTest GUI.
                    Console.WriteLine(string.Format((IFormatProvider)null,
                        "Qualitative Sequence P1:Qualitative Sequence Default score {0} is as expected.",
                        QualitativeSequence.GetDefaultQualScore(expectedFormatType)));
                    break;
                case QualitativeSequenceParameters.DefaultScoreWithSequence:
                    createdQualitativeSequence = new QualitativeSequence(alphabet,
                        expectedFormatType, inputSequence,
                        ((char)QualitativeSequence.GetDefaultQualScore(expectedFormatType)).ToString());

                    // Validate default score.
                    foreach (byte qualitativeScore in createdQualitativeSequence.QualityScores)
                    {
                        Assert.AreEqual(qualitativeScore,
                            QualitativeSequence.GetDefaultQualScore(expectedFormatType));
                    }

                    // Log VSTest GUI.
                    Console.WriteLine(string.Format((IFormatProvider)null,
                        "Qualitative Sequence P1:Qualitative Sequence Default score {0} is as expected.",
                        QualitativeSequence.GetDefaultQualScore(expectedFormatType)));
                    break;
                case QualitativeSequenceParameters.MaxDefaultScore:
                    createdQualitativeSequence = new QualitativeSequence(
                        alphabet, expectedFormatType, UTF8Encoding.UTF8.GetBytes(inputSequence),                       
                        new byte[] { byte.Parse(expectedMaxScore, (IFormatProvider)null) });

                    // Validate default maximum score.
                    foreach (byte qualitativeScore in createdQualitativeSequence.QualityScores)
                    {
                        Assert.AreEqual(qualitativeScore,
                            QualitativeSequence.GetMaxQualScore(expectedFormatType));
                    }

                    // Log VSTest GUI.
                    Console.WriteLine(string.Format((IFormatProvider)null,
                        "Qualitative Sequence P1:Qualitative Sequence Maximum score {0} is as expected.",
                        QualitativeSequence.GetMaxQualScore(expectedFormatType)));
                    break;
                case QualitativeSequenceParameters.MinDefaultScore:
                    createdQualitativeSequence = new QualitativeSequence(
                        alphabet, expectedFormatType, UTF8Encoding.UTF8.GetBytes(inputSequence),
                        new byte[] { byte.Parse(expectedMinScore, (IFormatProvider)null) });

                    // Validate default minimum score.
                    foreach (byte qualitativeScore in createdQualitativeSequence.QualityScores)
                    {
                        Assert.AreEqual(qualitativeScore,
                            QualitativeSequence.GetMinQualScore(expectedFormatType));
                    }

                    // Log VSTest GUI.
                    Console.WriteLine(string.Format((IFormatProvider)null,
                        "Qualitative Sequence P1:Qualitative Sequence Minimum score {0} is as expected.",
                        QualitativeSequence.GetMinQualScore(expectedFormatType)));
                    break;
                default:
                    break;
            }
        }

        #endregion Supporting Methods
    }
}
