﻿/****************************************************************************
 * FastQP1TestCases.cs
 * 
 *This file contains FastQ Parsers and Formatters P1 test cases.
 * 
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Bio.IO.FastQ;
using Bio;
using System.Linq;
using Bio.IO;
using Bio.Util.Logging;
using Bio.TestAutomation.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if (SILVERLIGHT == false)
    namespace Bio.TestAutomation.IO.FastQ
#else
    namespace Bio.Silverlight.TestAutomation.IO.FastQ
#endif
{
    /// <summary>
    /// FASTQ P1 parser and formatter Test cases implementation.
    /// </summary>
    [TestClass]
    public class FastQP1TestCases
    {

        #region Enums

        /// <summary>
        /// FastQ Parser Property Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum FastQPropertyParameters
        {
            AlphabetProperty,
            Default
        };

        /// <summary>
        /// FastQ Formatter Parameters which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum FastQFileParameters
        {
            FileName,
            FileNameReadOnly,
            ParseFileName
        };

        #endregion Enums

        #region Global Variables

        Utility utilityObj = new Utility(@"TestUtils\FastQTestsConfig.xml");

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static FastQP1TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("bio.automation.log");
            }
        }

        #endregion Constructor

        #region FastQ P1 Test cases

        /// <summary>
        /// Parse a valid small size FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithSangerUsingFastQExtensionFile()
        {
            ValidateFastQParser(Constants.SimpleSangerFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Rna FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Rna FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithRnaSanger()
        {
            ValidateFastQParser(Constants.SimpleRnaSangerFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Rna FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Rna FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithRnaIllumina()
        {
            ValidateFastQParser(Constants.SimpleRnaIlluminaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Rna FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Rna FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithRnaSolexa()
        {
            ValidateFastQParser(Constants.SimpleRnaSolexaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Dna FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Dna FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithDnaSanger()
        {
            ValidateFastQParser(Constants.SimpleSangerFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Dna FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Rna FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithDnaIllumina()
        {
            ValidateFastQParser(Constants.SimpleIlluminaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Dna FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Rna FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithDnaSolexa()
        {
            ValidateFastQParser(Constants.SimpleSolexaFqFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Protein FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Protein FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithProteinSanger()
        {
            ValidateFastQParser(Constants.SimpleProteinSangerFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Protein FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Protein FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithProteinIllumina()
        {
            ValidateFastQParser(Constants.SimpleProteinIlluminaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid small size Protein FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Protein FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserWithProteinSolexa()
        {
            ValidateFastQParser(Constants.SimpleProteinSolexaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Medium size Sanger FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Medium size FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithMediumSizeSangerDnaSequence()
        {
            ValidateFastQParser(Constants.MediumSizeDnaSangerFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Medium size Illumina FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Medium size FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithMediumSizeIlluminaDnaSequence()
        {
            ValidateFastQParser(Constants.MediumSizeDnaIlluminaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Medium size Solexa FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Medium size FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithMediumSizeSolexaDnaSequence()
        {
            ValidateFastQParser(Constants.MediumSizeDnaSolexaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Large size Sanger FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Large size FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithLargeSizeSangerDnaSequence()
        {
            ValidateFastQParser(Constants.LargeSizeDnaSangerFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Large size Illumina FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Large size FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithLargeSizeIlluminaDnaSequence()
        {
            ValidateFastQParser(Constants.LargeSizeDnaIlluminaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Large size Solexa FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Large size FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithLargeSizeSolexaDnaSequence()
        {
            ValidateFastQParser(Constants.LargeSizeDnaSolexaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid One line sequence Illumina FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : One line Dna sequence Illumina FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithOneLineIluminaDnaSequence()
        {
            ValidateFastQParser(Constants.SingleSequenceIlluminaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid One line sequence Sanger FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : One line Dna sequence Sanger FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithOneLineSangerDnaSequence()
        {
            ValidateFastQParser(Constants.SingleSequenceSangerFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid One line sequence Solexa FastQ file and convert the same to 
        /// sequence using Parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : One line Dna sequence Solexa FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithOneLineSolexaDnaSequence()
        {
            ValidateFastQParser(Constants.SingleSequenceSolexaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Two line Medium size sequence Sanger FastQ file and
        /// convert the same to sequence using Parse(file-name) method and 
        /// validate with the expected sequence.
        /// Input : Two line medium size Dna sequence Sanger FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithTwoLineMediumSizeSangerDnaSequence()
        {
            ValidateFastQParser(Constants.TwoLineDnaSangerFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Two line Medium size sequence Illumina FastQ file and
        /// convert the same to sequence using Parse(file-name) method and 
        /// validate with the expected sequence.
        /// Input : Two line medium size Dna sequence Sanger FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithTwoLineMediumSizeIlluminaDnaSequence()
        {
            ValidateFastQParser(Constants.TwoLineDnaIlluminaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Two line Medium size sequence Solexa FastQ file and
        /// convert the same to sequence using Parse(file-name) method and 
        /// validate with the expected sequence.
        /// Input : Two line medium size Dna sequence Solexa FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQParserValidateParseWithTwoLineMediumSizeSolexaDnaSequence()
        {
            ValidateFastQParser(Constants.TwoLineDnaSolexaFastQNode,
              FastQPropertyParameters.Default);
        }

        /// <summary>
        /// Parse a valid Dna Rna multiple sequence FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaRnaMultipleSeqFastQParserWithSolexa()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqSolexaDnaRnaNode, null);
        }

        /// <summary>
        /// Parse a valid Rna Protein multiple sequence FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateRnaProteinMultipleSeqFastQParserWithSanger()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqSangerRnaProNode, null);
        }

        /// <summary>
        /// Parse a valid Rna Protein multiple sequence FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateRnaProteinMultipleSeqFastQParserWithIllumina()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqIlluminaRnaProNode, null);
        }

        /// <summary>
        /// Parse a valid Rna Protein multiple sequence FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateRnaProteinMultipleSeqFastQParserWithSolexa()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqSolexaRnaProNode, null);
        }

        /// <summary>
        /// Parse a valid Dna Protein multiple sequence FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaProteinMultipleSeqFastQParserWithSanger()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqSangerDnaProNode, null);
        }

        /// <summary>
        /// Parse a valid Dna Protein multiple sequence FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaProteinMultipleSeqFastQParserWithIllumina()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqIlluminaDnaProNode, null);
        }

        /// <summary>
        /// Parse a valid Dna Protein multiple sequence FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaProteinMultipleSeqFastQParserWithSolexa()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqSolexaDnaProNode, null);
        }

        /// <summary>
        /// Parse a valid Dna Rna Protein sequences FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Sanger format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaRnaProteinMultipleSeqFastQParserWithSanger()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqSangerDnaRnaProNode,
              "MultiSequenceFastQ");
        }

        /// <summary>
        /// Parse a valid Dna Rna Protein sequences FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Illumina format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaRnaProteinMultipleSeqFastQParserWithIllumina()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqIlluminaDnaRnaProNode,
              "MultiSequenceFastQ");
        }

        /// <summary>
        /// Parse a valid Dna Rna Protein sequences FastQ file and 
        /// convert the same to sequence using Parse(file-name) method 
        /// and validate with the expected sequence.
        /// Input : Multiple sequence FastQ file with Solexa format.
        /// Output : Validation of Expected sequence, Sequence Id,Sequence Type.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaRnaProteinMultipleSeqFastQParserWithSolexa()
        {
            ValidateMulitpleSequenceFastQParser(Constants.MultiSeqSolexaDnaRnaProNode,
              "MultiSequenceFastQ");
        }

        /// <summary>
        /// Parse a Sanger FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input : Sanger FastQ file
        /// Output : Validate format Sanger FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatSangerFastQ()
        {
            ValidateFastQFormatter(Constants.SimpleSangerFastQNode, true);
        }

        /// <summary>
        /// Parse a Sanger FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Dna Sanger FastQ file
        /// Output : Validate format Sanger FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatIlluminaFastQ()
        {
            ValidateFastQFormatter(Constants.SimpleIlluminaFastQNode, true);
        }

        /// <summary>
        /// Parse a Sanger FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input : Sanger FastQ file
        /// Output : Validate format Sanger FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithDnaSanger()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleSangerFastQNode);
        }

        /// <summary>
        /// Parse a Illumina FastQ file and Format a valid Illumina Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input :Dna Illumina FastQ file
        /// Output : Validate format Illumina FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithDnaIllumina()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleIlluminaFastQNode);
        }

        /// <summary>
        /// Parse a Solexa FastQ file and Format a valid Solexa Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input :Dna Solexa FastQ file
        /// Output : Validate format Solexa FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithDnaSolexa()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleSolexaFqFastQNode);
        }

        /// <summary>
        /// Parse a Sanger FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input : Rna Sanger FastQ file
        /// Output : Validate format Sanger FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithRnaSanger()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleRnaSangerFastQNode);
        }

        /// <summary>
        /// Parse a Illumina FastQ file and Format a valid Illumina Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input : Rna Illumina FastQ file
        /// Output : Validate format Illumina FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithRnaIllumina()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleRnaIlluminaFastQNode);
        }

        /// <summary>
        /// Parse a Solexa FastQ file and Format a valid Solexa Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input : Rna Solexa FastQ file
        /// Output : Validate format Solexa FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithRnaSolexa()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleRnaSolexaFastQNode);
        }

        /// <summary>
        /// Parse a Sanger FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input : Protein Sanger FastQ file
        /// Output : Validate format Sanger FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithProteinSanger()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleProteinSangerFastQNode);
        }

        /// <summary>
        /// Parse a Illumina FastQ file and Format a valid Illumina Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input : Protein Illumina FastQ file
        /// Output : Validate format Illumina FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithProteinIllumina()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleProteinIlluminaFastQNode);
        }

        /// <summary>
        /// Parse a Solexa FastQ file and Format a valid Solexa Qualitative Sequence 
        /// to FastQ file using Format(file-name) and validate Sequence.
        /// Input : Protein Solexa FastQ file
        /// Output : Validate format Solexa FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatFastQFileWithProteinSolexa()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(Constants.SimpleProteinSolexaFastQNode);
        }

        /// <summary>
        /// Parse a Dna Sanger FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Dna Sanger FastQ file
        /// Output : Validate format Sanger FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatDnaSangerFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleSangerFastQNode, true);
        }

        /// <summary>
        /// Parse a Dna Illumina FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Dna Illumina FastQ file
        /// Output : Validate format Illumina FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatDnaIlluminaFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleIlluminaFastQNode, true);
        }

        /// <summary>
        /// Parse a Dna Solexa FastQ file and Format a valid Solexa Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Dna Solexa FastQ file
        /// Output : Validate format Solexa FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatDnaSolexaFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleSolexaFqFastQNode, true);
        }

        /// <summary>
        /// Parse a Rna Sanger FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Rna Sanger FastQ file
        /// Output : Validate format Sanger FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatRnaSangerFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleRnaSangerFastQNode, true);
        }

        /// <summary>
        /// Parse a Rna Illumina FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Rna Illumina FastQ file
        /// Output : Validate format Illumina FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatRnaIlluminaFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleRnaIlluminaFastQNode, true);
        }

        /// <summary>
        /// Parse a Rna Solexa FastQ file and Format a valid Solexa Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Rna Solexa FastQ file
        /// Output : Validate format Solexa FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatRnaSolexaFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleRnaSolexaFastQNode, true);
        }

        /// <summary>
        /// Parse a Protein Sanger FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Protein Sanger FastQ file
        /// Output : Validate format Sanger FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatProteinSangerFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleProteinSangerFastQNode, true);
        }

        /// <summary>
        /// Parse a Protein Illumina FastQ file and Format a valid Sanger Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Protein Illumina FastQ file
        /// Output : Validate format Illumina FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatProteinIlluminaFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleProteinIlluminaFastQNode, true);
        }

        /// <summary>
        /// Parse a Protein Solexa FastQ file and Format a valid Solexa Qualitative Sequence 
        /// to FastQ file using Format(text-writer) and validate Sequence.
        /// Input :Protein Solexa FastQ file
        /// Output : Validate format Solexa FastQ file to temp file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatValidateFormatProteinSolexaFastQUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.SimpleProteinSolexaFastQNode, true);
        }

        /// <summary>
        /// Format a medium size Solexa Qualitative sequence to FastQ
        /// file.using format(Text-Writer) method and validate the same.
        /// Input Data : Solexa Medium size sequence.
        /// Output Data : Validation of fromatting medium size Solexa 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatMediumSizeDnaSolexaUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.MediumSizeDnaSolexaFastQNode, true);
        }

        /// <summary>
        /// Format a medium size Illumina Qualitative sequence to FastQ
        /// file.using format(Text-Writer) method and validate the same.
        /// Input Data : Illumina Medium size sequence.
        /// Output Data : Validation of fromatting medium size Illumina 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatMediumSizeDnaIlluminaUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.MediumSizeDnaIlluminaFastQNode, true);
        }

        /// <summary>
        /// Format a medium size Sanger Qualitative sequence to FastQ
        /// file.using format(Text-Writer) method and validate the same.
        /// Input Data : Sanger Medium size sequence.
        /// Output Data : Validation of fromatting medium size sanger 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatMediumSizeDnaSangerUsingTextWriter()
        {
            ValidateFastQFormatter(Constants.MediumSizeDnaSangerFastQNode, true);
        }

        /// <summary>
        /// Format a medium size Solexa Qualitative sequence to FastQ
        /// file.using format(file-name) method and validate the same.
        /// Input Data : Solexa Medium size sequence.
        /// Output Data : Validation of fromatting medium size Solexa 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatMediumSizeDnaSolexaUsingFile()
        {
            ValidateFastQFormatter(Constants.MediumSizeDnaSolexaFastQNode, false);
        }

        /// <summary>
        /// Format a medium size Illumina Qualitative sequence to FastQ
        /// file.using format(file-name) method and validate the same.
        /// Input Data : Illumina Medium size sequence.
        /// Output Data : Validation of fromatting medium size Illumina 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatMediumSizeDnaIlluminaUsingFile()
        {
            ValidateFastQFormatter(Constants.MediumSizeDnaIlluminaFastQNode, false);
        }

        /// <summary>
        /// Format a medium size Sanger Qualitative sequence to FastQ
        /// file.using format(file-name) method and validate the same.
        /// Input Data : Sanger Medium size sequence.
        /// Output Data : Validation of fromatting medium size sanger 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatMediumSizeDnaSangerUsingFile()
        {
            ValidateFastQFormatter(Constants.MediumSizeDnaSangerFastQNode, false);
        }

        /// <summary>
        /// Format a Large size(>100KB) Sanger Qualitative sequence to FastQ
        /// file.using Format() method and validate the same.
        /// Input Data : Sanger Large size sequence.
        /// Output Data : Validation of fromatting Large size Sanger 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatLargeSizeDnaSangerSeq()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(
              Constants.LargeSizeDnaSangerFastQNode);
        }

        /// <summary>
        /// Format a Large size(>100KB) Illumina Qualitative sequence to FastQ
        /// file.using Format() method and validate the same.
        /// Input Data : Illumina Large size sequence.
        /// Output Data : Validation of fromatting Large size Illumina 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatLargeSizeDnaIlluminaSeq()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(
              Constants.LargeSizeDnaIlluminaFastQNode);
        }

        /// <summary>
        /// Format a Large size(>100KB) Solexa Qualitative sequence to FastQ
        /// file.using Format() method and validate the same.
        /// Input Data : Solexa Large size sequence.
        /// Output Data : Validation of fromatting Large size Solexa 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatLargeSizeDnaSolexaSeq()
        {
            ValidateFastQFormatByFormattingQualSeqeunce(
              Constants.LargeSizeDnaSolexaFastQNode);
        }

        /// <summary>
        /// Parse and Format a Large size(>100KB) Sanger Qualitative sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Sanger Large size sequence.
        /// Output Data : Validation of fromatting Large size Sanger 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatLargeSizeDnaSangerFile()
        {
            ValidateFastQFormatter(Constants.LargeSizeDnaSangerFastQNode, false);
        }

        /// <summary>
        /// Parse and Format a Large size(>100KB) Illumina Qualitative sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Illumina Large size sequence.
        /// Output Data : Validation of fromatting Large size Illumina 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatLargeSizeDnaIlluminaFile()
        {
            ValidateFastQFormatter(Constants.LargeSizeDnaIlluminaFastQNode, false);
        }

        /// <summary>
        /// Parse and Format a Large size(>100KB) Solexa Qualitative sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Solexa Large size sequence.
        /// Output Data : Validation of fromatting Large size Solexa 
        /// qualitative sequence to valid FastQ file.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatLargeSizeDnaSolexaFile()
        {
            ValidateFastQFormatter(Constants.LargeSizeDnaSolexaFastQNode, false);
        }

        /// <summary>
        /// Parse and Format a Sanger Qualitative Dna Rna sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Sanger Dna Rna multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaRnaSangerFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqSangerDnaRnaNode);
        }

        /// <summary>
        /// Parse and Format a Illumina Qualitative Dna Rna sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Illumina Dna Rna multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaRnaIlluminaFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqIlluminaDnaRnaNode);
        }

        /// <summary>
        /// Parse and Format a Solexa Qualitative Dna Rna sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Solexa Dna Rna multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaRnaSolexaFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqSolexaDnaRnaNode);
        }

        /// <summary>
        /// Parse and Format a Sanger Qualitative Rna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Sanger Rna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatRnaProteinSangerFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqSangerRnaProNode);
        }

        /// <summary>
        /// Parse and Format a Illumina Qualitative Rna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Illumina Rna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatRnaProteinIlluminaFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqIlluminaRnaProNode);
        }

        /// <summary>
        /// Parse and Format a Solexa Qualitative Rna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Solexa Rna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatRnaProteinSolexaFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqSolexaRnaProNode);
        }

        /// <summary>
        /// Parse and Format a Sanger Qualitative Dna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Sanger Dna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaProteinSangerFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqSangerDnaProNode);
        }

        /// <summary>
        /// Parse and Format a Illumina Qualitative Dna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Illumina Dna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaProteinIlluminaFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqIlluminaDnaProNode);
        }

        /// <summary>
        /// Parse and Format a Solexa Qualitative Dna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Solexa Dna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaProteinSolexaFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqSolexaDnaProNode);
        }

        /// <summary>
        /// Parse and Format a Sanger Qualitative Dna Rna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Sanger Dna Rna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaRnaProteinSangerFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqSangerDnaRnaProNode);
        }

        /// <summary>
        /// Parse and Format a Illumina Qualitative Dna Rna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Illumina Dna Rna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaRnaProteinIlluminaFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqIlluminaDnaRnaProNode);
        }

        /// <summary>
        /// Parse and Format a Solexa Qualitative Dna Rna Protein sequence
        /// to FastQ file.using Format() method and validate the same.
        /// Input Data : Solexa Dna Rna Protein multi sequence file.
        /// Output Data : Validation of Multi sequence FastQ format.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void FastQFormatDnaRnaProteinSolexaFile()
        {
            ValidateMultiSeqFastQFormatter(Constants.MultiSeqSolexaDnaRnaProNode);
        }

        /// <summary>
        /// Parse a valid small size Dna FastQ file and convert the same to 
        /// sequence using parse(file-name) method and validate with the 
        /// expected sequence.
        /// Input : Dna FastQ file with Sanger format.
        /// Output : Validation of Expected sequence.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateFastQParserBasicSequenceWithDnaSanger()
        {
            ValidateBasicSequenceParser(
                Constants.SimpleSangerFastQNode,
                FastQFileParameters.ParseFileName);
        }

        #endregion FastQ P1 Test cases

        #region Supporting Methods

        /// <summary>
        /// General method to validate FastQ Parser.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="fastQProperty">FastQ Parser properties.</param>
        /// </summary>
        void ValidateFastQParser(string nodeName,
          FastQPropertyParameters fastQProperty)
        {
            // Gets the expected sequence from the Xml
            string filePath = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.FilePathNode);
            string expectedQualitativeSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequenceNode);
            string expectedSequenceId = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.SequenceIdNode);
            string expectedSeqCount = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.SeqsCount);
            IAlphabet alphabet = Utility.GetAlphabet(utilityObj.xmlUtil.GetTextValue(
               nodeName, Constants.AlphabetNameNodeV2));
            FastQParser fastQParserObj = new FastQParser(filePath);

            try
            {
                switch (fastQProperty)
                {
                    case FastQPropertyParameters.AlphabetProperty:
                        fastQParserObj.AutoDetectFastQFormat = true;
                        break;
                    default:
                        fastQParserObj.AutoDetectFastQFormat = true;
                        break;
                }

                IEnumerable<QualitativeSequence> qualSequenceList = null;
                qualSequenceList = fastQParserObj.Parse();

                // Validate qualitative Sequence upon parsing FastQ file.
                Assert.AreEqual(expectedSeqCount, qualSequenceList.Count().ToString((IFormatProvider)null));
                Assert.AreEqual(expectedQualitativeSequence, new string(qualSequenceList.ElementAt(0).Select(a => (char)a).ToArray()));
                Assert.AreEqual(expectedSequenceId, qualSequenceList.ElementAt(0).ID.ToString((IFormatProvider)null));
                Assert.AreEqual(alphabet, qualSequenceList.ElementAt(0).Alphabet);

                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence '{0}' validation after Parse() is found to be as expected.",
                  qualSequenceList.ElementAt(0)));

                // Logs to the VSTest GUI (Console.Out) window
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  new string(qualSequenceList.ElementAt(0).Select(a => (char)a).ToArray())));
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  qualSequenceList.ElementAt(0).ID.ToString((IFormatProvider)null)));
            }
            finally
            {
                fastQParserObj.Dispose();
            }
        }

        /// <summary>
        /// General method to validate FastQ Parser for Multiple sequence with 
        /// different alphabets.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="triSeq">Tri Sequence</param>
        /// </summary>
        void ValidateMulitpleSequenceFastQParser(string nodeName, string triSeq)
        {
            // Gets the expected sequence from the Xml
            string filePath = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.FilePathNode);
            string expectedFirstQualitativeSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequence1Node);
            string expectedSecondQualitativeSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequence2Node);
            string expectedthirdQualitativeSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequence3Node);
            string expectedSequenceId = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.SequenceIdNode);
            string expectedSeqCount = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.SeqsCount);

            // Parse a multiple sequence FastQ file.
            using (FastQParser fastQParserObj = new FastQParser(filePath))
            {
                fastQParserObj.AutoDetectFastQFormat = true;

                IEnumerable<QualitativeSequence> qualSequenceList = null;
                qualSequenceList = fastQParserObj.Parse();

                // Validate first qualitative Sequence upon parsing FastQ file.
                Assert.AreEqual(qualSequenceList.Count().ToString((IFormatProvider)null), expectedSeqCount);
                Assert.AreEqual(new string(qualSequenceList.ElementAt(0).Select(a => (char)a).ToArray()),
                  expectedFirstQualitativeSequence);
                Assert.AreEqual(qualSequenceList.ElementAt(0).ID.ToString((IFormatProvider)null), expectedSequenceId);

                // Validate second qualitative Sequence upon parsing FastQ file.
                Assert.AreEqual(qualSequenceList.Count().ToString((IFormatProvider)null), expectedSeqCount);
                Assert.AreEqual(new string(qualSequenceList.ElementAt(1).Select(a => (char)a).ToArray()),
                  expectedSecondQualitativeSequence);
                Assert.AreEqual(qualSequenceList.ElementAt(1).ID.ToString((IFormatProvider)null), expectedSequenceId);

                // Validate third sequence in FastQ file if it is tri sequence FastQ file.
                if (0 == string.Compare(triSeq, "MultiSequenceFastQ",
                    CultureInfo.CurrentCulture, CompareOptions.IgnoreCase))
                {
                    // Validate second qualitative Sequence upon parsing FastQ file.
                    Assert.AreEqual(qualSequenceList.Count().ToString((IFormatProvider)null), expectedSeqCount);
                    Assert.AreEqual(new string(qualSequenceList.ElementAt(2).Select(a => (char)a).ToArray()),
                      expectedthirdQualitativeSequence);
                    Assert.AreEqual(qualSequenceList.ElementAt(2).ID.ToString((IFormatProvider)null), expectedSequenceId);

                    Console.WriteLine(string.Format((IFormatProvider)null,
                      "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                      new string(qualSequenceList.ElementAt(2).Select(a => (char)a).ToArray())));
                    Console.WriteLine(string.Format((IFormatProvider)null,
                      "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                      qualSequenceList.ElementAt(2).ID.ToString((IFormatProvider)null)));
                }

                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence '{0}' validation after Parse() is found to be as expected.",
                  qualSequenceList.ElementAt(0)));

                // Logs to the VSTest GUI (Console.Out) window
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  new string(qualSequenceList.ElementAt(0).Select(a => (char)a).ToArray())));
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  qualSequenceList.ElementAt(0).ID.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  new string(qualSequenceList.ElementAt(1).Select(a => (char)a).ToArray())));
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  qualSequenceList.ElementAt(1).ID.ToString((IFormatProvider)null)));
            }
        }


        /// <summary>
        /// General method to validate FastQ formatting 
        /// Qualitative Sequence by passing TextWriter as a parameter
        /// <param name="nodeName">xml node name.</param>
        /// </summary>
        void ValidateFastQFormatByFormattingQualSeqeunce(string nodeName)
        {
            // Gets the actual sequence and the alphabet from the Xml
            IAlphabet alphabet = Utility.GetAlphabet(utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.AlphabetNameNodeV2));
            FastQFormatType expectedFormatType = Utility.GetFastQFormatType(
              utilityObj.xmlUtil.GetTextValue(nodeName, Constants.FastQFormatType));
            string qualSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequenceNode);
            string expectedQualitativeSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequenceNode);            
            string qualityScores = null;
            int i;

            for (i = 0; i < qualSequence.Length; i++)
            {
                qualityScores = qualityScores + "}";
            }

            byte[] seq = UTF8Encoding.UTF8.GetBytes(qualSequence);
            byte[] qScore = UTF8Encoding.UTF8.GetBytes(qualityScores);
            string tempFileName = System.IO.Path.GetTempFileName();
            // Create a Qualitative Sequence.

            QualitativeSequence qualSeq = new QualitativeSequence(
              alphabet, expectedFormatType, seq, qScore);

            using (FastQFormatter formatter = new FastQFormatter(tempFileName))
            {
                formatter.Write(qualSeq);
                formatter.Close();

                using (FastQParser fastQParserObj = new FastQParser(tempFileName))
                {

                    // Read the new file and validate Sequences.
                    IEnumerable<QualitativeSequence> seqsNew =
                      fastQParserObj.Parse();

                    // Validate qualitative Sequence upon parsing FastQ file.
                    Assert.AreEqual(new string(seqsNew.ElementAt(0).Select(a => (char)a).ToArray()), expectedQualitativeSequence);
                    Assert.IsTrue(string.IsNullOrEmpty(seqsNew.ElementAt(0).ID));

                    ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                      "FastQ Parser P1: The FASTQ sequence '{0}' validation after Parse() is found to be as expected.",
                      seqsNew.ElementAt(0)));

                    // Logs to the VSTest GUI (Console.Out) window
                    Console.WriteLine(string.Format((IFormatProvider)null,
                      "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                      seqsNew.ElementAt(0).ToString()));
                    Console.WriteLine(string.Format((IFormatProvider)null,
                      "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                      seqsNew.ElementAt(0).ID.ToString((IFormatProvider)null)));
                }

                File.Delete(tempFileName);
            }
        }

        /// <summary>
        /// General method to validate FastQ Formatter by Passing Writer as parameter.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="isTextWriter">FastQ formatter Format() method parameter</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateFastQFormatter(string nodeName, bool isTextWriter)
        {
            // Gets the expected sequence from the Xml
            string filePath = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.FilePathNode);
            string expectedQualitativeSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequenceNode);
            string expectedSequenceId = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.SequenceIdNode);
            string tempFileName = System.IO.Path.GetTempFileName();

            // Parse a FastQ file.
            using (FastQParser fastQParserObj = new FastQParser(filePath))
            {
                fastQParserObj.AutoDetectFastQFormat = true;
                FastQParser fastQParserObjNew = null;
                IEnumerable<QualitativeSequence> seqsNew = null;
                IEnumerable<QualitativeSequence> qualSequenceList = null;
                qualSequenceList = fastQParserObj.Parse();
                FastQFormatter fastQFormatter = new FastQFormatter(tempFileName);

                // Format a Sequence using Text writer.
                if (isTextWriter)
                {
                    foreach (ISequence newQualSeq in qualSequenceList)
                    {
                        fastQFormatter.Write(newQualSeq);
                    }
                    fastQFormatter.Close();

                    // Read the new file and validate Sequences.                    
                    fastQParserObjNew = new FastQParser(tempFileName);
                    seqsNew = fastQParserObjNew.Parse();
                }
                else
                {
                    fastQFormatter.Write(qualSequenceList.ElementAt(0));
                    fastQFormatter.Close();
                    fastQParserObjNew = new FastQParser(tempFileName);
                    seqsNew = fastQParserObjNew.Parse();
                }

                // Validate qualitative Sequence upon parsing FastQ file.
                Assert.AreEqual(new string(seqsNew.ElementAt(0).Select(a => (char)a).ToArray()), expectedQualitativeSequence);
                Assert.AreEqual(seqsNew.ElementAt(0).ID.ToString((IFormatProvider)null), expectedSequenceId);

                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence '{0}' validation after Parse() is found to be as expected.",
                  seqsNew.ElementAt(0)));

                // Logs to the VSTest GUI (Console.Out) window
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  new string(seqsNew.ElementAt(0).Select(a => (char)a).ToArray())));
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  seqsNew.ElementAt(0).ID.ToString((IFormatProvider)null)));

                File.Delete(tempFileName);
            }
        }

        /// <summary>
        /// General method to validate multi sequence FastQ Format.
        /// <param name="nodeName">xml node name.</param>
        void ValidateMultiSeqFastQFormatter(string nodeName)
        {
            // Gets the expected sequence from the Xml
            string filePath = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.FilePathNode);
            string expectedQualitativeSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequenceNode);
            string expectedSequenceId = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.SequenceIdNode);
            string expectedSecondQualitativeSequence = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequence2Node);
            string expectedSecondSeqID = utilityObj.xmlUtil.GetTextValue(
              nodeName, Constants.ExpectedSequenceId1Node);

            // Parse a FastQ file.
            using (FastQParser fastQParserObj = new FastQParser(filePath))
            {
                fastQParserObj.AutoDetectFastQFormat = true;
                IEnumerable<QualitativeSequence> qualSequenceList = null;
                IEnumerable<QualitativeSequence> seqsNew = null;
                IEnumerable<QualitativeSequence> secondSeqsNew = null;
                qualSequenceList = fastQParserObj.Parse();

                // Format a first Qualitative sequence
                FastQFormatter fastQFormatterFirst = new FastQFormatter(Constants.FastQTempFileName);
                fastQFormatterFirst.Write(qualSequenceList.ElementAt(0));
                fastQFormatterFirst.Close();
                FastQParser fastQParserObjFirst = new FastQParser(Constants.FastQTempFileName);
                seqsNew = fastQParserObjFirst.Parse();

                // Format a Second Qualitative sequence
                FastQFormatter fastQFormatterSecond = new FastQFormatter(Constants.StreamWriterFastQTempFileName);
                fastQFormatterSecond.Write(qualSequenceList.ElementAt(1));
                fastQFormatterSecond.Close();
                FastQParser fastQParserObjSecond = new FastQParser(Constants.StreamWriterFastQTempFileName);
                secondSeqsNew = fastQParserObjSecond.Parse();

                // Validate Second qualitative Sequence upon parsing FastQ file.
                Assert.AreEqual(new string(secondSeqsNew.ElementAt(0).Select(a => (char)a).ToArray()), expectedSecondQualitativeSequence);
                Assert.AreEqual(secondSeqsNew.ElementAt(0).ID.ToString((IFormatProvider)null), expectedSecondSeqID);

                // Validate first qualitative Sequence upon parsing FastQ file.
                Assert.AreEqual(new string(seqsNew.ElementAt(0).Select(a => (char)a).ToArray()), expectedQualitativeSequence);
                Assert.AreEqual(seqsNew.ElementAt(0).ID.ToString((IFormatProvider)null), expectedSequenceId);

                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence '{0}' validation after Parse() is found to be as expected.",
                  seqsNew.ElementAt(0)));

                // Logs to the VSTest GUI (Console.Out) window
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  new string(seqsNew.ElementAt(0).Select(a => (char)a).ToArray())));
                Console.WriteLine(string.Format((IFormatProvider)null,
                  "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                  seqsNew.ElementAt(0).ID.ToString((IFormatProvider)null)));

                File.Delete(Constants.FastQTempFileName);
                File.Delete(Constants.StreamWriterFastQTempFileName);
            }
        }

        /// <summary>
        /// General method to validate BasicSequence Parser.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="methodName">Name of Method</param>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void ValidateBasicSequenceParser(
            string nodeName,
            FastQFileParameters methodName)
        {
            // Gets the expected sequence from the Xml
            string filepathOriginal =
                utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);
            string expectedQualitativeSequence =
                utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.ExpectedSequenceNode);
            string expectedSequenceId =
                utilityObj.xmlUtil.GetTextValue(
                nodeName, Constants.SequenceIdNode);
            IAlphabet alphabet = Utility.GetAlphabet(utilityObj.xmlUtil.GetTextValue(
               nodeName, Constants.AlphabetNameNode));
            Assert.IsTrue(File.Exists(filepathOriginal));

            ISequenceParser fastQParserObj = null;
            FastQFormatter formatter = new FastQFormatter(Path.GetTempFileName());
            try
            {
                fastQParserObj = new FastQParser(filepathOriginal);
                // Read the original file
                IEnumerable<ISequence> seqsOriginal = null;
                seqsOriginal = fastQParserObj.Parse();
                Assert.IsNotNull(seqsOriginal);

                // Use the formatter to write the original sequences to a temp file               
                formatter.Write(seqsOriginal.ElementAt(0));

                // Use the formatter to write the original sequences to a temp file
                string filepathTmp = Path.GetTempFileName();

                FastQParser fastQParserObjNew = new FastQParser(filepathTmp);

                // Read the new file, then compare the sequences
                IEnumerable<QualitativeSequence> seqsNew = null;
                seqsNew = fastQParserObjNew.Parse();
                Assert.IsNotNull(seqsNew);

                // Validate qualitative Sequence upon parsing FastQ file.
                Assert.AreEqual(
                    new string(seqsOriginal.ElementAt(0).Select(a => (char)a).ToArray()),
                    expectedQualitativeSequence);
                Assert.AreEqual(
                    seqsOriginal.ElementAt(0).ID.ToString((IFormatProvider)null),
                    expectedSequenceId);
                Assert.AreEqual(
                    seqsOriginal.ElementAt(0).Alphabet.Name,
                 alphabet.Name);

                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "FastQ Parser P1: The FASTQ sequence '{0}' validation after Parse() is found to be as expected.",
                    seqsOriginal.ElementAt(0)));

                // Logs to the VSTest GUI (Console.Out) window
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "FastQ Parser P1: The FASTQ sequence ID '{0}' validation after Parse() is found to be as expected.",
                    new string(seqsOriginal.ElementAt(0).Select(a => (char)a).ToArray())));

                File.Delete(filepathTmp);
            }
            finally
            {
                if (fastQParserObj != null)
                    (fastQParserObj as FastQParser).Dispose();
            }
        }

        #endregion Supporting Methods
    }
}
