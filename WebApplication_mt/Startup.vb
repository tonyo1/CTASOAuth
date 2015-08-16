
Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Owin
Imports Microsoft.Owin.Security.Google



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
        End With

        Return goa
    End Function


End Class






