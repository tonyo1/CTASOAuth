Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Google
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            SetStatus()
        End If


    End Sub

    Protected Sub ButtonGoogleClick(sender As Object, e As EventArgs)

        Response.SuppressFormsAuthenticationRedirect = True


        Dim properties = New AuthenticationProperties()
        properties.Dictionary.Add("login_hint ", "myemail@gmail.com")
        GetAuthenticationManager.Challenge(properties, "google")


    End Sub

    Protected Sub ButtonFrmClick(ByVal sender As Object, ByVal e As EventArgs)
        'check form variables and do old login
        IdentitySignin()
        SetStatus()


    End Sub

    Private Sub SetStatus()

        TextBox3.Text = "Loggedin : " & Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated

        Dim tmp = Request.GetOwinContext().Authentication.User.IsInRole("Admin")

        TextBox3.Text &= " | Is in 'Admin' role " & tmp
    End Sub

    Protected Sub btnLogOut(ByVal sender As Object, ByVal e As EventArgs)
        IdentitySignout()
        SetStatus()
    End Sub

    Public Sub IdentitySignin(Optional isPersistent As Boolean = False)
        Dim claims = New List(Of Claim)()

        ' create required claims
        claims.Add(New Claim(ClaimTypes.NameIdentifier, "aaaa"))
        claims.Add(New Claim(ClaimTypes.Name, "adasdf asdf asdf"))
        claims.Add(New Claim(ClaimTypes.Role, "Admin"))

        ' custom – my serialized AppUserState object - maybe
        claims.Add(New Claim("userState", "")) ' magic db here


        Dim identity = New ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie)


        GetAuthenticationManager.SignIn(New AuthenticationProperties() With { _
             .AllowRefresh = True, _
             .IsPersistent = isPersistent, _
             .ExpiresUtc = DateTime.UtcNow.AddDays(7) _
        }, identity)
    End Sub

    Public Sub IdentitySignout()
        GetAuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie)
    End Sub

    Private ReadOnly Property GetAuthenticationManager() As IAuthenticationManager
        Get
            Return Request.GetOwinContext().Authentication
        End Get
    End Property
End Class