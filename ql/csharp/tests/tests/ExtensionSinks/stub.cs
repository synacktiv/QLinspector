// Auto-generated stub.cs

namespace System.Security.Claims {
    public class ClaimsIdentity {
        public ClaimsIdentity(System.Runtime.Serialization.SerializationInfo param_0, System.Runtime.Serialization.StreamingContext param_1) { }
        public ClaimsIdentity() { }
    }

    public class ClaimsPrincipal {
        public ClaimsPrincipal(System.Runtime.Serialization.SerializationInfo param_0, System.Runtime.Serialization.StreamingContext param_1) { }
        public ClaimsPrincipal() { }
    }

}

namespace System.IdentityModel.Tokens {
    public class SessionSecurityTokenHandler {
        public void ReadToken(System.Byte[] param_0, System.IdentityModel.Selectors.SecurityTokenResolver param_1) { }
        public void ReadToken(System.Xml.XmlReader param_0, System.IdentityModel.Selectors.SecurityTokenResolver param_1) { }
        public void ReadToken(System.Xml.XmlReader param_0) { }
        public SessionSecurityTokenHandler() { }
    }

}

namespace System.Activities.Presentation {
    public class WorkflowDesigner {
        public void set_PropertyInspectorFontAndColorData(System.String param_0) { }
        public WorkflowDesigner() { }
    }

}

namespace System.Activities.Presentation.Internal {
    public class ManifestImages {
        public ManifestImages() { }
        public class XamlImageInfo {
            public XamlImageInfo(System.IO.Stream param_0) { }
            public XamlImageInfo() { }
        }
    }

}

namespace System.Data.Linq {
    public class DBConvert {
        public void ChangeType(System.Object param_0, System.Type param_1) { }
        public void ChangeType<T>(System.Object param_0) { }
        public DBConvert() { }
    }

}

namespace System.Security.Policy {
    public class ApplicationTrust {
        public void FromXml(System.Security.SecurityElement param_0) { }
        public void ObjectFromXml(System.Security.SecurityElement param_0) { }
        public ApplicationTrust() { }
    }

}

namespace System.Web.Caching {
    public class OutputCache {
        public void Deserialize(System.IO.Stream param_0) { }
        public OutputCache() { }
    }

}

namespace System.Web.Util {
    public class AltSerialization {
        public void ReadValueFromStream(System.IO.BinaryReader param_0) { }
        public AltSerialization() { }
    }

}

namespace System.Web {
    public class HttpStaticObjectsCollection {
        public void Deserialize(System.IO.BinaryReader param_0) { }
        public HttpStaticObjectsCollection() { }
    }

}

namespace System.Web.SessionState {
    public class SessionStateItemCollection {
        public void Deserialize(System.IO.BinaryReader param_0) { }
        public SessionStateItemCollection() { }
    }

}

namespace System.Security {
    public class SecurityException {
        public void ByteArrayToObject(System.Byte[] param_0) { }
        public SecurityException() { }
    }

    public class SecurityElement {
        public SecurityElement() { }
    }

}

namespace System.Web.Security {
    public class RolePrincipal {
        public RolePrincipal(System.Security.Principal.IIdentity param_0, System.String param_1) { }
        public RolePrincipal(System.String param_0, System.Security.Principal.IIdentity param_1, System.String param_2) { }
        public void InitFromEncryptedTicket(System.String param_0) { }
        public RolePrincipal() { }
    }

}

namespace System.ServiceModel.Channels {
    public class MsmqDecodeHelper {
        public void DeserializeForIntegration(System.ServiceModel.MsmqIntegration.MsmqIntegrationChannelListener param_0, System.IO.Stream param_1, System.ServiceModel.MsmqIntegration.MsmqIntegrationMessageProperty param_2, System.Int64 param_3) { }
        public MsmqDecodeHelper() { }
    }

}

namespace MS.Internal.AppModel {
    public class ApplicationProxyInternal {
        public void DeserializeJournaledObject(System.IO.MemoryStream param_0) { }
        public ApplicationProxyInternal() { }
    }

    public class DataStreams {
        public void LoadSubStreams(System.Windows.UIElement param_0, System.Collections.ArrayList param_1) { }
        public DataStreams() { }
    }

}

namespace System.Transactions.Oletx {
    public class OletxResourceManager {
        public void Reenlist(System.Int32 param_0, System.Byte[] param_1, System.Transactions.IEnlistmentNotificationInternal param_2) { }
        public OletxResourceManager() { }
    }

}

