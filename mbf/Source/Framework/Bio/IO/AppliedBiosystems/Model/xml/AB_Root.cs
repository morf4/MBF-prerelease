﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// This is generated based on the xml converter tool provided by applied biosystems.  It is mainly used for test purposes for validing the bio parser.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class AB_Root
{

   private object[] itemsField;

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute("Data", typeof(AB_RootData), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   [System.Xml.Serialization.XmlElementAttribute("Header", typeof(AB_RootHeader), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   [System.Xml.Serialization.XmlElementAttribute("Properties", typeof(AB_RootProperties), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public object[] Items
   {
      get
      {
         return this.itemsField;
      }
      set
      {
         this.itemsField = value;
      }
   }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AB_RootData
{

   private AB_RootDataTag[] tagField;

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute("Tag", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public AB_RootDataTag[] Tag
   {
      get
      {
         return this.tagField;
      }
      set
      {
         this.tagField = value;
      }
   }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AB_RootDataTag
{

   private string nameField;

   private string idField;

   private string typeField;

   private string elementsField;

   private string sizeField;

   private string valueField;

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Name
   {
      get
      {
         return this.nameField;
      }
      set
      {
         this.nameField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string ID
   {
      get
      {
         return this.idField;
      }
      set
      {
         this.idField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Type
   {
      get
      {
         return this.typeField;
      }
      set
      {
         this.typeField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Elements
   {
      get
      {
         return this.elementsField;
      }
      set
      {
         this.elementsField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Size
   {
      get
      {
         return this.sizeField;
      }
      set
      {
         this.sizeField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Value
   {
      get
      {
         return this.valueField;
      }
      set
      {
         this.valueField = value;
      }
   }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AB_RootHeader
{

   private string versionField;

   private string byteOrderField;

   private string oS_Reference_NumberField;

   private string directory_Tag_NameField;

   private string directory_Tag_NumberField;

   private string directory_TypeField;

   private string directory_ElementsField;

   private string swapSizeField;

   private string access_ModeField;

   private string next_Free_PositionField;

   private string file_ExtendField;

   private string dir_ExtendField;

   private string next_FileField;

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Version
   {
      get
      {
         return this.versionField;
      }
      set
      {
         this.versionField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string ByteOrder
   {
      get
      {
         return this.byteOrderField;
      }
      set
      {
         this.byteOrderField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string OS_Reference_Number
   {
      get
      {
         return this.oS_Reference_NumberField;
      }
      set
      {
         this.oS_Reference_NumberField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Directory_Tag_Name
   {
      get
      {
         return this.directory_Tag_NameField;
      }
      set
      {
         this.directory_Tag_NameField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Directory_Tag_Number
   {
      get
      {
         return this.directory_Tag_NumberField;
      }
      set
      {
         this.directory_Tag_NumberField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Directory_Type
   {
      get
      {
         return this.directory_TypeField;
      }
      set
      {
         this.directory_TypeField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Directory_Elements
   {
      get
      {
         return this.directory_ElementsField;
      }
      set
      {
         this.directory_ElementsField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string SwapSize
   {
      get
      {
         return this.swapSizeField;
      }
      set
      {
         this.swapSizeField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Access_Mode
   {
      get
      {
         return this.access_ModeField;
      }
      set
      {
         this.access_ModeField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Next_Free_Position
   {
      get
      {
         return this.next_Free_PositionField;
      }
      set
      {
         this.next_Free_PositionField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string File_Extend
   {
      get
      {
         return this.file_ExtendField;
      }
      set
      {
         this.file_ExtendField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Dir_Extend
   {
      get
      {
         return this.dir_ExtendField;
      }
      set
      {
         this.dir_ExtendField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Next_File
   {
      get
      {
         return this.next_FileField;
      }
      set
      {
         this.next_FileField = value;
      }
   }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AB_RootProperties
{

   private string creatorField;

   private string date_CreatedField;

   private string data_SourceField;

   private string data_Source_Last_ModifiedField;

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Creator
   {
      get
      {
         return this.creatorField;
      }
      set
      {
         this.creatorField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Date_Created
   {
      get
      {
         return this.date_CreatedField;
      }
      set
      {
         this.date_CreatedField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Data_Source
   {
      get
      {
         return this.data_SourceField;
      }
      set
      {
         this.data_SourceField = value;
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
   public string Data_Source_Last_Modified
   {
      get
      {
         return this.data_Source_Last_ModifiedField;
      }
      set
      {
         this.data_Source_Last_ModifiedField = value;
      }
   }
}
