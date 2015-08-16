Imports System.Security.Claims
Imports System.Linq

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim claimsIdentity = TryCast(Context.User.Identity, ClaimsIdentity)
        If (not claimsIdentity Is nothing)
            Response.Write(claimsIdentity.Claims.Where( Function(c) c.Type = ClaimTypes.Name).Select(Function(c) c.Value).FirstOrDefault())                                                                                       
        end if
    End Sub

End Class