namespace System.Transactions {
    public class TransactionManager {
        public void Reenlist(System.Guid param_0, System.Byte[] param_1, System.Transactions.IEnlistmentNotification param_2) { }
        public TransactionManager() { }
    }

    public class IEnlistmentNotification {
        public IEnlistmentNotification() { }
    }

    public class IEnlistmentNotificationInternal {
        public IEnlistmentNotificationInternal() { }
    }

}

namespace System.IO.IsolatedStorage {
    public class IsolatedStorage {
        public void InitStore(System.IO.IsolatedStorage.IsolatedStorageScope param_0, System.IO.Stream param_1, System.IO.Stream param_2, System.IO.Stream param_3, System.String param_4, System.String param_5, System.String param_6) { }
        public IsolatedStorage() { }
    }

    public class IsolatedStorageScope {
        public IsolatedStorageScope() { }
    }

}

namespace System.Windows {
    public class DataObject {
        public void SetData(System.Object param_0) { }
        public void SetData(System.String param_0, System.Object param_1) { }
        public void SetData(System.Type param_0, System.Object param_1) { }
        public void SetData(System.String param_0, System.Object param_1, System.Boolean param_2) { }
        public DataObject() { }
    }

    public class UIElement {
        public UIElement() { }
    }

}

namespace System.Windows.Forms {
    public class DataObject {
        public void SetData(System.Object param_0) { }
        public void SetData(System.String param_0, System.Object param_1) { }
        public void SetData(System.Type param_0, System.Object param_1) { }
        public void SetData(System.String param_0, System.Boolean param_1, System.Object param_2) { }
        public DataObject() { }
        public class OleConverter {
            public void SetData(System.Object param_0) { }
            public void SetData(System.String param_0, System.Object param_1) { }
            public void SetData(System.Type param_0, System.Object param_1) { }
            public void SetData(System.String param_0, System.Boolean param_1, System.Object param_2) { }
            public void ReadObjectFromHandle(System.IntPtr param_0, System.Boolean param_1) { }
            public void ReadObjectFromHandleDeserializer(System.IO.Stream param_0, System.Boolean param_1) { }
            public OleConverter() { }
        }
    }

    public class Control {
        public Control() { }
        public class ActiveXImpl {
            public void Load(System.Windows.Forms.UnsafeNativeMethods.IStream param_0) { }
            public void Load(System.Windows.Forms.UnsafeNativeMethods.IPropertyBag param_0, System.Windows.Forms.UnsafeNativeMethods.IErrorLog param_1) { }
            public ActiveXImpl() { }
            public class PropertyBagStream {
                public void Read(System.Windows.Forms.UnsafeNativeMethods.IStream param_0) { }
                public PropertyBagStream() { }
            }
        }
    }

    public class UnsafeNativeMethods {
        public UnsafeNativeMethods() { }
        public class IStream {
            public IStream() { }
        }
        public class IPropertyBag {
            public IPropertyBag() { }
        }
        public class IErrorLog {
            public IErrorLog() { }
        }
    }

    public class IDataObject {
        public IDataObject() { }
    }

}

namespace System.Data {
    public class DataSet {
        public void ReadXml(System.IO.Stream param_0) { }
        public void ReadXml(System.IO.Stream param_0, System.Data.XmlReadMode param_1) { }
        public void ReadXml(System.IO.TextReader param_0) { }
        public void ReadXml(System.IO.TextReader param_0, System.Data.XmlReadMode param_1) { }
        public void ReadXml(System.String param_0) { }
        public void ReadXml(System.String param_0, System.Data.XmlReadMode param_1) { }
        public void ReadXml(System.Xml.XmlReader param_0) { }
        public void ReadXml(System.Xml.XmlReader param_0, System.Boolean param_1) { }
        public void ReadXml(System.Xml.XmlReader param_0, System.Data.XmlReadMode param_1) { }
        public void ReadXml(System.Xml.XmlReader param_0, System.Data.XmlReadMode param_1, System.Boolean param_2) { }
        public void ReadXmlDiffgram(System.Xml.XmlReader param_0) { }
        public void ReadXmlSchema(System.IO.Stream param_0) { }
        public void ReadXmlSchema(System.IO.TextReader param_0) { }
        public void ReadXmlSchema(System.String param_0) { }
        public void ReadXmlSchema(System.Xml.XmlReader param_0) { }
        public void ReadXmlSchema(System.Xml.XmlReader param_0, System.Boolean param_1) { }
        public void ReadXmlSerializable(System.Xml.XmlReader param_0) { }
        public DataSet() { }
    }

