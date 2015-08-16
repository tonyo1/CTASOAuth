Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Google
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Not Page.IsPostBack Then
        '    SetStatus()
        'End If
        If Context.User.Identity Is Nothing Then
            Response.Write("im nobody")
        End If


        'Try
        '    Dim loginInfo = Await GetAuthenticationManager.GetExternalLoginInfoAsync()
        '    Dim i = 1
        'Catch ex As Exception
        '    Dim i = 1
        'End Try






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
        Response.Redirect("default.aspx")


    End Sub

    Private Sub SetStatus()

        TextBox3.Text = "Loggedin : " & Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated

        Dim tmp = Request.GetOwinContext().Authentication.User.IsInRole("Admin")

        TextBox3.Text &= " | Is in 'Admin' role " & tmp
    End Sub

    Protected Sub btnLogOut(ByVal sender As Object, ByVal e As EventArgs)
        IdentitySignout()

    End Sub

    Public Sub IdentitySignin(Optional isPersistent As Boolean = False)
        Dim claims = New List(Of Claim)()

        ' create required claims
        claims.Add(New Claim(ClaimTypes.NameIdentifier, "aaaa"))
        claims.Add(New Claim(ClaimTypes.Name, "I logged in from form"))
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

    Protected Sub LogIn(sender As Object, e As EventArgs)
        'If IsValid Then
        '    ' Validate the user password
        '    Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        '    Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()

        '    ' This doen't count login failures towards account lockout
        '    ' To enable password failures to trigger lockout, change to shouldLockout := True
        '    Dim result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout:=False)

        '    Select Case result
        '        Case SignInStatus.Success
        '            IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
        '            Exit Select
        '        Case SignInStatus.LockedOut
        '            Response.Redirect("/Account/Lockout")
        '            Exit Select
        '        Case SignInStatus.RequiresVerification
        '            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
        '                                            Request.QueryString("ReturnUrl"),
        '                                            RememberMe.Checked),
        '                              True)
        '            Exit Select
        '        Case Else
        '            FailureText.Text = "Invalid login attempt"
        '            ErrorMessage.Visible = True
        '            Exit Select
        '    End Select
        'End If

        Dim i = 1
    End Sub
End Class