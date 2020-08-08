Imports System.Net
Imports System.Text
Imports System.IO
Imports Microsoft.Win32
Imports System.ComponentModel

Public Class CRD_List_Item
    Dim ZeitGesamtInteger As Integer = 0
    Dim ListOfStreams As New List(Of String)
    Dim proc As Process
    Dim Canceld As Boolean = False

    Dim Label_website_Text As String = Nothing
    Dim StatusRunning As Boolean = True
    Dim UsedMap As String = Nothing
    Dim ffmpeg_command As String = Nothing
    Dim Debug2 As Boolean = False
    Dim MergeSubstoMP4 As Boolean = False
    Dim SaveLog As Boolean = False
    Dim DownloadPfad As String = Nothing
    Dim ToDispose As Boolean = False
    Dim HistoryDL_URL As String
    Dim HistoryDL_Pfad As String
    Dim HistoryFilename As String
    Dim Retry As Boolean = False
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
#Region "Set UI"
    Public Sub SetLabelWebsite(ByVal Text As String)
        Label_website.Text = Text
        Label_website_Text = Text
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
        If proc.HasExited = True Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region
#Region "Set Variables"
    Public Sub SetUsedMap(ByVal Value As String)
        UsedMap = Value
    End Sub
    Public Sub Setffmpeg_command(ByVal Value As String)
        ffmpeg_command = Value
    End Sub
    Public Sub SetMergeSubstoMP4(ByVal Value As Boolean)
        MergeSubstoMP4 = Value
    End Sub
    Public Sub SetDebug2(ByVal Value As Boolean)
        Debug2 = Value
    End Sub
    Public Sub SetSaveLog(ByVal Value As Boolean)
        SaveLog = Value
    End Sub
#End Region
    Public Sub KillRunningTask()
        proc.Kill()
        proc.WaitForExit(500)
        Label_percent.Text = "canceled -%"
    End Sub

    Private Sub bt_del_MouseEnter(sender As Object, e As EventArgs) Handles bt_del.MouseEnter
        Dim p As PictureBox = sender
        p.BackgroundImage = My.Resources.main_del_hover
    End Sub

    Private Sub bt_del_MouseLeave(sender As Object, e As EventArgs) Handles bt_del.MouseLeave
        Dim p As PictureBox = sender
        p.BackgroundImage = My.Resources.main_del
    End Sub
    Private Sub bt_pause_MouseEnter(sender As Object, e As EventArgs) Handles bt_pause.MouseEnter
        Dim p As PictureBox = sender
        If StatusRunning = True Then
            p.BackgroundImage = My.Resources.main_pause_hover
        Else
            p.BackgroundImage = My.Resources.main_pause_play_hover
        End If
    End Sub

    Private Sub bt_pause_MouseLeave(sender As Object, e As EventArgs) Handles bt_pause.MouseLeave
        Dim p As PictureBox = sender
        If StatusRunning = True Then
            p.BackgroundImage = My.Resources.main_pause
        Else
            p.BackgroundImage = My.Resources.main_pause_play
        End If
    End Sub

    Private Sub bt_pause_Click(sender As Object, e As EventArgs) Handles bt_pause.Click
        If proc.HasExited = True Then
            If ProgressBar1.Value < 100 Then
                MsgBox("The download process seems to have crashed", MsgBoxStyle.Exclamation)
                Label_percent.Text = "Press the play button again to retry."
                ProgressBar1.Value = 100
                Retry = True
                StatusRunning = False
            ElseIf Retry = True Then
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
                DownloadFFMPEG(HistoryDL_URL, HistoryDL_Pfad, HistoryFilename)
                StatusRunning = True
                Label_website.Text = Label_website_Text
            End If
            Exit Sub
        End If
        If StatusRunning = True Then
            StatusRunning = False
            bt_pause.BackgroundImage = My.Resources.main_pause_play
            SuspendProcess(proc)
        Else
            StatusRunning = True
            bt_pause.BackgroundImage = My.Resources.main_pause
            ResumeProcess(proc)
        End If
    End Sub
    Public Sub SetToolTip(ByVal Text As String)
        ToolTip1.SetToolTip(Me, Text)
    End Sub
    Private Sub Item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Dim locationY As Integer = 0
        bt_del.SetBounds(775, locationY + 10, 35, 29)
        bt_pause.SetBounds(740, locationY + 15, 25, 20)
        PB_Thumbnail.SetBounds(11, 20, 168, 95)
        PB_Thumbnail.BringToFront()
        Label_website.Location = New Point(195, locationY + 15)
        Label_Anime.Location = New Point(195, locationY + 42)
        Label_Reso.Location = New Point(195, locationY + 101)
        Label_Hardsub.Location = New Point(300, locationY + 101)
        Label_percent.SetBounds(455, locationY + 101, 355, 19)
        Label_percent.AutoSize = False
        ProgressBar1.SetBounds(195, locationY + 70, 601, 20)
    End Sub

    Public Function GetTextBound()
        Return Label_website.Location.Y
    End Function
    Public Sub SetLocations(ByVal locationY As Integer)
        bt_del.SetBounds(775, locationY + 10, 35, 29)
        bt_pause.SetBounds(740, locationY + 15, 25, 20)
        PB_Thumbnail.SetBounds(11, locationY + 20, 168, 95)
        PB_Thumbnail.BringToFront()
        Label_website.Location = New Point(195, locationY + 15)
        Label_Anime.Location = New Point(195, locationY + 42)
        Label_Reso.Location = New Point(195, locationY + 101)
        Label_Hardsub.Location = New Point(300, locationY + 101)
        Label_percent.SetBounds(455, locationY + 101, 355, 19)
        Label_percent.AutoSize = False
        ProgressBar1.SetBounds(195, locationY + 70, 601, 20)
    End Sub