    public class DataTable {
        public void ReadXml(System.IO.Stream param_0) { }
        public void ReadXml(System.IO.TextReader param_0) { }
        public void ReadXml(System.String param_0) { }
        public void ReadXml(System.Xml.XmlReader param_0) { }
        public void ReadXml(System.Xml.XmlReader param_0, System.Boolean param_1) { }
        public void ReadXml(System.Xml.XmlReader param_0, System.Data.XmlReadMode param_1, System.Boolean param_2) { }
        public void ReadXmlDiffgram(System.Xml.XmlReader param_0) { }
        public void ReadXmlSchema(System.IO.Stream param_0) { }
        public void ReadXmlSchema(System.IO.TextReader param_0) { }
        public void ReadXmlSchema(System.String param_0) { }
        public void ReadXmlSchema(System.Xml.XmlReader param_0) { }
        public void ReadXmlSchema(System.Xml.XmlReader param_0, System.Boolean param_1) { }
        public void ReadXmlSerializable(System.Xml.XmlReader param_0) { }
        public DataTable() { }
    }

    public class XmlReadMode {
        public XmlReadMode() { }
    }

}

namespace System.Data.Design {
    public class MethodSignatureGenerator {
        public void SetMethodSourceContent(System.String param_0) { }
        public MethodSignatureGenerator() { }
    }

    public class TypedDataSetGenerator {
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3) { }
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3, System.Collections.Hashtable param_4) { }
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3, System.Collections.Hashtable param_4, System.Data.Design.TypedDataSetGenerator.GenerateOption param_5) { }
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3, System.Collections.Hashtable param_4, System.Data.Design.TypedDataSetGenerator.GenerateOption param_5, System.String param_6) { }
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3, System.Collections.Hashtable param_4, System.Data.Design.TypedDataSetGenerator.GenerateOption param_5, System.String param_6, System.String param_7) { }
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3, System.Data.Common.DbProviderFactory param_4) { }
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3, System.Data.Design.TypedDataSetGenerator.GenerateOption param_4) { }
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3, System.Data.Design.TypedDataSetGenerator.GenerateOption param_4, System.String param_5) { }
        public void Generate(System.String param_0, System.CodeDom.CodeCompileUnit param_1, System.CodeDom.CodeNamespace param_2, System.CodeDom.Compiler.CodeDomProvider param_3, System.Data.Design.TypedDataSetGenerator.GenerateOption param_4, System.String param_5, System.String param_6) { }
        public void GetProviderName(System.String param_0) { }
        public void GetProviderName(System.String param_0, System.String param_1) { }
        public TypedDataSetGenerator() { }
        public class GenerateOption {
            public GenerateOption() { }
        }
    }

    public class TypedDataSetSchemaImporterExtension {
        public void ImportSchemaType(System.String param_0, System.String param_1, System.Xml.Schema.XmlSchemaObject param_2, System.Xml.Serialization.XmlSchemas param_3, System.Xml.Serialization.XmlSchemaImporter param_4, System.CodeDom.CodeCompileUnit param_5, System.CodeDom.CodeNamespace param_6, System.Xml.Serialization.CodeGenerationOptions param_7, System.CodeDom.Compiler.CodeDomProvider param_8) { }
        public void ImportSchemaType(System.Xml.Schema.XmlSchemaType param_0, System.Xml.Schema.XmlSchemaObject param_1, System.Xml.Serialization.XmlSchemas param_2, System.Xml.Serialization.XmlSchemaImporter param_3, System.CodeDom.CodeCompileUnit param_4, System.CodeDom.CodeNamespace param_5, System.Xml.Serialization.CodeGenerationOptions param_6, System.CodeDom.Compiler.CodeDomProvider param_7) { }
        public TypedDataSetSchemaImporterExtension() { }
    }

}

namespace System.IO {
    public class Path {
        public void GetFullPath(System.String param_0) { }
        public Path() { }
    }

}

