﻿using System;
using PadenaUtil.Properties;
using Bio.Util.ArgumentParser;

namespace PadenaUtil
{
    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function of program class.
        /// </summary>
        /// <param name="args">Arguments to the main function.</param>
        public static void Main(string[] args)
        {
            DisplayErrorMessage(Resources.PadenaSplashScreen);
            try
            {
                if ((args == null) || (args.Length < 1))
                {
                    DisplayErrorMessage(Resources.PadenaUtilHelp);
                }
                else
                {
                    if (args[0].Equals("Help", StringComparison.InvariantCultureIgnoreCase))
                    {
                        DisplayErrorMessage(Resources.PadenaUtilHelp);
                    }
                    else if (args[0].Equals("Assemble", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Assemble(RemoveCommandLine(args));
                    }
                    else if (args[0].Equals("AssembleWithScaffold", StringComparison.InvariantCultureIgnoreCase))
                    {
                        AssembleWithScaffolding(RemoveCommandLine(args));
                    }
                    else if (args[0].Equals("Scaffold", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Scaffold(RemoveCommandLine(args));
                    }
                    else
                    {
                        DisplayErrorMessage(Resources.PadenaUtilHelp);
                    }
                }
            }
            catch (Exception ex)
            {
                CatchInnerException(ex);
            }
        }

        #region Private Methods

        /// <summary>
        /// Catches Inner Exception Messages.
        /// </summary>
        /// <param name="ex">Exception</param>
        private static void CatchInnerException(Exception ex)
        {
            if (ex.InnerException == null || string.IsNullOrEmpty(ex.InnerException.Message))
            {
                DisplayErrorMessage(ex.Message);
            }
            else
            {
                CatchInnerException(ex.InnerException);
            }
        }

        /// <summary>
        /// Scaffold function.
        /// </summary>
        /// <param name="args">Arguments to Scaffold function.</param>
        private static void Scaffold(string[] args)
        {
            ScaffoldArguments options = new ScaffoldArguments();
            CommandLineArguments parser = new CommandLineArguments();

            // Add the parameters
            parser.Parameter(ArgumentType.Required, "KmerLength", ArgumentValueType.OptionalInt, "k", "Length of k-mer");
            parser.Parameter(ArgumentType.Optional, "Help", ArgumentValueType.Bool, "h", "");
            parser.Parameter(ArgumentType.Optional, "OutputFile", ArgumentValueType.String, "o", "Output file");
            parser.Parameter(ArgumentType.DefaultArgument, "Filenames", ArgumentValueType.MultipleUniqueStrings, "", "Input file of reads and contigs.");
            parser.Parameter(ArgumentType.Optional, "CloneLibraryName", ArgumentValueType.String, "n", "Clone Library Name");
            parser.Parameter(ArgumentType.Optional, "MeanLengthOfInsert", ArgumentValueType.Int, "m", "Mean Length of clone library.");
            parser.Parameter(ArgumentType.Optional, "StandardDeviationOfInsert", ArgumentValueType.Int, "s", "Standard Deviation of Clone Library.");
            parser.Parameter(ArgumentType.Optional, "Redundancy", ArgumentValueType.Int, "r", "Number of paired read required to connect two contigs.");
            parser.Parameter(ArgumentType.Optional, "Depth", ArgumentValueType.Int, "d", "Depth for graph traversal.");
            parser.Parameter(ArgumentType.Optional, "Verbose", ArgumentValueType.Bool, "v", "Display verbose logging during processing.");

            if (args.Length > 0)
            {
                try
                {
                    parser.Parse(args, options);
                }
                catch (ArgumentParserException ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    Console.Error.WriteLine(Resources.ScaffoldHelp);
                    Environment.Exit(-1);
                }
                if (options.Help)
                {
                    DisplayErrorMessage(Resources.ScaffoldHelp);
                }
                else if (options.FileNames != null)
                {
                    options.GenerateScaffold();
                }
                else
                {
                    DisplayErrorMessage(Resources.ScaffoldHelp);
                }

            }
            else
            {
                DisplayErrorMessage(Resources.ScaffoldHelp);
            }
        }

        /// <summary>
        /// Assemble With Scaffolding.
        /// </summary>
        /// <param name="args">Arguments to Scaffolding.</param>
        private static void AssembleWithScaffolding(string[] args)
        {
            AssembleWithScaffoldArguments options = new AssembleWithScaffoldArguments();
            CommandLineArguments parser = new CommandLineArguments();

            // add assemble related paraemeters.
            AddAssembleParameters(parser);
            
            // Add scaffold parameters
            parser.Parameter(ArgumentType.Optional, "CloneLibraryName", ArgumentValueType.String, "n", "Clone Library Name");
            parser.Parameter(ArgumentType.Optional, "MeanLengthOfInsert", ArgumentValueType.Int, "m", "Mean Length of clone library.");
            parser.Parameter(ArgumentType.Optional, "StandardDeviationOfInsert", ArgumentValueType.Int, "s", "Standard Deviation of Clone Library.");
            parser.Parameter(ArgumentType.Optional, "Redundancy", ArgumentValueType.Int, "b", "Number of paired read required to connect two contigs.");
            parser.Parameter(ArgumentType.Optional, "Depth", ArgumentValueType.Int, "f", "Depth for graph traversal.");

            if (args.Length > 0)
            {
                try
                {
                    parser.Parse(args, options);
                }
                catch (ArgumentParserException ex)
                {
                    DisplayErrorMessage(ex.Message);
                    DisplayErrorMessage(Resources.AssembleWithScaffoldHelp);
                    Environment.Exit(-1);
                }

                if (options.Help)
                {
                    DisplayErrorMessage(Resources.AssembleWithScaffoldHelp);
                }
                else
                {
                    options.AssembleSequences();
                }
            }
            else
            {
                DisplayErrorMessage(Resources.AssembleWithScaffoldHelp);
            }
        }

        /// <summary>
        /// Assemble function.
        /// </summary>
        /// <param name="args">Arguments to Assemble.</param>
        private static void Assemble(string[] args)
        {
            AssembleArguments options = new AssembleArguments();
            CommandLineArguments parser = new CommandLineArguments();

            AddAssembleParameters(parser);

            if (args.Length > 0)
            {
                try
                {
                    parser.Parse(args, options);
                }
                catch (ArgumentParserException ex)
                {
                    DisplayErrorMessage(ex.Message);
                    DisplayErrorMessage(Resources.AssembleHelp);
                    Environment.Exit(-1);
                }
                if (options.Help)
                {
                    DisplayErrorMessage(Resources.AssembleHelp);
                }
                else
                {
                    options.AssembleSequences();
                }
              
            }
            else
            {
                DisplayErrorMessage(Resources.AssembleHelp);
            }
        }

        private static void AddAssembleParameters(CommandLineArguments parser)
        {
            // Add the parameters to be parsed
            parser.Parameter(ArgumentType.Optional, "KmerLength", ArgumentValueType.OptionalInt, "k", "Length of k-mer");
            parser.Parameter(ArgumentType.Optional, "DangleThreshold", ArgumentValueType.OptionalInt, "d", "Threshold for removing dangling ends in graph");
            parser.Parameter(ArgumentType.Optional, "RedundantPathLengthThreshold", ArgumentValueType.OptionalInt, "r", "Length Threshold for removing redundant paths in graph");
            parser.Parameter(ArgumentType.Optional, "ErosionThreshold", ArgumentValueType.OptionalInt, "e", "Threshold for eroding low coverage ends");
            parser.Parameter(ArgumentType.Optional, "AllowErosion", ArgumentValueType.Bool, "i", "Bool to do erosion or not.");
            parser.Parameter(ArgumentType.Optional, "AllowKmerLengthEstimation", ArgumentValueType.Bool, "a", "Whether to estimate kmer length.");
            parser.Parameter(ArgumentType.Optional, "ContigCoverageThreshold", ArgumentValueType.Int, "c", "Threshold used for removing low-coverage contigs.");
            parser.Parameter(ArgumentType.Optional, "LowCoverageContigRemovalEnabled", ArgumentValueType.Bool, "l", "Enable removal of low coverage contigs.");
            parser.Parameter(ArgumentType.Optional, "Help", ArgumentValueType.Bool, "h", "");
            parser.Parameter(ArgumentType.Optional, "OutputFile", ArgumentValueType.String, "o", "Output file");
            parser.Parameter(ArgumentType.Optional, "Verbose", ArgumentValueType.Bool, "v", "Display verbose logging during processing.");
            parser.Parameter(ArgumentType.DefaultArgument, "Filename", ArgumentValueType.String, "", "Input file of reads");
        }

        /// <summary>
        /// Display error message on console.
        /// </summary>
        /// <param name="message">Error message.</param>
        private static void DisplayErrorMessage(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Removes the command line.
        /// </summary>
        /// <param name="args">Arguments to remove.</param>
        /// <returns>Returns the arguments.</returns>
        private static string[] RemoveCommandLine(string[] args)
        {
            string[] arguments = new string[args.Length - 1];
            for (int index = 0; index < args.Length - 1; index++)
            {
                arguments[index] = args[index + 1];
            }

            return arguments;
        }

        #endregion
    }
}
