﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

/****************************************************************************
 * EbiBlastP1TestCases.cs
 * 
 * This file contains the Ebi Blast Web Service P1 test cases.
 * 
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

using MBF.TestAutomation.Util;
using MBF.Util.Logging;
using MBF.Web;
using MBF.Web.Blast;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MBF.TestAutomation.Web.EbiBlast
{
    /// <summary>
    /// Test Automation code for MBF Ebi Blast Web Service and P1 level validations.
    /// </summary>
    [TestClass]
    public class EbiBlastP1TestCases
    {

        #region Enums

        /// <summary>
        /// Ebi blast parameter which are used for different test cases 
        /// based on which the test cases are executed.
        /// </summary>
        enum ParameterGroup
        {
            MandatoryParams,
            OptionalParams,
            MandatoryAndOptionalParams,
            StrandParams,
            SensitivityParams,
            Default
        };

        /// <summary>
        /// EbiBlastHandler Constructor parameters
        /// Used for the different test cases.
        /// </summary>
        enum EbiWebServiceCtorParameters
        {
            ConfigPams,
            ParserAndConfigPams,
            Default
        };

        #endregion Enums

        #region Global Variables

        Utility _utilityObj = new Utility(@"TestUtils\TestsConfig.xml");

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static EbiBlastP1TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.automation.log");
            }
        }

        #endregion Constructor

        #region EbiBlast P1 Test Cases

        /// <summary>
        /// Validate valid Ebi blast mandatory parameters with Dna sequence.
        /// Input data : valid dna seqeunce with mandatory parameters.
        /// Ouptut data : Validation of mandatory Ebi paramters.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEBIWuBlastMandatoryParamsWithDNAData()
        {
            GeneralMethodToValidateEbiBlastParameters(
                Constants.EBlastDnaSequenceParameters, ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate valid Ebi blast mandatory parameters with Rna sequence.
        /// Input data : Valid Rna seqeunce with mandatory parameters.
        /// Ouptut data : Validation of mandatory Ebi paramters.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEBIWuBlastMandatoryParamsWithRNAData()
        {
            GeneralMethodToValidateEbiBlastParameters(
                Constants.EBlastRnaSequenceParametersNode, ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate valid Ebi blast mandatory parameters with Protein sequence.
        /// Input data : Valid Protein seqeunce with mandatory parameters.
        /// Ouptut data : Validation of mandatory Ebi paramters.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEBIWuBlastMandatoryParamsWithPROTEINData()
        {
            GeneralMethodToValidateEbiBlastParameters(
                Constants.EbiBlastParametersNode, ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate valid Ebi blast mandatory parameters with
        /// some optional parameters.
        /// Input data : Valid Dna seqeunce with optional parameters.
        /// Ouptut data : Validation of optional Ebi paramters.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEBIWuBlastMandatoryParamsWithOptionalParams()
        {
            GeneralMethodToValidateEbiBlastParameters(
                Constants.EBlastDnaSequenceParameters,
                ParameterGroup.MandatoryAndOptionalParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing medium sized
        /// DNA sequence(>10KB) as query value.
        /// Input Data :Valid medium sized DNA sequence.
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEBIWuBlastSubmitSearchRequestWithMediumSizeDNAData()
        {
            ValidateSubmitSearchMethod(
                Constants.EBlastMediumSizeDnaSequenceParametersNode,
                ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing medium sized
        /// Protein sequence(>10KB) as query value.
        /// Input Data :Valid medium sized Protein sequence.
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEBIWuBlastSubmitSearchRequestWithMediumSizePROTEINData()
        {
            ValidateSubmitSearchMethod(
                Constants.EbiBlastMediumSizeProteinSequenceParametersNode,
                ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// Protein sequence with blastx program.
        /// Input Data :Valid Protein sequence.
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEBIWuBlastSubmitSearchRequestUsingBLASTXParam()
        {
            ValidateSubmitSearchMethod(
                Constants.EbiBlastParametersWithBlastXNode,
                ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// Dna sequence with tblastx program.
        /// Input Data :Valid Dna sequence.
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubmitSearchRequestUsingTBlastXProgram()
        {
            ValidateSubmitSearchMethod(
                Constants.EBlastDnaSequenceParametersWithTblastxNode,
                ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// Dna sequence with blastn program.
        /// Input Data :Valid Dna sequence.
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubmitSearchRequestUsingBlastnProgram()
        {
            ValidateSubmitSearchMethod(
                Constants.EBlastDnaSequenceParametersWithBlastNNode,
                ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// protein sequence with Swissprot database and blastx program
        /// Input Data :Valid Dna sequence.
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSearchRequestUsingSwissprotDatabase()
        {
            ValidateSubmitSearchMethod(
                Constants.EbiBlastParametersWithSwissprotNode,
                ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// protein sequence with EPOP database and blastx program
        /// Input Data :Valid Dna sequence.
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubmitSearchRequestUsingEPOPDatabase()
        {
            ValidateSubmitSearchMethod(
                Constants.EbiBlastParametersWithEPOPDatabaseNode,
                ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// protein sequence with JPOP database and blastx program
        /// Input Data :Valid Dna sequence.
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubmitSearchRequestUsingJPOPDatabase()
        {
            ValidateSubmitSearchMethod(
                Constants.EbiBlastParametersWithJPOPDatabaseNode,
                ParameterGroup.MandatoryParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// mandatory parameters with some optional parameters.
        /// Input Data :Valid Dna sequence and some optional parameters..
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubmitSearchRequestUsingOptionalParameters()
        {
            ValidateSubmitSearchMethod(
                Constants.EBlastDnaSequenceParameters,
                ParameterGroup.MandatoryAndOptionalParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// Dna strand paramters with configuring value to "Both"..
        /// Input Data :Valid Dna sequence and  strand parameter..
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubmitSearchRequestUsingStrandParameter()
        {
            ValidateSubmitSearchMethod(
                Constants.EBlastDnaSequenceParameters,
                ParameterGroup.StrandParams);
        }

        /// <summary>
        /// Validate SubmitSearchRequest() method by passing 
        /// Dna sensitivity paramters with configuring value to "medium".
        /// Input Data :Valid Dna sequence and  strand parameter..
        /// Output Data : Validation of SubmitSearchRequest() method.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSubmitSearchRequestUsingSensitivityParameter()
        {
            ValidateSubmitSearchMethod(
                Constants.EBlastDnaSequenceParameters,
                ParameterGroup.SensitivityParams);
        }

        /// <summary>
        /// Validate asynchronous reulsts fetching for Dna sequence.
        /// Input Data :Valid Dna query sequence, email address, program and database value.
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithDNASeqeunceData()
        {
            ValidateGeneralFetchResults(
                Constants.EbiAsynchronousResultsNode, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with Rna Sequence 
        /// Input Data :Valid Rna query sequence, email address,
        /// blastn program and "em_rel" database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithRNASequenceData()
        {
            ValidateGeneralFetchResults(
                Constants.EbiRnaAsynchronousResultsNode, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with Protein Sequence 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "swissprot" database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithProteinSequenceUsingSwissprot()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithSwissprotNode, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// Protein Sequence with Uniprot database 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "uniprot" database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithProteinSequenceUsingUniprot()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithUniprotNode, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// Protein Sequence for EPOP database 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "EPOP" database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithProteinSequenceUsingEPOPDatabase()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithEPOPDataBase, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// Protein Sequence for JPOP database 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "JPOP" database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithProteinSequenceUsingJPOPDatabase()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithJPOPDataBase, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// Protein Sequence for KPOP database 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "KPOP" database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithProteinSequenceUsingKPOPDatabase()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithKIPOPDataBase, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// Protein Sequence for swissprot database and blastp program
        /// Input Data :Valid Protein query sequence, email address,
        /// blastp program and database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithProteinSequenceUsingBlastPProgram()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsBlastpProgram, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// Dna Sequence with swissprot database and blastn program
        /// Input Data :Valid Dna query sequence, email address,
        /// blastn program and database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithDNASequenceUsingBlastNProgram()
        {
            ValidateGeneralFetchResults(
                Constants.EbiAsynchronousResultsWithBlastn, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// RNA Sequence with swissprot database and blastn program
        /// Input Data :Valid Rna query sequence, email address,
        /// blastn program and database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithRNASequenceUsingBlastNProgram()
        {
            ValidateGeneralFetchResults(
                Constants.EbiRnaAsynchronousResultsWithBlastN, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// DNA Sequence with EBI DataBase database and blastn program
        /// Input Data :Valid Dna query sequence, email address,
        /// tblastX program and database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchWithDNASequenceUaingEBIDatabase()
        {
            ValidateGeneralFetchResults(
                Constants.EbiAsynchronousResultsWithEBIDataBaseNode, null, null, false);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// DNA Sequence with EBI DataBase database and blastn program
        /// with Some optional parameters
        /// Input Data :Valid Dna query sequence, email address,
        /// tblastX program and database 
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateAsyncResultsFetchUsingSomeOptionalParams()
        {
            ValidateGeneralFetchResults(
                Constants.EbiAsynchronousResultsWithOptionalparametersNode,
                "strandParameter", "sensitivityParameter", false);
        }


        /// <summary>
        /// Validate synchronous reulsts fetching for Dna sequence.
        /// Input Data :Valid Dna query sequence, email address, program and database value.
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithDnaSequence()
        {
            ValidateGeneralFetchResults(
                Constants.EbiAsynchronousResultsNode, null, null, true);
        }

        /// <summary>
        /// Validate fetching results Synchronous with Rna Sequence 
        /// Input Data :Valid Rna query sequence, email address,
        /// blastn program and "em_rel" database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithRNASequence()
        {
            ValidateGeneralFetchResults(
                Constants.EbiRnaAsynchronousResultsNode, null, null, true);
        }

        /// <summary>
        /// Validate fetching results Synchronous with Protein Sequence 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "swissprot" database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithProteinSequenceUsingSwissprot()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithSwissprotNode,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results synchronous with 
        /// Protein Sequence with Uniprot database 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "uniprot" database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithProteinSequenceUsingUniprot()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithUniprotNode,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results synchronous with 
        /// Protein Sequence for EPOP database 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "EPOP" database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithProteinSequenceUsingEPOPDatabase()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithEPOPDataBase,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results synchronous with 
        /// Protein Sequence for JPOP database 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "JPOP" database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithProteinSequenceUsingJPOPDatabase()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithJPOPDataBase,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results synchronous with 
        /// Protein Sequence for KPOP database 
        /// Input Data :Valid Protein query sequence, email address,
        /// blastx program and "KPOP" database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithProteinSequenceUsingKPOPDatabase()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsWithKIPOPDataBase,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results asynchronous with 
        /// Protein Sequence for swissprot database and blastp program
        /// Input Data :Valid Protein query sequence, email address,
        /// blastp program and database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithProteinSequenceUsingBlastPProgram()
        {
            ValidateGeneralFetchResults(
                Constants.EbiProteinAsynchronousResultsBlastpProgram,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results synchronous with 
        /// Dna Sequence with swissprot database and blastn program
        /// Input Data :Valid Dna query sequence, email address,
        /// blastn program and database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithDNASequenceUsingBlastNProgram()
        {
            ValidateGeneralFetchResults(
                Constants.EbiAsynchronousResultsWithBlastn,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results synchronous with 
        /// RNA Sequence with swissprot database and blastn program
        /// Input Data :Valid Rna query sequence, email address,
        /// blastn program and database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithRNASequenceUsingBlastNProgram()
        {
            ValidateGeneralFetchResults(
                Constants.EbiRnaAsynchronousResultsWithBlastN,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results synchronous with 
        /// DNA Sequence with EBI DataBase database and blastn program
        /// Input Data :Valid Dna query sequence, email address,
        /// tblastX program and database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchWithDnaSequenceUsingEBIDatabase()
        {
            ValidateGeneralFetchResults(
                Constants.EbiAsynchronousResultsWithEBIDataBaseNode,
                null, null, true);
        }

        /// <summary>
        /// Validate fetching results synchronous with 
        /// DNA Sequence with EBI DataBase database and blastn program
        /// with Some optional parameters
        /// Input Data :Valid Dna query sequence, email address,
        /// tblastX program and database 
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSyncResultsFetchUsingSomeOptionalParams()
        {
            ValidateGeneralFetchResults(
                Constants.EbiAsynchronousResultsWithOptionalparametersNode,
                "strandParameter", "sensitivityParameter", true);
        }

        /// <summary>
        /// Validate Ebi Web Service Em_rel db results by passing config parameters to Ebi constructor.
        /// Input Data : Valid Config Parameters
        /// Output Data : Validation of blast results 
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEbiBlastResultsUsingConstructorPams()
        {
            GeneralMethodToValidateResults(Constants.EbiAsynchronousResultsWithEBIDataBaseNode,
                EbiWebServiceCtorParameters.ParserAndConfigPams);
        }

        /// <summary>
        /// Validate Ebi Web Service Em_rel db results by passing config parameters and Blast Parameter to Ebi constructor.
        /// Input Data : Valid Config and Blast Parameters
        /// Output Data : Validation of blast results 
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEbiBlastResultsUsingConfigPams()
        {
            GeneralMethodToValidateResults(Constants.EbiAsynchronousResultsWithEBIDataBaseNode,
                EbiWebServiceCtorParameters.ConfigPams);
        }

        /// <summary>
        /// Validate Ebi Web Service Submit request for List of Sequences.
        /// Input Data : Valid List of sequences.
        /// Output Data : Validation of blast results 
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEbiSubmitSearchRequestForQueryList()
        {
            string nodeName = Constants.EmRelDatabaseParametersNode;

            // Gets the search query parameter and their values.
            string querySequence = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.QuerySequency);
            string queryDatabaseValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseValue);
            string queryProgramValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramValue);
            string queryDatabaseParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseParameter);
            string queryProgramParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramParameter);
            string alphabetName = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.AlphabetNameNode);
            string emailParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string email = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);

            // Create a sequence.
            IList<ISequence> seqList = new List<ISequence>();
            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName),
                querySequence);
            seqList.Add(seq);

            // Set Service confiruration parameters true.
            IBlastServiceHandler ebiBlastService = null;
            try
            {
                ebiBlastService = new EbiWuBlastHandler();
                ConfigParameters configParameters = new ConfigParameters();
                configParameters.UseBrowserProxy = true;
                ebiBlastService.Configuration = configParameters;

                // Create search parameters object.
                BlastParameters ebiParams = new BlastParameters();

                // Add parameter values to search query parameters.
                ebiParams.Add(queryDatabaseParameter, queryDatabaseValue);
                ebiParams.Add(queryProgramParameter, queryProgramValue);
                ebiParams.Add(emailParameter, email);

                // Get Request identifier from web service for SeqList.
                // Automated this case to hit the Code.
                ebiBlastService.SubmitRequest(seqList, ebiParams);
                Assert.Fail();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1 : Validated the exception successfully."));
            }
            finally
            {
                if (ebiBlastService != null)
                    ((IDisposable)ebiBlastService).Dispose();
            }
        }

        /// <summary>
        /// Validate Ebi Service metadata.
        /// Input Data : Valid MetaData.
        /// Output Data : Validation of MetaData
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateEbiServiceMetaData()
        {
            EbiWuBlastHandler ebiHandler = null;
            try
            {
                ebiHandler = new EbiWuBlastHandler();
                Assert.IsNotNull(ebiHandler.GetServiceMetadata(Constants.MetadataDatabases));
                Assert.IsNotNull(ebiHandler.GetServiceMetadata(Constants.MetadataFilter));
                Assert.IsNotNull(ebiHandler.GetServiceMetadata(Constants.MetadataMatrices));
                Assert.IsNotNull(ebiHandler.GetServiceMetadata(Constants.MetadataPrograms));
                Assert.IsNotNull(ebiHandler.GetServiceMetadata(Constants.MetadataSensitivity));
                Assert.IsNotNull(ebiHandler.GetServiceMetadata(Constants.MetadataSort));
                Assert.IsNotNull(ebiHandler.GetServiceMetadata(Constants.MetadataStatistics));
                Assert.IsNotNull(ebiHandler.GetServiceMetadata(Constants.MetadataXmlFormats));

                ApplicationLog.WriteLine(
                    "EbiWebService P1 : Successfully validated the Ebi WebService MetaData");
                Console.WriteLine(
                    "EbiWebService P1 : Successfully validated the Ebi WebService MetaData");
            }
            finally
            {
                if (ebiHandler != null)
                    ((IDisposable)ebiHandler).Dispose();
            }
        }

        #endregion EbiBlast P1 Test Cases

        #region Supporting Methods

        /// <summary>
        /// Validate general ebi blast parameters.
        /// with the different parameters specified.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="ParameterGroup">parameter Group.</param>
        /// </summary>
        void GeneralMethodToValidateEbiBlastParameters(
            string nodeName, ParameterGroup parameters)
        {
            // Gets the search query parameter and their values.
            string querySequence = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.QuerySequency);
            string queryDatabaseValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseValue);
            string queryProgramValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramValue);
            string queryDatabaseParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseParameter);
            string queryProgramParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramParameter);
            string emailParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string email = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string optionalStrandParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string strandValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string optionalSensitivityParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string sensitivityValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);


            // Set Service confiruration parameters true.
            EbiWuBlastHandler blastService = null;
            try
            {
                blastService = new EbiWuBlastHandler();
                ConfigParameters configParameters = new ConfigParameters();
                configParameters.UseBrowserProxy = true;
                blastService.Configuration = configParameters;

                // Create search parameters object.
                BlastParameters ebiParams = new BlastParameters();

                switch (parameters)
                {
                    case ParameterGroup.MandatoryParams:
                        ebiParams.Add(queryDatabaseParameter, queryDatabaseValue);
                        ebiParams.Add(queryProgramParameter, queryProgramValue);
                        ebiParams.Add(emailParameter, email);
                        break;
                    case ParameterGroup.MandatoryAndOptionalParams:
                        ebiParams.Add(queryDatabaseParameter, queryDatabaseValue);
                        ebiParams.Add(queryProgramParameter, queryProgramValue);
                        ebiParams.Add(emailParameter, email);
                        ebiParams.Add(optionalSensitivityParameter, sensitivityValue);
                        ebiParams.Add(optionalStrandParameter, strandValue);
                        break;
                    default:
                        break;
                }

                // Validate search query parameters.
                ParameterValidationResult validateParameters =
                    EbiWuBlastHandler.ValidateParameters(ebiParams);
                bool result = validateParameters.IsValid;

                Assert.IsTrue(result);
                Assert.IsTrue(ebiParams.Settings.ContainsValue(queryDatabaseValue));
                Assert.IsTrue(ebiParams.Settings.ContainsValue(queryProgramValue));
                Assert.IsTrue(ebiParams.Settings.ContainsValue(email));
                Assert.AreEqual(ebiParams.Settings.Count, 3);

                // Logs to the NUnit GUI (Console.Out) window
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: Query Sequence {0} is as expected.", querySequence));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: DataBase Value {0} is as expected.", queryDatabaseValue));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: Program Value {0} is as expected.", queryProgramValue));
            }
            finally
            {
                if (blastService != null)
                    ((IDisposable)blastService).Dispose();
            }
        }

        /// <summary>
        /// Validate general SubmitSearchRequest method test cases 
        /// with the different parameters specified.
        /// <param name="nodeName">xml node name.</param>
        /// <param name="parameters">Parameter group</param>
        /// <returns>Request Identifier returned by Ebi Blast web service.</returns>
        /// </summary>
        void ValidateSubmitSearchMethod(
            string nodeName, ParameterGroup parameters)
        {
            // Gets the search query parameter and their values.
            string querySequence = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.QuerySequency);
            string queryDatabaseValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseValue);
            string queryProgramValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramValue);
            string queryDatabaseParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseParameter);
            string queryProgramParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramParameter);
            string alphabetName = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.AlphabetNameNode);
            string emailParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string email = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string optionalStrandParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string strandValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string optionalSensitivityParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string sensitivityValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string reqId = string.Empty;

            // Create a sequence.
            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName),
                querySequence);

            // Set Service confiruration parameters true.
            IBlastServiceHandler ebiBlastService = null;
            try
            {
                ebiBlastService = new EbiWuBlastHandler();
                ConfigParameters configParameters = new ConfigParameters();
                configParameters.UseBrowserProxy = true;
                ebiBlastService.Configuration = configParameters;

                // Create search parameters object.
                BlastParameters ebiParams = new BlastParameters();

                // Add parameter values to search query parameters.
                switch (parameters)
                {
                    case ParameterGroup.MandatoryParams:
                        ebiParams.Add(queryDatabaseParameter, queryDatabaseValue);
                        ebiParams.Add(queryProgramParameter, queryProgramValue);
                        ebiParams.Add(emailParameter, email);
                        break;
                    case ParameterGroup.StrandParams:
                        ebiParams.Add(queryDatabaseParameter, queryDatabaseValue);
                        ebiParams.Add(queryProgramParameter, queryProgramValue);
                        ebiParams.Add(emailParameter, email);
                        ebiParams.Add(optionalStrandParameter, strandValue);
                        break;
                    case ParameterGroup.SensitivityParams:
                        ebiParams.Add(queryDatabaseParameter, queryDatabaseValue);
                        ebiParams.Add(queryProgramParameter, queryProgramValue);
                        ebiParams.Add(emailParameter, email);
                        ebiParams.Add(optionalSensitivityParameter, sensitivityValue);
                        break;
                    case ParameterGroup.MandatoryAndOptionalParams:
                        ebiParams.Add(queryDatabaseParameter, queryDatabaseValue);
                        ebiParams.Add(queryProgramParameter, queryProgramValue);
                        ebiParams.Add(emailParameter, email);
                        ebiParams.Add(optionalSensitivityParameter, sensitivityValue);
                        ebiParams.Add(optionalStrandParameter, strandValue);
                        break;
                    default:
                        break;
                }

                // Submit search request with valid parameter.
                reqId = ebiBlastService.SubmitRequest(seq, ebiParams);

                // Validate request identifier returned by web service.
                Assert.IsNotNull(reqId);

                // Logs to the NUnit GUI (Console.Out) window
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: Validation of SubmitSearchRequest() method was completed successfully."));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: Request Id {0} is as expected.", reqId));
            }
            finally
            {
                if (ebiBlastService != null)
                    ((IDisposable)ebiBlastService).Dispose();
            }
        }

        /// <summary>
        /// Validate general fetching results.by passing 
        /// differnt parameters for Ebi web service..
        /// <param name="nodeName">xml node name.</param>
        /// <param name="strandParameter">Strand parameter</param>
        /// <param name="sensitivityParameter">Sensitivity parameter</param>
        /// <param name="isFetchSynchronous">Is fetch synchronous?</param>
        /// </summary>
        void ValidateGeneralFetchResults(
            string nodeName, string strandParameter,
            string sensitivityParameter,
            bool isFetchSynchronous)
        {
            // Gets the search query parameter and their values.
            string alphabetName = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.AlphabetNameNode);
            string querySequence = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.QuerySequency);
            string queryDatabaseValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseValue);
            string emailParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string email = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string queryProgramValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramValue);
            string queryDatabaseParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseParameter);
            string queryProgramParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramParameter);
            string optionalStrandParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string strandValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string optionalSensitivityParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string sensitivityValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string expectedHitId = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.HitID);
            string expectedAccession = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.HitAccession);
            string expectedResultCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ResultsCount);
            string expectedHitsCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.HitsCount);
            string expectedEntropyStatistics = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EntropyStatistics);
            string expectedKappaStatistics = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.KappaStatistics);
            string expectedLambdaStatistics = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.LambdaStatistics);
            string expectedLength = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Length);

            object responseResults = null;

            Sequence seq = new Sequence(
                Utility.GetAlphabet(alphabetName), querySequence);

            // create Ebi Blast service object.
            IBlastServiceHandler ebiBlastService = null;
            try
            {
                ebiBlastService = new EbiWuBlastHandler();
                ConfigParameters configParams = new ConfigParameters();
                configParams.UseBrowserProxy = true;
                ebiBlastService.Configuration = configParams;

                BlastParameters searchParams = new BlastParameters();

                // Set Request parameters.
                if ((0 == string.Compare(strandParameter, null, true,
                    CultureInfo.CurrentCulture))
                    && (0 == string.Compare(sensitivityParameter, null, true,
                    CultureInfo.CurrentCulture)))
                {
                    searchParams.Add(queryDatabaseParameter, queryDatabaseValue);
                    searchParams.Add(queryProgramParameter, queryProgramValue);
                    searchParams.Add(emailParameter, email);
                }
                else
                {
                    searchParams.Add(queryDatabaseParameter, queryDatabaseValue);
                    searchParams.Add(queryProgramParameter, queryProgramValue);
                    searchParams.Add(emailParameter, email);
                    searchParams.Add(optionalSensitivityParameter, sensitivityValue);
                    searchParams.Add(optionalStrandParameter, strandValue);
                }

                // Create a request without passing sequence.
                string reqId = ebiBlastService.SubmitRequest(seq, searchParams);

                // validate request identifier.
                Assert.IsNotNull(reqId);

                // query the status
                ServiceRequestInformation info = ebiBlastService.GetRequestStatus(reqId);
                if (info.Status != ServiceRequestStatus.Waiting
                    && info.Status != ServiceRequestStatus.Ready)
                {
                    string err =
                        ApplicationLog.WriteLine("Unexpected status: '{0}'", info.Status);
                    Assert.Fail(err);
                }

                // get async results, poll until ready
                int maxAttempts = 20;
                int attempt = 1;
                while (attempt <= maxAttempts
                    && info.Status != ServiceRequestStatus.Error
                    && info.Status != ServiceRequestStatus.Ready)
                {
                    if (isFetchSynchronous)
                    {
                        info = ebiBlastService.GetRequestStatus(reqId);
                        Thread.Sleep(info.Status == ServiceRequestStatus.Waiting
                            || info.Status == ServiceRequestStatus.Queued
                            ? 20000 * attempt : 0);
                    }
                    else
                    {
                        Thread.Sleep(info.Status == ServiceRequestStatus.Waiting ? 40000 : 0);
                        info = ebiBlastService.GetRequestStatus(reqId);
                    }
                    ++attempt;
                }

                IBlastParser blastXmlParser = new BlastXmlParser();
                responseResults = blastXmlParser.Parse(
                        new StringReader(ebiBlastService.GetResult(reqId, searchParams)));

                // Validate blast results.
                Assert.IsNotNull(responseResults);
                List<BlastResult> eBlastResults = responseResults as List<BlastResult>;
                Assert.IsNotNull(eBlastResults);
                Assert.AreEqual(eBlastResults.Count.ToString(
                    (IFormatProvider)null), expectedResultCount);
                Assert.AreEqual(eBlastResults[0].Records.Count.ToString((
                    IFormatProvider)null), expectedResultCount);
                BlastSearchRecord record = eBlastResults[0].Records[0];
                Assert.AreEqual(record.Statistics.Kappa.ToString(
                    (IFormatProvider)null), expectedKappaStatistics);
                Assert.AreEqual(record.Statistics.Lambda.ToString(
                    (IFormatProvider)null), expectedLambdaStatistics);
                Assert.AreEqual(record.Statistics.Entropy.ToString(
                    (IFormatProvider)null), expectedEntropyStatistics);
                Assert.AreEqual(record.Hits.Count.ToString(
                    (IFormatProvider)null), expectedHitsCount);
                Hit hit = record.Hits[0];
                Assert.AreEqual(hit.Accession, expectedAccession);
                Assert.AreEqual(hit.Length.ToString((IFormatProvider)null), expectedLength);
                Assert.AreEqual(hit.Id.ToString((IFormatProvider)null), expectedHitId);
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: Hits count '{0}'.", eBlastResults.Count));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: Accession '{0}'.", hit.Accession));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: Hit Id '{0}'.", hit.Id));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: Hits Count '{0}'.", hit.Hsps.Count));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "Ebi Blast P1: length '{0}'.", hit.Length));

                // Validate the results Synchronously with the results got earlier.
                if (isFetchSynchronous)
                {
                    IList<BlastResult> syncBlastResults =
                        ebiBlastService.FetchResultsSync(reqId, searchParams) as List<BlastResult>;
                    Assert.IsNotNull(syncBlastResults);
                    if (null != eBlastResults[0].Records[0].Hits
                        && 0 < eBlastResults[0].Records[0].Hits.Count
                        && null != eBlastResults[0].Records[0].Hits[0].Hsps
                        && 0 < eBlastResults[0].Records[0].Hits[0].Hsps.Count)
                    {
                        Assert.AreEqual(eBlastResults[0].Records[0].Hits[0].Hsps[0].QuerySequence,
                            syncBlastResults[0].Records[0].Hits[0].Hsps[0].QuerySequence);
                    }
                    else
                    {
                        ApplicationLog.WriteLine("No significant hits found with the these parameters.");
                        Console.WriteLine("No significant hits found with the these parameters.");
                    }
                }
            }
            finally
            {
                if (ebiBlastService != null)
                    ((IDisposable)ebiBlastService).Dispose();
            }
        }

        /// <summary>
        /// Validate general fetching results by passing 
        /// differnt parameters for Ebi web service.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        /// <param name="ebiCtorPam">Ebi constructor different parameters</param>
        void GeneralMethodToValidateResults(string nodeName,
            EbiWebServiceCtorParameters ebiCtorPam)
        {
            // Gets the search query parameter and their values.
            string alphabetName = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.AlphabetNameNode);
            string querySequence = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.QuerySequency);
            string queryDatabaseValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseValue);
            string emailParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Emailparameter);
            string email = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EmailAdress);
            string queryProgramValue = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramValue);
            string queryDatabaseParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.DatabaseParameter);
            string queryProgramParameter = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProgramParameter);
            string expectedHitId = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.HitID);
            string expectedAccession = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.HitAccession);
            string expectedResultCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ResultsCount);
            string expectedHitsCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.HitsCount);
            string expectedEntropyStatistics = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.EntropyStatistics);
            string expectedKappaStatistics = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.KappaStatistics);
            string expectedLambdaStatistics = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.LambdaStatistics);
            string expectedLength = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.Length);

            object responseResults = null;

            Sequence seq = new Sequence(
                Utility.GetAlphabet(alphabetName), querySequence);

            // create Ebi Blast service object.
            IBlastServiceHandler ebiBlastService;
            BlastXmlParser blastXmlParser = new BlastXmlParser();
            ConfigParameters configParams = new ConfigParameters();
            configParams.UseBrowserProxy = true;

            // Set Service confiruration parameters 
            switch (ebiCtorPam)
            {
                case EbiWebServiceCtorParameters.ConfigPams:
                    ebiBlastService = new EbiWuBlastHandler(configParams);
                    break;
                case EbiWebServiceCtorParameters.ParserAndConfigPams:
                    ebiBlastService = new EbiWuBlastHandler(blastXmlParser, configParams);
                    break;
                default:
                    ebiBlastService = new EbiWuBlastHandler();
                    break;
            }

            BlastParameters searchParams = new BlastParameters();

            searchParams.Add(queryDatabaseParameter, queryDatabaseValue);
            searchParams.Add(queryProgramParameter, queryProgramValue);
            searchParams.Add(emailParameter, email);

            // Create a request without passing sequence.
            string reqId = ebiBlastService.SubmitRequest(seq, searchParams);

            // validate request identifier.
            Assert.IsNotNull(reqId);

            // query the status
            ServiceRequestInformation info = ebiBlastService.GetRequestStatus(reqId);
            if (info.Status != ServiceRequestStatus.Waiting
                && info.Status != ServiceRequestStatus.Ready)
            {
                string err = ApplicationLog.WriteLine("Unexpected status: '{0}'", info.Status);
                Assert.Fail(err);
            }

            // get async results, poll until ready
            int maxAttempts = 15;
            int attempt = 1;
            while (attempt <= maxAttempts
                && info.Status != ServiceRequestStatus.Error
                && info.Status != ServiceRequestStatus.Ready)
            {
                Thread.Sleep(info.Status == ServiceRequestStatus.Waiting ? 40000 : 0);
                info = ebiBlastService.GetRequestStatus(reqId);
                ++attempt;
            }

            responseResults = blastXmlParser.Parse(
                new StringReader(ebiBlastService.GetResult(reqId, searchParams)));

            // Validate blast results.
            Assert.IsNotNull(responseResults);
            List<BlastResult> eBlastResults = responseResults as List<BlastResult>;
            Assert.IsNotNull(eBlastResults);
            Assert.AreEqual(eBlastResults.Count.ToString(
                (IFormatProvider)null), expectedResultCount);
            Assert.AreEqual(eBlastResults[0].Records.Count.ToString((
                IFormatProvider)null), expectedResultCount);
            BlastSearchRecord record = eBlastResults[0].Records[0];
            Assert.AreEqual(record.Statistics.Kappa.ToString(
                (IFormatProvider)null), expectedKappaStatistics);
            Assert.AreEqual(record.Statistics.Lambda.ToString(
                (IFormatProvider)null), expectedLambdaStatistics);
            Assert.AreEqual(record.Statistics.Entropy.ToString(
                (IFormatProvider)null), expectedEntropyStatistics);
            Assert.AreEqual(record.Hits.Count.ToString(
                (IFormatProvider)null), expectedHitsCount);
            Hit hit = record.Hits[0];
            Assert.AreEqual(hit.Accession, expectedAccession);
            Assert.AreEqual(hit.Length.ToString((IFormatProvider)null), expectedLength);
            Assert.AreEqual(hit.Id.ToString((IFormatProvider)null), expectedHitId);
            Console.WriteLine(string.Format((IFormatProvider)null,
                "Ebi Blast P1: Hits count '{0}'.", eBlastResults.Count));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "Ebi Blast P1: Accession '{0}'.", hit.Accession));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "Ebi Blast P1: Hit Id '{0}'.", hit.Id));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "Ebi Blast P1: Hits Count '{0}'.", hit.Hsps.Count));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "Ebi Blast P1: length '{0}'.", hit.Length));

            ((IDisposable)ebiBlastService).Dispose();
        }

        #endregion Supporting Methods
    }
}