namespace System {
    public class Activator {
        public void CreateInstance(System.Type param_0) { }
        public void CreateInstance(System.Type param_0, System.Type param_1) { }
        public void CreateInstance(System.Type param_0, System.Type param_1, System.Type param_2, System.Type param_3, System.Type param_4) { }
        public void CreateInstance(System.AppDomain param_0, System.AppDomain param_1, System.AppDomain param_2, System.AppDomain param_3, System.AppDomain param_4, System.AppDomain param_5, System.AppDomain param_6, System.AppDomain param_7, System.AppDomain param_8, System.AppDomain param_9) { }
        public void CreateInstance(System.Type param_0, System.Type param_1, System.Type param_2, System.Type param_3, System.Type param_4, System.Type param_5) { }
        public void CreateInstance(System.Type param_0, System.Type param_1, System.Type param_2) { }
        public void CreateInstance(System.String param_0, System.String param_1) { }
        public void CreateInstance(System.String param_0, System.String param_1, System.String param_2) { }
        public void CreateInstance(System.String param_0, System.String param_1, System.String param_2, System.String param_3, System.String param_4, System.String param_5, System.String param_6, System.String param_7, System.String param_8) { }
        public void CreateInstance(System.String param_0, System.String param_1, System.String param_2, System.String param_3, System.String param_4, System.String param_5, System.String param_6, System.String param_7) { }
        public void CreateInstance(System.String param_0, System.String param_1, System.String param_2, System.String param_3, System.String param_4, System.String param_5, System.String param_6, System.String param_7, System.String param_8, System.String param_9) { }
        public void CreateInstance(System.AppDomain param_0, System.AppDomain param_1, System.AppDomain param_2) { }
        public void CreateInstance(System.AppDomain param_0, System.AppDomain param_1, System.AppDomain param_2, System.AppDomain param_3, System.AppDomain param_4, System.AppDomain param_5, System.AppDomain param_6, System.AppDomain param_7, System.AppDomain param_8) { }
        public void CreateInstance(System.ActivationContext param_0) { }
        public void CreateInstance(System.ActivationContext param_0, System.ActivationContext param_1) { }
        public Activator() { }
    }

    public class AppDomain {
        public void SetData(System.String param_0, System.Object param_1) { }
        public void Deserialize(System.Byte[] param_0) { }
        public AppDomain() { }
    }

    public class AppDomainSetup {
        public void set_ApplicationBase(System.String param_0) { }
        public AppDomainSetup() { }
    }

    public class ActivationContext {
        public ActivationContext(System.ApplicationIdentity param_0, System.String[] param_1) { }
        public void CreateFromNameAndManifests(System.ApplicationIdentity param_0, System.String[] param_1) { }
        public void CreatePartialActivationContext(System.ApplicationIdentity param_0, System.String[] param_1) { }
        public ActivationContext() { }
    }

    public class Type {
        public Type() { }
    }

    public class ApplicationIdentity {
        public ApplicationIdentity() { }
    }

    public class IServiceProvider {
        public IServiceProvider() { }
    }

    public class IntPtr {
        public IntPtr() { }
    }

    public class Int64 {
        public Int64() { }
    }

    public class Guid {
        public Guid() { }
    }

    public class Uri {
        public Uri() { }
    }

}

namespace System.DirectoryServices {
    public class DirectoryEntry {
        public DirectoryEntry(System.Object param_0) { }
        public DirectoryEntry(System.String param_0) { }
        public DirectoryEntry(System.String param_0, System.String param_1, System.String param_2) { }
        public DirectoryEntry(System.String param_0, System.String param_1, System.String param_2, System.DirectoryServices.AuthenticationTypes param_3) { }
        public DirectoryEntry() { }
    }

    public class AuthenticationTypes {
        public AuthenticationTypes() { }
    }

}

namespace System.Resources {
    public class ResourceReader {
        public ResourceReader(System.String param_0) { }
        public ResourceReader(System.IO.Stream param_0) { }
        public ResourceReader() { }
    }

    public class ResourceManager {
        public void GetObject(System.String param_0, System.Globalization.CultureInfo param_1) { }
        public void GetObject(System.String param_0) { }
        public ResourceManager() { }
    }

    public class ResourceSet {
        public ResourceSet(System.String param_0) { }
        public ResourceSet(System.IO.Stream param_0) { }
        public ResourceSet() { }
    }

}

namespace System.Configuration {
    public class SettingsPropertyValue {
        public SettingsPropertyValue(System.Configuration.SettingsProperty param_0) { }
        public SettingsPropertyValue() { }
    }

    public class SettingsProperty {
        public SettingsProperty() { }
    }

}

