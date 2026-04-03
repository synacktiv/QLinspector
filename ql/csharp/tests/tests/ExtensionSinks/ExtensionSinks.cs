// Auto-generated ExtensionSinks.cs.cs

using MS.Internal.AppModel;
using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Internal;
using System.Configuration;
using System.Data;
using System.Data.Design;
using System.Data.Linq;
using System.DirectoryServices;
using System.IO;
using System.IO.IsolatedStorage;
using System.IdentityModel.Tokens;
using System.Management;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Security;
using System.Security.Claims;
using System.Security.Policy;
using System.ServiceModel.Channels;
using System.Text;
using System.Transactions;
using System.Transactions.Oletx;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Util;
using System.Windows;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace Test {
    public class ExtensionSink {

        // Simulated tainted input
        string taintedString;
        byte[] taintedBytes;
        MemoryStream taintedStream;
        TextReader taintedReader;
        XmlReader taintedXmlReader;
        BinaryReader taintedBinaryReader;

        public ExtensionSink() {
            taintedString = "tainted_input";
            taintedBytes = Encoding.UTF8.GetBytes(taintedString);
            taintedStream = new MemoryStream(taintedBytes);
            taintedReader = new StringReader(taintedString);
            taintedXmlReader = XmlReader.Create(taintedReader);
            taintedBinaryReader = new BinaryReader(taintedStream);
        }

        // #- ['System.Security.Claims', 'ClaimsIdentity', True, 'ClaimsIdentity', '(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ClaimsIdentity() {
            var tmp = new System.Security.Claims.ClaimsIdentity(default(System.Runtime.Serialization.SerializationInfo), default(System.Runtime.Serialization.StreamingContext));
        }

        // #- ['System.Security.Claims', 'ClaimsPrincipal', True, 'ClaimsPrincipal', '(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ClaimsPrincipal() {
            var tmp = new System.Security.Claims.ClaimsPrincipal(default(System.Runtime.Serialization.SerializationInfo), default(System.Runtime.Serialization.StreamingContext));
        }

        // #- ['System.IdentityModel.Tokens', 'SessionSecurityTokenHandler', True, 'ReadToken', '(System.Byte[],System.IdentityModel.Selectors.SecurityTokenResolver)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadToken() {
            var obj = new System.IdentityModel.Tokens.SessionSecurityTokenHandler();
            obj.ReadToken(taintedBytes, default(System.IdentityModel.Selectors.SecurityTokenResolver));
        }

        // #- ['System.IdentityModel.Tokens', 'SessionSecurityTokenHandler', True, 'ReadToken', '(System.Xml.XmlReader,System.IdentityModel.Selectors.SecurityTokenResolver)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadToken1() {
            var obj = new System.IdentityModel.Tokens.SessionSecurityTokenHandler();
            obj.ReadToken(taintedXmlReader, default(System.IdentityModel.Selectors.SecurityTokenResolver));
        }

        // #- ['System.IdentityModel.Tokens', 'SessionSecurityTokenHandler', True, 'ReadToken', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadToken2() {
            var obj = new System.IdentityModel.Tokens.SessionSecurityTokenHandler();
            obj.ReadToken(taintedXmlReader);
        }

        // #- ['System.Activities.Presentation', 'WorkflowDesigner', True, 'set_PropertyInspectorFontAndColorData', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_set_PropertyInspectorFontAndColorData() {
            var obj = new System.Activities.Presentation.WorkflowDesigner();
            obj.set_PropertyInspectorFontAndColorData(taintedString);
        }

        // #- ['System.Activities.Presentation.Internal', 'ManifestImages+XamlImageInfo', True, 'XamlImageInfo', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_XamlImageInfo() {
            var tmp = new System.Activities.Presentation.Internal.ManifestImages.XamlImageInfo(taintedStream);
        }

        // #- ['System.Data.Linq', 'DBConvert', True, 'ChangeType', '(System.Object,System.Type)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ChangeType() {
            var obj = new System.Data.Linq.DBConvert();
            obj.ChangeType(default(System.Object), default(System.Type));
        }

        // #- ['System.Data.Linq', 'DBConvert', True, 'ChangeType<T>', '(System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ChangeType<T>() {
            var obj = new System.Data.Linq.DBConvert();
            obj.ChangeType<T>(default(System.Object));
        }

        // #- ['System.Security.Policy', 'ApplicationTrust', True, 'FromXml', '(System.Security.SecurityElement)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_FromXml() {
            var obj = new System.Security.Policy.ApplicationTrust();
            obj.FromXml(default(System.Security.SecurityElement));
        }

        // #- ['System.Web.Caching', 'OutputCache', True, 'Deserialize', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Deserialize() {
            var obj = new System.Web.Caching.OutputCache();
            obj.Deserialize(taintedStream);
        }

        // #- ['System.Web.Util', 'AltSerialization', True, 'ReadValueFromStream', '(System.IO.BinaryReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadValueFromStream() {
            var obj = new System.Web.Util.AltSerialization();
            obj.ReadValueFromStream(taintedBinaryReader);
        }

        // #- ['System.Web', 'HttpStaticObjectsCollection', True, 'Deserialize', '(System.IO.BinaryReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Deserialize1() {
            var obj = new System.Web.HttpStaticObjectsCollection();
            obj.Deserialize(taintedBinaryReader);
        }

        // #- ['System.Web.SessionState', 'SessionStateItemCollection', True, 'Deserialize', '(System.IO.BinaryReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Deserialize2() {
            var obj = new System.Web.SessionState.SessionStateItemCollection();
            obj.Deserialize(taintedBinaryReader);
        }

        // #- ['System.Security', 'SecurityException', True, 'ByteArrayToObject', '(System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ByteArrayToObject() {
            var obj = new System.Security.SecurityException();
            obj.ByteArrayToObject(taintedBytes);
        }

        // #- ['System.Web.Security', 'RolePrincipal', True, 'RolePrincipal', '(System.Security.Principal.IIdentity,System.String)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_RolePrincipal() {
            var tmp = new System.Web.Security.RolePrincipal(default(System.Security.Principal.IIdentity), taintedString);
        }

        // #- ['System.Web.Security', 'RolePrincipal', True, 'RolePrincipal', '(System.String,System.Security.Principal.IIdentity,System.String)', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_RolePrincipal1() {
            var tmp = new System.Web.Security.RolePrincipal(default(System.String), default(System.Security.Principal.IIdentity), taintedString);
        }

        // #- ['System.Web.Security', 'RolePrincipal', True, 'InitFromEncryptedTicket', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_InitFromEncryptedTicket() {
            var obj = new System.Web.Security.RolePrincipal();
            obj.InitFromEncryptedTicket(taintedString);
        }

        // #- ['System.ServiceModel.Channels', 'MsmqDecodeHelper', True, 'DeserializeForIntegration', '(System.ServiceModel.MsmqIntegration.MsmqIntegrationChannelListener,System.IO.Stream,System.ServiceModel.MsmqIntegration.MsmqIntegrationMessageProperty,System.Int64)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_DeserializeForIntegration() {
            var obj = new System.ServiceModel.Channels.MsmqDecodeHelper();
            obj.DeserializeForIntegration(default(System.ServiceModel.MsmqIntegration.MsmqIntegrationChannelListener), taintedStream, default(System.ServiceModel.MsmqIntegration.MsmqIntegrationMessageProperty), default(System.Int64));
        }

        // #- ['MS.Internal.AppModel', 'ApplicationProxyInternal', True, 'DeserializeJournaledObject', '(System.IO.MemoryStream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DeserializeJournaledObject() {
            var obj = new MS.Internal.AppModel.ApplicationProxyInternal();
            obj.DeserializeJournaledObject(default(System.IO.MemoryStream));
        }

        // #- ['MS.Internal.AppModel', 'DataStreams', True, 'LoadSubStreams', '(System.Windows.UIElement,System.Collections.ArrayList)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_LoadSubStreams() {
            var obj = new MS.Internal.AppModel.DataStreams();
            obj.LoadSubStreams(default(System.Windows.UIElement), default(System.Collections.ArrayList));
        }

        // #- ['System.Transactions.Oletx', 'OletxResourceManager', True, 'Reenlist', '(System.Int32,System.Byte[],System.Transactions.IEnlistmentNotificationInternal)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_Reenlist() {
            var obj = new System.Transactions.Oletx.OletxResourceManager();
            obj.Reenlist(default(System.Int32), taintedBytes, default(System.Transactions.IEnlistmentNotificationInternal));
        }

        // #- ['System.Transactions', 'TransactionManager', True, 'Reenlist', '(System.Guid,System.Byte[],System.Transactions.IEnlistmentNotification)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_Reenlist1() {
            var obj = new System.Transactions.TransactionManager();
            obj.Reenlist(default(System.Guid), taintedBytes, default(System.Transactions.IEnlistmentNotification));
        }

        // #- ['System.IO.IsolatedStorage', 'IsolatedStorage', True, 'InitStore', '(System.IO.IsolatedStorage.IsolatedStorageScope,System.IO.Stream,System.IO.Stream,System.IO.Stream,System.String,System.String,System.String)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_InitStore() {
            var obj = new System.IO.IsolatedStorage.IsolatedStorage();
            obj.InitStore(default(System.IO.IsolatedStorage.IsolatedStorageScope), taintedStream, default(System.IO.Stream), default(System.IO.Stream), default(System.String), default(System.String), default(System.String));
        }

        // #- ['System.IO.IsolatedStorage', 'IsolatedStorage', True, 'InitStore', '(System.IO.IsolatedStorage.IsolatedStorageScope,System.IO.Stream,System.IO.Stream,System.IO.Stream,System.String,System.String,System.String)', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_InitStore1() {
            var obj = new System.IO.IsolatedStorage.IsolatedStorage();
            obj.InitStore(default(System.IO.IsolatedStorage.IsolatedStorageScope), default(System.IO.Stream), taintedStream, default(System.IO.Stream), default(System.String), default(System.String), default(System.String));
        }

        // #- ['System.IO.IsolatedStorage', 'IsolatedStorage', True, 'InitStore', '(System.IO.IsolatedStorage.IsolatedStorageScope,System.IO.Stream,System.IO.Stream,System.IO.Stream,System.String,System.String,System.String)', '', 'Argument[3]', 'gadget-sink', 'manual']
        public void Test_InitStore2() {
            var obj = new System.IO.IsolatedStorage.IsolatedStorage();
            obj.InitStore(default(System.IO.IsolatedStorage.IsolatedStorageScope), default(System.IO.Stream), default(System.IO.Stream), taintedStream, default(System.String), default(System.String), default(System.String));
        }

        // #- ['System.Windows', 'DataObject', True, 'SetData', '(System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_SetData() {
            var obj = new System.Windows.DataObject();
            obj.SetData(default(System.Object));
        }

        // #- ['System.Windows', 'DataObject', True, 'SetData', '(System.String,System.Object)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_SetData1() {
            var obj = new System.Windows.DataObject();
            obj.SetData(default(System.String), default(System.Object));
        }

        // #- ['System.Windows', 'DataObject', True, 'SetData', '(System.Type,System.Object)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_SetData2() {
            var obj = new System.Windows.DataObject();
            obj.SetData(default(System.Type), default(System.Object));
        }

        // #- ['System.Windows', 'DataObject', True, 'SetData', '(System.String,System.Object,System.Boolean)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_SetData3() {
            var obj = new System.Windows.DataObject();
            obj.SetData(default(System.String), default(System.Object), default(System.Boolean));
        }

        // #- ['System.Windows.Forms', 'DataObject', True, 'SetData', '(System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_SetData4() {
            var obj = new System.Windows.Forms.DataObject();
            obj.SetData(default(System.Object));
        }

        // #- ['System.Windows.Forms', 'DataObject+OleConverter', True, 'SetData', '(System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_SetData5() {
            var obj = new System.Windows.Forms.DataObject.OleConverter();
            obj.SetData(default(System.Object));
        }

        // #- ['System.Windows.Forms', 'DataObject', True, 'SetData', '(System.String,System.Object)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_SetData6() {
            var obj = new System.Windows.Forms.DataObject();
            obj.SetData(default(System.String), default(System.Object));
        }

        // #- ['System.Windows.Forms', 'DataObject+OleConverter', True, 'SetData', '(System.String,System.Object)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_SetData7() {
            var obj = new System.Windows.Forms.DataObject.OleConverter();
            obj.SetData(default(System.String), default(System.Object));
        }

        // #- ['System.Windows.Forms', 'DataObject', True, 'SetData', '(System.Type,System.Object)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_SetData8() {
            var obj = new System.Windows.Forms.DataObject();
            obj.SetData(default(System.Type), default(System.Object));
        }

        // #- ['System.Windows.Forms', 'DataObject+OleConverter', True, 'SetData', '(System.Type,System.Object)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_SetData9() {
            var obj = new System.Windows.Forms.DataObject.OleConverter();
            obj.SetData(default(System.Type), default(System.Object));
        }

        // #- ['System.Windows.Forms', 'DataObject', True, 'SetData', '(System.String,System.Boolean,System.Object)', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_SetData10() {
            var obj = new System.Windows.Forms.DataObject();
            obj.SetData(default(System.String), default(System.Boolean), default(System.Object));
        }

        // #- ['System.Windows.Forms', 'DataObject+OleConverter', True, 'SetData', '(System.String,System.Boolean,System.Object)', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_SetData11() {
            var obj = new System.Windows.Forms.DataObject.OleConverter();
            obj.SetData(default(System.String), default(System.Boolean), default(System.Object));
        }

        // #- ['System.Windows.Forms', 'DataObject+OleConverter', True, 'ReadObjectFromHandle', '(System.IntPtr,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadObjectFromHandle() {
            var obj = new System.Windows.Forms.DataObject.OleConverter();
            obj.ReadObjectFromHandle(default(System.IntPtr), default(System.Boolean));
        }

        // #- ['System.Windows.Forms', 'DataObject+OleConverter', True, 'ReadObjectFromHandleDeserializer', '(System.IO.Stream,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadObjectFromHandleDeserializer() {
            var obj = new System.Windows.Forms.DataObject.OleConverter();
            obj.ReadObjectFromHandleDeserializer(taintedStream, default(System.Boolean));
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedStream);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.IO.Stream,System.Data.XmlReadMode)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml1() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedStream, default(System.Data.XmlReadMode));
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.IO.TextReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml2() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedReader);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.IO.TextReader,System.Data.XmlReadMode)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml3() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedReader, default(System.Data.XmlReadMode));
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml4() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedString);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.String,System.Data.XmlReadMode)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml5() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedString, default(System.Data.XmlReadMode));
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml6() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedXmlReader);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.Xml.XmlReader,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml7() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedXmlReader, default(System.Boolean));
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.Xml.XmlReader,System.Data.XmlReadMode)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml8() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedXmlReader, default(System.Data.XmlReadMode));
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXml', '(System.Xml.XmlReader,System.Data.XmlReadMode,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml9() {
            var obj = new System.Data.DataSet();
            obj.ReadXml(taintedXmlReader, default(System.Data.XmlReadMode), default(System.Boolean));
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXmlDiffgram', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlDiffgram() {
            var obj = new System.Data.DataSet();
            obj.ReadXmlDiffgram(taintedXmlReader);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXmlSchema', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema() {
            var obj = new System.Data.DataSet();
            obj.ReadXmlSchema(taintedStream);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXmlSchema', '(System.IO.TextReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema1() {
            var obj = new System.Data.DataSet();
            obj.ReadXmlSchema(taintedReader);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXmlSchema', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema2() {
            var obj = new System.Data.DataSet();
            obj.ReadXmlSchema(taintedString);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXmlSchema', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema3() {
            var obj = new System.Data.DataSet();
            obj.ReadXmlSchema(taintedXmlReader);
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXmlSchema', '(System.Xml.XmlReader,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema4() {
            var obj = new System.Data.DataSet();
            obj.ReadXmlSchema(taintedXmlReader, default(System.Boolean));
        }

        // #- ['System.Data', 'DataSet', True, 'ReadXmlSerializable', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSerializable() {
            var obj = new System.Data.DataSet();
            obj.ReadXmlSerializable(taintedXmlReader);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXml', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml10() {
            var obj = new System.Data.DataTable();
            obj.ReadXml(taintedStream);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXml', '(System.IO.TextReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml11() {
            var obj = new System.Data.DataTable();
            obj.ReadXml(taintedReader);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXml', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml12() {
            var obj = new System.Data.DataTable();
            obj.ReadXml(taintedString);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXml', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml13() {
            var obj = new System.Data.DataTable();
            obj.ReadXml(taintedXmlReader);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXml', '(System.Xml.XmlReader,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml14() {
            var obj = new System.Data.DataTable();
            obj.ReadXml(taintedXmlReader, default(System.Boolean));
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXml', '(System.Xml.XmlReader,System.Data.XmlReadMode,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXml15() {
            var obj = new System.Data.DataTable();
            obj.ReadXml(taintedXmlReader, default(System.Data.XmlReadMode), default(System.Boolean));
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXmlDiffgram', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlDiffgram1() {
            var obj = new System.Data.DataTable();
            obj.ReadXmlDiffgram(taintedXmlReader);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXmlSchema', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema5() {
            var obj = new System.Data.DataTable();
            obj.ReadXmlSchema(taintedStream);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXmlSchema', '(System.IO.TextReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema6() {
            var obj = new System.Data.DataTable();
            obj.ReadXmlSchema(taintedReader);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXmlSchema', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema7() {
            var obj = new System.Data.DataTable();
            obj.ReadXmlSchema(taintedString);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXmlSchema', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema8() {
            var obj = new System.Data.DataTable();
            obj.ReadXmlSchema(taintedXmlReader);
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXmlSchema', '(System.Xml.XmlReader,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSchema9() {
            var obj = new System.Data.DataTable();
            obj.ReadXmlSchema(taintedXmlReader, default(System.Boolean));
        }

        // #- ['System.Data', 'DataTable', True, 'ReadXmlSerializable', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ReadXmlSerializable1() {
            var obj = new System.Data.DataTable();
            obj.ReadXmlSerializable(taintedXmlReader);
        }

        // #- ['System.Data.Design', 'MethodSignatureGenerator', True, 'SetMethodSourceContent', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_SetMethodSourceContent() {
            var obj = new System.Data.Design.MethodSignatureGenerator();
            obj.SetMethodSourceContent(taintedString);
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider,System.Collections.Hashtable)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate1() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider), default(System.Collections.Hashtable));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider,System.Collections.Hashtable,System.Data.Design.TypedDataSetGenerator+GenerateOption)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate2() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider), default(System.Collections.Hashtable), default(System.Data.Design.TypedDataSetGenerator.GenerateOption));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider,System.Collections.Hashtable,System.Data.Design.TypedDataSetGenerator+GenerateOption,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate3() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider), default(System.Collections.Hashtable), default(System.Data.Design.TypedDataSetGenerator.GenerateOption), default(System.String));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider,System.Collections.Hashtable,System.Data.Design.TypedDataSetGenerator+GenerateOption,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate4() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider), default(System.Collections.Hashtable), default(System.Data.Design.TypedDataSetGenerator.GenerateOption), default(System.String), default(System.String));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider,System.Data.Common.DbProviderFactory)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate5() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider), default(System.Data.Common.DbProviderFactory));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider,System.Data.Design.TypedDataSetGenerator+GenerateOption)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate6() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider), default(System.Data.Design.TypedDataSetGenerator.GenerateOption));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider,System.Data.Design.TypedDataSetGenerator+GenerateOption,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate7() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider), default(System.Data.Design.TypedDataSetGenerator.GenerateOption), default(System.String));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'Generate', '(System.String,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.CodeDom.Compiler.CodeDomProvider,System.Data.Design.TypedDataSetGenerator+GenerateOption,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Generate8() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.Generate(taintedString, default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.CodeDom.Compiler.CodeDomProvider), default(System.Data.Design.TypedDataSetGenerator.GenerateOption), default(System.String), default(System.String));
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'GetProviderName', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetProviderName() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.GetProviderName(taintedString);
        }

        // #- ['System.Data.Design', 'TypedDataSetGenerator', True, 'GetProviderName', '(System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetProviderName1() {
            var obj = new System.Data.Design.TypedDataSetGenerator();
            obj.GetProviderName(taintedString, default(System.String));
        }

        // #- ['System.Data.Design', 'TypedDataSetSchemaImporterExtension', True, 'ImportSchemaType', '(System.String,System.String,System.Xml.Schema.XmlSchemaObject,System.Xml.Serialization.XmlSchemas,System.Xml.Serialization.XmlSchemaImporter,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.Xml.Serialization.CodeGenerationOptions,System.CodeDom.Compiler.CodeDomProvider)', '', 'Argument[3]', 'gadget-sink', 'manual']
        public void Test_ImportSchemaType() {
            var obj = new System.Data.Design.TypedDataSetSchemaImporterExtension();
            obj.ImportSchemaType(default(System.String), default(System.String), default(System.Xml.Schema.XmlSchemaObject), default(System.Xml.Serialization.XmlSchemas), default(System.Xml.Serialization.XmlSchemaImporter), default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.Xml.Serialization.CodeGenerationOptions), default(System.CodeDom.Compiler.CodeDomProvider));
        }

        // #- ['System.Data.Design', 'TypedDataSetSchemaImporterExtension', True, 'ImportSchemaType', '(System.Xml.Schema.XmlSchemaType,System.Xml.Schema.XmlSchemaObject,System.Xml.Serialization.XmlSchemas,System.Xml.Serialization.XmlSchemaImporter,System.CodeDom.CodeCompileUnit,System.CodeDom.CodeNamespace,System.Xml.Serialization.CodeGenerationOptions,System.CodeDom.Compiler.CodeDomProvider)', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_ImportSchemaType1() {
            var obj = new System.Data.Design.TypedDataSetSchemaImporterExtension();
            obj.ImportSchemaType(default(System.Xml.Schema.XmlSchemaType), default(System.Xml.Schema.XmlSchemaObject), default(System.Xml.Serialization.XmlSchemas), default(System.Xml.Serialization.XmlSchemaImporter), default(System.CodeDom.CodeCompileUnit), default(System.CodeDom.CodeNamespace), default(System.Xml.Serialization.CodeGenerationOptions), default(System.CodeDom.Compiler.CodeDomProvider));
        }

        // #- ['System.Windows.Forms', 'Control+ActiveXImpl', True, 'Load', '(System.Windows.Forms.UnsafeNativeMethods+IStream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load() {
            var obj = new System.Windows.Forms.Control.ActiveXImpl();
            obj.Load(default(System.Windows.Forms.UnsafeNativeMethods.IStream));
        }

        // #- ['System.Windows.Forms', 'Control+ActiveXImpl', True, 'Load', '(System.Windows.Forms.UnsafeNativeMethods+IPropertyBag,System.Windows.Forms.UnsafeNativeMethods+IErrorLog)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load1() {
            var obj = new System.Windows.Forms.Control.ActiveXImpl();
            obj.Load(default(System.Windows.Forms.UnsafeNativeMethods.IPropertyBag), default(System.Windows.Forms.UnsafeNativeMethods.IErrorLog));
        }

        // #- ['System.Windows.Forms', 'Control+ActiveXImpl+PropertyBagStream', True, 'Read', '(System.Windows.Forms.UnsafeNativeMethods+IStream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Read() {
            var obj = new System.Windows.Forms.Control.ActiveXImpl.PropertyBagStream();
            obj.Read(default(System.Windows.Forms.UnsafeNativeMethods.IStream));
        }

        // #- ['System.Security.Policy', 'ApplicationTrust', True, 'ObjectFromXml', '(System.Security.SecurityElement)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ObjectFromXml() {
            var obj = new System.Security.Policy.ApplicationTrust();
            obj.ObjectFromXml(default(System.Security.SecurityElement));
        }

        // #- ['System.IO', 'Path', True, 'GetFullPath', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetFullPath() {
            var obj = new System.IO.Path();
            obj.GetFullPath(taintedString);
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.Type)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.Type));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.Type,System.Type)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance1() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.Type), default(System.Type));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.Type,System.Type,System.Type,System.Type,System.Type)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance2() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.Type), default(System.Type), default(System.Type), default(System.Type), default(System.Type));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance3() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.Type,System.Type,System.Type,System.Type,System.Type,System.Type)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance4() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.Type), default(System.Type), default(System.Type), default(System.Type), default(System.Type), default(System.Type));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.Type,System.Type,System.Type)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance5() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.Type), default(System.Type), default(System.Type));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance6() {
            var obj = new System.Activator();
            obj.CreateInstance(taintedString, default(System.String));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.String,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance7() {
            var obj = new System.Activator();
            obj.CreateInstance(taintedString, default(System.String), default(System.String));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance8() {
            var obj = new System.Activator();
            obj.CreateInstance(taintedString, default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance9() {
            var obj = new System.Activator();
            obj.CreateInstance(taintedString, default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance10() {
            var obj = new System.Activator();
            obj.CreateInstance(taintedString, default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String), default(System.String));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.AppDomain,System.AppDomain,System.AppDomain)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance11() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.AppDomain), default(System.AppDomain), default(System.AppDomain));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain,System.AppDomain)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance12() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain), default(System.AppDomain));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.ActivationContext)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance13() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.ActivationContext));
        }

        // #- ['System', 'Activator', True, 'CreateInstance', '(System.ActivationContext,System.ActivationContext)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_CreateInstance14() {
            var obj = new System.Activator();
            obj.CreateInstance(default(System.ActivationContext), default(System.ActivationContext));
        }

        // #- ['System', 'AppDomain', True, 'SetData', '(System.String,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_SetData12() {
            var obj = new System.AppDomain();
            obj.SetData(taintedString, default(System.Object));
        }

        // #- ['System', 'AppDomain', True, 'SetData', '(System.String,System.Object)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_SetData13() {
            var obj = new System.AppDomain();
            obj.SetData(default(System.String), default(System.Object));
        }

        // #- ['System', 'AppDomain', True, 'Deserialize', '(System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Deserialize3() {
            var obj = new System.AppDomain();
            obj.Deserialize(taintedBytes);
        }

        // #- ['System.DirectoryServices', 'DirectoryEntry', True, 'DirectoryEntry', '(System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DirectoryEntry() {
            var tmp = new System.DirectoryServices.DirectoryEntry(default(System.Object));
        }

        // #- ['System.DirectoryServices', 'DirectoryEntry', True, 'DirectoryEntry', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DirectoryEntry1() {
            var tmp = new System.DirectoryServices.DirectoryEntry(taintedString);
        }

        // #- ['System.DirectoryServices', 'DirectoryEntry', True, 'DirectoryEntry', '(System.String,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DirectoryEntry2() {
            var tmp = new System.DirectoryServices.DirectoryEntry(taintedString, default(System.String), default(System.String));
        }

        // #- ['System.DirectoryServices', 'DirectoryEntry', True, 'DirectoryEntry', '(System.String,System.String,System.String,System.DirectoryServices.AuthenticationTypes)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DirectoryEntry3() {
            var tmp = new System.DirectoryServices.DirectoryEntry(taintedString, default(System.String), default(System.String), default(System.DirectoryServices.AuthenticationTypes));
        }

        // #- ['System.Resources', 'ResourceReader', True, 'ResourceReader', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ResourceReader() {
            var tmp = new System.Resources.ResourceReader(taintedString);
        }

        // #- ['System.Resources', 'ResourceReader', True, 'ResourceReader', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ResourceReader1() {
            var tmp = new System.Resources.ResourceReader(taintedStream);
        }

        // #- ['System.Resources', 'ResourceManager', True, 'GetObject', '(System.String,System.Globalization.CultureInfo)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetObject() {
            var obj = new System.Resources.ResourceManager();
            obj.GetObject(taintedString, default(System.Globalization.CultureInfo));
        }

        // #- ['System.Resources', 'ResourceManager', True, 'GetObject', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetObject1() {
            var obj = new System.Resources.ResourceManager();
            obj.GetObject(taintedString);
        }

        // #- ['System.Resources', 'ResourceSet', True, 'ResourceSet', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ResourceSet() {
            var tmp = new System.Resources.ResourceSet(taintedString);
        }

        // #- ['System.Resources', 'ResourceSet', True, 'ResourceSet', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ResourceSet1() {
            var tmp = new System.Resources.ResourceSet(taintedStream);
        }

        // #- ['System.Configuration', 'SettingsPropertyValue', True, 'SettingsPropertyValue', '(System.Configuration.SettingsProperty)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_SettingsPropertyValue() {
            var tmp = new System.Configuration.SettingsPropertyValue(default(System.Configuration.SettingsProperty));
        }

        // #- ['System', 'AppDomainSetup', True, 'set_ApplicationBase', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_set_ApplicationBase() {
            var obj = new System.AppDomainSetup();
            obj.set_ApplicationBase(taintedString);
        }

        // #- ['System', 'ActivationContext', True, 'ActivationContext', '(System.ApplicationIdentity,System.String[])', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_ActivationContext() {
            var tmp = new System.ActivationContext(default(System.ApplicationIdentity), default(System.String[]));
        }

        // #- ['System', 'ActivationContext', True, 'CreateFromNameAndManifests', '(System.ApplicationIdentity,System.String[])', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_CreateFromNameAndManifests() {
            var obj = new System.ActivationContext();
            obj.CreateFromNameAndManifests(default(System.ApplicationIdentity), default(System.String[]));
        }

        // #- ['System', 'ActivationContext', True, 'CreatePartialActivationContext', '(System.ApplicationIdentity,System.String[])', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_CreatePartialActivationContext() {
            var obj = new System.ActivationContext();
            obj.CreatePartialActivationContext(default(System.ApplicationIdentity), default(System.String[]));
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'AddCommand', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_AddCommand() {
            var obj = new System.Management.Automation.PowerShell();
            obj.AddCommand(taintedString);
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'AddScript', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_AddScript() {
            var obj = new System.Management.Automation.PowerShell();
            obj.AddScript(taintedString);
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'Create', '(System.Management.Automation.RunspaceMode)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Create() {
            var obj = new System.Management.Automation.PowerShell();
            obj.Create(default(System.Management.Automation.RunspaceMode));
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'AddCommand', '(System.Management.Automation.Runspaces.Command)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_AddCommand1() {
            var obj = new System.Management.Automation.PowerShell();
            obj.AddCommand(default(System.Management.Automation.Runspaces.Command));
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'AddCommand', '(System.Management.Automation.CommandInfo)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_AddCommand2() {
            var obj = new System.Management.Automation.PowerShell();
            obj.AddCommand(default(System.Management.Automation.CommandInfo));
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'Create', '(System.Management.Automation.Runspaces.InitialSessionState)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Create1() {
            var obj = new System.Management.Automation.PowerShell();
            obj.Create(default(System.Management.Automation.Runspaces.InitialSessionState));
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'Create', '(System.Boolean,System.Management.Automation.PSCommand,System.Collections.ObjectModel.Collection<System.Management.Automation.PSCommand>)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Create2() {
            var obj = new System.Management.Automation.PowerShell();
            obj.Create(default(System.Boolean), default(System.Management.Automation.PSCommand), default(System.Collections.ObjectModel.Collection<System.Management.Automation.PSCommand>));
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'AddCommand', '(System.String,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_AddCommand3() {
            var obj = new System.Management.Automation.PowerShell();
            obj.AddCommand(taintedString, default(System.Boolean));
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'AddScript', '(System.String,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_AddScript1() {
            var obj = new System.Management.Automation.PowerShell();
            obj.AddScript(taintedString, default(System.Boolean));
        }

        // #- ['System.Management.Automation', 'PowerShell', True, 'set_Commands', '(System.Management.Automation.PSCommand)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_set_Commands() {
            var obj = new System.Management.Automation.PowerShell();
            obj.set_Commands(default(System.Management.Automation.PSCommand));
        }

        // #- ['System.Reflection', 'Assembly', True, 'LoadFrom', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_LoadFrom() {
            var obj = new System.Reflection.Assembly();
            obj.LoadFrom(taintedString);
        }

        // #- ['System.Reflection', 'Assembly', True, 'LoadFile', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_LoadFile() {
            var obj = new System.Reflection.Assembly();
            obj.LoadFile(taintedString);
        }

        // #- ['System.Reflection', 'Assembly', True, 'Load', '(System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load2() {
            var obj = new System.Reflection.Assembly();
            obj.Load(taintedBytes);
        }

        // #- ['System.Reflection', 'Assembly', True, 'Load', '(System.Byte[],System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load3() {
            var obj = new System.Reflection.Assembly();
            obj.Load(taintedBytes, default(System.Byte[]));
        }

        // #- ['System.Net', 'WebRequest', True, 'Create', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Create3() {
            var obj = new System.Net.WebRequest();
            obj.Create(default(System.Uri));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenRead', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenRead() {
            var obj = new System.Net.WebClient();
            obj.OpenRead(default(System.Uri));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenWrite', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenWrite() {
            var obj = new System.Net.WebClient();
            obj.OpenWrite(default(System.Uri));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadData', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadData() {
            var obj = new System.Net.WebClient();
            obj.DownloadData(default(System.Uri));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadString', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadString() {
            var obj = new System.Net.WebClient();
            obj.DownloadString(default(System.Uri));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenReadAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenReadAsync() {
            var obj = new System.Net.WebClient();
            obj.OpenReadAsync(default(System.Uri));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenWriteAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenWriteAsync() {
            var obj = new System.Net.WebClient();
            obj.OpenWriteAsync(default(System.Uri));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadStringAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadStringAsync() {
            var obj = new System.Net.WebClient();
            obj.DownloadStringAsync(default(System.Uri));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadDataAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadDataAsync() {
            var obj = new System.Net.WebClient();
            obj.DownloadDataAsync(default(System.Uri));
        }

        // #- ['System.Net', 'WebRequest', True, 'Create', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Create4() {
            var obj = new System.Net.WebRequest();
            obj.Create(taintedString);
        }

        // #- ['System.Net', 'WebClient', True, 'OpenRead', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenRead1() {
            var obj = new System.Net.WebClient();
            obj.OpenRead(taintedString);
        }

        // #- ['System.Net', 'WebClient', True, 'OpenWrite', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenWrite1() {
            var obj = new System.Net.WebClient();
            obj.OpenWrite(taintedString);
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadData', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadData1() {
            var obj = new System.Net.WebClient();
            obj.DownloadData(taintedString);
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadString', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadString1() {
            var obj = new System.Net.WebClient();
            obj.DownloadString(taintedString);
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadFile', '(System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadFile() {
            var obj = new System.Net.WebClient();
            obj.DownloadFile(taintedString, default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadFile', '(System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadFile() {
            var obj = new System.Net.WebClient();
            obj.UploadFile(taintedString, default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenWrite', '(System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenWrite2() {
            var obj = new System.Net.WebClient();
            obj.OpenWrite(taintedString, default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadString', '(System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadString() {
            var obj = new System.Net.WebClient();
            obj.UploadString(taintedString, default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadFile', '(System.Uri,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadFile1() {
            var obj = new System.Net.WebClient();
            obj.DownloadFile(default(System.Uri), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadFile', '(System.Uri,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadFile1() {
            var obj = new System.Net.WebClient();
            obj.UploadFile(default(System.Uri), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenWrite', '(System.Uri,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenWrite3() {
            var obj = new System.Net.WebClient();
            obj.OpenWrite(default(System.Uri), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadString', '(System.Uri,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadString1() {
            var obj = new System.Net.WebClient();
            obj.UploadString(default(System.Uri), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenWriteAsync', '(System.Uri,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenWriteAsync1() {
            var obj = new System.Net.WebClient();
            obj.OpenWriteAsync(default(System.Uri), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadFileAsync', '(System.Uri,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadFileAsync() {
            var obj = new System.Net.WebClient();
            obj.DownloadFileAsync(default(System.Uri), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadStringAsync', '(System.Uri,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadStringAsync() {
            var obj = new System.Net.WebClient();
            obj.UploadStringAsync(default(System.Uri), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadFileAsync', '(System.Uri,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadFileAsync() {
            var obj = new System.Net.WebClient();
            obj.UploadFileAsync(default(System.Uri), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadData', '(System.String,System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadData() {
            var obj = new System.Net.WebClient();
            obj.UploadData(taintedString, default(System.Byte[]));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadData', '(System.Uri,System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadData1() {
            var obj = new System.Net.WebClient();
            obj.UploadData(default(System.Uri), default(System.Byte[]));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadDataAsync', '(System.Uri,System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadDataAsync() {
            var obj = new System.Net.WebClient();
            obj.UploadDataAsync(default(System.Uri), default(System.Byte[]));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadData', '(System.String,System.String,System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadData2() {
            var obj = new System.Net.WebClient();
            obj.UploadData(taintedString, default(System.String), default(System.Byte[]));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadData', '(System.Uri,System.String,System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadData3() {
            var obj = new System.Net.WebClient();
            obj.UploadData(default(System.Uri), default(System.String), default(System.Byte[]));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadDataAsync', '(System.Uri,System.String,System.Byte[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadDataAsync1() {
            var obj = new System.Net.WebClient();
            obj.UploadDataAsync(default(System.Uri), default(System.String), default(System.Byte[]));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadFile', '(System.String,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadFile2() {
            var obj = new System.Net.WebClient();
            obj.UploadFile(taintedString, default(System.String), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadString', '(System.String,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadString2() {
            var obj = new System.Net.WebClient();
            obj.UploadString(taintedString, default(System.String), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadFile', '(System.Uri,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadFile3() {
            var obj = new System.Net.WebClient();
            obj.UploadFile(default(System.Uri), default(System.String), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadString', '(System.Uri,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadString3() {
            var obj = new System.Net.WebClient();
            obj.UploadString(default(System.Uri), default(System.String), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadStringAsync', '(System.Uri,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadStringAsync1() {
            var obj = new System.Net.WebClient();
            obj.UploadStringAsync(default(System.Uri), default(System.String), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadFileAsync', '(System.Uri,System.String,System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadFileAsync1() {
            var obj = new System.Net.WebClient();
            obj.UploadFileAsync(default(System.Uri), default(System.String), default(System.String));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadValues', '(System.String,System.Collections.Specialized.NameValueCollection)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadValues() {
            var obj = new System.Net.WebClient();
            obj.UploadValues(taintedString, default(System.Collections.Specialized.NameValueCollection));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadValues', '(System.Uri,System.Collections.Specialized.NameValueCollection)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadValues1() {
            var obj = new System.Net.WebClient();
            obj.UploadValues(default(System.Uri), default(System.Collections.Specialized.NameValueCollection));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadValuesAsync', '(System.Uri,System.Collections.Specialized.NameValueCollection)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadValuesAsync() {
            var obj = new System.Net.WebClient();
            obj.UploadValuesAsync(default(System.Uri), default(System.Collections.Specialized.NameValueCollection));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadValues', '(System.String,System.String,System.Collections.Specialized.NameValueCollection)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadValues2() {
            var obj = new System.Net.WebClient();
            obj.UploadValues(taintedString, default(System.String), default(System.Collections.Specialized.NameValueCollection));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadValues', '(System.Uri,System.String,System.Collections.Specialized.NameValueCollection)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadValues3() {
            var obj = new System.Net.WebClient();
            obj.UploadValues(default(System.Uri), default(System.String), default(System.Collections.Specialized.NameValueCollection));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadValuesAsync', '(System.Uri,System.String,System.Collections.Specialized.NameValueCollection)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadValuesAsync1() {
            var obj = new System.Net.WebClient();
            obj.UploadValuesAsync(default(System.Uri), default(System.String), default(System.Collections.Specialized.NameValueCollection));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenReadAsync', '(System.Uri,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenReadAsync1() {
            var obj = new System.Net.WebClient();
            obj.OpenReadAsync(default(System.Uri), default(System.Object));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadStringAsync', '(System.Uri,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadStringAsync1() {
            var obj = new System.Net.WebClient();
            obj.DownloadStringAsync(default(System.Uri), default(System.Object));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadDataAsync', '(System.Uri,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadDataAsync1() {
            var obj = new System.Net.WebClient();
            obj.DownloadDataAsync(default(System.Uri), default(System.Object));
        }

        // #- ['System.Net', 'WebClient', True, 'OpenWriteAsync', '(System.Uri,System.String,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_OpenWriteAsync2() {
            var obj = new System.Net.WebClient();
            obj.OpenWriteAsync(default(System.Uri), default(System.String), default(System.Object));
        }

        // #- ['System.Net', 'WebClient', True, 'DownloadFileAsync', '(System.Uri,System.String,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DownloadFileAsync1() {
            var obj = new System.Net.WebClient();
            obj.DownloadFileAsync(default(System.Uri), default(System.String), default(System.Object));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadStringAsync', '(System.Uri,System.String,System.String,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadStringAsync2() {
            var obj = new System.Net.WebClient();
            obj.UploadStringAsync(default(System.Uri), default(System.String), default(System.String), default(System.Object));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadFileAsync', '(System.Uri,System.String,System.String,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadFileAsync2() {
            var obj = new System.Net.WebClient();
            obj.UploadFileAsync(default(System.Uri), default(System.String), default(System.String), default(System.Object));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadDataAsync', '(System.Uri,System.String,System.Byte[],System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadDataAsync2() {
            var obj = new System.Net.WebClient();
            obj.UploadDataAsync(default(System.Uri), default(System.String), default(System.Byte[]), default(System.Object));
        }

        // #- ['System.Net', 'WebClient', True, 'UploadValuesAsync', '(System.Uri,System.String,System.Collections.Specialized.NameValueCollection,System.Object)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_UploadValuesAsync2() {
            var obj = new System.Net.WebClient();
            obj.UploadValuesAsync(default(System.Uri), default(System.String), default(System.Collections.Specialized.NameValueCollection), default(System.Object));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'set_BaseAddress', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_set_BaseAddress() {
            var obj = new System.Net.Http.HttpClient();
            obj.set_BaseAddress(default(System.Uri));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetStringAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetStringAsync() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetStringAsync(default(System.Uri));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetByteArrayAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetByteArrayAsync() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetByteArrayAsync(default(System.Uri));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetStreamAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetStreamAsync() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetStreamAsync(default(System.Uri));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetAsync() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetAsync(default(System.Uri));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'DeleteAsync', '(System.Uri)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DeleteAsync() {
            var obj = new System.Net.Http.HttpClient();
            obj.DeleteAsync(default(System.Uri));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetStringAsync', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetStringAsync1() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetStringAsync(taintedString);
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetByteArrayAsync', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetByteArrayAsync1() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetByteArrayAsync(taintedString);
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetStreamAsync', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetStreamAsync1() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetStreamAsync(taintedString);
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetAsync', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetAsync1() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetAsync(taintedString);
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'DeleteAsync', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DeleteAsync1() {
            var obj = new System.Net.Http.HttpClient();
            obj.DeleteAsync(taintedString);
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetAsync', '(System.String,System.Net.Http.HttpCompletionOption)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetAsync2() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetAsync(taintedString, default(System.Net.Http.HttpCompletionOption));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetAsync', '(System.Uri,System.Net.Http.HttpCompletionOption)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetAsync3() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetAsync(default(System.Uri), default(System.Net.Http.HttpCompletionOption));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetAsync', '(System.String,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetAsync4() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetAsync(taintedString, default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'DeleteAsync', '(System.String,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DeleteAsync2() {
            var obj = new System.Net.Http.HttpClient();
            obj.DeleteAsync(taintedString, default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetAsync', '(System.Uri,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetAsync5() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetAsync(default(System.Uri), default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'DeleteAsync', '(System.Uri,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DeleteAsync3() {
            var obj = new System.Net.Http.HttpClient();
            obj.DeleteAsync(default(System.Uri), default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetAsync', '(System.String,System.Net.Http.HttpCompletionOption,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetAsync6() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetAsync(taintedString, default(System.Net.Http.HttpCompletionOption), default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'GetAsync', '(System.Uri,System.Net.Http.HttpCompletionOption,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetAsync7() {
            var obj = new System.Net.Http.HttpClient();
            obj.GetAsync(default(System.Uri), default(System.Net.Http.HttpCompletionOption), default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'PostAsync', '(System.String,System.Net.Http.HttpContent)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_PostAsync() {
            var obj = new System.Net.Http.HttpClient();
            obj.PostAsync(taintedString, default(System.Net.Http.HttpContent));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'PutAsync', '(System.String,System.Net.Http.HttpContent)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_PutAsync() {
            var obj = new System.Net.Http.HttpClient();
            obj.PutAsync(taintedString, default(System.Net.Http.HttpContent));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'PostAsync', '(System.Uri,System.Net.Http.HttpContent)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_PostAsync1() {
            var obj = new System.Net.Http.HttpClient();
            obj.PostAsync(default(System.Uri), default(System.Net.Http.HttpContent));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'PutAsync', '(System.Uri,System.Net.Http.HttpContent)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_PutAsync1() {
            var obj = new System.Net.Http.HttpClient();
            obj.PutAsync(default(System.Uri), default(System.Net.Http.HttpContent));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'PostAsync', '(System.String,System.Net.Http.HttpContent,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_PostAsync2() {
            var obj = new System.Net.Http.HttpClient();
            obj.PostAsync(taintedString, default(System.Net.Http.HttpContent), default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'PutAsync', '(System.String,System.Net.Http.HttpContent,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_PutAsync2() {
            var obj = new System.Net.Http.HttpClient();
            obj.PutAsync(taintedString, default(System.Net.Http.HttpContent), default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'PostAsync', '(System.Uri,System.Net.Http.HttpContent,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_PostAsync3() {
            var obj = new System.Net.Http.HttpClient();
            obj.PostAsync(default(System.Uri), default(System.Net.Http.HttpContent), default(System.Threading.CancellationToken));
        }

        // #- ['System.Net.Http', 'HttpClient', True, 'PutAsync', '(System.Uri,System.Net.Http.HttpContent,System.Threading.CancellationToken)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_PutAsync3() {
            var obj = new System.Net.Http.HttpClient();
            obj.PutAsync(default(System.Uri), default(System.Net.Http.HttpContent), default(System.Threading.CancellationToken));
        }

        // #- ['System.Management', 'ManagementObject', True, 'InvokeMethod', '(System.Management.ManagementOperationObserver,System.String,System.Object[])', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_InvokeMethod() {
            var obj = new System.Management.ManagementObject();
            obj.InvokeMethod(default(System.Management.ManagementOperationObserver), default(System.String), default(System.Object[]));
        }

        // #- ['System.Management', 'ManagementObject', True, 'InvokeMethod', '(System.String,System.Management.ManagementBaseObject,System.Management.InvokeMethodOptions)', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_InvokeMethod1() {
            var obj = new System.Management.ManagementObject();
            obj.InvokeMethod(default(System.String), default(System.Management.ManagementBaseObject), default(System.Management.InvokeMethodOptions));
        }

        // #- ['System.Management', 'ManagementObject', True, 'InvokeMethod', '(System.String,System.Object[])', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_InvokeMethod2() {
            var obj = new System.Management.ManagementObject();
            obj.InvokeMethod(default(System.String), default(System.Object[]));
        }

        // #- ['System.Management', 'ManagementObject', True, 'InvokeMethod', '(System.Management.ManagementOperationObserver,System.String,System.Object[])', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_InvokeMethod3() {
            var obj = new System.Management.ManagementObject();
            obj.InvokeMethod(default(System.Management.ManagementOperationObserver), taintedString, default(System.Object[]));
        }

        // #- ['System.Management', 'ManagementObject', True, 'InvokeMethod', '(System.Management.ManagementOperationObserver,System.String,System.Management.ManagementBaseObject,System.Management.InvokeMethodOptions)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_InvokeMethod4() {
            var obj = new System.Management.ManagementObject();
            obj.InvokeMethod(default(System.Management.ManagementOperationObserver), taintedString, default(System.Management.ManagementBaseObject), default(System.Management.InvokeMethodOptions));
        }

        // #- ['System.Management', 'ManagementObjectSearcher', True, 'ManagementObjectSearcher', '(System.Management.ManagementScope,System.Management.ObjectQuery)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_ManagementObjectSearcher() {
            var tmp = new System.Management.ManagementObjectSearcher(default(System.Management.ManagementScope), default(System.Management.ObjectQuery));
        }

        // #- ['System.Management', 'ManagementObjectSearcher', True, 'ManagementObjectSearcher', '(System.Management.ManagementScope,System.Management.ObjectQuery,System.Management.EnumerationOptions)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_ManagementObjectSearcher1() {
            var tmp = new System.Management.ManagementObjectSearcher(default(System.Management.ManagementScope), default(System.Management.ObjectQuery), default(System.Management.EnumerationOptions));
        }

        // #- ['System.Management', 'ManagementObject', True, 'InvokeMethod', '(System.Management.ManagementOperationObserver,System.String,System.Management.ManagementBaseObject,System.Management.InvokeMethodOptions)', '', 'Argument[3]', 'gadget-sink', 'manual']
        public void Test_InvokeMethod5() {
            var obj = new System.Management.ManagementObject();
            obj.InvokeMethod(default(System.Management.ManagementOperationObserver), default(System.String), default(System.Management.ManagementBaseObject), default(System.Management.InvokeMethodOptions));
        }

        // #- ['System.Management', 'ManagementScope', True, 'ManagementScope', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementScope() {
            var tmp = new System.Management.ManagementScope(taintedString);
        }

        // #- ['System.Management', 'ManagementObject', True, 'InvokeMethod', '(System.String,System.Object[])', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_InvokeMethod6() {
            var obj = new System.Management.ManagementObject();
            obj.InvokeMethod(taintedString, default(System.Object[]));
        }

        // #- ['System.Management', 'ManagementObject', True, 'InvokeMethod', '(System.String,System.Management.ManagementBaseObject,System.Management.InvokeMethodOptions)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_InvokeMethod7() {
            var obj = new System.Management.ManagementObject();
            obj.InvokeMethod(taintedString, default(System.Management.ManagementBaseObject), default(System.Management.InvokeMethodOptions));
        }

        // #- ['System.Management', 'ManagementScope', True, 'ManagementScope', '(System.Management.ManagementPath,System.Management.ManagementScope)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementScope1() {
            var tmp = new System.Management.ManagementScope(default(System.Management.ManagementPath), default(System.Management.ManagementScope));
        }

        // #- ['System.Management', 'ManagementObjectSearcher', True, 'ManagementObjectSearcher', '(System.Management.ObjectQuery)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementObjectSearcher2() {
            var tmp = new System.Management.ManagementObjectSearcher(default(System.Management.ObjectQuery));
        }

        // #- ['System.Management', 'ManagementScope', True, 'ManagementScope', '(System.Management.ManagementPath,System.Management.IWbemServices,System.Management.ConnectionOptions)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementScope2() {
            var tmp = new System.Management.ManagementScope(default(System.Management.ManagementPath), default(System.Management.IWbemServices), default(System.Management.ConnectionOptions));
        }

        // #- ['System.Management', 'ManagementScope', True, 'ManagementScope', '(System.Management.ManagementPath)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementScope3() {
            var tmp = new System.Management.ManagementScope(default(System.Management.ManagementPath));
        }

        // #- ['System.Management', 'ManagementScope', True, 'ManagementScope', '(System.String,System.Management.ConnectionOptions)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementScope4() {
            var tmp = new System.Management.ManagementScope(taintedString, default(System.Management.ConnectionOptions));
        }

        // #- ['System.Management', 'ManagementScope', True, 'ManagementScope', '(System.Management.ManagementPath,System.Management.ConnectionOptions)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementScope5() {
            var tmp = new System.Management.ManagementScope(default(System.Management.ManagementPath), default(System.Management.ConnectionOptions));
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.Management.ManagementScope,System.Management.ManagementPath,System.Management.ObjectGetOptions)', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_ManagementObject() {
            var tmp = new System.Management.ManagementObject(default(System.Management.ManagementScope), default(System.Management.ManagementPath), default(System.Management.ObjectGetOptions));
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.String,System.String,System.Management.ObjectGetOptions)', '', 'Argument[2]', 'gadget-sink', 'manual']
        public void Test_ManagementObject1() {
            var tmp = new System.Management.ManagementObject(default(System.String), default(System.String), default(System.Management.ObjectGetOptions));
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.Management.ManagementScope,System.Management.ManagementPath,System.Management.ObjectGetOptions)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_ManagementObject2() {
            var tmp = new System.Management.ManagementObject(default(System.Management.ManagementScope), default(System.Management.ManagementPath), default(System.Management.ObjectGetOptions));
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.Management.ManagementPath,System.Management.ObjectGetOptions)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_ManagementObject3() {
            var tmp = new System.Management.ManagementObject(default(System.Management.ManagementPath), default(System.Management.ObjectGetOptions));
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.String,System.Management.ObjectGetOptions)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_ManagementObject4() {
            var tmp = new System.Management.ManagementObject(default(System.String), default(System.Management.ObjectGetOptions));
        }

        // #- ['System.Management', 'ObjectQuery', True, 'ObjectQuery', '(System.String,System.String)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_ObjectQuery() {
            var tmp = new System.Management.ObjectQuery(default(System.String), taintedString);
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementObject5() {
            var tmp = new System.Management.ManagementObject(taintedString);
        }

        // #- ['System.Management', 'ObjectQuery', True, 'ObjectQuery', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ObjectQuery1() {
            var tmp = new System.Management.ObjectQuery(taintedString);
        }

        // #- ['System.Management', 'ManagementQuery', True, 'ParseQuery', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ParseQuery() {
            var obj = new System.Management.ManagementQuery();
            obj.ParseQuery(taintedString);
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.Management.ManagementPath)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementObject6() {
            var tmp = new System.Management.ManagementObject(default(System.Management.ManagementPath));
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.Management.ManagementPath,System.Management.ObjectGetOptions)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementObject7() {
            var tmp = new System.Management.ManagementObject(default(System.Management.ManagementPath), default(System.Management.ObjectGetOptions));
        }

        // #- ['System.Management', 'ManagementObject', True, 'ManagementObject', '(System.String,System.Management.ObjectGetOptions)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_ManagementObject8() {
            var tmp = new System.Management.ManagementObject(taintedString, default(System.Management.ObjectGetOptions));
        }

        // #- ['System.Workflow.ComponentModel', 'Activity', True, 'Load', '(System.IO.Stream,System.Workflow.ComponentModel.Activity)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load4() {
            var obj = new System.Workflow.ComponentModel.Activity();
            obj.Load(taintedStream, default(System.Workflow.ComponentModel.Activity));
        }

        // #- ['System.Workflow.ComponentModel', 'Activity', True, 'Load', '(System.IO.Stream,System.Workflow.ComponentModel.Activity,System.Runtime.Serialization.IFormatter)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load5() {
            var obj = new System.Workflow.ComponentModel.Activity();
            obj.Load(taintedStream, default(System.Workflow.ComponentModel.Activity), default(System.Runtime.Serialization.IFormatter));
        }

        // #- ['System.Workflow.ComponentModel.Serialization', 'WorkflowMarkupSerializer', True, 'Deserialize', '(System.ComponentModel.Design.Serialization.IDesignerSerializationManager,System.Xml.XmlReader)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_Deserialize4() {
            var obj = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer();
            obj.Deserialize(default(System.ComponentModel.Design.Serialization.IDesignerSerializationManager), taintedXmlReader);
        }

        // #- ['System.Workflow.ComponentModel.Serialization', 'WorkflowMarkupSerializationHelpers', True, 'LoadXomlDocument', '(System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationManager,System.Xml.XmlReader,System.String)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_LoadXomlDocument() {
            var obj = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationHelpers();
            obj.LoadXomlDocument(default(System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationManager), taintedXmlReader, default(System.String));
        }

        // #- ['System.Workflow.ComponentModel.Serialization', 'WorkflowMarkupSerializationHelpers', True, 'LoadXomlDocument', '(System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationManager,System.Xml.XmlReader,System.String,System.Workflow.ComponentModel.Serialization.ITypeAuthorizer)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_LoadXomlDocument1() {
            var obj = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationHelpers();
            obj.LoadXomlDocument(default(System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationManager), taintedXmlReader, default(System.String), default(System.Workflow.ComponentModel.Serialization.ITypeAuthorizer));
        }

        // #- ['System.Workflow.ComponentModel.Serialization', 'WorkflowMarkupSerializer', True, 'Deserialize', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Deserialize5() {
            var obj = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer();
            obj.Deserialize(taintedXmlReader);
        }

        // #- ['System.Workflow.ComponentModel.Design', 'CompositeActivityDesigner', True, 'DeserializeActivitiesFromDataObject', '(System.IServiceProvider,System.Windows.Forms.IDataObject)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_DeserializeActivitiesFromDataObject() {
            var obj = new System.Workflow.ComponentModel.Design.CompositeActivityDesigner();
            obj.DeserializeActivitiesFromDataObject(default(System.IServiceProvider), default(System.Windows.Forms.IDataObject));
        }

        // #- ['System.Workflow.ComponentModel.Design', 'CompositeActivityDesigner', True, 'DeserializeActivitiesFromDataObject', '(System.IServiceProvider,System.Windows.Forms.IDataObject,System.Boolean)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_DeserializeActivitiesFromDataObject1() {
            var obj = new System.Workflow.ComponentModel.Design.CompositeActivityDesigner();
            obj.DeserializeActivitiesFromDataObject(default(System.IServiceProvider), default(System.Windows.Forms.IDataObject), default(System.Boolean));
        }

        // #- ['System.Workflow.ComponentModel.Design', 'WorkflowTheme', True, 'Load', '(System.ComponentModel.Design.Serialization.IDesignerSerializationManager,System.String)', '', 'Argument[1]', 'gadget-sink', 'manual']
        public void Test_Load6() {
            var obj = new System.Workflow.ComponentModel.Design.WorkflowTheme();
            obj.Load(default(System.ComponentModel.Design.Serialization.IDesignerSerializationManager), taintedString);
        }

        // #- ['System.Workflow.ComponentModel.Design', 'WorkflowTheme', True, 'Load', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load7() {
            var obj = new System.Workflow.ComponentModel.Design.WorkflowTheme();
            obj.Load(taintedString);
        }

        // #- ['System.Workflow.ComponentModel.Design', 'WorkflowDesignerLoader', True, 'GetFileReader', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_GetFileReader() {
            var obj = new System.Workflow.ComponentModel.Design.WorkflowDesignerLoader();
            obj.GetFileReader(taintedString);
        }

        // #- ['System.Workflow.ComponentModel.Design', 'WorkflowDesignerLoader', True, 'LoadDesignerLayout', '(System.Xml.XmlReader,System.Collections.IList)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_LoadDesignerLayout() {
            var obj = new System.Workflow.ComponentModel.Design.WorkflowDesignerLoader();
            obj.LoadDesignerLayout(taintedXmlReader, default(System.Collections.IList));
        }

        // #- ['System.Workflow.ComponentModel.Design', 'XomlComponentSerializationService', True, 'LoadStore', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_LoadStore() {
            var obj = new System.Workflow.ComponentModel.Design.XomlComponentSerializationService();
            obj.LoadStore(taintedStream);
        }

        // #- ['System.Workflow.ComponentModel.Design', 'XomlComponentSerializationService', True, 'Deserialize', '(System.ComponentModel.Design.Serialization.SerializationStore)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Deserialize6() {
            var obj = new System.Workflow.ComponentModel.Design.XomlComponentSerializationService();
            obj.Deserialize(default(System.ComponentModel.Design.Serialization.SerializationStore));
        }

        // #- ['System.Workflow.ComponentModel.Design', 'XomlComponentSerializationService', True, 'Deserialize', '(System.ComponentModel.Design.Serialization.SerializationStore,System.ComponentModel.IContainer)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Deserialize7() {
            var obj = new System.Workflow.ComponentModel.Design.XomlComponentSerializationService();
            obj.Deserialize(default(System.ComponentModel.Design.Serialization.SerializationStore), default(System.ComponentModel.IContainer));
        }

        // #- ['System.Workflow.ComponentModel.Design', 'XomlComponentSerializationService', True, 'DeserializeTo', '(System.ComponentModel.Design.Serialization.SerializationStore,System.ComponentModel.IContainer,System.Boolean,System.Boolean)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_DeserializeTo() {
            var obj = new System.Workflow.ComponentModel.Design.XomlComponentSerializationService();
            obj.DeserializeTo(default(System.ComponentModel.Design.Serialization.SerializationStore), default(System.ComponentModel.IContainer), default(System.Boolean), default(System.Boolean));
        }

        // #- ['System.Xml', 'XmlDocument', True, 'Load', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load8() {
            var obj = new System.Xml.XmlDocument();
            obj.Load(taintedString);
        }

        // #- ['System.Xml', 'XmlDocument', True, 'LoadXml', '(System.String)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_LoadXml() {
            var obj = new System.Xml.XmlDocument();
            obj.LoadXml(taintedString);
        }

        // #- ['System.Xml', 'XmlDocument', True, 'Load', '(System.IO.Stream)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load9() {
            var obj = new System.Xml.XmlDocument();
            obj.Load(taintedStream);
        }

        // #- ['System.Xml', 'XmlDocument', True, 'Load', '(System.IO.TextReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load10() {
            var obj = new System.Xml.XmlDocument();
            obj.Load(taintedReader);
        }

        // #- ['System.Xml', 'XmlDocument', True, 'Load', '(System.Xml.XmlReader)', '', 'Argument[0]', 'gadget-sink', 'manual']
        public void Test_Load11() {
            var obj = new System.Xml.XmlDocument();
            obj.Load(taintedXmlReader);
        }

    }
}
