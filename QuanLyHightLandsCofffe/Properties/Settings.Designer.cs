﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyHightLandsCofffe.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=KHABIN\\SQLEXPRESS;Initial Catalog=QLHighLandsCoffee;Integrated Securi" +
            "ty=True;Encrypt=True;TrustServerCertificate=True")]
        public string QLHighLandsCoffeeConnectionString {
            get {
                return ((string)(this["QLHighLandsCoffeeConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=KHABIN\\SQLEXPRESS;Initial Catalog=QLHightLandsCF;Integrated Security=" +
            "True;Encrypt=True;TrustServerCertificate=True")]
        public string QLHightLandsCFConnectionString {
            get {
                return ((string)(this["QLHightLandsCFConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=THCF0C\\SQLEXPRESS;Initial Catalog=QLCF_4;Integrated Security=True;Enc" +
            "rypt=True;TrustServerCertificate=True")]
        public string QLCF_4ConnectionString {
            get {
                return ((string)(this["QLCF_4ConnectionString"]));
            }
        }
    }
}