namespace System.Management.Automation {
    public class PowerShell {
        public void AddCommand(System.String param_0) { }
        public void AddScript(System.String param_0) { }
        public void Create(System.Management.Automation.RunspaceMode param_0) { }
        public void AddCommand(System.Management.Automation.Runspaces.Command param_0) { }
        public void AddCommand(System.Management.Automation.CommandInfo param_0) { }
        public void Create(System.Management.Automation.Runspaces.InitialSessionState param_0) { }
        public void Create(System.Boolean param_0, System.Management.Automation.PSCommand param_1, System.Collections.ObjectModel.Collection<System.Management.Automation.PSCommand> param_2) { }
        public void AddCommand(System.String param_0, System.Boolean param_1) { }
        public void AddScript(System.String param_0, System.Boolean param_1) { }
        public void set_Commands(System.Management.Automation.PSCommand param_0) { }
        public PowerShell() { }
    }

    public class CommandInfo {
        public CommandInfo() { }
    }

    public class RunspaceMode {
        public RunspaceMode() { }
    }

    public class PSCommand {
        public PSCommand() { }
    }

}

namespace System.Reflection {
    public class Assembly {
        public void LoadFrom(System.String param_0) { }
        public void LoadFile(System.String param_0) { }
        public void Load(System.Byte[] param_0) { }
        public void Load(System.Byte[] param_0, System.Byte[] param_1) { }
        public Assembly() { }
    }

}

namespace System.Net {
    public class WebRequest {
        public void Create(System.Uri param_0) { }
        public void Create(System.String param_0) { }
        public WebRequest() { }
    }

    public class WebClient {
        public void OpenRead(System.Uri param_0) { }
        public void OpenWrite(System.Uri param_0) { }
        public void DownloadData(System.Uri param_0) { }
        public void DownloadString(System.Uri param_0) { }
        public void OpenReadAsync(System.Uri param_0) { }
        public void OpenWriteAsync(System.Uri param_0) { }
        public void DownloadStringAsync(System.Uri param_0) { }
        public void DownloadDataAsync(System.Uri param_0) { }
        public void OpenRead(System.String param_0) { }
        public void OpenWrite(System.String param_0) { }
        public void DownloadData(System.String param_0) { }
        public void DownloadString(System.String param_0) { }
        public void DownloadFile(System.String param_0, System.String param_1) { }
        public void UploadFile(System.String param_0, System.String param_1) { }
        public void OpenWrite(System.String param_0, System.String param_1) { }
        public void UploadString(System.String param_0, System.String param_1) { }
        public void DownloadFile(System.Uri param_0, System.String param_1) { }
        public void UploadFile(System.Uri param_0, System.String param_1) { }
        public void OpenWrite(System.Uri param_0, System.String param_1) { }
        public void UploadString(System.Uri param_0, System.String param_1) { }
        public void OpenWriteAsync(System.Uri param_0, System.String param_1) { }
        public void DownloadFileAsync(System.Uri param_0, System.String param_1) { }
        public void UploadStringAsync(System.Uri param_0, System.String param_1) { }
        public void UploadFileAsync(System.Uri param_0, System.String param_1) { }
        public void UploadData(System.String param_0, System.Byte[] param_1) { }
        public void UploadData(System.Uri param_0, System.Byte[] param_1) { }
        public void UploadDataAsync(System.Uri param_0, System.Byte[] param_1) { }
        public void UploadData(System.String param_0, System.String param_1, System.Byte[] param_2) { }
        public void UploadData(System.Uri param_0, System.String param_1, System.Byte[] param_2) { }
        public void UploadDataAsync(System.Uri param_0, System.String param_1, System.Byte[] param_2) { }
        public void UploadFile(System.String param_0, System.String param_1, System.String param_2) { }
        public void UploadString(System.String param_0, System.String param_1, System.String param_2) { }
        public void UploadFile(System.Uri param_0, System.String param_1, System.String param_2) { }
        public void UploadString(System.Uri param_0, System.String param_1, System.String param_2) { }
        public void UploadStringAsync(System.Uri param_0, System.String param_1, System.String param_2) { }
        public void UploadFileAsync(System.Uri param_0, System.String param_1, System.String param_2) { }
        public void UploadValues(System.String param_0, System.Collections.Specialized.NameValueCollection param_1) { }
        public void UploadValues(System.Uri param_0, System.Collections.Specialized.NameValueCollection param_1) { }
        public void UploadValuesAsync(System.Uri param_0, System.Collections.Specialized.NameValueCollection param_1) { }
        public void UploadValues(System.String param_0, System.String param_1, System.Collections.Specialized.NameValueCollection param_2) { }
        public void UploadValues(System.Uri param_0, System.String param_1, System.Collections.Specialized.NameValueCollection param_2) { }
        public void UploadValuesAsync(System.Uri param_0, System.String param_1, System.Collections.Specialized.NameValueCollection param_2) { }
        public void OpenReadAsync(System.Uri param_0, System.Object param_1) { }
        public void DownloadStringAsync(System.Uri param_0, System.Object param_1) { }
        public void DownloadDataAsync(System.Uri param_0, System.Object param_1) { }
        public void OpenWriteAsync(System.Uri param_0, System.String param_1, System.Object param_2) { }
        public void DownloadFileAsync(System.Uri param_0, System.String param_1, System.Object param_2) { }
        public void UploadStringAsync(System.Uri param_0, System.String param_1, System.String param_2, System.Object param_3) { }
        public void UploadFileAsync(System.Uri param_0, System.String param_1, System.String param_2, System.Object param_3) { }
        public void UploadDataAsync(System.Uri param_0, System.String param_1, System.Byte[] param_2, System.Object param_3) { }
        public void UploadValuesAsync(System.Uri param_0, System.String param_1, System.Collections.Specialized.NameValueCollection param_2, System.Object param_3) { }
        public WebClient() { }
    }

}

