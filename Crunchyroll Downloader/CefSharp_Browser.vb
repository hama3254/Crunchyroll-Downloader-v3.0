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



    Public keks As String = Nothing
    'Public c As Boolean = True
    Dim t As Thread
    Public ScanTrue As Boolean = False
    Public ScanTime As Integer = 0


    Dim DocumentTitle As String = ""
    Dim Document As String = ""
    Dim Cookie As String = ""



    Private Sub WebBrowser1_FrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs) Handles WebBrowser1.FrameLoadEnd
        If e.Frame.IsMain Then



            Me.Invoke(New Action(Function() As Object

                                     Main.LoadedUrls.Clear()
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
        Dim locale As String = "en-US"
        Main.CR_Cookies = "Cookies: "
        Try
            Dim Collector As New TaskCookieVisitor
            Dim CM As ICookieManager = WebBrowser1.GetCookieManager
            CM.VisitAllCookies(Collector)
            Dim DeviceRegion As String = Nothing
            Dim list As List(Of Global.CefSharp.Cookie) = Collector.Task.Result()
            For i As Integer = 0 To list.Count - 1

                If CBool(InStr(list.Item(i).Domain, ".crunchyroll.com")) = True And CBool(InStr(list.Item(i).Name, "_evidon_suppress")) = False Then
                    Main.CR_Cookies = Main.CR_Cookies + list.Item(i).Name + "=" + list.Item(i).Value + ";"
                    ' MsgBox(list.Item(i).Domain + vbNewLine + list.Item(i).Name + ":" + vbNewLine + list.Item(i).Value)
                End If
                If CBool(InStr(list.Item(i).Domain, ".crunchyroll.com")) And CBool(InStr(list.Item(i).Name, "c_locale")) Then
                    locale = list.Item(i).Value

                End If
            Next
        Catch ex As Exception
            Debug.Write(ex.ToString)
        End Try

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

        If CBool(InStr(WebBrowser1.Address, "https://proxer.me/read/")) Then
            Main.WebbrowserURL = WebBrowser1.Address
            Dim NameDLFinal As String = Nothing
            Dim NameDL As String() = Document.Split(New String() {"<div id=" + Chr(34) + "breadcrumb" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NameDL2 As String() = NameDL(1).Split(New String() {"<div>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NameDL3 As String() = NameDL2(0).Split(New String() {Chr(34) + "true" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To NameDL3.Count - 1
                If CBool(InStr(NameDL3(i), "</a>")) Then
                    Dim NameDL4 As String() = NameDL3(i).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    If NameDLFinal = Nothing Then
                        NameDLFinal = NameDL4(0)
                    Else
                        NameDLFinal = NameDLFinal + " " + NameDL4(0)
                    End If
                End If
            Next
            NameDLFinal = String.Join(" ", NameDLFinal.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) ''System.Text.RegularExpressions.Regex.Replace(NameDLFinal, "[^\w\\-]", " ")
            If Main.Debug2 = True Then
                MsgBox(NameDLFinal)
            End If
            Dim SiteData As String() = Document.Split(New String() {"var pages ="}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim SiteData2 As String() = SiteData(1).Split(New String() {"</script>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ImageNumbers As String() = SiteData2(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ImageList As New List(Of String)
            Dim ImageListString As String = Nothing
            For i As Integer = 0 To ImageNumbers.Count - 1
                If CBool(InStr(ImageNumbers(i), ".jpg")) Then
                    ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
                    ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
                ElseIf CBool(InStr(ImageNumbers(i), ".png")) Then
                    ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
                    ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
                End If
            Next
            If Main.Debug2 = True Then
                MsgBox(ImageListString)
            End If
            Dim BaseURL As String() = SiteData2(0).Split(New String() {"var serverurl = '"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim BaseURL2 As String() = BaseURL(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim BaseURL3 As String = "https:" + BaseURL2(0)
            If Main.Debug2 = True Then
                MsgBox(BaseURL3)
            End If
            'AsyncWorkerX.RunAsync(AddressOf Main.DownloadMangaPages, BaseURL3, ImageList, Main.RemoveExtraSpaces(NameDLFinal))
            Dim Thumbnail As String = BaseURL3 + ImageList(0)
            Main.MangaListItemAdd(Main.RemoveExtraSpaces(NameDLFinal), Thumbnail, BaseURL3, ImageList)

        ElseIf CBool(InStr(WebBrowser1.Address, "cr-cookie-ui.php")) Then
            'MsgBox(WebBrowser1.Document.Body.InnerHtml)
        Else
            Main.m3u8List.Clear()
            Main.mpdList.Clear()
            Main.txtList.Clear()
            Btn_Scan.Enabled = False
            ScanTrue = True
            Main.LogBrowserData = True
            NetworkScanEnd()
        End If
    End Sub

    Private Sub GeckoFX_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        If Main.Debug2 = True Then
            Debug_Mode.Location = New Point(Me.Location.X + Me.Width - 15, Me.Location.Y)
        End If
    End Sub


    Private Sub ObserveHttp(e As RequestResourceEventArgs) 'Handles RequestResource.GetUrl
        'Debug.WriteLine(e.Request.Url)

        'If (Me.InvokeRequired) Then
        '    If Main.b = True And Main.FunimationJsonBrowser = Nothing Then

        '        Exit Sub
        '    Else
        '        Debug.WriteLine("false i guess?")
        '        Debug.WriteLine(Main.b.ToString)
        '        Debug.WriteLine(Main.FunimationJsonBrowser)
        '    End If

        'Else
        '    If Main.b = True And Main.FunimationJsonBrowser = Nothing Then
        '        Exit Sub
        '    Else
        '        Debug.WriteLine("false i guess?")
        '        Debug.WriteLine(Main.b.ToString)
        '        Debug.WriteLine(Main.FunimationJsonBrowser)
        '    End If
        'End If



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





        '
        'Debug.WriteLine(e.Request.Url)

        If CBool(InStr(e.Request.Url, "?deviceType=web")) Then
            'Debug.WriteLine(e.Request.Url)
            Dim parms As String() = e.Request.Url.Split(New String() {"?deviceType="}, System.StringSplitOptions.RemoveEmptyEntries)
            Main.FunimationDeviceRegion = "?deviceType=" + parms(1)

        End If
        If CBool(InStr(e.Request.Url, "https://title-api.prd.funimationsvc.com")) Then
            If (Me.InvokeRequired) Then
                Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                Exit Sub
            Else
                Main.LoadedUrls.Add(e.Request.Url)
                Exit Sub
            End If
            Debug.WriteLine(e.Request.Url)
        ElseIf CBool(InStr(e.Request.Url, "/data/v2/shows/")) Then
            If (Me.InvokeRequired) Then
                Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                Exit Sub
            Else
                Main.LoadedUrls.Add(e.Request.Url)
                Exit Sub
            End If
            Debug.WriteLine(e.Request.Url)
        ElseIf CBool(InStr(e.Request.Url, "/data/v1/episodes/")) Then
            If (Me.InvokeRequired) Then
                Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                Exit Sub
            Else
                Main.LoadedUrls.Add(e.Request.Url)
                Exit Sub
            End If
            Debug.WriteLine(e.Request.Url)
        ElseIf CBool(InStr(e.Request.Url, "crunchyroll.com/")) And CBool(InStr(e.Request.Url, "streams?")) Then
            If (Me.InvokeRequired) Then
                Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                Exit Sub
            Else
                Main.LoadedUrls.Add(e.Request.Url)
                Exit Sub
            End If
            Debug.WriteLine(e.Request.Url)
        ElseIf CBool(InStr(e.Request.Url, "crunchyroll.com/")) And CBool(InStr(e.Request.Url, "seasons?series_id=")) Then
            If (Me.InvokeRequired) Then
                Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                Exit Sub
            Else
                Main.LoadedUrls.Add(e.Request.Url)
                Exit Sub
            End If
            Debug.WriteLine(e.Request.Url)
        ElseIf CBool(InStr(e.Request.Url, "https://api.vrv.co")) And CBool(InStr(e.Request.Url, "streams?")) Then
            If (Me.InvokeRequired) Then
                Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                Exit Sub
            Else
                Main.LoadedUrls.Add(e.Request.Url)
                Exit Sub
            End If
            Debug.WriteLine(e.Request.Url)
        ElseIf CBool(InStr(e.Request.Url, "https://api.vrv.co")) And CBool(InStr(e.Request.Url, "seasons?series_id=")) Then
            If (Me.InvokeRequired) Then
                Me.Invoke(Sub() Main.LoadedUrls.Add(e.Request.Url))
                Exit Sub
            Else
                Main.LoadedUrls.Add(e.Request.Url)
                Exit Sub
            End If
            Debug.WriteLine(e.Request.Url)

        End If


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

