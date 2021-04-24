Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Threading
Imports Microsoft.Win32
Imports System.ComponentModel
Imports MetroFramework
Imports MetroFramework.Components
Imports MetroFramework.Forms


Public Class CRD_List_Item
    Inherits Controls.MetroUserControl


    Dim ZeitGesamtInteger As Integer = 0
    Dim ListOfStreams As New List(Of String)
    Dim proc As Process
    Dim ThreadList As New List(Of Thread)
    Dim Item_ErrorTolerance As Integer
    Dim Canceld As Boolean = False
    Dim Finished As Boolean = False
    Dim Label_website_Text As String = Nothing
    Dim StatusRunning As Boolean = True
    Dim ffmpeg_command As String = Nothing
    Dim Debug2 As Boolean = False
    Dim MergeSubstoMP4 As Boolean = False

    Dim DownloadPfad As String = Nothing
    Dim ToDispose As Boolean = False
    Dim Failed As Boolean = False
    Dim FailedCount As Integer = 0
    Dim HistoryDL_URL As String
    Dim HistoryDL_Pfad As String
    Dim HistoryFilename As String
    Dim Retry As Boolean = False
    Dim HybridMode As Boolean = False
    Dim HybridModePath As String = Nothing
    Dim HybridRunning As Boolean = False
    Dim TargetReso As Integer = 1080
    Dim HybrideLog As String = Nothing
    Dim Service As String = "CR"
    Dim ServiceSleep As Integer = 0

    Dim LastDate As Date = Date.Now
    Dim LastSize As Double = 0
    Dim LastDataRate1 As Double = 0
    Dim LastDataRate2 As Double = 0
    Dim LastDataRate3 As Double = 0

    Dim FailedSegments As New List(Of FailedSegemtsWithURL)
    Dim LogText As New List(Of String)


    Dim PauseTime As Integer = 0
    Dim Threads As Integer = Environment.ProcessorCount / 2 - 1

#Region "Remove from list"
    Public Sub DisposeItem(ByVal Dispose As Boolean)
        If Dispose = True Then
            Me.Dispose()
        End If
    End Sub
    Public Function GetToDispose() As Boolean
        Return ToDispose
    End Function
#End Region
#Region "UI"

    Private Sub CRD_List_Item_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        bt_del.SetBounds(775, 10, 35, 29)
        bt_pause.SetBounds(740, 15, 25, 20)
        PB_Thumbnail.SetBounds(11, 20, 168, 95)
        PB_Thumbnail.BringToFront()
        Label_website.Location = New Point(195, 12)
        Label_Anime.Location = New Point(195, 40)
        Label_Reso.Location = New Point(195, 97)
        Label_Hardsub.Location = New Point(265, 97)
        Label_percent.SetBounds(Me.Width - 400, 97, 378, 27)
        Label_percent.AutoSize = False
        ProgressBar1.SetBounds(195, 70, 601, 20)
        PictureBox5.Location = New Point(0, 136)
        PictureBox5.Height = 6

        If Service = "AoD" Then
            MetroStyleManager1.Style = MetroColorStyle.LightGreen
            ServiceSleep = 1000
        ElseIf Service = "FM" Then
            MetroStyleManager1.Style = MetroColorStyle.DarkPurple
        Else
            MetroStyleManager1.Style = MetroColorStyle.Orange
        End If
        MetroStyleManager1.Theme = Main.Manager.Theme
        MetroStyleManager1.Owner = Me
        Me.StyleManager = MetroStyleManager1


        PictureBox5.Width = Me.Width - 40

        bt_del.Location = New Point(Me.Width - 63, 10)
        bt_pause.Location = New Point(Me.Width - 98, 15)

        ProgressBar1.Width = Me.Width - 223

    End Sub


    Public Sub SetLabelWebsite(ByVal Text As String)
        Label_website.Text = Text
        Label_website_Text = Text
    End Sub

    Public Sub SetTheme(ByVal Theme As MetroThemeStyle)
        MetroStyleManager1.Theme = Theme
    End Sub

    Public Sub SetTolerance(ByVal value As Integer)
        Item_ErrorTolerance = value
    End Sub
    Public Sub SetLabelAnimeTitel(ByVal Text As String)
        Label_Anime.Text = Text
    End Sub
    Public Sub SetLabelResolution(ByVal Text As String)
        Label_Reso.Text = Text
    End Sub
    Public Sub SetLabelHardsub(ByVal Text As String)
        Label_Hardsub.Text = Text
    End Sub
    Public Sub SetLabelPercent(ByVal Text As String)
        Label_percent.Text = Text
    End Sub
    Public Sub SetThumbnailImage(ByVal Thumbnail As Image)
        PB_Thumbnail.BackgroundImage = Thumbnail
    End Sub
#End Region
#Region "Get Variables"
    Public Function GetPauseStatus() As Boolean
        Return StatusRunning
    End Function
    Public Function GetIsStatusFinished() As Boolean
        If Canceld = True Then
            Return True
        ElseIf HybridRunning = True Then
            Return False
        Else
            If proc.HasExited = True Then
                Return True
            Else
                Return False
            End If
        End If

    End Function
    Public Function GetLabelPercent()
        Try
            Return Label_percent.Text
        Catch ex As Exception
            Return 0
        End Try

    End Function
    Public Function GetPercentValue()
        Try
            Return ProgressBar1.Value
        Catch ex As Exception

            Return 0
        End Try

    End Function
    Public Function GetNameAnime()
        Try
            Return Label_Anime.Text
        Catch ex As Exception
            Return "error"
        End Try

    End Function