namespace System.Net.Http {
    public class HttpClient {
        public void set_BaseAddress(System.Uri param_0) { }
        public void GetStringAsync(System.Uri param_0) { }
        public void GetByteArrayAsync(System.Uri param_0) { }
        public void GetStreamAsync(System.Uri param_0) { }
        public void GetAsync(System.Uri param_0) { }
        public void DeleteAsync(System.Uri param_0) { }
        public void GetStringAsync(System.String param_0) { }
        public void GetByteArrayAsync(System.String param_0) { }
        public void GetStreamAsync(System.String param_0) { }
        public void GetAsync(System.String param_0) { }
        public void DeleteAsync(System.String param_0) { }
        public void GetAsync(System.String param_0, System.Net.Http.HttpCompletionOption param_1) { }
        public void GetAsync(System.Uri param_0, System.Net.Http.HttpCompletionOption param_1) { }
        public void GetAsync(System.String param_0, System.Threading.CancellationToken param_1) { }
        public void DeleteAsync(System.String param_0, System.Threading.CancellationToken param_1) { }
        public void GetAsync(System.Uri param_0, System.Threading.CancellationToken param_1) { }
        public void DeleteAsync(System.Uri param_0, System.Threading.CancellationToken param_1) { }
        public void GetAsync(System.String param_0, System.Net.Http.HttpCompletionOption param_1, System.Threading.CancellationToken param_2) { }
        public void GetAsync(System.Uri param_0, System.Net.Http.HttpCompletionOption param_1, System.Threading.CancellationToken param_2) { }
        public void PostAsync(System.String param_0, System.Net.Http.HttpContent param_1) { }
        public void PutAsync(System.String param_0, System.Net.Http.HttpContent param_1) { }
        public void PostAsync(System.Uri param_0, System.Net.Http.HttpContent param_1) { }
        public void PutAsync(System.Uri param_0, System.Net.Http.HttpContent param_1) { }
        public void PostAsync(System.String param_0, System.Net.Http.HttpContent param_1, System.Threading.CancellationToken param_2) { }
        public void PutAsync(System.String param_0, System.Net.Http.HttpContent param_1, System.Threading.CancellationToken param_2) { }
        public void PostAsync(System.Uri param_0, System.Net.Http.HttpContent param_1, System.Threading.CancellationToken param_2) { }
        public void PutAsync(System.Uri param_0, System.Net.Http.HttpContent param_1, System.Threading.CancellationToken param_2) { }
        public HttpClient() { }
    }

    public class HttpCompletionOption {
        public HttpCompletionOption() { }
    }

    public class HttpContent {
        public HttpContent() { }
    }

}

