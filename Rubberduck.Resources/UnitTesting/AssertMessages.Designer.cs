﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rubberduck.Resources.UnitTesting {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AssertMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AssertMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Rubberduck.Resources.UnitTesting.AssertMessages", typeof(AssertMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected COM exception while running tests..
        /// </summary>
        public static string Assert_ComException {
            get {
                return ResourceManager.GetString("Assert_ComException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to expected has {0} dimensions; actual has {1} dimensions. {2}.
        /// </summary>
        public static string Assert_DimensionMismatchFormat {
            get {
                return ResourceManager.GetString("Assert_DimensionMismatchFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OK, Rubberduck asserted.  Now what?.
        /// </summary>
        public static string Assert_EasterEggAssertClassPassed {
            get {
                return ResourceManager.GetString("Assert_EasterEggAssertClassPassed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected: Stack overflow?; Actual: Guard clause..
        /// </summary>
        public static string Assert_EasterEggIFakePassed {
            get {
                return ResourceManager.GetString("Assert_EasterEggIFakePassed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IVerify too..
        /// </summary>
        public static string Assert_EasterEggIVerifyPassed {
            get {
                return ResourceManager.GetString("Assert_EasterEggIVerifyPassed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} assertion failed. {1}.
        /// </summary>
        public static string Assert_FailedMessageFormat {
            get {
                return ResourceManager.GetString("Assert_FailedMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected exception while running tests..
        /// </summary>
        public static string Assert_GenericException {
            get {
                return ResourceManager.GetString("Assert_GenericException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid setup of IFake {0}. PassThrough property must be False..
        /// </summary>
        public static string Assert_InvalidFakePassThrough {
            get {
                return ResourceManager.GetString("Assert_InvalidFakePassThrough", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dimension {0}: expected has an LBound of {1}; actual has an LBound of {2}. {3}.
        /// </summary>
        public static string Assert_LBoundMismatchFormat {
            get {
                return ResourceManager.GetString("Assert_LBoundMismatchFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [expected] and [actual] values are not the same type..
        /// </summary>
        public static string Assert_MismatchedTypes {
            get {
                return ResourceManager.GetString("Assert_MismatchedTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Neither [expected] or [actual] is an array..
        /// </summary>
        public static string Assert_NeitherParameterIsArray {
            get {
                return ResourceManager.GetString("Assert_NeitherParameterIsArray", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not implemented..
        /// </summary>
        public static string Assert_NotImplemented {
            get {
                return ResourceManager.GetString("Assert_NotImplemented", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not an array..
        /// </summary>
        public static string Assert_ParameterIsNotArrayFormat {
            get {
                return ResourceManager.GetString("Assert_ParameterIsNotArrayFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected: {0}; Actual: {1}. {2}.
        /// </summary>
        public static string Assert_ParameterResultFormat {
            get {
                return ResourceManager.GetString("Assert_ParameterResultFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [expected] is a reference type and [actual] is a value type..
        /// </summary>
        public static string Assert_ReferenceValueMismatch {
            get {
                return ResourceManager.GetString("Assert_ReferenceValueMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dimension {0}: expected has a UBound of {1}; actual has a UBound of {2}. {3}.
        /// </summary>
        public static string Assert_UBoundMismatchFormat {
            get {
                return ResourceManager.GetString("Assert_UBoundMismatchFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [expected] and [actual] are arrays. Consider using {0}..
        /// </summary>
        public static string Assert_UnexpectedArrayFormat {
            get {
                return ResourceManager.GetString("Assert_UnexpectedArrayFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [expected] and [actual] are Nothing. Consider using {0}..
        /// </summary>
        public static string Assert_UnexpectedNullArraysFormat {
            get {
                return ResourceManager.GetString("Assert_UnexpectedNullArraysFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [expected] and [actual] are reference types. Consider using {0}..
        /// </summary>
        public static string Assert_UnexpectedReferenceComparisonFormat {
            get {
                return ResourceManager.GetString("Assert_UnexpectedReferenceComparisonFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [expected] and [actual] are value types. Consider using {0}..
        /// </summary>
        public static string Assert_UnexpectedValueComparisonFormat {
            get {
                return ResourceManager.GetString("Assert_UnexpectedValueComparisonFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [expected] is a value type and [actual] is a reference type..
        /// </summary>
        public static string Assert_ValueReferenceMismatch {
            get {
                return ResourceManager.GetString("Assert_ValueReferenceMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rubberduck could not process the invocation results..
        /// </summary>
        public static string Assert_VerifyInternalErrorMessage {
            get {
                return ResourceManager.GetString("Assert_VerifyInternalErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No matching invocation for parameter {0}; Only {1} invocations. {2}.
        /// </summary>
        public static string Assert_VerifyNoInvocationFormat {
            get {
                return ResourceManager.GetString("Assert_VerifyNoInvocationFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter {0} was not a numeric value on invocation {1}. {2}.
        /// </summary>
        public static string Assert_VerifyParameterNonNumeric {
            get {
                return ResourceManager.GetString("Assert_VerifyParameterNonNumeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter {0} was not passed on invocation {1}. {2}.
        /// </summary>
        public static string Assert_VerifyParameterNotPassed {
            get {
                return ResourceManager.GetString("Assert_VerifyParameterNotPassed", resourceCulture);
            }
        }
    }
}
