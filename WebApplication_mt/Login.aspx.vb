Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Google
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity

Public Class Login
    Inherits System.Web.UI.Page

    'Private Const xsrf = "testsite*asdasd"


    Protected Async Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' check for external provider info
        Dim loginInfo = Await AuthManager.GetExternalLoginInfoAsync()

        ' if there is and we have no claims yet it's a new login
        If Not loginInfo Is Nothing Then
            If Not Context.User.Identity.IsAuthenticated Then
                ' YOU ARE LOGGED IN            
                ExternalLoginCallback()
            End If
        End If

    End Sub


    ''' <summary>
    ''' Called when you have a successful signin
    ''' </summary>
    Protected Sub ExternalLoginCallback()
        ' YOU ARE SIGNED IN        
        IdentitySignin(True)       
        
        Dim url = Request.QueryString("returnurl")        
        If not String.IsNullOrEmpty(url)
            Response.Redirect(url, false)                                
        End If
    End Sub


    Protected Sub ButtonGoogleClick(sender As Object, e As EventArgs)

        Response.SuppressFormsAuthenticationRedirect = True

        Dim properties = New AuthenticationProperties()

        AuthManager.Challenge(properties, "google")
    End Sub

    Protected Sub ButtonFrmClick(ByVal sender As Object, ByVal e As EventArgs)
        'check form variables and do old login
        IdentitySignin()

        Dim url = Request.QueryString("returnurl")
        If (String.IsNullOrEmpty(url))
            url = "default.aspx"
        End If
        
        Response.Redirect( url)
    End Sub


    Protected Sub btnLogOut(ByVal sender As Object, ByVal e As EventArgs)
        IdentitySignout()
    End Sub

    Public Sub IdentitySignin(Optional isPersistent As Boolean = False)
        Dim claims = New List(Of Claim)()

        ' create required claims
        claims.Add(New Claim(ClaimTypes.NameIdentifier, "aaaa"))
        claims.Add(New Claim(ClaimTypes.Name, "BigMan"))
        claims.Add(New Claim(ClaimTypes.Role, "NotAdmin"))

        ' custom – my serialized AppUserState object - maybe
        claims.Add(New Claim("userState", "")) ' magic db here


        Dim identity = New ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie)

        AuthManager.SignIn(New AuthenticationProperties() With { _
             .AllowRefresh = True, _
             .IsPersistent = isPersistent, _
             .ExpiresUtc = DateTime.UtcNow.AddDays(7) _
        }, identity)
    End Sub

    Public Sub IdentitySignout()
        AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie)
    End Sub

    Private ReadOnly Property AuthManager() As IAuthenticationManager
        Get
            Return Request.GetOwinContext().Authentication
        End Get
    End Property


End Class