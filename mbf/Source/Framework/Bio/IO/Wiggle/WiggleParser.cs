﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Bio.IO.Wiggle
{
    /// <summary>
    /// Implementation of wiggle parser with support for fixed/variable step formats.
    /// BED Wiggle is not supported in this implementation as its obselete.
    /// </summary>
    public class WiggleParser
    {
        /// <summary>
        /// Gets the name of this parser.
        /// </summary>
        public string Name
        {
            get { return Properties.Resource.WiggleName; }
        }

        /// <summary>
        /// Gets a short description of this parser.
        /// </summary>
        public string Description
        {
            get { return Properties.Resource.WiggleParserDescription; }
        }

        /// <summary>
        /// Gets the known file extensions for Wiggle files.
        /// </summary>
        public string FileTypes
        {
            get { return Properties.Resource.Wiggle_FileExtension; }
        }
        
        /// <summary>
        /// Parse a wiggle file.
        /// </summary>
        /// <param name="fileName">File to parse.</param>
        /// <returns>WiggleAnnotation object.</returns>
        public WiggleAnnotation Parse(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            using (StreamReader reader = new StreamReader(fileName))
            {
                return this.Parse(reader);
            }
        }

        /// <summary>
        /// Parse a wiggle file.
        /// </summary>
        /// <param name="reader">Stream to parse.</param>
        /// <returns>WiggleAnnotation object.</returns>
        public WiggleAnnotation Parse(StreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (reader.EndOfStream)
            {
                return null;
            }

            string line;
            WiggleAnnotation result = ParseHeader(reader);

            if (result.AnnotationType == WiggleAnnotationType.FixedStep)
            {
                List<float> fixedStepValues = new List<float>();
                while ((line = reader.ReadLine()) != null)
                {
                    fixedStepValues.Add(float.Parse(line.Trim(), CultureInfo.InvariantCulture));
                }

                result.SetFixedStepAnnotationData(fixedStepValues.ToArray());
            }
            else
            {
                List<KeyValuePair<long, float>> variableStepValues = new List<KeyValuePair<long, float>>();
                try
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        string[] keyValue = line.Split(' ', '\t');
                        variableStepValues.Add(new KeyValuePair<long, float>(long.Parse(keyValue[0], CultureInfo.InvariantCulture), float.Parse(keyValue[1], CultureInfo.InvariantCulture)));
                    }
                }
                catch
                {
                    throw new FileFormatException(Properties.Resource.WiggleBadInputInFile);
                }

                result.SetVariableStepAnnotationData(variableStepValues.ToArray());
            }

            return result;
        }

        /// <summary>
        /// Parse wiggle header including track line and metadata.
        /// </summary>
        /// <param name="reader">Stream reader to parse.</param>
        /// <returns>Wiggle annotation object initialized with data from the header.</returns>
        private static WiggleAnnotation ParseHeader(StreamReader reader)
        {
            WiggleAnnotation result = new WiggleAnnotation();

            string line = reader.ReadLine();
            
            // read comments
            while (line != null && (line.StartsWith(WiggleSchema.CommentLineStart, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(line)))
            {
                result.Comments.Add(line);
                line = reader.ReadLine();
            }

            if (line == null || !line.StartsWith(WiggleSchema.Track + " " , StringComparison.Ordinal))
            {
                throw new FormatException(Properties.Resource.WiggleInvalidHeader);
            }

            try
            {
                result.Metadata = ExtractMetadata(line.Substring((WiggleSchema.Track + " ").Length));
            }
            catch
            {
                throw new FormatException(Properties.Resource.WiggleInvalidHeader);
            }

            // step and span details
            line = reader.ReadLine();
            if (line.StartsWith(WiggleSchema.FixedStep + " ", StringComparison.Ordinal))
            {
                result.AnnotationType = WiggleAnnotationType.FixedStep;
            }
            else if (line.StartsWith(WiggleSchema.VariableStep + " ", StringComparison.Ordinal))
            {
                result.AnnotationType = WiggleAnnotationType.VariableStep;
            }
            else
            {
                throw new FormatException(Properties.Resource.WiggleInvalidHeader);
            }

            string[] tokens = line.Split(' ');
            Dictionary<string, string> encodingDetails = new Dictionary<string, string>();
            for (int i = 1; i < tokens.Length; i++)
            {
                string[] metadataArray = tokens[i].Split('=');
                encodingDetails.Add(metadataArray[0], metadataArray[1]);
            }

            try
            {
                result.Chromosome = encodingDetails[WiggleSchema.Chrom];
                string spanString;
                if (encodingDetails.TryGetValue(WiggleSchema.Span, out spanString))
                {
                    result.Span = int.Parse(spanString, CultureInfo.InvariantCulture);
                }

                if (result.AnnotationType == WiggleAnnotationType.FixedStep)
                {
                    result.Step = int.Parse(encodingDetails[WiggleSchema.Step], CultureInfo.InvariantCulture);
                    result.BasePosition = long.Parse(encodingDetails[WiggleSchema.Start], CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                throw new FormatException(Properties.Resource.WiggleInvalidHeader);
            }

            return result;
        }

        /// <summary>
        /// Reads the track line and converts to key value pairs.
        /// </summary>
        /// <param name="trackLine">Track line.</param>
        /// <returns>Track line data in key-value format.</returns>
        private static Dictionary<string, string> ExtractMetadata(string trackLine)
        {
            List<string> tokens = trackLine.Split(' ').ToList();
            int i = 0;

            // Values might be enclosed in double-quotes to support spaces in values. 
            // (space is a delimited if not inside double-quotes.)
            while (i < tokens.Count)
            {
                if (!tokens[i].Contains('='))
                {
                    tokens[i - 1] += " " + tokens[i];
                    tokens.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            Dictionary<string, string> metadata = new Dictionary<string, string>();

            // Remove quotes from values and add to result.
            foreach (string token in tokens)
            {
                string[] metadataArray = token.Split('=');
                metadata.Add(metadataArray[0], metadataArray[1].Replace("\"", string.Empty));
            }

            return metadata;
        }
    }
}
