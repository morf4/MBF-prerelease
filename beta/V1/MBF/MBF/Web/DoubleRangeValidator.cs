﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System;
using MBF.Properties;
using MBF.Util.Logging;

namespace MBF.Web
{
    /// <summary>
    /// A validator for double values that defines an inclusive (both first and last) range of 
    /// allowed values.
    /// </summary>
    public class DoubleRangeValidator : IParameterValidator
    {
        /// <summary>
        /// The lowest allowed value.
        /// </summary>
        public double First { get; set; }

        /// <summary>
        /// The highest value allowed.
        /// </summary>
        public double Last { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="first">The lowest value.</param>
        /// <param name="last">The highest value.</param>
        public DoubleRangeValidator(double first, double last)
        {
            if (first > last)
            {
                string message = "DoubleRangeValidator: Invalid arguments.";
                Trace.Report(message);
                throw new ArgumentOutOfRangeException("first", Resource.ARGUMENT_OUT_OF_RANGE);
            }
            First = first;
            Last = last;
        }

        /// <summary>
        /// Given an int value as an object, return true if the value is in-range.
        /// </summary>
        /// <param name="parameterValue">The value.</param>
        /// <returns>True if the value is valid.</returns>
        public bool IsValid(object parameterValue)
        {
            double? val = parameterValue as double?;
            if (val == null)
            {
                return false;
            }
            if (val.Value < First || val.Value > Last)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Given an int value as a string, return true if the value is in-range.
        /// </summary>
        /// <param name="parameterValue">The value.</param>
        /// <returns>True if the value is valid.</returns>
        public bool IsValid(string parameterValue)
        {
            double val = 0;
            if (!double.TryParse(parameterValue, out val))
            {
                return false;
            }
            if (val < First || val > Last)
            {
                return false;
            }
            return true;
        }
    }
}