namespace System.Management {
    public class ManagementObject {
        public void InvokeMethod(System.Management.ManagementOperationObserver param_0, System.String param_1, System.Object[] param_2) { }
        public void InvokeMethod(System.String param_0, System.Management.ManagementBaseObject param_1, System.Management.InvokeMethodOptions param_2) { }
        public void InvokeMethod(System.String param_0, System.Object[] param_1) { }
        public void InvokeMethod(System.Management.ManagementOperationObserver param_0, System.String param_1, System.Management.ManagementBaseObject param_2, System.Management.InvokeMethodOptions param_3) { }
        public ManagementObject(System.Management.ManagementScope param_0, System.Management.ManagementPath param_1, System.Management.ObjectGetOptions param_2) { }
        public ManagementObject(System.String param_0, System.String param_1, System.Management.ObjectGetOptions param_2) { }
        public ManagementObject(System.Management.ManagementPath param_0, System.Management.ObjectGetOptions param_1) { }
        public ManagementObject(System.String param_0, System.Management.ObjectGetOptions param_1) { }
        public ManagementObject(System.String param_0) { }
        public ManagementObject(System.Management.ManagementPath param_0) { }
        public ManagementObject() { }
    }

    public class ManagementObjectSearcher {
        public ManagementObjectSearcher(System.Management.ManagementScope param_0, System.Management.ObjectQuery param_1) { }
        public ManagementObjectSearcher(System.Management.ManagementScope param_0, System.Management.ObjectQuery param_1, System.Management.EnumerationOptions param_2) { }
        public ManagementObjectSearcher(System.Management.ObjectQuery param_0) { }
        public ManagementObjectSearcher() { }
    }

    public class ManagementScope {
        public ManagementScope(System.String param_0) { }
        public ManagementScope(System.Management.ManagementPath param_0, System.Management.ManagementScope param_1) { }
        public ManagementScope(System.Management.ManagementPath param_0, System.Management.IWbemServices param_1, System.Management.ConnectionOptions param_2) { }
        public ManagementScope(System.Management.ManagementPath param_0) { }
        public ManagementScope(System.String param_0, System.Management.ConnectionOptions param_1) { }
        public ManagementScope(System.Management.ManagementPath param_0, System.Management.ConnectionOptions param_1) { }
        public ManagementScope() { }
    }

    public class ObjectQuery {
        public ObjectQuery(System.String param_0, System.String param_1) { }
        public ObjectQuery(System.String param_0) { }
        public ObjectQuery() { }
    }

    public class ManagementQuery {
        public void ParseQuery(System.String param_0) { }
        public ManagementQuery() { }
    }

    public class IWbemServices {
        public IWbemServices() { }
    }

    public class ManagementPath {
        public ManagementPath() { }
    }

    public class ManagementBaseObject {
        public ManagementBaseObject() { }
    }

    public class EnumerationOptions {
        public EnumerationOptions() { }
    }

    public class InvokeMethodOptions {
        public InvokeMethodOptions() { }
    }

    public class ConnectionOptions {
        public ConnectionOptions() { }
    }

    public class ManagementOperationObserver {
        public ManagementOperationObserver() { }
    }

    public class ObjectGetOptions {
        public ObjectGetOptions() { }
    }

}

namespace System.Workflow.ComponentModel {
    public class Activity {
        public void Load(System.IO.Stream param_0, System.Workflow.ComponentModel.Activity param_1) { }
        public void Load(System.IO.Stream param_0, System.Workflow.ComponentModel.Activity param_1, System.Runtime.Serialization.IFormatter param_2) { }
        public Activity() { }
    }

}

namespace System.Workflow.ComponentModel.Serialization {
    public class WorkflowMarkupSerializer {
        public void Deserialize(System.ComponentModel.Design.Serialization.IDesignerSerializationManager param_0, System.Xml.XmlReader param_1) { }
        public void Deserialize(System.Xml.XmlReader param_0) { }
        public WorkflowMarkupSerializer() { }
    }

    public class WorkflowMarkupSerializationHelpers {
        public void LoadXomlDocument(System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationManager param_0, System.Xml.XmlReader param_1, System.String param_2) { }
        public void LoadXomlDocument(System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationManager param_0, System.Xml.XmlReader param_1, System.String param_2, System.Workflow.ComponentModel.Serialization.ITypeAuthorizer param_3) { }
        public WorkflowMarkupSerializationHelpers() { }
    }

    public class WorkflowMarkupSerializationManager {
        public WorkflowMarkupSerializationManager() { }
    }

    public class ITypeAuthorizer {
        public ITypeAuthorizer() { }
    }

}

