import csharp

abstract class KnownDangerousType extends RefType {}

/**
 * To catch all *Identity gadgets
 */
class IIdentityType extends RefType {
  IIdentityType() {
    this.hasFullyQualifiedName("System.Security.Principal", "IIdentity")
  }
}

/**
 * To catch all *Principal gadgets
 */
class IPrincipalType extends RefType {
  IPrincipalType() {
    this.hasFullyQualifiedName("System.Security.Claims", "IPrincipal")
  }
}

/**
 * Keep all the Identity / Principal gadget here to avoid
 * transitive type search. Maybe it's useless.
 */
class ClaimsIdentityType extends KnownDangerousType {
  ClaimsIdentityType() {
    this.hasFullyQualifiedName("System.Security.Claims", "ClaimsIdentity")
  }
}

class ClaimsPrincipalType extends KnownDangerousType {
  ClaimsPrincipalType() {
    this.hasFullyQualifiedName("System.Security.Claims", "ClaimsPrincipal")
  }
}

class DataSetType extends KnownDangerousType {
  DataSetType() {
    this.hasFullyQualifiedName("System.Data", "DataSet")
  }
}

class DataTableType extends KnownDangerousType {
  DataTableType() {
    this.hasFullyQualifiedName("System.Data", "DataTable")
  }
}

class PSObjectType extends KnownDangerousType {
  PSObjectType() {
    this.hasFullyQualifiedName("System.Management.Automation", "PSObject")
  }
}

class RolePrincipalType extends KnownDangerousType {
  RolePrincipalType() {
    this.hasFullyQualifiedName("System.Web.Security", "RolePrincipal")
  }
}

class SessionSecurityTokenType extends KnownDangerousType {
  SessionSecurityTokenType() {
    this.hasFullyQualifiedName("System.IdentityModel.Tokens", "SessionSecurityToken")
  }
}

class WindowsClaimsIdentityType extends KnownDangerousType {
  WindowsClaimsIdentityType() {
    this.hasFullyQualifiedName("System.Security.Claims", "WindowsClaimsIdentity")
  }
}

class WindowsIdentityType extends KnownDangerousType {
  WindowsIdentityType() {
    this.hasFullyQualifiedName("System.Security.Principal", "WindowsIdentity")
  }
}

class WindowsPrincipalType extends KnownDangerousType {
  WindowsPrincipalType() {
    this.hasFullyQualifiedName("System.Security.Principal", "WindowsPrincipal")
  }
}

class AxHostStateType extends KnownDangerousType {
  AxHostStateType() {
    this.hasFullyQualifiedName("System.Windows.Forms", "AxHost+State")
  }
}

class SessionViewStateHistoryItemType extends KnownDangerousType {
  SessionViewStateHistoryItemType() {
    this.hasFullyQualifiedName("System.Web.UI.MobileControls", "SessionViewState+SessionViewStateHistoryItem")
  }
}

class ToolboxItemContainerType extends KnownDangerousType {
  ToolboxItemContainerType() {
    this.hasFullyQualifiedName("System.Drawing.Design", "ToolboxItemContainer")
  }
}

class TextFormattingRunPropertiesType extends KnownDangerousType {
  TextFormattingRunPropertiesType() {
    this.hasFullyQualifiedName("System.Windows.Media.TextFormatting", "TextFormattingRunProperties")
  }
}

class ObjRefType extends KnownDangerousType {
  ObjRefType() {
    this.hasFullyQualifiedName("System.Runtime.Remoting", "ObjRef")
  }
}