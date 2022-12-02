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

Public Class Browser




    Private Sub WebView2_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WebView2.CoreWebView2InitializationCompleted
        WebView2.CoreWebView2.AddWebResourceRequestedFilter("https://www.crunchyroll.com/*", CoreWebView2WebResourceContext.All)
        WebView2.CoreWebView2.AddWebResourceRequestedFilter("https://www.funimation.com/*", CoreWebView2WebResourceContext.All)
        'WebView2.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All)

        AddHandler WebView2.CoreWebView2.WebResourceRequested, AddressOf ObserveHttp
        WebView2.CoreWebView2.Settings.UserAgent = My.Resources.ffmpeg_user_agend.Replace(Chr(34), "").Replace("User-Agent: ", "")
        If WebView2.CoreWebView2.Source = "about:blank" Or WebView2.CoreWebView2.Source = Nothing Then
            'TextBox1.Text = Main.Startseite
            WebView2.CoreWebView2.Navigate(Main.Startseite)

        End If

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

            'Main.WebbrowserText = "" 'HTML
            Main.WebbrowserTitle = DocumentTitle
            Main.ProcessHTML("", Main.WebbrowserURL, DocumentTitle)

            GetCookies(Main.WebbrowserURL)
        End If

    End Sub

    Public Async Sub GetCookies(ByVal Uri As String)
        Main.CookieList = Await WebView2.CoreWebView2.CookieManager.GetCookiesAsync(Uri)
    End Sub


    Private Sub GeckoFX_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        End If
        WebView2.Source = New Uri(Main.Startseite)
    End Sub

    Private Sub Browser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
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








    Private Sub ObserveHttp(ByVal sender As Object, ByVal e As CoreWebView2WebResourceRequestedEventArgs) 'Handles RequestResource.GetUrl

        If CBool(InStr(e.Request.Uri, "crunchyroll.com")) = True And Main.CrBetaBasic = Nothing Then
            Dim Headers As New List(Of KeyValuePair(Of String, String))
            Headers.AddRange(e.Request.Headers.ToList)
            For i As Integer = 0 To Headers.Count
                If CBool(InStr(Headers.Item(i).Value, "Basic")) Then
                    Main.CrBetaBasic = Headers.Item(i).Value
                    Debug.WriteLine(Main.CrBetaBasic)
                End If
            Next
        End If



        If CBool(InStr(Main.LoadingUrl, "crunchyroll.com")) Then
            If CBool(InStr(e.Request.Uri, "crunchyroll.com/")) And CBool(InStr(e.Request.Uri, "streams?")) Then
                Debug.WriteLine("Crunchyroll-Single: " + e.Request.Uri)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Uri))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Uri)
                    Exit Sub
                End If
            ElseIf CBool(InStr(e.Request.Uri, "crunchyroll.com/")) And CBool(InStr(e.Request.Uri, "/objects/")) And CBool(InStr(e.Request.Uri, "/watch/")) Then
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Uri))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Uri)
                    Exit Sub
                End If
                Debug.WriteLine(e.Request.Uri)
            ElseIf CBool(InStr(e.Request.Uri, "crunchyroll.com/")) And CBool(InStr(e.Request.Uri, "seasons?series_id=")) Then
                Debug.WriteLine("Crunchyroll-Season: " + e.Request.Uri)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Uri))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Uri)
                    Exit Sub
                End If
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
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Uri))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Uri)
                    Exit Sub
                End If
            ElseIf CBool(InStr(e.Request.Uri, "/data/v2/shows/")) Then
                Debug.WriteLine("Funimtaion: " + e.Request.Uri)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Uri))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Uri)
                    Exit Sub
                End If
            ElseIf CBool(InStr(e.Request.Uri, "/data/v1/episodes/")) Then
                Debug.WriteLine("Funimtaion: " + e.Request.Uri)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Uri))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Uri)
                    Exit Sub
                End If
            End If

        End If

    End Sub



    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Me.Close()
    End Sub















    'Private Sub WebBrowser1_ConsoleMessage(sender As Object, e As ConsoleMessageEventArgs) Handles WebBrowser1.ConsoleMessage
    '    Debug.WriteLine(e.Message)
    'End Sub

End Class

