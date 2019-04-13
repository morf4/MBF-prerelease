﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.Win32;

namespace MBF.TemplateWizard
{
    /// <summary>
    /// Entry point of wizard which will be invoked by VS
    /// </summary>
    public class VSTemplateWizard : IWizard
    {
        /// <summary>
        /// Key containing installation path of MBF
        /// </summary>
        private const string MBFRegistryInstallationPathKeyName = "InstallationPath";

        /// <summary>
        /// Called by VS before opening a project item
        /// </summary>
        /// <param name="projectItem">Item being opened</param>
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        /// <summary>
        /// Called by VS after generating the project
        /// </summary>
        /// <param name="project">Project which got generated</param>
        public void ProjectFinishedGenerating(Project project)
        {   
        }

        /// <summary>
        /// Called by VS after loading a project item
        /// </summary>
        /// <param name="projectItem">Item which is loaded.</param>
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        /// <summary>
        /// Called by VS once any custom code is done executing
        /// </summary>
        public void RunFinished()
        {
        }

        /// <summary>
        /// Called by VS before initiating any other project creation activity.
        /// Any customizations / wizards goes in here.
        /// </summary>
        /// <param name="automationObject">VS application object. (DTE object)</param>
        /// <param name="replacementsDictionary">Dictionary which holds name-value pairs to make replacements of placeholders in any project item</param>
        /// <param name="runKind">Context of item creation. (ex: project / project item)</param>
        /// <param name="customParams">Any other environment variables set by VS</param>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (replacementsDictionary == null)
            {
                replacementsDictionary = new Dictionary<string, string>();
            }

            // Show the wizard
            using (WizardForm wizard = new WizardForm())
            {
                Application.EnableVisualStyles();
                wizard.ShowDialog();

                // Replace placeholder for MBF reference.
                replacementsDictionary["$MBFReferencePath$"] = wizard.MBFAssemblyPath + @"mbf.dll";

                // Load snippets and push it to code file
                StringBuilder finalSnips = new StringBuilder();
                List<string> namespaces = new List<string>();
                List<string> assemblies = new List<string>();

                // Get what snippets are selected by user
                foreach (string tag in wizard.SnippetTags)
                {
                    Snippet currentSnip = Snippets.GetSnippet(tag);
                    if (currentSnip != null)
                    {
                        // Get the code snippet
                        finalSnips.Append(currentSnip.Code);

                        // Add any namespaces required
                        foreach (string namespaceRef in currentSnip.Namespaces)
                        {
                            if (!namespaces.Contains(namespaceRef))
                            {
                                namespaces.Add(namespaceRef);
                            }
                        }

                        // Check for any assembly references
                        if (!string.IsNullOrEmpty(currentSnip.Assembly))
                        {
                            assemblies.Add(currentSnip.Assembly);
                        }
                    }
                }

                // Add code snippets to file
                replacementsDictionary["$CodeSnippets$"] = finalSnips.ToString().Trim();

                // Add necessary namespaces to file
                StringBuilder finalNamespaces = new StringBuilder();
                foreach (string namespaceRef in namespaces)
                {
                    finalNamespaces.AppendLine("using " + namespaceRef);
                }
                replacementsDictionary["$MBFNamespaces$"] = finalNamespaces.ToString().Trim();

                // TODO: find this path from registry
                // Add additional assemblies if required
                StringBuilder finalAssemblies = new StringBuilder();
                if (assemblies.Contains("WebServiceHandlers"))
                {
                    string assemblyReference = "<Reference Include=\"MBF.WebServiceHandlers\">" + Environment.NewLine +
                                                    @"<HintPath>" + wizard.MBFAssemblyPath + @"MBF.WebServiceHandlers.dll</HintPath>" + Environment.NewLine +
                                                "</Reference>";
                    finalAssemblies.AppendLine(assemblyReference);
                }
                if (assemblies.Contains("PAMSAM"))
                {
                    string assemblyReference = "<Reference Include=\"MBF.PAMSAM\">" + Environment.NewLine +
                                                    @"<HintPath>" + wizard.MBFAssemblyPath + @"Addin\MBF.PAMSAM.dll</HintPath>" + Environment.NewLine +
                                                "</Reference>";
                    finalAssemblies.AppendLine(assemblyReference);
                }
                if (assemblies.Contains("PaDeNA"))
                {
                    string assemblyReference = "<Reference Include=\"MBF.PaDeNA\">" + Environment.NewLine +
                                                    @"<HintPath>" + wizard.MBFAssemblyPath + @"Addin\MBF.PaDeNA.dll</HintPath>" + Environment.NewLine +
                                                "</Reference>";
                    finalAssemblies.AppendLine(assemblyReference);
                }
                replacementsDictionary["$OtherAssemblies$"] = finalAssemblies.ToString().Trim();

            }
        }

        /// <summary>
        /// Called by VS to check if a particular item should be added to the project
        /// </summary>
        /// <param name="filePath">Path to the item being added</param>
        /// <returns>True to add, false not to add.</returns>
        public bool ShouldAddProjectItem(string filePath)
        {
            // Always return true as we dont want to skip any files in the template.
            return true;
        }
    }
}