#Region "Download + Update UI"

    Public Function DownloadFFMPEG(ByVal DL_URL As String, ByVal DL_Pfad As String, ByVal Filename As String) As String
        DownloadPfad = DL_Pfad
        HistoryDL_URL = DL_URL
        HistoryDL_Pfad = DL_Pfad
        HistoryFilename = Filename

        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        'Dim cmd As String = "-i " + Chr(34) + URL_DL + Chr(34) + " -c copy -bsf:a aac_adtstoasc " + Pfad_DL 'start ffmpeg with command strFFCMD string
        Dim cmd As String = "-i " + Chr(34) + DL_URL + Chr(34) + " " + ffmpeg_command + " " + DL_Pfad 'start ffmpeg with command strFFCMD string
        If MergeSubstoMP4 = True Then
            If CBool(InStr(DL_URL, "-i " + Chr(34))) = True Then
                cmd = DL_URL + " " + DL_Pfad
            End If
        End If
        If UsedMap = Nothing Then
        Else
            cmd = "-i " + Chr(34) + DL_URL + Chr(34) + " -map 0:a " + "-map " + UsedMap + " " + ffmpeg_command + " " + DL_Pfad
            UsedMap = Nothing
        End If
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
        AddHandler proc.ErrorDataReceived, AddressOf TestOutput
        AddHandler proc.OutputDataReceived, AddressOf TestOutput
        proc.StartInfo = startinfo
        'PR_List.Add(proc)
        proc.Start() ' start the process
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()
        Return Nothing
    End Function

    Sub TestOutput(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        Try
            Dim logfile As String = DownloadPfad.Replace(".mp4", ".log").Replace(Chr(34), "")
            If SaveLog = True Then
                If File.Exists(logfile) Then
                    Using sw As StreamWriter = File.AppendText(logfile)
                        sw.Write(vbNewLine)
                        sw.Write(Date.Now + e.Data)
                    End Using
                Else
                    File.WriteAllText(logfile, Date.Now + " " + e.Data)
                End If
            End If
        Catch ex As Exception
        End Try

#Region "Detect Auto resolution"
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
                                                     Label_Reso.Text = ResoSearch2(0) + "p"
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
                                     ProgressBar1.Value = percent
                                     Label_percent.Text = Math.Round(DownloadFinished, 2, MidpointRounding.AwayFromZero).ToString + "MB/" + Math.Round(FileSize, 2, MidpointRounding.AwayFromZero).ToString + "MB " + percent.ToString + "%"
                                     Return Nothing
                                 End Function))
        End If


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
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:70.0) Gecko/20100101 Firefox/70.0")
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

    Private Sub bt_del_Click(sender As Object, e As EventArgs) Handles bt_del.Click
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try


            If proc.HasExited = True Then
                If ProgressBar1.Value < 100 Then
                    If Canceld = False Then
                        Label_website.Text = "The download process seems to have crashed"
                        Label_percent.Text = "Press the play button again to retry."
                        ProgressBar1.Value = 100
                        Retry = True
                        StatusRunning = False
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

