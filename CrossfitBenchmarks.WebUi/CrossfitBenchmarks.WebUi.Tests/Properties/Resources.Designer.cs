﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrossfitBenchmarks.WebUi.Tests.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CrossfitBenchmarks.WebUi.Tests.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;odata.metadata&quot;:&quot;http://localhost:33203/odata/$metadata#WorkoutLogs&quot;,&quot;odata.count&quot;:&quot;20&quot;,&quot;value&quot;:[
        ///    {
        ///      &quot;DateCreated&quot;:&quot;2013-03-18T11:48:49.509-06:00&quot;,&quot;DateOfWod&quot;:&quot;2013-03-18T11:48:00-06:00&quot;,&quot;IsAPersonalRecord&quot;:false,&quot;Score&quot;:&quot;test&quot;,&quot;UserId&quot;:3,&quot;UserNameIdentifier&quot;:&quot;100000144938400&quot;,&quot;WorkoutId&quot;:3,&quot;WorkoutLogId&quot;:&quot;252&quot;,&quot;WorkoutName&quot;:&quot;Deadlift&quot;,&quot;Notes&quot;:&quot;test&quot;,&quot;WorkoutType&quot;:&quot;Benchmark&quot;,&quot;WorkoutTypeId&quot;:&quot;B&quot;
        ///    },{
        ///      &quot;DateCreated&quot;:&quot;2013-03-12T12:36:03.3904-06:00&quot;,&quot;DateOfWod&quot;:&quot;2013-03-12T12:30:00- [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TestJson {
            get {
                return ResourceManager.GetString("TestJson", resourceCulture);
            }
        }
    }
}
