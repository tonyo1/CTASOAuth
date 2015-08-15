
Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Owin

Imports System.IO
Imports Microsoft.Owin.Security.Google
Imports System.Threading.Tasks
Imports Microsoft.Owin.Security
Imports Microsoft.AspNet.Identity.Owin
Imports System.Security.Claims

<Assembly: OwinStartup(GetType(Startup))> 


Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        ' For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888



        app.UseCookieAuthentication(New CookieAuthenticationOptions With
        {
            .AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            .LoginPath = New PathString("/Login.aspx")
        })

        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

        app.UseGoogleAuthentication(GetGoogleOption())


    End Sub


    Private Function GetGoogleOption() As GoogleOAuth2AuthenticationOptions
        Dim goa = New GoogleOAuth2AuthenticationOptions()

        With goa
            .ClientId = "1015270710557-c31v4iqa0qteec2sjg1r6je9ngopqn82.apps.googleusercontent.com"
            .ClientSecret = "m7PdANK7Hpbp1YInvlJDaoXP"
            .AuthenticationType = "google"
            .SignInAsAuthenticationType = "ExternalCookie"
            .CallbackPath = New PathString("/default.aspx")

            .Provider = New GoogleOAuth2AuthenticationProvider With
                {
                .OnAuthenticated = Function(context)

                                       IdentitySignin(context)
                                       Return Task.FromResult(0)
                                   End Function
            }


        End With



        Return goa
    End Function

    Public Sub IdentitySignin(ctx As GoogleOAuth2AuthenticatedContext, Optional isPersistent As Boolean = False)
        Dim claims = New List(Of Claim)()

        ' create required claims
        claims.Add(New Claim(ClaimTypes.NameIdentifier, "aaaa"))
        claims.Add(New Claim(ClaimTypes.Name, "adasdf asdf asdf"))
        claims.Add(New Claim(ClaimTypes.Role, "Admin"))

        ' custom – my serialized AppUserState object - maybe
        claims.Add(New Claim("userState", "")) ' magic db here


        Dim identity = New ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie)


        'ctx.SignIn(New AuthenticationProperties() With {
        '     .AllowRefresh = True,
        '     .IsPersistent = isPersistent,
        '     .ExpiresUtc = DateTime.UtcNow.AddDays(7)
        '}, identity)
    End Sub
End Class






