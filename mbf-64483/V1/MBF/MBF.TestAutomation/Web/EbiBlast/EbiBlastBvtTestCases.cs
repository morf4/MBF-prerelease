﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

/****************************************************************************
 * EbiBlastBvtTestCases.cs
 * 
 * This file contains the Ebi Blast Web Service BVT test cases.
 * 
******************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using MBF.TestAutomation.Util;
using MBF.Util.Logging;
using MBF.Web;
using MBF.Web.Blast;

using NUnit.Framework;

namespace MBF.TestAutomation.Web.EbiBlast
{
    /// <summary>
    /// Test Automation code for MBF Ebi Blast Web Service and BVT level validations.
    /// </summary>
    [TestFixture]
    public class EbiBlastBvtTestCases
    {

        #region Global Variables

        static bool _IsWebServiceAvailable = true;

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static EbiBlastBvtTestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.automation.log");
            }

            Utility._xmlUtil = new XmlUtility(@"TestUtils\TestsConfig.xml");

            _IsWebServiceAvailable = ValidateWebServiceConnection();
        }

        #endregion Constructor

        #region EbiBlast Bvt TestCases

        /// <summary>
        /// Validate fetching results asynchronous 
        /// Input Data :Valid query sequence, email address, program and database value.
        /// Output Data : Validation of Eblast results by asynchronous fetching.
        /// </summary>
        [Test]
        public void ValidateAsyncResultsFetch()
        {
            ValidateEBIWuBlastResultsFetch(
                Constants.EbiAsynchronousResultsNode, false);
        }

        /// <summary>
        /// Validate fetching results Synchronous 
        /// Input Data :Valid query sequence, email address, program and database value.
        /// Output Data : Validation of Eblast results by synchronous fetching.
        /// </summary>
        [Test]
        public void ValidateSyncResultsFetch()
        {
            ValidateEBIWuBlastResultsFetch(
                Constants.EbiSynchronousResults, true);
        }

        /// <summary>
        /// Validate Request status returned from Ebi web service by passing request 
        /// Identifier for DNA  sequence.
        /// Input Data :Valid search query, Database value and program value and email adress.
        /// Output Data : Validation of GetRequestStatus() method.
        /// </summary>
        [Test]
        public void ValidateEbiGetRequestStatusMethodForDna()
        {
            ValidateEbiGeneralGetRequestStatusMethod(
                Constants.EBlastDnaSequenceParameters);
        }

        /// <summary>
        /// Validate Request status returned from Ebi web service by passing request Identifier 
        /// for Protein  sequence.
        /// Input Data :Valid search query, Database value and program value and email adress.
        /// Output Data : Validation of GetRequestStatus() method.
        /// </summary>
        [Test]
        public void ValidateEbiGetRequestStatusMethodForProtein()
        {
            ValidateEbiGeneralGetRequestStatusMethod(
                Constants.EbiBlastParametersNode);
        }

        /// <summary>
        /// Validate Ebi blast result by passing search Query as parameter.
        /// Input Data :Valid search query, Database value,Email address and program value.
        /// Output Data : Validation of blast results by asynchronous fetching.
        /// </summary>
        [Test]
        public void EBIWuBlastResultsWithQueryParams()
        {
            // Gets the search query parameter and their values.
            string alphabetName = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.AlphabetNameNode);
            string querySequence = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.QuerySequency);
            string queryDatabaseValue = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.DatabaseValue);
            string emailParameter = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.Emailparameter);
            string email = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.EmailAdress);
            string queryProgramValue = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.ProgramValue);
            string queryDatabaseParameter = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.DatabaseParameter);
            string queryProgramParameter = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.ProgramParameter);
            string expectedHitId = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.HitID);
            string expectedAccession = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.HitAccession);
            string expectedResultCount = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.ResultsCount);
            string expectedHitsCount = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.HitsCount);
            string expectedEntropyStatistics = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.EntropyStatistics);
            string expectedKappaStatistics = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.KappaStatistics);
            string expectedLambdaStatistics = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.LambdaStatistics);
            string expectedLength = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.Length);

            object responseResults = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName),
                querySequence);

            // create Ebi Blast service object.
            IBlastServiceHandler service = new EbiWuBlastHandler();
            ConfigParameters configParams = new ConfigParameters();
            configParams.UseBrowserProxy = true;
            service.Configuration = configParams;

            BlastParameters searchParams = new BlastParameters();

            // Set Request parameters.
            searchParams.Add(queryDatabaseParameter, queryDatabaseValue);
            searchParams.Add(queryProgramParameter, queryProgramValue);
            searchParams.Add(emailParameter, email);

            // Create a request without passing sequence.
            string reqId = service.SubmitRequest(seq, searchParams);

            // validate request identifier.
            Assert.IsNotNull(reqId);

            // query the status
            ServiceRequestInformation info = service.GetRequestStatus(reqId);
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
                Thread.Sleep(
                    info.Status == ServiceRequestStatus.Waiting ? 20000 : 0);
                info = service.GetRequestStatus(reqId);
                ++attempt;
            }

            IBlastParser blastXmlParser = new BlastXmlParser();
            responseResults = blastXmlParser.Parse(
                    new StringReader(service.GetResult(reqId, searchParams)));

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
            Assert.AreEqual(hit.Length.ToString(), expectedLength);
            Assert.AreEqual(hit.Id.ToString((IFormatProvider)null), expectedHitId);
            Assert.AreEqual(hit.Hsps.Count.ToString((IFormatProvider)null), expectedResultCount);
            Console.WriteLine(string.Format(null,
                "Ebi Blast BVT: Hits count '{0}'.", eBlastResults.Count));
            Console.WriteLine(string.Format(null,
                "Ebi Blast BVT: Accession '{0}'.", hit.Accession));
            Console.WriteLine(string.Format(null,
                "Ebi Blast BVT: Hit Id '{0}'.", hit.Id));
            Console.WriteLine(string.Format(null,
                "Ebi Blast BVT: Hits Count '{0}'.", hit.Hsps.Count));
            Console.WriteLine(string.Format(null,
                "Ebi Blast BVT: Hits Length '{0}'.", hit.Length));
        }

        /// <summary>
        /// Validate valid Ebi blast mandatory parameters.
        /// Input data : Valid ebi parameters..
        /// Ouptut data : Validation of mandatory Ebi paramters.
        /// </summary>
        [Test]
        public void ValidateEBIWuBlastManadatoryParams()
        {
            // Gets the search query parameter and their values.
            string querySequence = Utility._xmlUtil.GetTextValue(
                Constants.BlastParametersNode, Constants.QuerySequency);
            string queryDatabaseValue = Utility._xmlUtil.GetTextValue(
                Constants.BlastParametersNode, Constants.DatabaseValue);
            string queryProgramValue = Utility._xmlUtil.GetTextValue(
                Constants.BlastParametersNode, Constants.ProgramValue);
            string queryDatabaseParameter = Utility._xmlUtil.GetTextValue(
                Constants.BlastParametersNode, Constants.DatabaseParameter);
            string queryProgramParameter = Utility._xmlUtil.GetTextValue(
                Constants.BlastParametersNode, Constants.ProgramParameter);
            string emailParameter = Utility._xmlUtil.GetTextValue(
                Constants.BlastParametersNode, Constants.Emailparameter);
            string email = Utility._xmlUtil.GetTextValue(
                Constants.BlastParametersNode, Constants.EmailAdress);

            // Set Service confiruration parameters true.
            IBlastServiceHandler service = new EbiWuBlastHandler();
            ConfigParameters configParameters = new ConfigParameters();
            configParameters.UseBrowserProxy = true;
            service.Configuration = configParameters;

            // Create search parameters object.
            BlastParameters ebiParams = new BlastParameters();

            // Add mandatory parameter values to search query parameters.
            //ebiParams.Add(querySequenceParameter, querySequence);
            ebiParams.Add(queryDatabaseParameter, queryDatabaseValue);
            ebiParams.Add(queryProgramParameter, queryProgramValue);
            ebiParams.Add(emailParameter, email);

            // Validate search query parameters.
            ParameterValidationResult validateParameters =
                EbiWuBlastHandler.ValidateParameters(ebiParams);
            bool result = validateParameters.IsValid;

            Assert.IsTrue(result);
            // Assert.IsTrue(ebiParams.Settings.ContainsValue(querySequence));
            Assert.IsTrue(ebiParams.Settings.ContainsValue(queryDatabaseValue));
            Assert.IsTrue(ebiParams.Settings.ContainsValue(queryProgramValue));
            Assert.IsTrue(ebiParams.Settings.ContainsValue(email));
            Assert.AreEqual(ebiParams.Settings.Count, 3);

            // Logs to the NUnit GUI (Console.Out) window
            Console.WriteLine(string.Format(null,
                "Ebi Blast BVT: Query Sequence{0} is as expected.", querySequence));
            Console.WriteLine(string.Format(null,
                "Ebi Blast BVT: DataBase Value{0} is as expected.", queryDatabaseValue));
            Console.WriteLine(string.Format(null,
                "Ebi Blast BVT: Program Value {0} is as expected.", queryProgramValue));
        }

        /// <summary>
        /// Validate Cancelling Submitted request for Dna Sequence query.
        /// Input Data : Dna Sequence Query.
        /// Output Data : Validation of Cancelling Submitted job.
        /// </summary>
        [Test]
        public void ValidateCancelDnaQuerySequenceRequest()
        {
            ValidateCancelSubmittedJob(Constants.EbiDnaSeqAsynchronousResultsNode);
        }

        /// <summary>
        /// Validate Cancelling Submitted request for Medium sized Dna Sequence query.
        /// Input Data : Medium sized Dna Sequence Query.
        /// Output Data : Validation of Cancelling Submitted job.
        /// </summary>
        [Test]
        public void ValidateCancelRequestForMediumSizedDnaSequence()
        {
            ValidateCancelSubmittedJob(Constants.EbiBlastMediumSizeEbiDnaSequenceParametersNode);
        }

        /// <summary>
        /// Validate Cancelling Submitted request for Medium sized Protein Sequence query.
        /// Input Data : Medium sized Protein Sequence Query.
        /// Output Data : Validation of Cancelling Submitted job.
        /// </summary>
        [Test]
        public void ValidateCancelProteinQuerySequenceRequest()
        {
            ValidateCancelSubmittedJob(
                Constants.EbiBlastMediumSizeEbiProteinSequenceParametersNode);
        }

        /// <summary>
        /// Validate EBI Webservice Properties.
        /// Input Data : Valid Config Parameter.
        /// Output Data : Validation of Ebi Service properties.
        /// </summary>
        [Test]
        public void ValidateEbiWebServiceProperties()
        {
            ConfigParameters configParams = new ConfigParameters();
            configParams.UseBrowserProxy = true;

            IBlastServiceHandler service = new EbiWuBlastHandler(configParams);

            // Validate EBI Web Service properties.
            Assert.AreEqual(Constants.EbiWebServiceDescription, service.Description);
            Assert.AreEqual(Constants.EbiWebServiceName, service.Name);

            ApplicationLog.WriteLine(
                "EbiWebService : Successfully validated the Ebi WebService Properties");
            Console.WriteLine(
                "EbiWebService : Successfully validated the Ebi WebService Properties");
        }

        #endregion EbiBlast Bvt TestCases

        #region Supported Methods

        /// <summary>
        /// Validate general fetching results.by passing 
        /// differnt parameters for Ebi web service..
        /// <param name="nodeName">xml node name.</param>
        /// <param name="isFetchSynchronous">Is Fetch Synchronous?</param>
        /// </summary>
        static void ValidateEBIWuBlastResultsFetch(
            string nodeName,
            bool isFetchSynchronous)
        {
            if (_IsWebServiceAvailable)
            {
                // Gets the search query parameter and their values.
                string alphabetName = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.AlphabetNameNode);
                string querySequence = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.QuerySequency);
                string queryDatabaseValue = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.DatabaseValue);
                string emailParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.Emailparameter);
                string email = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.EmailAdress);
                string queryProgramValue = Utility._xmlUtil.GetTextValue(
                    Constants.EbiAsynchronousResultsNode, Constants.ProgramValue);
                string queryDatabaseParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.DatabaseParameter);
                string queryProgramParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.ProgramParameter);
                string expectedHitId = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.HitID);
                string expectedAccession = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.HitAccession);
                string expectedResultCount = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.ResultsCount);
                string expectedHitsCount = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.HitsCount);
                string expectedEntropyStatistics = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.EntropyStatistics);
                string expectedKappaStatistics = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.KappaStatistics);
                string expectedLambdaStatistics = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.LambdaStatistics);
                string expectedLength = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.Length);

                object responseResults = null;
                Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName),
                    querySequence);

                // create Ebi Blast service object.
                IBlastServiceHandler service = new EbiWuBlastHandler();
                ConfigParameters configParams = new ConfigParameters();
                configParams.UseBrowserProxy = true;
                service.Configuration = configParams;

                BlastParameters searchParams = new BlastParameters();

                // Set Request parameters.
                searchParams.Add(queryDatabaseParameter, queryDatabaseValue);
                searchParams.Add(queryProgramParameter, queryProgramValue);
                searchParams.Add(emailParameter, email);

                // Create a request without passing sequence.
                string reqId = service.SubmitRequest(seq, searchParams);

                // validate request identifier.
                Assert.IsNotNull(reqId);

                // query the status
                ServiceRequestInformation info = service.GetRequestStatus(reqId);
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
                        info = service.GetRequestStatus(reqId);
                        Thread.Sleep(info.Status == ServiceRequestStatus.Waiting
                            || info.Status == ServiceRequestStatus.Queued
                            ? 20000 * attempt : 0);
                    }
                    else
                    {
                        Thread.Sleep(info.Status == ServiceRequestStatus.Waiting ? 20000 : 0);
                        info = service.GetRequestStatus(reqId);
                    }
                    ++attempt;
                }

                IBlastParser blastXmlParser = new BlastXmlParser();
                responseResults = blastXmlParser.Parse(
                        new StringReader(service.GetResult(reqId, searchParams)));

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
                Assert.AreEqual(hit.Length.ToString(), expectedLength);
                Assert.AreEqual(hit.Id.ToString((IFormatProvider)null), expectedHitId);
                Assert.AreEqual(hit.Hsps.Count.ToString((IFormatProvider)null),
                    expectedResultCount);
                Console.WriteLine(string.Format(null,
                    "Ebi Blast BVT: Hits count '{0}'.", eBlastResults.Count));
                Console.WriteLine(string.Format(null,
                    "Ebi Blast BVT: Accession '{0}'.", hit.Accession));
                Console.WriteLine(string.Format(null,
                    "Ebi Blast BVT: Hit Id '{0}'.", hit.Id));
                Console.WriteLine(string.Format(null,
                    "Ebi Blast BVT: Hits Count '{0}'.", hit.Hsps.Count));

                // Validate the results Synchronously with the results got earlier.
                if (isFetchSynchronous)
                {
                    IList<BlastResult> syncBlastResults =
                        service.FetchResultsSync(reqId, searchParams) as List<BlastResult>;
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
                        ApplicationLog.WriteLine(
                            "No significant hits found with the these parameters.");
                        Console.WriteLine("No significant hits found with the these parameters.");
                    }
                }
            }
            else
            {
                Assert.Ignore("The test case ignored due to connection failure");
            }
        }

        /// <summary>
        /// Validate general http request status by
        /// differnt parameters for Ebi web service..
        /// <param name="nodeName">different alphabet node name</param>
        /// </summary>
        static void ValidateEbiGeneralGetRequestStatusMethod(string nodeName)
        {
            if (_IsWebServiceAvailable)
            {
                // Gets the search query parameter and their values.
                string alphabetName = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.AlphabetNameNode);
                string querySequence = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.QuerySequency);
                string queryDatabaseValue = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.DatabaseValue);
                string queryProgramValue = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.ProgramValue);
                string queryDatabaseParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.DatabaseParameter);
                string queryProgramParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.ProgramParameter);
                string emailParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.Emailparameter);
                string email = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.EmailAdress);
                string reqId = string.Empty;

                // Create a sequence.
                Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName),
                    querySequence);

                // Set Service confiruration parameters true.
                IBlastServiceHandler ebiBlastService = new EbiWuBlastHandler();

                ConfigParameters configParameters = new ConfigParameters();
                configParameters.UseBrowserProxy = true;
                ebiBlastService.Configuration = configParameters;

                // Create search parameters object.
                BlastParameters queryParams = new BlastParameters();

                // Add mandatory parameter values to search query parameters.
                queryParams.Add(queryDatabaseParameter, queryDatabaseValue);
                queryParams.Add(queryProgramParameter, queryProgramValue);
                queryParams.Add(emailParameter, email);

                // Create a request 
                reqId = ebiBlastService.SubmitRequest(seq, queryParams);

                // validate request identifier.
                Assert.IsNotNull(reqId);

                // submit request identifier and get the status
                ServiceRequestInformation reqInfo = ebiBlastService.GetRequestStatus(reqId);

                // Validate job status.
                if (reqInfo.Status != ServiceRequestStatus.Waiting
                    && reqInfo.Status != ServiceRequestStatus.Ready)
                {
                    string error = ApplicationLog.WriteLine(string.Format(
                        null, "Unexpected error", reqInfo.Status));
                    Assert.Fail(error);
                    Console.WriteLine(string.Format(null,
                        "Unexpected error", reqInfo.Status));
                }
                else
                {
                    Console.WriteLine(string.Format(null,
                        "Request status {0} ", reqInfo.Status));
                }
            }
            else
            {
                Assert.Ignore("The test case ignored due to connection failure");
            }
        }

        /// <summary>
        /// Validate Cancel submitted job by passing job id.
        /// <param name="nodeName">different alphabet node name</param>
        /// </summary>
        static void ValidateCancelSubmittedJob(string nodeName)
        {
            if (_IsWebServiceAvailable)
            {
                // Gets the search query parameter and their values.
                string alphabetName = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.AlphabetNameNode);
                string querySequence = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.QuerySequency);
                string queryDatabaseValue = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.DatabaseValue);
                string emailParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.Emailparameter);
                string email = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.EmailAdress);
                string queryProgramValue = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.ProgramValue);
                string queryDatabaseParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.DatabaseParameter);
                string queryProgramParameter = Utility._xmlUtil.GetTextValue(
                    nodeName, Constants.ProgramParameter);

                Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName),
                    querySequence);

                // create Ebi Blast service object.
                IBlastServiceHandler service = new EbiWuBlastHandler();
                ConfigParameters configParams = new ConfigParameters();
                configParams.UseBrowserProxy = true;
                service.Configuration = configParams;

                BlastParameters searchParams = new BlastParameters();

                // Set Request parameters.
                searchParams.Add(queryDatabaseParameter, queryDatabaseValue);
                searchParams.Add(queryProgramParameter, queryProgramValue);
                searchParams.Add(emailParameter, email);

                // Create a request without passing sequence.
                string reqId = service.SubmitRequest(seq, searchParams);

                // Cancel subitted job.
                bool result = service.CancelRequest(reqId);

                // validate the cancelled job.
                Assert.IsTrue(result);

                Console.WriteLine(string.Format(null,
                    "EBI Blast P1 : Submitted job cancelled was successfully.",
                    queryProgramValue));
            }
            else
            {
                Assert.Ignore("The test case ignored due to connection failure");
            }
        }

        /// <summary>
        /// Validate if the connection is successful
        /// </summary>
        /// <returns>True, if connection is successful</returns>
        static bool ValidateWebServiceConnection()
        {
            // Gets the search query parameter and their values.
            string alphabetName = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.AlphabetNameNode);
            string querySequence = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.QuerySequency);
            string queryDatabaseValue = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.DatabaseValue);
            string emailParameter = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.Emailparameter);
            string email = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.EmailAdress);
            string queryProgramValue = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.ProgramValue);
            string queryDatabaseParameter = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.DatabaseParameter);
            string queryProgramParameter = Utility._xmlUtil.GetTextValue(
                Constants.EbiAsynchronousResultsNode, Constants.ProgramParameter);

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName),
                querySequence);

            // create Ebi Blast service object.
            IBlastServiceHandler service = new EbiWuBlastHandler();
            ConfigParameters configParams = new ConfigParameters();
            configParams.UseBrowserProxy = true;
            service.Configuration = configParams;

            BlastParameters searchParams = new BlastParameters();

            // Set Request parameters.
            searchParams.Add(queryDatabaseParameter, queryDatabaseValue);
            searchParams.Add(queryProgramParameter, queryProgramValue);
            searchParams.Add(emailParameter, email);

            try
            {
                // Create a request without passing sequence.
                string reqId = service.SubmitRequest(seq, searchParams);

                if (string.IsNullOrEmpty(reqId))
                {
                    Console.WriteLine(
                        "Ebi Blast Bvt : Connection not successful with no Request ID");
                    ApplicationLog.WriteLine(
                        "Ebi Blast Bvt : Connection not successful with no Request ID");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(
                    "Ebi Blast Bvt : Connection not successful with error '{0}'", ex.Message));
                ApplicationLog.WriteLine(string.Format(
                    "Ebi Blast Bvt : Connection not successful with error '{0}'", ex.Message));
                return false;
            }

            Console.WriteLine("Ebi Blast BVT : Connection Successful");
            return true;
        }

        #endregion Supported Methods
    }
}