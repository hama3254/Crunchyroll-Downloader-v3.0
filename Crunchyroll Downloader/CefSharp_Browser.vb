Option Strict On

Imports System.IO
Imports CefSharp
Imports System.ComponentModel
Imports System.Threading
Imports System.Net
Imports System.Net.WebUtility
Imports System.IO.Compression
Imports System.Text
Imports AdapterRequestHandler
Imports System.Security.Policy

Public Class CefSharp_Browser



    'Public keks As String = Nothing
    'Public c As Boolean = True
    Dim t As Thread
    Public ScanTrue As Boolean = False
    Public ScanTime As Integer = 0


    Dim DocumentTitle As String = ""
    Dim Document As String = ""
    Dim Cookie As String = ""

    Dim LoadingUrl As String = ""

    Private Sub WebBrowser1_FrameLoadStart(sender As Object, e As FrameLoadStartEventArgs) Handles WebBrowser1.FrameLoadStart
        LoadingUrl = e.Url
        Main.LoadedUrls.Clear()
    End Sub




    Private Sub WebBrowser1_FrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs) Handles WebBrowser1.FrameLoadEnd
        If e.Frame.IsMain Then



            Me.Invoke(New Action(Function() As Object
                                     '  Main.LoadedUrls.Clear()
                                     Debug.WriteLine("FrameLoadEnd" + Date.Now.ToString)
                                     Main.WebbrowserURL = WebBrowser1.Address
                                     TextBox1.Text = Main.WebbrowserURL

                                     Try
                                         If Btn_Scan.Enabled = False And Btn_Scan.Text = "Start network scan" Then
                                             Btn_Scan.Enabled = True

                                         End If
                                     Catch ex As Exception
                                     End Try


                                     Return Nothing
                                 End Function))

            GetHTML()

        End If
    End Sub


    'Private Sub WebBrowser1_LoadingStateChanged(sender As Object, ByVal Status As LoadingStateChangedEventArgs) Handles WebBrowser1.LoadingStateChanged
    '    'Debug.WriteLine("b:" + Main.b.ToString)
    '    Debug.WriteLine(InvokeRequired.ToString)

    '    If Status.IsLoading = True Then
    '        Exit Sub
    '    End If

    'End Sub

    Async Sub GetHTML()
        Try

            Dim HTML As String = Await WebBrowser1.GetSourceAsync
            ' Dim HTML2 As String = Await 

            Document = HTML
            Debug.WriteLine("get html")

            Me.Invoke(New Action(Function() As Object

                                     Main.WebbrowserText = HTML
                                     Main.WebbrowserURL = WebBrowser1.Address
                                     Main.WebbrowserTitle = DocumentTitle
                                     Main.ProcessHTML(HTML, WebBrowser1.Address, DocumentTitle)
                                     'If Main.UserBowser = False Then
                                     '    Me.Close()
                                     'End If
                                     Return Nothing
                                 End Function))

        Catch ex As Exception

        End Try
    End Sub




    Private Sub GeckoFX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler RequestResource.GetUrl, AddressOf ObserveHttp
        WebBrowser1.RequestHandler = New RequestEventHandler()
        Main.waveOutSetVolume(0, 0)
        If Me.Width > My.Computer.Screen.Bounds.Width Then
            Me.Width = My.Computer.Screen.Bounds.Width
            WebBrowser1.Width = Me.Size.Width - 15 ', Me.Size.Height - 69)
            WebBrowser1.Location = New Point(0, 30)
            TextBox1.Width = My.Computer.Screen.Bounds.Width - 435

        End If

        If Me.Size.Height > My.Computer.Screen.Bounds.Height Then
            Me.Height = My.Computer.Screen.Bounds.Height
            WebBrowser1.Height = Me.Size.Height - 69
            WebBrowser1.Location = New Point(0, 30)
        End If

        If Main.Debug2 = True Then
            Debug_Mode.Show()
            Debug_Mode.Location = New Point(Me.Location.X + Me.Width - 15, Me.Location.Y)
        End If
        'MsgBox(WebBrowser1.Address)
        If WebBrowser1.Address = "about:blank" Or WebBrowser1.Address = Nothing Then
            TextBox1.Text = Main.Startseite
            WebBrowser1.Load(Main.Startseite)

        End If
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        If Main.UserBowser = False Then
            Me.Location = New Point(-10000, -10000)

        End If
    End Sub

    Private Sub CefSharp_Browser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Main.UserBowser = False
        Me.Location = New Point(-10000, -10000)

        e.Cancel = True
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'MsgBox(Main.CR_etp_rt)
        'MsgBox(Main.CR_ajs_user_id)
        'MsgBox(Main.CheckCRLogin.ToString)

        Try
            My.Computer.Clipboard.SetText(WebBrowser1.Address)
            MsgBox("copied: " + Chr(34) + WebBrowser1.Address + Chr(34))
        Catch ex As Exception
        End Try

        'My.Computer.Clipboard.SetText(WebBrowser1.Document.Body.InnerHtml)

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        Try
            If e.KeyCode = Keys.Return Then
                e.SuppressKeyPress = True
                Debug.WriteLine("Start loading: " + Date.Now.ToString)
                WebBrowser1.Load(TextBox1.Text)
            End If

        Catch ex As Exception
            MsgBox("Error in URL", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub WebBrowser1_DocumentTitleChanged(sender As Object, e As TitleChangedEventArgs) Handles WebBrowser1.TitleChanged

        If (Me.InvokeRequired) Then
            Me.Invoke(Sub()
                          DocumentTitle = e.Title
                          Me.Text = "Browser - " + e.Title

                      End Sub)
        Else
            DocumentTitle = e.Title
            Me.Text = "Browser - " + e.Title

        End If

        Try
            TextBox1.Text = WebBrowser1.Address
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click

        Main.m3u8List.Clear()
        Main.mpdList.Clear()
            Main.txtList.Clear()
            Btn_Scan.Enabled = False
            ScanTrue = True
            Main.LogBrowserData = True
            NetworkScanEnd()

    End Sub

    Private Sub GeckoFX_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        If Main.Debug2 = True Then
            Debug_Mode.Location = New Point(Me.Location.X + Me.Width - 15, Me.Location.Y)
        End If
    End Sub


    Private Sub ObserveHttp(e As RequestResourceEventArgs) 'Handles RequestResource.GetUrl

        If CBool(InStr(LoadingUrl, "crunchyroll.com")) Then
            If CBool(InStr(e.Request.Url, "crunchyroll.com/")) And CBool(InStr(e.Request.Url, "streams?")) Then
                Debug.WriteLine("Crunchyroll-Single: " + e.Request.Url)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Url)
                    Exit Sub
                End If
            ElseIf CBool(InStr(e.Request.Url, "crunchyroll.com/")) And CBool(InStr(e.Request.Url, "/objects/")) And CBool(InStr(e.Request.Url, "/watch/")) Then
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Url)
                    Exit Sub
                End If
                Debug.WriteLine(e.Request.Url)
            ElseIf CBool(InStr(e.Request.Url, "crunchyroll.com/")) And CBool(InStr(e.Request.Url, "seasons?series_id=")) Then
                Debug.WriteLine("Crunchyroll-Season: " + e.Request.Url)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Url)
                    Exit Sub
                End If
            End If


            If (Me.InvokeRequired) Then
                Me.Invoke(Sub()
                              If CBool(InStr(e.Request.Url, "crunchyroll.com")) = True And CBool(InStr(e.Request.Headers, "Basic ")) = True And Main.CrBetaBasic = Nothing Then
                                  Dim Basic As String() = e.Request.Headers.Split(New String() {"Basic "}, System.StringSplitOptions.RemoveEmptyEntries)
                                  Dim Basic2 As String() = Basic(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                                  Main.CrBetaBasic = "Basic " + Basic2(0)
                                  Debug.WriteLine(Main.CrBetaBasic)
                              End If



                          End Sub)
            Else
                If CBool(InStr(e.Request.Url, "crunchyroll.com")) = True And CBool(InStr(e.Request.Headers, "Basic ")) = True And Main.CrBetaBasic = Nothing Then

                    Dim Basic As String() = e.Request.Headers.Split(New String() {"Basic "}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim Basic2 As String() = Basic(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                    Main.CrBetaBasic = "Basic " + Basic2(0)
                    Debug.WriteLine(Main.CrBetaBasic)

                End If

            End If
        ElseIf CBool(InStr(LoadingUrl, "funimation.com")) Then
            If CBool(InStr(e.Request.Url, "?deviceType=web")) Then
                'Debug.WriteLine(e.Request.Url)
                Dim parms As String() = e.Request.Url.Split(New String() {"?deviceType="}, System.StringSplitOptions.RemoveEmptyEntries)
                Main.FunimationDeviceRegion = "?deviceType=" + parms(1)

            End If
            If CBool(InStr(e.Request.Url, "https://title-api.prd.funimationsvc.com")) Then
                Debug.WriteLine("Funimtaion: " + e.Request.Url)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Url)
                    Exit Sub
                End If
            ElseIf CBool(InStr(e.Request.Url, "/data/v2/shows/")) Then
                Debug.WriteLine("Funimtaion: " + e.Request.Url)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Url)
                    Exit Sub
                End If
            ElseIf CBool(InStr(e.Request.Url, "/data/v1/episodes/")) Then
                Debug.WriteLine("Funimtaion: " + e.Request.Url)
                If (Me.InvokeRequired) Then
                    Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                    Exit Sub
                Else
                    Main.LoadedUrls.Add(e.Request.Url)
                    Exit Sub
                End If
            End If

        End If




        '
        'Debug.WriteLine(e.Request.Url)





    End Sub

    Public Sub NetworkScanEnd()
        For i As Integer = 20 To 0 Step -1
            Pause(1)
            Btn_Scan.Text = "network scan is in progess " + Math.Abs(i).ToString
            Try
                Anime_Add.StatusLabel.Text = "network scan is in progess " + Math.Abs(i).ToString
            Catch ex As Exception

            End Try
        Next
        ScanTrue = False
        Main.LogBrowserData = False
        Try
            Main.WebbrowserURL = WebBrowser1.Address
            Main.WebbrowserText = Document
            Main.WebbrowserTitle = DocumentTitle
            Main.WebbrowserCookie = Cookie
        Catch ex As Exception
        End Try
        Btn_Scan.Text = "use network scan dialog"
        network_scan.Show()
    End Sub






    'Private Sub WebBrowser1_ConsoleMessage(sender As Object, e As ConsoleMessageEventArgs) Handles WebBrowser1.ConsoleMessage
    '    Debug.WriteLine(e.Message)
    'End Sub

End Class