namespace System.Workflow.ComponentModel.Design {
    public class CompositeActivityDesigner {
        public void DeserializeActivitiesFromDataObject(System.IServiceProvider param_0, System.Windows.Forms.IDataObject param_1) { }
        public void DeserializeActivitiesFromDataObject(System.IServiceProvider param_0, System.Windows.Forms.IDataObject param_1, System.Boolean param_2) { }
        public CompositeActivityDesigner() { }
    }

    public class WorkflowTheme {
        public void Load(System.ComponentModel.Design.Serialization.IDesignerSerializationManager param_0, System.String param_1) { }
        public void Load(System.String param_0) { }
        public WorkflowTheme() { }
    }

    public class WorkflowDesignerLoader {
        public void GetFileReader(System.String param_0) { }
        public void LoadDesignerLayout(System.Xml.XmlReader param_0, System.Collections.IList param_1) { }
        public WorkflowDesignerLoader() { }
    }

    public class XomlComponentSerializationService {
        public void LoadStore(System.IO.Stream param_0) { }
        public void Deserialize(System.ComponentModel.Design.Serialization.SerializationStore param_0) { }
        public void Deserialize(System.ComponentModel.Design.Serialization.SerializationStore param_0, System.ComponentModel.IContainer param_1) { }
        public void DeserializeTo(System.ComponentModel.Design.Serialization.SerializationStore param_0, System.ComponentModel.IContainer param_1, System.Boolean param_2, System.Boolean param_3) { }
        public XomlComponentSerializationService() { }
    }

}

namespace System.Xml {
    public class XmlDocument {
        public void Load(System.String param_0) { }
        public void LoadXml(System.String param_0) { }
        public void Load(System.IO.Stream param_0) { }
        public void Load(System.IO.TextReader param_0) { }
        public void Load(System.Xml.XmlReader param_0) { }
        public XmlDocument() { }
    }

}

namespace System.IdentityModel.Selectors {
    public class SecurityTokenResolver {
        public SecurityTokenResolver() { }
    }

}

namespace System.Runtime.Serialization {
    public class StreamingContext {
        public StreamingContext() { }
    }

    public class SerializationInfo {
        public SerializationInfo() { }
    }

    public class IFormatter {
        public IFormatter() { }
    }

}

namespace System.Xml.Serialization {
    public class CodeGenerationOptions {
        public CodeGenerationOptions() { }
    }

    public class XmlSchemaImporter {
        public XmlSchemaImporter() { }
    }

    public class XmlSchemas {
        public XmlSchemas() { }
    }

}

namespace System.Management.Automation.Runspaces {
    public class Command {
        public Command() { }
    }

    public class InitialSessionState {
        public InitialSessionState() { }
    }

}

namespace System.CodeDom {
    public class CodeCompileUnit {
        public CodeCompileUnit() { }
    }

    public class CodeNamespace {
        public CodeNamespace() { }
    }

}

namespace System.Data.Common {
    public class DbProviderFactory {
        public DbProviderFactory() { }
    }

}

namespace System.Xml.Schema {
    public class XmlSchemaType {
        public XmlSchemaType() { }
    }

    public class XmlSchemaObject {
        public XmlSchemaObject() { }
    }

}

namespace System.Globalization {
    public class CultureInfo {
        public CultureInfo() { }
    }

}

namespace System.Collections.ObjectModel {
    public class Collection {
        public Collection() { }
    }

}

namespace System.Collections {
    public class IList {
        public IList() { }
    }

    public class ArrayList {
        public ArrayList() { }
    }

    public class Hashtable {
        public Hashtable() { }
    }

}

namespace System.ComponentModel.Design.Serialization {
    public class IDesignerSerializationManager {
        public IDesignerSerializationManager() { }
    }

    public class SerializationStore {
        public SerializationStore() { }
    }

}

namespace System.Security.Principal {
    public class IIdentity {
        public IIdentity() { }
    }

}

namespace System.Threading {
    public class CancellationToken {
        public CancellationToken() { }
    }

}

namespace System.Collections.Specialized {
    public class NameValueCollection {
        public NameValueCollection() { }
    }

}

namespace System.CodeDom.Compiler {
    public class CodeDomProvider {
        public CodeDomProvider() { }
    }

}

namespace System.ServiceModel.MsmqIntegration {
    public class MsmqIntegrationChannelListener {
        public MsmqIntegrationChannelListener() { }
    }

    public class MsmqIntegrationMessageProperty {
        public MsmqIntegrationMessageProperty() { }
    }

}

namespace System.ComponentModel {
    public class IContainer {
        public IContainer() { }
    }

}