#End Region
#Region "Set Variables"
    Public Sub Setffmpeg_command(ByVal Value As String)
        ffmpeg_command = Value
    End Sub
    Public Sub SetMergeSubstoMP4(ByVal Value As Boolean)
        MergeSubstoMP4 = Value
    End Sub
    Public Sub SetDebug2(ByVal Value As Boolean)
        Debug2 = Value
    End Sub

    Public Sub SetTargetReso(ByVal Value As Integer)
        TargetReso = Value
    End Sub
    Public Sub SetService(ByVal Value As String)
        Service = Value
    End Sub
#End Region
    Public Sub KillRunningTask()
        If HybridRunning = True Then
            Canceld = True
        Else
            Try
                If proc.HasExited Then
                Else
                    proc.Kill()
                    proc.WaitForExit(500)
                    Label_percent.Text = "canceled -%"
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub BT_del_MouseEnter(sender As Object, e As EventArgs) Handles bt_del.MouseEnter
        Dim p As PictureBox = sender
        p.BackgroundImage = My.Resources.main_del_hover
    End Sub

    Private Sub BT_del_MouseLeave(sender As Object, e As EventArgs) Handles bt_del.MouseLeave
        Dim p As PictureBox = sender
        p.BackgroundImage = My.Resources.main_del
    End Sub
    Private Sub BT_pause_MouseEnter(sender As Object, e As EventArgs) Handles bt_pause.MouseEnter
        Dim p As PictureBox = sender
        If StatusRunning = True Then
            p.BackgroundImage = My.Resources.main_pause_hover
        Else
            p.BackgroundImage = My.Resources.main_pause_play_hover
        End If
    End Sub

    Private Sub BT_pause_MouseLeave(sender As Object, e As EventArgs) Handles bt_pause.MouseLeave
        Dim p As PictureBox = sender
        If StatusRunning = True Then
            p.BackgroundImage = My.Resources.main_pause
        Else
            p.BackgroundImage = My.Resources.main_pause_play
        End If
    End Sub

    Private Sub BT_pause_Click(sender As Object, e As EventArgs) Handles bt_pause.Click
        If Canceld = True And HybridRunning = True Then

            If Main.RunningDownloads < Main.MaxDL Then

            Else
                If MessageBox.Show("You have currtenly on your set Download limit." + vbNewLine + " You can Press OK to ignore it.", "Download maximum reached", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If
            Canceld = False
            'If My.Computer.FileSystem.FileExists(HistoryDL_Pfad.Replace(Chr(34), "")) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
            '    Try
            '        My.Computer.FileSystem.DeleteFile(HistoryDL_Pfad.Replace(Chr(34), ""))
            '    Catch ex As Exception
            '    End Try
            'End If
            StartDownload(HistoryDL_URL, HistoryDL_Pfad, HistoryFilename, HybridMode)
            StatusRunning = True
            Label_website.Text = Label_website_Text
            Exit Sub
        ElseIf HybridRunning = True Then
            If StatusRunning = True Then
                StatusRunning = False
                bt_pause.BackgroundImage = My.Resources.main_pause_play

            ElseIf Failed = True Then
                Dim Result As DialogResult = MessageBox.Show("The hybride mode has failed to download a fragment." + vbNewLine + "Press 'Retry' to retry the fragment or 'Ignore' to continue.", "Download Error", MessageBoxButtons.AbortRetryIgnore) '= DialogResult.Ignore Then

                If Result = DialogResult.Ignore Then
                    Failed = False
                    StatusRunning = True
                    bt_pause.BackgroundImage = My.Resources.main_pause
                    FailedSegments.Clear()
                ElseIf Result = DialogResult.Retry Then
                    If FailedSegments.Count > 0 Then
                        For i As Integer = 0 To FailedSegments.Count - 1
                            Dim ii As Integer = i
                            Dim Evaluator = New Thread(Sub() Me.TS_DownloadAsync(FailedSegments.Item(ii).url, FailedSegments.Item(ii).path))
                            FailedSegments.RemoveAt(i)
                            Evaluator.Start()
                            ThreadList.Add(Evaluator)
                        Next
                        Failed = False
                        StatusRunning = True
                        bt_pause.BackgroundImage = My.Resources.main_pause
                    End If
                ElseIf Result = DialogResult.Abort Then
                    If MessageBox.Show("Are you sure you want to cancel the Download?", "Cancel Download!", MessageBoxButtons.YesNo) = DialogResult.No Then
                        Exit Sub
                    End If
                    Canceld = True
                End If

            Else
                StatusRunning = True
                bt_pause.BackgroundImage = My.Resources.main_pause
            End If

        Else
            If proc.HasExited = True Then
                If ProgressBar1.Value < 100 Then
                    If Retry = True Then
                        If Main.RunningDownloads < Main.MaxDL Then

                        Else
                            If MessageBox.Show("You have currtenly on your set Download limit." + vbNewLine + " You can Press OK to ignore it.", "Download maximum reached", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
                                Exit Sub
                            End If
                        End If
                        If My.Computer.FileSystem.FileExists(HistoryDL_Pfad.Replace(Chr(34), "")) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                            Try
                                My.Computer.FileSystem.DeleteFile(HistoryDL_Pfad.Replace(Chr(34), ""))
                            Catch ex As Exception
                            End Try
                        End If
                        StartDownload(HistoryDL_URL, HistoryDL_Pfad, HistoryFilename, HybridMode)
                        StatusRunning = True
                        Label_website.Text = Label_website_Text
                    Else
                        MsgBox("The download process seems to have crashed", MsgBoxStyle.Exclamation)
                        Label_percent.Text = "Press the play button again to retry."
                        ProgressBar1.Value = 0
                        Retry = True
                        StatusRunning = False
                    End If

                Else
                End If
                Exit Sub
            End If
            If StatusRunning = True Then
                StatusRunning = False
                bt_pause.BackgroundImage = My.Resources.main_pause_play
                SuspendProcess(proc)
            Else
                If Failed = True Then
                    'If HybridMode = True Then

                    'Else
                    Dim Result As DialogResult = MessageBox.Show("The download has " + FailedCount.ToString + " failded segments" + vbNewLine + "Press 'Ignore' to continue", "Download Error", MessageBoxButtons.AbortRetryIgnore) '= DialogResult.Ignore Then

                    If Result = DialogResult.Ignore Then
                        Failed = False
                        StatusRunning = True
                        bt_pause.BackgroundImage = My.Resources.main_pause
                        ResumeProcess(proc)
                    ElseIf Result = DialogResult.Retry Then
                        Try
                            proc.Kill()
                            proc.WaitForExit(500)
                            Label_percent.Text = "retrying -%"
                            Label_website.Text = Label_website_Text
                        Catch ex As Exception
                        End Try

                        If proc.HasExited Then
                            StartDownload(HistoryDL_URL, HistoryDL_Pfad, HistoryFilename, HybridMode)
                            StatusRunning = True
                            Label_website.Text = Label_website_Text
                            bt_pause.BackgroundImage = My.Resources.main_pause
                        End If
                    ElseIf Result = DialogResult.Abort Then
                        Try
                            proc.Kill()
                            proc.WaitForExit(500)
                            Label_percent.Text = "canceled -%"
                            Label_website.Text = Label_website_Text
                        Catch ex As Exception
                        End Try
                    End If
                    ' End If
                Else
                    If StatusRunning = True Then
                        StatusRunning = False
                        bt_pause.BackgroundImage = My.Resources.main_pause_play
                        SuspendProcess(proc)
                    Else
                        StatusRunning = True
                        bt_pause.BackgroundImage = My.Resources.main_pause
                        ResumeProcess(proc)
                    End If
                End If

            End If
        End If

    End Sub
    Public Sub SetToolTip(ByVal Text As String)
        ToolTip1.SetToolTip(Me, Text)
    End Sub
    Private Sub Item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ContextMenuStrip = ContextMenuStrip1 '.ContextMenu
        If Threads < 2 Then
            Threads = 2
        End If
        'bt_del.SetBounds(775, 10, 35, 29)
        'bt_pause.SetBounds(740, 15, 25, 20)
        'PB_Thumbnail.SetBounds(11, 20, 168, 95)
        'PB_Thumbnail.BringToFront()
        'Label_website.Location = New Point(195, 15)
        'Label_Anime.Location = New Point(195, 37)
        'Label_Reso.Location = New Point(195, 97)
        'Label_Hardsub.Location = New Point(265, 97)
        'Label_percent.SetBounds(432, 97, 378, 27)
        'Label_percent.AutoSize = False
        'ProgressBar1.SetBounds(195, 70, 601, 20)
        'PictureBox5.Location = New Point(0, 136)
        'PictureBox5.Height = 6


        'MetroStyleManager1.Theme = Main.Manager.Theme
    End Sub

    Public Function GetTextBound()
        'Return Label_website.Location.Y
        Return bt_del.Size.Height
    End Function


#Region "Download + Update UI"

    Public Sub StartDownload(ByVal DL_URL As String, ByVal DL_Pfad As String, ByVal Filename As String, ByVal DownloadHybridMode As Boolean)
        'MsgBox(DL_URL)

        Me.StyleManager = MetroStyleManager1

        DownloadPfad = DL_Pfad
        HistoryDL_URL = DL_URL
        HistoryDL_Pfad = DL_Pfad
        HistoryFilename = Filename
        If InStr(DL_URL, "-i [Subtitles only]") Then
            Me.Invoke(New Action(Function()

                                     ProgressBar1.Value = 100
                                     Label_percent.Text = "selected subtiles have been dowloaded"
                                     Canceld = True
                                     PlaybackVideoFileToolStripMenuItem.Enabled = False
                                     LogTocClipboard.Enabled = False
                                     SaveToFile.Enabled = False
                                     Return Nothing
                                 End Function))
        ElseIf DownloadHybridMode = True Then
            Dim Evaluator = New Thread(Sub() DownloadHybrid(DL_URL, DL_Pfad, Filename))
            Evaluator.Start()
            HybridMode = True
            HybridRunning = True
        Else
            DownloadFFMPEG(DL_URL, DL_Pfad, Filename)
        End If

    End Sub

#Region "Download Cache"

    Public WithEvents WC_TS As WebClient



    Private Sub TS_DownloadAsync(ByVal DL_URL As String, ByVal DL_Pfad As String)
        HybrideLog = HybrideLog + vbNewLine + DL_Pfad + " - " + DL_URL
        Try
            'Dim wc_ts As New WebClient
            WC_TS = New WebClient

            WC_TS.DownloadFile(New Uri(DL_URL), DL_Pfad)
        Catch ex As Exception
            Debug.WriteLine("Download error #1: " + DL_Pfad)
            Try
                Dim wc_ts As New WebClient
                wc_ts.DownloadFile(New Uri(DL_URL), DL_Pfad)
            Catch ex2 As Exception
                FailedCount = FailedCount + 1
                If Item_ErrorTolerance = 0 Then

                ElseIf FailedCount >= Item_ErrorTolerance Then
                    FailedSegments.Add(New FailedSegemtsWithURL(DL_Pfad, DL_URL))
                    Failed = True
                    StatusRunning = False
                    bt_pause.BackgroundImage = My.Resources.main_pause_play
                    Me.Invoke(New Action(Function()

                                             Label_percent.Text = "Missing segment detected, retry or resume with the play button"
                                             Return Nothing
                                         End Function))
                End If


                Debug.WriteLine("Download error #2: " + DL_Pfad + vbNewLine + ex2.ToString + vbNewLine + DL_URL)
            End Try
        End Try

    End Sub
    Private Function TS_StatusAsync(ByVal prozent As Integer, ByVal di As IO.DirectoryInfo, ByVal pausetime As Integer)
        'Dim Now As Date = Date.Now

        Dim FinishedSize As Double = 0
        Dim AproxFinalSize As Double = 0

        Try

            Dim aryFi As IO.FileInfo() = di.GetFiles("*.*")
            Dim fi As IO.FileInfo
            For Each fi In aryFi
                FinishedSize = FinishedSize + fi.Length 'Math.Round(fi.Length / 1048576, 2, MidpointRounding.AwayFromZero).ToString()
            Next
        Catch ex As Exception
        End Try
        ''Thread.Sleep(1000)
        ''Pause(1)

        If prozent > 0 Then
            AproxFinalSize = Math.Round((FinishedSize / 1048576) * 100 / prozent, 2, MidpointRounding.AwayFromZero).ToString() ' Math.Round( / 1048576, 2, MidpointRounding.AwayFromZero).ToString()
        End If

        'Dim duration As TimeSpan = Date.Now - di.CreationTime
        'Dim TimeinSeconds As Integer = duration.Hours * 3600 + duration.Minutes * 60 + duration.Seconds
        'TimeinSeconds = TimeinSeconds - pausetime
        'Dim DataRate As Double = FinishedSize / TimeinSeconds
        'Dim DataRateString As String = Math.Round(DataRate, 2, MidpointRounding.AwayFromZero).ToString()
        Dim duration As TimeSpan = Date.Now - LastDate
        Dim TimeinMilliSeconds As Integer = duration.Seconds * 1000 + duration.Milliseconds

        If FinishedSize = LastSize Then
        ElseIf TimeinMilliSeconds < 250 Then
        Else


            LastDate = Date.Now
            'TimeinSeconds = TimeinSeconds - pausetime
            Dim SinceLast = FinishedSize - LastSize
            LastSize = FinishedSize

            Dim DataRate As Double = (SinceLast / 1048576) / (TimeinMilliSeconds / 1000)
            Dim DataRateFinal As Double = (DataRate + LastDataRate1 + LastDataRate2 + LastDataRate3) / 4
            LastDataRate3 = LastDataRate2
            LastDataRate2 = LastDataRate1
            LastDataRate1 = DataRate
            Dim DataRateString As String = Math.Round(DataRateFinal, 2, MidpointRounding.AwayFromZero).ToString()

            'Debug.WriteLine("----------------")
            'Debug.WriteLine(SinceLast)
            'Debug.WriteLine(TimeinMilliSeconds)
            'Debug.WriteLine(DataRate)
            If prozent > 100 Then
                prozent = 100
            ElseIf prozent < 0 Then
                prozent = 0
            End If
            Try
                Me.Invoke(New Action(Function()

                                         ProgressBar1.Value = prozent 'ThreadList.Count.ToString + " " +
                                         Label_percent.Text = DataRateString + "MB\s " + Math.Round(FinishedSize / 1048576, 2, MidpointRounding.AwayFromZero).ToString + "MB/" + Math.Round(AproxFinalSize, 2, MidpointRounding.AwayFromZero).ToString + "MB " + prozent.ToString + "%"

                                         Return Nothing
                                     End Function))
            Catch ex As Exception
            End Try
        End If
        'RaiseEvent UpdateUI(Filename, prozent, FinishedSize, AproxFinalSize, Color.FromArgb(247, 140, 37), DataRateString + "MB\s")

        Return Nothing

    End Function

    Private Function GetFullUri(ByVal MainUri As String, ByVal CurrentPath As String)
        Dim path As String = Nothing
        If InStr(CurrentPath, "../") Then
            Dim countDot() As String = CurrentPath.Split(New String() {"./"}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim c() As String = New Uri(MainUri).Segments
            path = "https://" + New Uri(MainUri).Host
            For i3 As Integer = 0 To c.Count - (2 + countDot.Count - 1)
                path = path + c(i3)
            Next
            path = path + countDot(countDot.Count - 1)
        Else
            Dim c() As String = New Uri(MainUri).Segments
            path = "https://" + New Uri(MainUri).Host
            For i3 As Integer = 0 To c.Count - 2
                path = path + c(i3)
            Next
        End If

        Return path
    End Function

    Private Function ProcessV4(ByVal InputData As String, ByVal Folder As String) As String

        If Not Directory.Exists(Path.GetDirectoryName(Folder)) Then
            ' Nein! Jetzt erstellen...
            Try
                Directory.CreateDirectory(Path.GetDirectoryName(Folder))
            Catch ex As Exception
                Debug.WriteLine("folder issue")
                Return "Error"
                Exit Function
            End Try
        End If

        Dim KeyFile As String = Einstellungen.GeräteID + ".key"
        Dim KeyFilePath As String = Application.StartupPath + "\" + KeyFile 'needs to be in the ffmpeg/downloader directory
        Dim Fragments() As String = InputData.Split(New String() {"#EXT-X-BYTERANGE:"}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim FragmentsInt As Integer = Fragments.Count - 2

        Dim di As New IO.DirectoryInfo(Folder)
        Dim m3u8FileContent As String = Nothing

        Dim Client As New WebClient
        Client.Encoding = Encoding.UTF8
        Client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))

        Dim m3u8Text As String = InputData
        Dim m3u8Key() As String = m3u8Text.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim m3u8Key2() As String = m3u8Key(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Client.DownloadFile(m3u8Key2(0), KeyFilePath)

        Dim StreamFile() As String = m3u8Text.Split(New String() {"https://"}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim StreamFile2() As String = StreamFile(2).Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim DownloadFile As String = "https:\\" + StreamFile2(0)

        Dim text() As String = m3u8Text.Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim zeile As String
        Dim Count As Integer = 0


        For i As Integer = 0 To text.Count - 1
            zeile = text(i)

            For w As Integer = 0 To Integer.MaxValue

                If StatusRunning = False Then
                    'MsgBox(True.ToString)
                    Thread.Sleep(5000)
                    PauseTime = PauseTime + 5
                ElseIf ThreadList.Count > Threads Then
                    Thread.Sleep(50)
                ElseIf Canceld = True Then
                    For www As Integer = 0 To Integer.MaxValue
                        If ThreadList.Count > 0 Then
                            Thread.Sleep(250)
                        Else
                            Try
                                System.IO.Directory.Delete(HybridModePath, True)
                            Catch ex As Exception
                            End Try
                            Me.Invoke(New Action(Function()
                                                     ProgressBar1.Value = 0
                                                     Label_percent.Text = "canceled -%"
                                                     bt_pause.BackgroundImage = My.Resources.main_pause_play
                                                     Return Nothing
                                                 End Function))
                            Exit For
                        End If
                    Next
                    Return Nothing
                    Exit Function

                Else
                    Thread.Sleep(ServiceSleep)
                    Exit For
                End If
            Next


            If zeile.Contains("#EXT-X-BYTERANGE:") Then
                Dim Zeile2 As String = zeile.Replace("#EXT-X-BYTERANGE:", "")
                Dim Zeile3() As String = Zeile2.Split(New String() {"@"}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim CurrentSize As Integer = Integer.Parse(Zeile3(1))
                Dim NewBytes As Integer = Integer.Parse(Zeile3(0))
                Dim File As String = Folder + String.Format("{0:0000}", Count)
                Dim Evaluator = New Thread(Sub() Me.DownloadTSv4(DownloadFile, File, CurrentSize, NewBytes))
                Evaluator.Start()
                ThreadList.Add(Evaluator)
                m3u8FileContent = m3u8FileContent + File + vbNewLine

                Count = Count + 1
                Dim FragmentsFinised = Count * 100 / FragmentsInt

                Dim Update = New Thread(Sub() Me.TS_StatusAsync(FragmentsFinised, di, PauseTime))
                Update.Start()

            ElseIf zeile.Contains("URI=" + Chr(34)) Then
                Dim Zeile2() As String = zeile.Split(New String() {"URI=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                m3u8FileContent = m3u8FileContent + Zeile2(0) + "URI=" + Chr(34) + KeyFile + Chr(34) + vbNewLine ' a full path does not work here, that's why KeyFilePath is in the ffmpeg/downloader folder

            ElseIf zeile.Contains("https://") Then
                'If zeile = DownloadFile Then
                'Else
                '    DownloadFile = zeile
                'End If
            Else
                m3u8FileContent = m3u8FileContent + zeile + vbNewLine

            End If

        Next

        My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\m3u8FileContent.txt", m3u8FileContent, False)

        Dim utf8WithoutBom As New System.Text.UTF8Encoding(False)
        Using sink As New StreamWriter(Folder + "\index.m3u8", False, utf8WithoutBom)
            sink.WriteLine(m3u8FileContent)
        End Using

        Return Folder + "\index.m3u8"

    End Function

    Private Sub DownloadTSv4(ByVal DL_URL As String, ByVal DL_Pfad As String, ByVal CurrentSize As Integer, ByVal NewBytes As Integer)
        Dim retry As Boolean = True
        Dim retryCount As Integer = 3
        While retry
            Try


                Dim Request As Net.HttpWebRequest = Net.HttpWebRequest.Create(DL_URL)
            Dim Bytes(NewBytes) As Byte
            Request.UserAgent = My.Resources.ffmpeg_user_agend.Replace(Chr(34), "").Replace("User-Agent: ", "")
            Request.Timeout = 30000
            Request.Method = "GET"
            Request.AddRange(CurrentSize, CurrentSize + NewBytes)


            Dim Response As Net.HttpWebResponse = Request.GetResponse()
            If Response.StatusCode = Net.HttpStatusCode.PartialContent Or Net.HttpStatusCode.OK Then

                Using binaryReader As New BinaryReader(Response.GetResponseStream())
                    Bytes = binaryReader.ReadBytes(NewBytes)
                    binaryReader.Close()
                End Using

            End If

            File.WriteAllBytes(DL_Pfad, Bytes)




                retry = False
                Catch ex As Exception
                    If retryCount > 0 Then
                        retryCount = retryCount - 1
                        Me.Invoke(New Action(Function()
                                                 Label_percent.Text = "Access Error - retrying"
                                                 Debug.WriteLine(ex.ToString)
                                                 Return Nothing
                                             End Function))

                    Else
                        Me.Invoke(New Action(Function()
                                                 Label_percent.Text = "Access Error - download canceled"
                                                 Debug.WriteLine(ex.ToString)
                                                 Return Nothing
                                             End Function))


                    End If
                End Try

            End While

    End Sub

    Public Function DownloadHybrid(ByVal DL_URL As String, ByVal DL_Pfad As String, ByVal Filename As String) As String
        Dim Folder As String = Einstellungen.GeräteID()

        Dim PauseTime As Integer = 0
        Dim Pfad2 As String = Path.GetDirectoryName(DL_Pfad.Replace(Chr(34), "")) + "\" + Folder + "\"
        Dim di As New IO.DirectoryInfo(Pfad2)
        Dim m3u8_url As String() = DL_URL.Split(New [Char]() {Chr(34)})
        Dim m3u8FFmpeg As String = Nothing

        Dim InuputStreams As String() = DL_URL.Split(New String() {"-i " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

        Me.Invoke(New Action(Function()
                                 Label_percent.Text = "Checking input..."
                                 Return Nothing
                                 End Function))

        For i As Integer = 0 To InuputStreams.Count - 1
            Dim int As Integer = i
            Dim InputURL As String() = InuputStreams(i).Split(New [Char]() {Chr(34)})
            Dim InputClient As New WebClient
            InputClient.Encoding = Encoding.UTF8
            If Main.WebbrowserCookie = Nothing Then
            Else
                InputClient.Headers.Add(HttpRequestHeader.Cookie, Main.WebbrowserCookie)
            End If
            Dim SubsFile As String = Pfad2 + Einstellungen.GeräteID() + ".txt"

            Try
                Dim InputData As String = InputClient.DownloadString(InputURL(0))

                If InStr(InputData, "#EXT-X-VERSION:3") Then

                ElseIf InStr(InputData, "#EXT-X-VERSION:4") Then
                    ProcessV4(InputData, Pfad2 + "Stream-" + int.ToString + "\")
                    DL_URL = DL_URL.Replace("-i " + Chr(34) + InputURL(0), "-allowed_extensions ALL " + "-i " + Chr(34) + Pfad2 + "Stream-" + int.ToString + "\index.m3u8")
                Else
                    'write string to file
                    Dim utf8WithoutBom2 As New System.Text.UTF8Encoding(False)
                    Using sink As New StreamWriter(SubsFile, False, utf8WithoutBom2)
                        sink.WriteLine(InputData)
                    End Using
                    'replace url with local file
                    DL_URL = DL_URL.Replace(InputURL(0), SubsFile)
                End If
            Catch ex As Exception

            End Try


        Next


        For w As Integer = 0 To Integer.MaxValue
            If ThreadList.Count > 0 Then
                Thread.Sleep(250)
            Else
                Thread.Sleep(250)
                Exit For
            End If
        Next


        TS_StatusAsync(100, di, PauseTime)



        If InStr(DL_URL, " -headers " + My.Resources.ffmpeg_user_agend) Then
            DL_URL = DL_URL.Replace(" -headers " + My.Resources.ffmpeg_user_agend, "")
        End If

        'MsgBox(DL_URL)
        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo

        Dim cmd As String = DL_URL + " " + DL_Pfad

        If Debug2 = True Then
            MsgBox(cmd)
        End If


        'all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardInput = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc = New Process
        proc.EnableRaisingEvents = True
        AddHandler proc.ErrorDataReceived, AddressOf FFMPEGOutput
        AddHandler proc.OutputDataReceived, AddressOf FFMPEGOutput
        AddHandler proc.Exited, AddressOf ProcessClosed
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()
        HybridRunning = False
        Return Nothing
    End Function




#End Region



    Public Function DownloadFFMPEG(ByVal DLCommand As String, ByVal DL_Pfad As String, ByVal Filename As String) As String


        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim cmd As String = "-user-agent " + My.Resources.ffmpeg_user_agend.Replace("User-Agent: ", "") + " -headers " + Chr(34) + "ACCEPT-ENCODING: *" + Chr(34) + " " + DLCommand + " " + DL_Pfad 'start ffmpeg with command strFFCMD string
        LogText.Add(Date.Now + " " + cmd)
        If Debug2 = True Then
            MsgBox(cmd)
        End If
        'all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardInput = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc = New Process
        proc.EnableRaisingEvents = True
        AddHandler proc.ErrorDataReceived, AddressOf FFMPEGOutput
        AddHandler proc.OutputDataReceived, AddressOf FFMPEGOutput
        AddHandler proc.Exited, AddressOf ProcessClosed
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()
        Return Nothing
    End Function

    Sub ProcessClosed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Pause(5)
            If Finished = False Then
                If Canceld = False Then
                    Label_website.Text = "The download process seems to have crashed"
                    Label_percent.Text = "Press the play button again to retry."
                    ProgressBar1.Value = 0
                    Retry = True
                    StatusRunning = False
                End If
            End If
        Catch ex As Exception

        End Try
        'Me.Invoke(New Action(Function()
        '                         Label_percent.Text = "Finished - event"
        '                         Return Nothing
        '                     End Function))
    End Sub

    Sub FFMPEGOutput(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        Try
            LogText.Add(Date.Now + " " + e.Data)
        Catch ex As Exception
        End Try

#Region "Detect Auto resolution"
        Try


            If MergeSubstoMP4 = False Then
                If CBool(InStr(e.Data, "Stream #")) And CBool(InStr(e.Data, "Video")) = True Then
                    'MsgBox(True.ToString + vbNewLine + e.Data)
                    'MsgBox(InStr(e.Data, "Stream #").ToString + vbNewLine + InStr(e.Data, "Video").ToString)

                    'MsgBox("with CBool" + vbNewLine + CBool(InStr(e.Data, "Stream #")).ToString + vbNewLine + CBool(InStr(e.Data, "Video")).ToString)

                    ListOfStreams.Add(e.Data)
                End If
                If InStr(e.Data, "Stream #") And InStr(e.Data, " -> ") Then
                    'UsesStreams.Add(e.Data)
                    'MsgBox(e.Data)
                    Dim StreamSearch() As String = e.Data.Split(New String() {" -> "}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim StreamSearch2 As String = StreamSearch(0) + ":"
                    For i As Integer = 0 To ListOfStreams.Count - 1
                        If CBool(InStr(ListOfStreams(i), StreamSearch2)) Then 'And CBool(InStr(ListOfStreams(i), " Video:")) Then
                            'MsgBox(ListOfStreams(i))
                            Dim ResoSearch() As String = ListOfStreams(i).Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                            'MsgBox(ResoSearch(1))
                            If CBool(InStr(ResoSearch(2), " [")) = True Then
                                Dim ResoSearch2() As String = ResoSearch(2).Split(New String() {" ["}, System.StringSplitOptions.RemoveEmptyEntries)
                                Me.Invoke(New Action(Function()
                                                         If Label_Reso.Text = "1080p+" Then
                                                         Else
                                                             Label_Reso.Text = ResoSearch2(0) + "p"
                                                         End If

                                                         Return Nothing
                                                     End Function))
                            End If
                        End If
                    Next
                End If
            End If
#End Region

            If InStr(e.Data, "Duration: N/A, bitrate: N/A") Then

            ElseIf InStr(e.Data, "Duration: ") Then
                Dim ZeitGesamt As String() = e.Data.Split(New String() {"Duration: "}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeitGesamt2 As String() = ZeitGesamt(1).Split(New [Char]() {System.Convert.ToChar(".")})
                Dim ZeitGesamtSplit() As String = ZeitGesamt2(0).Split(New [Char]() {System.Convert.ToChar(":")})
                'MsgBox(ZeitGesamt2(0))
                ZeitGesamtInteger = CInt(ZeitGesamtSplit(0)) * 3600 + CInt(ZeitGesamtSplit(1)) * 60 + CInt(ZeitGesamtSplit(2))



            ElseIf InStr(e.Data, " time=") Then
                'MsgBox(e.Data)
                Dim ZeitFertig As String() = e.Data.Split(New String() {" time="}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeitFertig2 As String() = ZeitFertig(1).Split(New [Char]() {System.Convert.ToChar(".")})
                Dim ZeitFertigSplit() As String = ZeitFertig2(0).Split(New [Char]() {System.Convert.ToChar(":")})
                Dim ZeitFertigInteger As Integer = CInt(ZeitFertigSplit(0)) * 3600 + CInt(ZeitFertigSplit(1)) * 60 + CInt(ZeitFertigSplit(2))
                Dim bitrate3 As String = 0
                If InStr(e.Data, "bitrate=") Then
                    Dim bitrate As String() = e.Data.Split(New String() {"bitrate="}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim bitrate2 As String() = bitrate(1).Split(New String() {"kbits/s"}, System.StringSplitOptions.RemoveEmptyEntries)

                    If InStr(bitrate2(0), ".") Then
                        Dim bitrateTemo As String() = bitrate2(0).Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)
                        bitrate3 = bitrateTemo(0)
                    ElseIf InStr(bitrate2(0), ",") Then
                        Dim bitrateTemo As String() = bitrate2(0).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                        bitrate3 = bitrateTemo(0)
                    End If
                End If
                Dim bitrateInt As Double = CInt(bitrate3) / 1024
                Dim FileSize As Double = ZeitGesamtInteger * bitrateInt / 8
                Dim DownloadFinished As Double = ZeitFertigInteger * bitrateInt / 8
                Dim percent As Integer = ZeitFertigInteger / ZeitGesamtInteger * 100
                Me.Invoke(New Action(Function()
                                         If percent > 100 Then
                                             percent = 100
                                         End If
                                         ProgressBar1.Value = percent
                                         Label_percent.Text = Math.Round(DownloadFinished, 2, MidpointRounding.AwayFromZero).ToString + "MB/" + Math.Round(FileSize, 2, MidpointRounding.AwayFromZero).ToString + "MB " + percent.ToString + "%"
                                         Return Nothing
                                     End Function))
            ElseIf InStr(e.Data, "Failed to open segment") Then
                FailedCount = FailedCount + 1
                If Item_ErrorTolerance = 0 Then

                ElseIf FailedCount >= Item_ErrorTolerance Then
                    Failed = True
                    StatusRunning = False
                    bt_pause.BackgroundImage = My.Resources.main_pause_play
                    SuspendProcess(proc)
                    Me.Invoke(New Action(Function()

                                             Label_percent.Text = "Missing segment detected, retry or resume with the play button"
                                             Return Nothing
                                         End Function))
                End If

            ElseIf InStr(e.Data, "muxing overhead:") Then
                Finished = True
                Me.Invoke(New Action(Function()
                                         Dim Done As String() = Label_percent.Text.Split(New String() {"MB"}, System.StringSplitOptions.RemoveEmptyEntries)
                                         Label_percent.Text = "Finished - " + Done(0) + "MB"
                                         Return Nothing
                                     End Function))
                If HybridMode = True Then
                    Thread.Sleep(5000)
                    Try
                        System.IO.Directory.Delete(HybridModePath, True)
                    Catch ex As Exception
                    End Try
                End If
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try

    End Sub

#Region "Manga DL"


    Public Sub DownloadMangaPages(ByVal Pfad As String, ByVal BaseURL As String, ByVal SiteList As List(Of String), ByVal FolderName As String)

        Dim Pfad_DL As String = Pfad + "\" + FolderName
        If Debug2 = True Then
            MsgBox(BaseURL + SiteList(0))
        End If


        Try
            Directory.CreateDirectory(Pfad_DL)
            'MsgBox(True.ToString)
        Catch ex As Exception
        End Try

        For i As Integer = 0 To SiteList.Count - 1
            'MsgBox(BaseURL + SiteList(i) + vbNewLine + Pfad_DL + "\" + SiteList(i))
            Dim iWert As Integer = i
            Using client As New WebClient()
                client.Headers.Add(My.Resources.ffmpeg_user_agend.Replace(Chr(34), ""))
                client.DownloadFile(BaseURL + SiteList(i), Pfad_DL + "\" + SiteList(i))
                Pause(1)
            End Using
            Me.Invoke(New Action(Function()
                                     iWert = iWert + 1
                                     Dim Prozent As Integer = iWert / SiteList.Count * 100
                                     Label_percent.Text = iWert.ToString + "/" + SiteList.Count.ToString + " " + Prozent.ToString + "%"
                                     ProgressBar1.Value = Prozent
                                     Return Nothing
                                 End Function))

        Next

    End Sub
#End Region

    Private Sub BT_del_Click(sender As Object, e As EventArgs) Handles bt_del.Click
        If Canceld = True Then
            If MessageBox.Show("The Download is not running anymore, press ok to remove it from the list.", "Remove from list!", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
                Exit Sub
            End If
            ToDispose = True
        ElseIf HybridRunning = True Then
            If MessageBox.Show("Are you sure you want to cancel the Download?", "Cancel Download!", MessageBoxButtons.YesNo) = DialogResult.No Then
                Exit Sub
            End If
            Canceld = True
            'KillRunningTask()

        Else
            If proc.HasExited Then
                If MessageBox.Show("The Download is not running anymore, press ok to remove it from the list.", "Remove from list!", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
                    Exit Sub
                End If
                ToDispose = True
            Else
                If MessageBox.Show("Are you sure you want to cancel the Download?", "Cancel Download!", MessageBoxButtons.YesNo) = DialogResult.No Then
                    Exit Sub
                End If
                Canceld = True
                KillRunningTask()
            End If
        End If

    End Sub

#End Region


    Private Sub SuspendProcess(ByVal process As System.Diagnostics.Process)
        For Each t As ProcessThread In process.Threads
            Dim th As IntPtr
            th = OpenThread(ThreadAccess.SUSPEND_RESUME, False, t.Id)
            If th <> IntPtr.Zero Then
                SuspendThread(th)
                CloseHandle(th)
            End If
        Next
    End Sub


    Private Sub ResumeProcess(ByVal process As System.Diagnostics.Process)
        For Each t As ProcessThread In process.Threads
            Dim th As IntPtr
            th = OpenThread(ThreadAccess.SUSPEND_RESUME, False, t.Id)
            If th <> IntPtr.Zero Then
                ResumeThread(th)
                CloseHandle(th)
            End If
        Next
    End Sub



    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            For tlc As Integer = 0 To ThreadList.Count - 1
                If ThreadList.Item(tlc).IsAlive Then
                Else
                    ThreadList.Remove(ThreadList.Item(tlc))
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label_Anime_Click(sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label_Anime.Click, PB_Thumbnail.Click, Label_Reso.Click, Label_percent.Click, ProgressBar1.Click, Label_website.Click, Me.Click
        If e.Button = MouseButtons.Right Then
            ' MsgBox("Right Button Clicked")

            ContextMenuStrip1.ContextMenu.Show(Me, MousePosition)
        End If
    End Sub

    Private Sub ViewInExplorerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewInExplorerToolStripMenuItem.Click
        Process.Start(Path.GetDirectoryName(DownloadPfad.Replace(Chr(34), "")))
    End Sub

    Private Sub PlaybackVideoFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlaybackVideoFileToolStripMenuItem.Click
        If GetIsStatusFinished() = True Then
            PlaybackVideoFileToolStripMenuItem.Enabled = True
        Else
            PlaybackVideoFileToolStripMenuItem.Enabled = False
        End If
        Process.Start(DownloadPfad.Replace(Chr(34), ""))
    End Sub

    Private Sub SaveToFile_Click(sender As Object, e As EventArgs) Handles SaveToFile.Click
        Try

            Dim logfile As String = DownloadPfad.Replace(Main.VideoFormat, ".log").Replace(Chr(34), "")
            'If File.Exists(logfile) Then
            Using sw As StreamWriter = File.AppendText(logfile)
                sw.Write(LogText.Item(0))
                sw.Write(vbNewLine)
                For i As Integer = 1 To LogText.Count - 1
                    sw.Write(vbNewLine)
                    sw.Write(LogText.Item(i))
                Next

            End Using
            'Else
            'File.WriteAllText(logfile, Date.Now + " " + e.Data)
            'End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub LogTocClipboard_Click(sender As Object, e As EventArgs) Handles LogTocClipboard.Click
        Try
            Dim Text As String = LogText.Item(0) + vbNewLine
            For i As Integer = 1 To LogText.Count - 1
                Text = Text + vbNewLine + LogText.Item(i)
            Next
            My.Computer.Clipboard.SetText(Text)
        Catch ex As Exception
        End Try
    End Sub

End Class


Public Class FailedSegemtsWithURL
    Public path As String
    Public url As String

    Public Sub New(ByVal path As String, ByVal url As Integer)
        Me.path = path
        Me.url = url
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.path, Me.url)
    End Function


End Class