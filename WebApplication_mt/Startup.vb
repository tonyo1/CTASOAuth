
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

        'app.UseGoogleAuthentication(clientSecret:="", clientId:="")

        app.UseGoogleAuthentication(GetGoogleOption())

    End Sub


    Private Function GetGoogleOption() As GoogleOAuth2AuthenticationOptions
        Dim goa = New GoogleOAuth2AuthenticationOptions()

        With goa
            .ClientId = "1015270710557-c31v4iqa0qteec2sjg1r6je9ngopqn82.apps.googleusercontent.com"
            .ClientSecret = "m7PdANK7Hpbp1YInvlJDaoXP"
            .AuthenticationType = "google"
            .SignInAsAuthenticationType = "ExternalCookie"
            '.CallbackPath = New PathString("/Login.aspx")
            .Scope.Add("email")
            .Provider = New GoogleOAuth2AuthenticationProvider With
                {
                .OnAuthenticated = Function(context)
                                       context.Identity.AddClaim(New Claim(ClaimTypes.NameIdentifier, "bbbb"))
                                       context.Identity.AddClaim(New Claim(ClaimTypes.Name, "I logged in from elsewhere"))
                                       context.Identity.AddClaim(New Claim(ClaimTypes.Role, "Admin"))
                                       context.Identity.AddClaim(New Claim("userState", ""))
                                       Return Task.FromResult(0)
                                   End Function
            }

        End With



        Return goa
    End Function


End Class






