Option Strict On

Imports System.IO
Imports System.ComponentModel
Imports System.Threading
Imports System.Net
Imports System.Net.WebUtility
Imports System.IO.Compression
Imports System.Text
Imports System.Security.Policy
Imports Microsoft.Web.WebView2.Core
Imports MetroFramework.Drawing
Imports Newtonsoft.Json.Linq

Public Class Browser




    Private Sub WebView2_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WebView2.CoreWebView2InitializationCompleted
        Try
            WebView2.CoreWebView2.AddWebResourceRequestedFilter("https://www.crunchyroll.com/*", CoreWebView2WebResourceContext.All)
            WebView2.CoreWebView2.AddWebResourceRequestedFilter("https://www.funimation.com/*", CoreWebView2WebResourceContext.All)
            'WebView2.CoreWebView2.AddWebResourceRequestedFilter("https://cr-play-service.prd.crunchyrollsvc.com/*", CoreWebView2WebResourceContext.All)
            '   WebView2.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All)
            'cr-play-service.prd.crunchyrollsvc.com
            'WebView2.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All)
            AddHandler WebView2.CoreWebView2.WebResourceResponseReceived, AddressOf ObserveResponse
            AddHandler WebView2.CoreWebView2.WebResourceRequested, AddressOf ObserveHttp
            'WebView2.CoreWebView2.Settings.UserAgent = My.Settings.User_Agend.Replace(Chr(34), "").Replace("User-Agent: ", "")
            '
            'WebView2.CoreWebView2.Settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko"
            My.Settings.User_Agend = Chr(34) + "User-Agent: " + WebView2.CoreWebView2.Settings.UserAgent + Chr(34)
            'My.Settings.User_Agend = Chr(34) + "User-Agent: " + "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:120.0) Gecko/20100101 Firefox/120.0" + Chr(34)

            'MsgBox(My.Settings.User_Agend)

            If WebView2.CoreWebView2.Source = "about:blank" Or WebView2.CoreWebView2.Source = Nothing Then
                'TextBox1.Text = Main.Startseite
                WebView2.CoreWebView2.Navigate(Main.Startseite)


            End If
        Catch ex As Exception

        End Try


    End Sub



    Private Sub WebView2_SourceChanged(sender As Object, e As CoreWebView2SourceChangedEventArgs) Handles WebView2.SourceChanged
        Try
            TextBox1.Text = WebView2.CoreWebView2.Source

        Catch ex As Exception
        End Try

    End Sub



    Private Sub WebView2_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WebView2.NavigationCompleted
        ' Dim HTML As String = WebView2.CoreWebView2.
        'TextBox1.Text = WebView2.CoreWebView2.Source
        ' Exit Sub


        If e.HttpStatusCode = 200 Then
            Dim DocumentTitle As String = WebView2.CoreWebView2.DocumentTitle

            Debug.WriteLine("NavigationCompleted: " + Date.Now.ToString)
            Main.WebbrowserURL = WebView2.CoreWebView2.Source
            TextBox1.Text = Main.WebbrowserURL

            Main.WebbrowserTitle = DocumentTitle
            Main.ProcessHTML("", Main.WebbrowserURL, DocumentTitle)

            GetCookies(Main.WebbrowserURL)

            Main.BowserWasOpen = True

            If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
                Anime_Add.btn_dl.Cursor = Cursors.Default
                Anime_Add.btn_dl.BackgroundImage = My.Resources.main_button_download_default
            End If

            If Main.Startseite IsNot My.Settings.Startseite Then
                Main.LoadBrowser(Main.Startseite, 1)
                Main.Startseite = My.Settings.Startseite
            End If

        End If
    End Sub

    Public Async Sub GetCookies(ByVal Uri As String)
        Try
            Main.CookieList = Await WebView2.CoreWebView2.CookieManager.GetCookiesAsync(Uri)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Browser_Load(sender As Object, e As EventArgs) Handles Me.Load

        Main.waveOutSetVolume(0, 0)
        If Me.Width > My.Computer.Screen.Bounds.Width Then
            Me.Width = My.Computer.Screen.Bounds.Width
            Panel1.Width = Me.Size.Width - 15 ', Me.Size.Height - 69)
            Panel1.Location = New Point(0, 30)
            TextBox1.Width = My.Computer.Screen.Bounds.Width - 435

        End If

        If Me.Size.Height > My.Computer.Screen.Bounds.Height Then
            Me.Height = My.Computer.Screen.Bounds.Height
            Panel1.Height = Me.Size.Height - 69
            Panel1.Location = New Point(0, 30)
        End If


        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        If Main.UserBowser = False Then
            Me.Location = New Point(-10000, 10000)
            Timer1.Enabled = True
        End If
        WebView2.Source = New Uri(Main.Startseite)
    End Sub

    Private Sub Browser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Main.BowserWasOpen = False

        'Main.UserBowser = False
        'Me.Location = New Point(-10000, -10000)
        'Main.LoadingUrl = ""
        'Debug.WriteLine("Collecting")
        'Dim Collector As New TaskCookieVisitor
        'Dim CM As ICookieManager = WebBrowser1.GetCookieManager
        'CM.VisitAllCookies(Collector)
        'Main.CookieList = Collector.Task.Result()
        'Debug.WriteLine("Collecting-end")
        ''e.Cancel = True
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'MsgBox(Main.CR_etp_rt)
        'MsgBox(Main.CR_ajs_user_id)
        'MsgBox(Main.CheckCRLogin.ToString)

        Try
            My.Computer.Clipboard.SetText(WebView2.CoreWebView2.Source)
            MsgBox("copied: " + Chr(34) + WebView2.CoreWebView2.Source + Chr(34))
        Catch ex As Exception
        End Try

        'My.Computer.Clipboard.SetText(WebBrowser1.Document.Body.InnerHtml)

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        Try
            If e.KeyCode = Keys.Return Then
                e.SuppressKeyPress = True
                Debug.WriteLine("Start loading: " + Date.Now.ToString)
                WebView2.CoreWebView2.Navigate(TextBox1.Text)
            End If

        Catch ex As Exception
            MsgBox("Error in URL", MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Async Sub ObserveResponse(ByVal sender As Object, ByVal e As CoreWebView2WebResourceResponseReceivedEventArgs)


        If CBool(InStr(Main.LoadingUrl, "crunchyroll.com")) Then



            If CBool(InStr(e.Request.Uri, "crunchyroll.com/")) And CBool(InStr(e.Request.Uri, "v1/token")) And Main.CR_v1Token = "Get" Then
                Debug.WriteLine("Crunchyroll-v1_token: " + e.Request.Uri)
                Dim Content As Stream = Await e.Response.GetContentAsync
                Dim ContentString As String = Nothing
                Dim reader As New StreamReader(Content)
                ContentString = reader.ReadToEnd

                Dim Loc_CR_Cookies = " -H " + Chr(34) + Main.CR_Cookies + Chr(34)
                Dim Token() As String = ContentString.Split(New String() {Chr(34) + "access_token" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim Token2() As String = Token(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim Auth As String = "Bearer " + Token2(0)
                Dim Auth2 As String = " -H " + Chr(34) + "Authorization: " + Auth + Chr(34)
                Main.ProcessLoading(Main.LoadingUrl, Auth2, Loc_CR_Cookies, 0)
                Main.CR_v1Token = ""
                Exit Sub


            ElseIf CBool(InStr(e.Request.Uri, "crunchyroll.com/")) And CBool(InStr(e.Request.Uri, "seasons?preferred_audio_language=")) And CBool(InStr(Main.LoadingUrl, "/series/")) Then
                Debug.WriteLine("Crunchyroll-Season: " + e.Request.Uri)
                Main.LoadedUrls.Add(e.Request)

                Exit Sub
                'ElseIf CBool(InStr(e.Request.Uri, "crunchyroll.com/")) And CBool(InStr(e.Request.Uri, "content/v2/cms/objects/")) Then
                '    Debug.WriteLine("Crunchyroll-objects added to cache: " + e.Request.Uri)
                '    Main.LoadedUrls.Add(e.Request)

                '    Exit Sub
                'ElseIf CBool(InStr(e.Request.Uri, "crunchyroll.com/")) And CBool(InStr(e.Request.Uri, "seasons?preferred_audio_language=")) And CBool(InStr(Main.LoadingUrl, "/series/")) Then
                '    Debug.WriteLine("Crunchyroll-objects added to cache: " + e.Request.Uri)
                '    Main.LoadedUrls.Add(e.Request)

                '    Exit Sub
            End If


        ElseIf CBool(InStr(Main.LoadingUrl, "funimation.com")) Then
            If CBool(InStr(e.Request.Uri, "?deviceType=web")) Then
                'Debug.WriteLine(e.Request.Uri)
                Dim parms As String() = e.Request.Uri.Split(New String() {"?deviceType="}, System.StringSplitOptions.RemoveEmptyEntries)
                Main.FunimationDeviceRegion = "?deviceType=" + parms(1)

            End If
            If CBool(InStr(e.Request.Uri, "https://title-api.prd.funimationsvc.com")) Then
                Debug.WriteLine("Funimtaion: " + e.Request.Uri)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request)
                    Exit Sub
                End If
            ElseIf CBool(InStr(e.Request.Uri, "/data/v2/shows/")) Then
                Debug.WriteLine("Funimtaion: " + e.Request.Uri)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request)
                    Exit Sub
                End If
            ElseIf CBool(InStr(e.Request.Uri, "/data/v1/episodes/")) Then
                Debug.WriteLine("Funimtaion: " + e.Request.Uri)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request)
                    Exit Sub
                End If
            End If



        End If

    End Sub







    Private Sub ObserveHttp(ByVal sender As Object, ByVal e As CoreWebView2WebResourceRequestedEventArgs) 'Handles RequestResource.GetUrl


        ' Debug.WriteLine(e.Request.Uri)

        If CBool(InStr(e.Request.Uri, "crunchyroll.com")) = True And Main.CrBetaBasic = Nothing Then
            Dim Headers As New List(Of KeyValuePair(Of String, String))
            Headers.AddRange(e.Request.Headers.ToList)
            For i As Integer = 0 To Headers.Count
                If CBool(InStr(Headers.Item(i).Value, "Basic")) Then
                    Main.CrBetaBasic = Headers.Item(i).Value
                    Debug.WriteLine("Auth-Basic: " + Main.CrBetaBasic)
                ElseIf CBool(InStr(Headers.Item(i).Value, "Bearer")) Then
                    Main.CR_AuthToken = Headers.Item(i).Value
                    Debug.WriteLine("Auth-Bearer: " + Main.CR_AuthToken)
                End If
            Next
        End If

        'If CBool(InStr(e.Request.Uri, "crunchyroll.com/")) And CBool(InStr(e.Request.Uri, "streams?")) Then
        '    Dim Headers As New List(Of KeyValuePair(Of String, String))
        '    Headers.AddRange(e.Request.Headers.ToList)
        '    For i As Integer = 0 To Headers.Count
        '        If CBool(InStr(Headers.Item(i).Value, "Bearer")) Then
        '            Main.CR_AuthToken = Headers.Item(i).Value
        '            Debug.WriteLine("Auth-Bearer: " + Main.CR_AuthToken)
        '        End If
        '    Next
        'End If

        '
        '
        'If CBool(InStr(e.Request.Uri, "9016.2bd48f1e07adf6596b2d.js")) = True Then '
        '    Debug.WriteLine(e.Request.Uri)
        '    'MsgBox("Found!")
        '    e.Response = WebView2.CoreWebView2.Environment.CreateWebResourceResponse(StringToStream(File.ReadAllText(Application.StartupPath + "\WebInterface\9016.2bd48f1e07adf6596b2d.js"), Encoding.UTF8), 200, "Not found", "content-type: application/javascript")


        'End If


    End Sub


    Public Function StringToStream(input As String, enc As Encoding) As Stream
        Dim memoryStream = New MemoryStream()
        Dim streamWriter = New StreamWriter(memoryStream, enc)
        streamWriter.Write(input)
        streamWriter.Flush()
        memoryStream.Position = 0
        Return memoryStream
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'If Main.UserBowser = False Then
        '    WebView2.Reload()
        'Else
        '    Timer1.Enabled = False
        'End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Me.Close()
    End Sub


End Class

