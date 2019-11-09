Imports System.Net
Imports System.Text
Imports System.IO
Imports Microsoft.Win32
Imports System.ComponentModel
Public Class Main
    Public gIndexH As Integer = -1
    Public DialogTaskString As String
    Public UserCloseDialog As Boolean = False
    Dim Aktuell As String
    Dim Gesamt As String
    Public LabelUpdate As String = "Status: idle"
    Public LabelEpisode As String = "..."
    Public b As Boolean = True
    Public LoginOnly As String = "False"
    Public CreditsOnly As Boolean = False
    Public Pfad As String = My.Computer.FileSystem.CurrentDirectory
    Dim ffmpeg_command As String
    Public Resu As Integer
    Dim Resu2 As String
    Public ResuSave As String = "6666x6666"
    Public SubSprache As String
    Public Unlock As Integer = 0
    Public Unlock2 As Integer
    Public SubFolder As Integer
    Public SoftSubs As New List(Of String)
    Public AbourtList As New List(Of String)
    Public watingList As New List(Of String)
    Dim SoftSubsString As String
    Dim CR_Unlock_Error As String
    Dim versuch2 As Integer = 0
    Public keks As String = Nothing
    Dim SubSprache2 As String
    Dim URL_DL As String
    Dim Pfad_DL As String
    Public Grapp_RDY As Boolean = True
    Public Grapp_Abord As Boolean = False
    Public MaxDL As Integer
    Public TaskCount As Integer = 0
    Public Event UpdateUI(ByVal sender As String, ByVal Int As Integer)
    Public ResoNotFoundString As String
    Public ResoBackString As String
    Dim PB_list As New List(Of PictureBox)
    Public bt_dl As New List(Of PictureBox)
    Public WebbrowserURL As String = Nothing
    Public WebbrowserText As String = Nothing
    Public WebbrowserTitle As String = Nothing
    Public UserBowser As Boolean = False
#Region "Sprachen Vairablen"
    Public URL_Invaild As String = "invalid URL, this Downloader is only for crunchyroll.com"
    Public SubFolder_automatic As String = "[automatic : Series/Season]"
    Public SubFolder_Nothing As String = "[ ignore subfolder ]"

    Dim DL_Path_String As String = "Please choose download directory."
    Public CR_Premium_Failed As String = "Can not verify the active premium membership."
    Public No_Stream As String = "Please make sure that the URL is correct."
    Dim TaskNotCompleed As String = "Please wait until the current task is completed."
    Dim Premium_Stream As String = "Please make sure that you logged in for this premium episode."
    Dim Error_Mass_DL As String = "We run into a problem here." + vbNewLine + "You can try to download every episode individually."
    Dim User_Fault_NoName As String = "no name, fallback solution : "
    Dim Sub_language_NotFound As String = "Could not find the sub language" + vbNewLine + "please make sure the language is available: "
    Dim Resolution_NotFound As String = "Could not find any resolution."
    Dim Error_unknown As String = "We run into a unknown problem here." + vbNewLine + "Do you like to send an Bug report?"
    Public CR_Unlock_Error_String As String = "unable to get an CR-US cookie."
    Dim ErrorNoPermisson As String = "Access is denied."
    'UI Variablen
    Public GB_Resolution_Text As String = "Resolution"
    Public GB_SubLanguage_Text As String = "Hardsub language"
    Public GB_Sub_Path_Text As String = "Sub directory"
    Public UL_US_Text As String = "enable US Cookie"
    Public RBAnime_Text As String = "series name"
    Public RBStaffel_Text As String = "series name + season"
    Public NewStart_String As String = "to adopt all the settings, a restart is necessary."
    Public DL_Count_simultaneousText As String = "Simultaneous Downloads"
    Public GB_Sub_FormatText As String = "extended Sub Settings"
    Public LabelResoNotFoundText As String = "resolution not found" + vbNewLine + "Select another one below"
    Public LabelLangNotFoundText As String = "language not found" + vbNewLine + "Select another one below"
    Public ButtonResoNotFoundText As String = "Submit"
    Public CB_SuB_Nothing As String = "[ without  (none) ]"
    Dim StatusToolTip As ToolTip = New ToolTip()
    Dim StatusToolTipText As String
    Public RunGecko As String = "Startup"
#End Region

#Region "UI"

    Private Sub pictureBox1_MouseHover(sender As Object, e As EventArgs) Handles pictureBox1.MouseMove
        pictureBox1.BackColor = SystemColors.Control
    End Sub

    Private Sub pictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox1.MouseLeave
        pictureBox1.BackColor = Color.Transparent
    End Sub


    Private Sub pictureBox2_MouseHover(sender As Object, e As EventArgs) Handles pictureBox2.MouseMove
        pictureBox2.BackColor = SystemColors.Control
    End Sub

    Private Sub pictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox2.MouseLeave
        pictureBox2.BackColor = Color.Transparent
    End Sub



    Private Sub pictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox3.MouseEnter
        pictureBox3.BackColor = SystemColors.Control
    End Sub

    Private Sub pictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox3.MouseLeave
        pictureBox3.BackColor = Color.Transparent
    End Sub

    Private Sub pictureBox4_MouseHover(sender As Object, e As EventArgs) Handles pictureBox4.MouseMove
        pictureBox4.BackColor = SystemColors.Control
    End Sub

    Private Sub pictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox4.MouseLeave
        pictureBox4.BackColor = Color.Transparent
    End Sub




#End Region

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Me.Icon = My.Resources.icon
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            keks = rkg.GetValue("keks").ToString
        Catch ex As Exception
        End Try

#Region "Startup IU"
        StatusToolTip.Active = True
#End Region



        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            ffmpeg_command = rkg.GetValue("ffmpeg_command").ToString
        Catch ex As Exception
            ffmpeg_command = "-c copy -bsf:a aac_adtstoasc -movflags +faststart"
        End Try
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Resu = Integer.Parse(rkg.GetValue("Resu").ToString)
            'MsgBox(Resu)
        Catch ex As Exception
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubSprache = rkg.GetValue("Sub").ToString
        Catch ex As Exception
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubFolder = Integer.Parse(rkg.GetValue("SubFolder").ToString)
        Catch ex As Exception
            SubFolder = 1
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            MaxDL = Integer.Parse(rkg.GetValue("SL_DL").ToString)


        Catch ex As Exception
            MaxDL = 1
        End Try

        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SoftSubsString = rkg.GetValue("AddedSubs").ToString
            If SoftSubsString = "none" Then

            Else
                Dim SoftSubsStringSplit() As String = SoftSubsString.Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To SoftSubsStringSplit.Count - 1
                    SoftSubs.Add(SoftSubsStringSplit(i))
                Next
            End If

        Catch ex As Exception
        End Try
        'Label10.TextAlign = ContentAlignment.MiddleCenter




        If Resu = Nothing Then
            Resu = 1080
        End If

        If SubSprache = Nothing Then
            SubSprache = "enUS"
        End If

    End Sub

    Public Sub ListAdd(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal Reso As String, ByVal HardSub As String, ByVal SoftSubs As String, ByVal ThumbnialURL As String, ByVal VideoURL As String)
        Dim b As New Bitmap(838, 142, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim ZeroPoint As Point = New Point(0, 0)
        Dim TextPoint As Point = New Point(195, 15)
        Dim TextPointL2 As Point = New Point(195, 42)
        Dim TextPointL3 As Point = New Point(773, 95)
        Dim TextPointL4 As Point = New Point(195, 101)
        Dim TextPointL4A2 As Point = New Point(300, 101)
        Dim ThumbnialPoint As Point = New Point(11, 20)
        Dim ProgressbarPoint As Point = New Point(195, 70)
        Dim newImage As Image = My.Resources.backgroud
        Dim img As Image = My.Resources.main_del
        Try
            Dim wc As New WebClient()
            Dim bytes As Byte() = wc.DownloadData(ThumbnialURL)
            Dim ms As New MemoryStream(bytes)
            img = System.Drawing.Image.FromStream(ms)
        Catch ex As Exception
            MsgBox(ex.ToString)
            MsgBox(ThumbnialURL)
        End Try
        g.DrawImage(newImage, ZeroPoint)
        Dim Thumnail As New Bitmap(168, 95, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
        Dim gr_dest As Graphics = Graphics.FromImage(Thumnail)
        gr_dest.DrawImage(img, 0, 0,
        Thumnail.Width + 1,
        Thumnail.Height + 1)
        g.DrawImage(Thumnail, ThumbnialPoint)
        g.DrawString(NameP1, FontLabel.Font, Brushes.Black, TextPoint)
        g.DrawString(NameP2, FontLabel.Font, Brushes.Black, TextPointL2)
        g.DrawRectangle(Pens.Black, ProgressbarPoint.X, ProgressbarPoint.Y, 601, 20)
        Dim brGradient As Brush = New SolidBrush(Color.FromArgb(247, 140, 37))
        g.FillRectangle(brGradient, ProgressbarPoint.X + 1, ProgressbarPoint.Y + 1, 0, 19)
        g.DrawString("0%", FontLabel2.Font, Brushes.Black, TextPointL3)
        g.DrawString(Reso, FontLabel.Font, Brushes.Black, TextPointL4)
        g.DrawString(HardSub, FontLabel.Font, Brushes.Black, TextPointL4A2)
        g.Dispose()
        gIndexH = gIndexH + 1

        With ListView1.Items.Add(0)
            LVPictureBox(ListView1, gIndexH, b, "Softsubs: " + SoftSubs, NameKomplett)
            bt_del(ListView1, gIndexH, NameKomplett)
        End With
    End Sub

    Public Function bt_del(ByVal pListView As ListView, ByVal ItemIndex As Integer, ByVal NameKomplett As String) As PictureBox
        'btn erstellen funktion
        Dim r As Rectangle
        Dim bt_r As New PictureBox
        Dim c As Integer = ListView1.Items.Count - 1
        r = pListView.Items(c).Bounds()
        bt_r.Parent = pListView
        bt_r.SetBounds(755, r.Y + 20, 50, 40)
        bt_dl.Add(bt_r)
        bt_r.Name = NameKomplett
        'bt_r.FlatStyle = FlatStyle.System
        bt_r.Visible = True
        bt_r.BringToFront()
        bt_r.Enabled = True
        bt_r.Image = My.Resources.main_close
        bt_r.Image = My.Resources.main_del
        bt_r.BackgroundImageLayout = ImageLayout.Center
        ToolTip1.SetToolTip(bt_r, NameKomplett)
        'bt_r.FlatAppearance.BorderSize = 1
        'bt_r.FlatAppearance.BorderColor = Color.Black
        AddHandler bt_r.Click, AddressOf Me.bt_r_click
        AddHandler bt_r.MouseEnter, AddressOf Me.bt_r_ME
        AddHandler bt_r.MouseLeave, AddressOf Me.bt_r_ML
        Return Nothing
    End Function

    Private Sub bt_r_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim b As PictureBox = sender
        b.Image = My.Resources.main_close
        If MessageBox.Show("Cancel this Download?", "Cancel?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            AbourtList.Add(b.Name)
            b.Enabled = False
            TaskCount = TaskCount - 1
        Else
            b.Image = My.Resources.main_del
        End If

    End Sub
    Private Sub bt_r_ME(ByVal sender As Object, ByVal e As EventArgs)
        Dim b As PictureBox = sender
        b.Image = My.Resources.main_del_hover
    End Sub
    Private Sub bt_r_ML(ByVal sender As Object, ByVal e As EventArgs)
        Dim b As PictureBox = sender
        b.Image = My.Resources.main_del
    End Sub
    Public Function LVPictureBox(ByVal pListView As ListView, ByVal ItemIndex As Integer, ByVal img As Bitmap, ByVal SoftSubs As String, ByVal NameKomplett As String) As PictureBox
        'btn erstellen funktion
        Dim r As Rectangle
        Dim bt_d As New PictureBox
        Dim TT As New ToolTip
        Dim c As Integer = ListView1.Items.Count - 1
        r = pListView.Items(c).Bounds()
        r.Width = 838
        r.Height = 142
        bt_d.Parent = pListView
        bt_d.SetBounds(r.X, r.Y, r.Width, r.Height)
        bt_d.Name = NameKomplett
        bt_d.BackgroundImage = img
        PB_list.Add(bt_d)
        ToolTip1.SetToolTip(bt_d, SoftSubs)
        bt_d.BackgroundImageLayout = ImageLayout.Center
        'bt_d.FlatAppearance.BorderColor = Color.Orange
        bt_d.Visible = True
        bt_d.Enabled = True

        ' AddHandler LVPictureBox., AddressOf Me.LVPictureBox_MouseHover
        Return Nothing
    End Function


    Public Sub Pause(ByVal pau As Single)

        'Programmausführung verzögern *******************************************************

        Dim start, finish As Single
        start = Microsoft.VisualBasic.DateAndTime.Timer

        finish = start + pau
        Do While Microsoft.VisualBasic.DateAndTime.Timer < finish
            Application.DoEvents()
        Loop

    End Sub

#Region "Season DL"

    Public Sub MassGrapp()
        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
        Anime_Add.comboBox3.Enabled = True
        Anime_Add.comboBox4.Enabled = True
        Dim Anzahl As String() = WebbrowserText.Split(New String() {"wrapper container-shadow hover-classes"}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim Titel As String() = Anzahl(0).Split(New String() {"<meta content=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim Titel2 As String() = Titel(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        'Label10.Text = Titel2(0)
        Dim c As Integer = Anzahl.Count - 1
        '  FolgenZahl.Text = c.ToString + " Folgen gefunden."

        Array.Reverse(Anzahl)
        For i As Integer = 0 To Anzahl.Count - 2
            Dim URLGrapp As String() = Anzahl(i).Split(New String() {"title=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox("1" + Chr(34))

            Dim URLGrapp2 As String() = URLGrapp(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Anime_Add.comboBox3.Items.Add(URLGrapp2(0))
            Anime_Add.comboBox4.Items.Add(URLGrapp2(0))
        Next

    End Sub

    Public Sub SeasonDropdownGrapp()

        Anime_Add.groupBox2.Visible = True
        Anime_Add.PictureBox1.Enabled = True
        Anime_Add.PictureBox1.Visible = True
        Anime_Add.groupBox1.Visible = False
        Anime_Add.ComboBox1.Items.Clear()
        Anime_Add.comboBox3.Items.Clear()
        Anime_Add.comboBox4.Items.Clear()
        Anime_Add.ComboBox1.Enabled = True
        Anime_Add.comboBox3.Enabled = True
        Anime_Add.comboBox4.Enabled = True
        Dim Anzahl As String() = WebbrowserText.Split(New String() {"season-dropdown content-menu block"}, System.StringSplitOptions.RemoveEmptyEntries)
        Array.Reverse(Anzahl)
        For i As Integer = 0 To Anzahl.Count - 2
            Dim Titel As String() = Anzahl(i).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Titel2 As String() = Titel(0).Split(New String() {">"}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox(Titel2(0))
            Anime_Add.ComboBox1.Items.Add(Titel2(1))
        Next

    End Sub


    Public Async Sub MassDL()
        If Anime_Add.comboBox3.Text = Nothing Then
            Exit Sub
        End If
        Anime_Add.Add_Display.Text = "preparing ..."
        Dim Website As String = WebbrowserText

        If Anime_Add.ComboBox1.Enabled = True Then
            Dim SeasonDropdownAnzahl As String() = Website.Split(New String() {"season-dropdown content-menu block"}, System.StringSplitOptions.RemoveEmptyEntries)
            Array.Reverse(SeasonDropdownAnzahl)
            Dim SDV As Integer = 0
            For i As Integer = 0 To SeasonDropdownAnzahl.Count - 1
                If InStr(SeasonDropdownAnzahl(i), Chr(34) + ">" + Anime_Add.ComboBox1.SelectedItem.ToString + "</a>") Then
                    SDV = i
                End If
            Next
            Website = SeasonDropdownAnzahl(SDV)
        End If
        Try
            Dim Anzahl As String() = Website.Split(New String() {"wrapper container-shadow hover-classes"}, System.StringSplitOptions.RemoveEmptyEntries)
            Array.Reverse(Anzahl)
            Dim c As Integer = Anime_Add.comboBox4.SelectedIndex - Anime_Add.comboBox3.SelectedIndex + 1
            'AnzahlGesamt.Text = c.ToString
            Gesamt = c.ToString
            Aktuell = "0"
            If Anime_Add.comboBox4.SelectedIndex > Anime_Add.comboBox3.SelectedIndex Then

                For i As Integer = Anime_Add.comboBox3.SelectedIndex To Anime_Add.comboBox4.SelectedIndex

                    For e As Integer = 0 To Integer.MaxValue

                        If Grapp_RDY = True Then
                            If TaskCount < MaxDL Then
                                Exit For
                            Else
                                'MsgBox(e)
                                Await Task.Delay(2000)
                            End If
                        Else
                            Await Task.Delay(2000)
                        End If
                    Next
                    If Anime_Add.Mass_DL_Cancel = False Then
                        b = True
                        Exit For
                        Grapp_Abord = True
                        'MsgBox("dl_abourd")
                    End If
                    Dim d As Integer = i - Anime_Add.comboBox3.SelectedIndex + 1
                    Dim URLGrapp As String() = Anzahl(i).Split(New String() {"<a href=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim URLGrapp2 As String() = URLGrapp(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    'MsgBox("https://www.crunchyroll.com" + URLGrapp2(0))
                    Grapp_RDY = False
                    b = False
                    GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com" + URLGrapp2(0))
                    'Await Task.Delay(500)
                    'GrappURL()
                    Aktuell = d.ToString
                    '  AnzahlFertig.Text = d.ToString
                    Anime_Add.Add_Display.Text = Aktuell + " / " + Gesamt
                    If CBool(InStr(WebbrowserText, Chr(34) + "premium_status" + Chr(34) + ":" + Chr(34) + "premium" + Chr(34))) Then
                    ElseIf CBool(InStr(WebbrowserText, Chr(34) + "premium_status" + Chr(34) + ":" + Chr(34) + "free_trial" + Chr(34))) Then
                        'Else
                        '    MsgBox(CR_Premium_Failed, MsgBoxStyle.Information)
                        '    Anime_Add.groupBox1.Visible = True
                        '    Anime_Add.groupBox2.Visible = False
                        '    Anime_Add.GroupBox3.Visible = False
                        '    Anime_Add.Mass_DL_Cancel = False
                        '    Anime_Add.pictureBox4.Image = My.Resources.main_button_download_default
                        '    Exit Sub
                    End If
                Next



            End If
        Catch ex As Exception
            Anime_Add.comboBox4.Items.Clear()
            Anime_Add.comboBox3.Items.Clear()
            ' MsgBox(Error_Mass_DL, MsgBoxStyle.Information)
            'MsgBox(ex.ToString)
            Aktuell = 0.ToString
            Gesamt = 0.ToString

            Anime_Add.groupBox1.Visible = True
            Anime_Add.groupBox2.Visible = False
            Anime_Add.GroupBox3.Visible = False
            Anime_Add.Mass_DL_Cancel = False
            Anime_Add.pictureBox4.Image = My.Resources.main_button_download_default
        End Try
        Pause(5)
        Anime_Add.groupBox1.Visible = True
        Anime_Add.groupBox2.Visible = False
        Anime_Add.GroupBox3.Visible = False
        Anime_Add.Mass_DL_Cancel = False
        Anime_Add.pictureBox4.Image = My.Resources.main_button_download_default
    End Sub
#End Region
#Region "Sub to display"

    Public Function SubValuesToDisplay() As String
        Try
            Dim deDE As Boolean = False
            Dim enUS As Boolean = False
            Dim ptBR As Boolean = False
            Dim esLA As Boolean = False
            Dim frFR As Boolean = False
            Dim arME As Boolean = False
            Dim ruRU As Boolean = False
            Dim itIT As Boolean = False
            Dim esES As Boolean = False
            Dim ListReturn As String = Nothing
            For i As Integer = 0 To SoftSubs.Count - 1
                If SoftSubs(i) = "deDE" Then
                    deDE = True
                ElseIf SoftSubs(i) = "enUS" Then
                    enUS = True
                ElseIf SoftSubs(i) = "ptBR" Then
                    ptBR = True
                ElseIf SoftSubs(i) = "esLA" Then
                    esLA = True
                ElseIf SoftSubs(i) = "frFR" Then
                    frFR = True
                ElseIf SoftSubs(i) = "arME" Then
                    arME = True
                ElseIf SoftSubs(i) = "ruRU" Then
                    ruRU = True
                ElseIf SoftSubs(i) = "itIT" Then
                    itIT = True
                ElseIf SoftSubs(i) = "esES" Then
                    esES = True
                End If
            Next
            If deDE = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Deutsch"
                Else
                    ListReturn = ListReturn + ", Deutsch"
                End If

            End If
            If enUS = True Then
                If ListReturn = Nothing Then
                    ListReturn = "English"
                Else
                    ListReturn = ListReturn + ", English"
                End If
            End If
            If esLA = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Español (LA)"
                Else
                    ListReturn = ListReturn + ", Español (LA)"
                End If

            End If
            If ptBR = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Português (Brasil)"
                Else
                    ListReturn = ListReturn + ", Português (Brasil)"
                End If

            End If
            If frFR = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Français (France)"
                Else
                    ListReturn = ListReturn + ", Français (France)"
                End If
            End If
            If arME = True Then
                If ListReturn = Nothing Then
                    ListReturn = "العربية (Arabic)"
                Else
                    ListReturn = ListReturn + ", العربية (Arabic)"
                End If
            End If
            If ruRU = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Русский (Russian)"
                Else
                    ListReturn = ListReturn + ", Русский (Russian)"
                End If
            End If
            If itIT = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Italiano (Italian)"
                Else
                    ListReturn = ListReturn + ", Italiano (Italian)"
                End If

            End If
            If esES = True Then
                If ListReturn = Nothing Then
                    ListReturn = "Español (España)"
                Else
                    ListReturn = ListReturn + ", Español (España)"
                End If
            End If

            Return ListReturn

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function HardSubValuesToDisplay(ByVal HardSub As String) As String
        Try
            If HardSub = Chr(34) + "deDE" + Chr(34) Then
                Return "Deutsch"
            ElseIf HardSub = Chr(34) + "enUS" + Chr(34) Then
                Return "English"
            ElseIf HardSub = Chr(34) + "ptBR" + Chr(34) Then
                Return "Português (Brasil)"
            ElseIf HardSub = Chr(34) + "esLA" + Chr(34) Then
                Return "Español (LA)"
            ElseIf HardSub = Chr(34) + "frFR" + Chr(34) Then
                Return "Français (France)"
            ElseIf HardSub = Chr(34) + "arME" + Chr(34) Then
                Return "العربية (Arabic)"
            ElseIf HardSub = Chr(34) + "ruRU" + Chr(34) Then
                Return "Русский (Russian)"
            ElseIf HardSub = Chr(34) + "itIT" + Chr(34) Then
                Return "Italiano (Italian)"
            ElseIf HardSub = Chr(34) + "esES" + Chr(34) Then
                Return "Español (España)"
            End If

            Return CB_SuB_Nothing

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

    Public Sub GrappURL()
        Try
            Grapp_RDY = False
            TaskCount = TaskCount + 1
            Dim CR_Anime_Titel As String = Nothing
            Dim CR_Anime_Staffel As String = Nothing
            Dim CR_Anime_Folge As String = Nothing
#Region "Name + Pfad"
            Dim Pfad2 As String
            Dim CR_FilenName As String = Nothing
            Dim Bug_Deutsch As String = "-"
            'Dim CR_Anime_Titel As String
            'Dim CR_Anime_Staffel As String
            'Dim CR_Anime_Folge As String
            'Dim CR_Name_by_Titel As String() = GeckoFX.WebBrowser1.Document.Body.OuterHtml.Split(New String() {"<title>"}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim CR_Name_by_Titel_2_Patch As String =CR_Name_by_Titel(1).Split(New String() {"</title>"}, System.StringSplitOptions.RemoveEmptyEntries)
            If CBool(InStr(WebbrowserTitle, "Anschauen auf Crunchyroll")) Then
                Bug_Deutsch = ":"
                'Throw New System.Exception("Test")
            Else
            End If
            Dim CR_Name_by_Titel_2 As String() = WebbrowserTitle.Split(New String() {Bug_Deutsch}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim CR_Name_by_Script As String() = WebbrowserText.Split(New String() {Chr(34) + "name" + Chr(34) + ": " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim CR_Name_by_Script2 As String() = CR_Name_by_Script(1).Split(New [Char]() {Chr(34)})
            CR_FilenName = CR_Name_by_Titel_2(0).Trim() '+ " " + CR_Name_by_Script2(0).Trim

            Dim CR_FilenName_Backup As String = Nothing

            Dim SubfolderValue As String = Nothing
            If CBool(InStr(WebbrowserText, "<h4>")) Then ' Film statt Serie
                Dim CR_Name_1 As String() = WebbrowserText.Split(New String() {"<h4>"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim CR_Name_2 As String() = CR_Name_1(1).Split(New String() {"</h4>"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                Dim CR_Name_Staffel0_Folge1 As String()
                If CBool(InStr(CR_Name_2(0), ",")) Then
                    CR_Name_Staffel0_Folge1 = CR_Name_2(0).Split(New [Char]() {System.Convert.ToChar(",")}, System.StringSplitOptions.RemoveEmptyEntries)
                    CR_Anime_Staffel = CR_Name_Staffel0_Folge1(0).Trim()
                    CR_Anime_Folge = CR_Name_Staffel0_Folge1(1).Trim()
                    CR_Anime_Folge = System.Text.RegularExpressions.Regex.Replace(CR_Anime_Folge, "[^\w\\-]", " ")
                Else
                    CR_Anime_Staffel = ""
                    CR_Anime_Folge = CR_Name_2(0).Trim()
                    CR_Anime_Folge = System.Text.RegularExpressions.Regex.Replace(CR_Anime_Folge, "[^\w\\-]", " ")
                End If


                Dim CR_Name_4 As String() = CR_Name_1(0).Split(New String() {"class=" + Chr(34) + "text-link" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
                Dim CR_Name_Anime0 As String() = CR_Name_4(CR_Name_4.Length - 1).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
                CR_Name_Anime0(0) = System.Text.RegularExpressions.Regex.Replace(CR_Name_Anime0(0), "[^\w\\-]", " ")
                CR_Anime_Titel = CR_Name_Anime0(0).Trim
                If CR_Anime_Staffel = Nothing Then
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Folge
                Else
                    CR_FilenName = CR_Anime_Titel + " " + CR_Anime_Staffel + " " + CR_Anime_Folge
                End If

                CR_FilenName_Backup = RemoveExtraSpaces(CR_FilenName)
                Me.Invoke(New Action(Function()
                                         If Anime_Add.ComboBox2.Text = SubFolder_automatic Then
                                             If SubFolder = 2 Then
                                                 SubfolderValue = CR_Anime_Titel + "\" + CR_Anime_Staffel + "\"
                                             ElseIf SubFolder = 1 Then
                                                 SubfolderValue = CR_Anime_Titel + "\"
                                             End If
                                         ElseIf Anime_Add.ComboBox2.Text = SubFolder_Nothing Then
                                         Else
                                             SubfolderValue = Anime_Add.ComboBox2.Text + "\"
                                         End If
                                         Return Nothing
                                     End Function))
            End If
            CR_FilenName = System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
            CR_FilenName = RemoveExtraSpaces(CR_FilenName)
            If SubfolderValue = Nothing Then
                Pfad2 = Pfad + "\" + CR_FilenName + ".mp4"
            Else
                Pfad2 = Pfad + "\" + SubfolderValue + CR_FilenName + ".mp4"
            End If
            If Not Directory.Exists(Path.GetDirectoryName(Pfad2)) Then
                ' Nein! Jetzt erstellen...
                Try
                    Directory.CreateDirectory(Path.GetDirectoryName(Pfad2))
                Catch ex As Exception
                    ' Ordner wurde nich erstellt
                    Pfad2 = Pfad + "\" + CR_FilenName_Backup + ".mp4"
                End Try
            End If
            Pfad2 = Chr(34) + Pfad2 + Chr(34)

#End Region
#Region "Subs"
            Dim SoftSubs2 As New List(Of String)
            If SoftSubs.Count > 0 Then
                For i As Integer = 0 To SoftSubs.Count - 1
                    If CBool(InStr(WebbrowserText, Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SoftSubs(i) + Chr(34) + ",")) Then
                        SoftSubs2.Add(SoftSubs(i))
                    Else
                        'MsgBox("Softsubtitle for " + SoftSubs(i) + " is not avalible.", MsgBoxStyle.Information)
                    End If
                Next

            End If
            If SubSprache = "None" Then
                If CBool(InStr(WebbrowserText, Chr(34) + "hardsub_lang" + Chr(34) + ":null")) Then
                    SubSprache2 = "null"
                Else
                    ResoNotFoundString = WebbrowserText
                    DialogTaskString = "Language"
                    Reso.ShowDialog()
                    If UserCloseDialog = True Then
                        Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                    Else
                        If ResoBackString = Nothing Then
                        Else
                            SubSprache2 = ResoBackString
                        End If
                    End If
                    'Throw New System.Exception("Could not find the sub language")
                End If


            Else
                If CBool(InStr(WebbrowserText, Chr(34) + "hardsub_lang" + Chr(34) + ":" + Chr(34) + SubSprache + Chr(34) + ",")) Then
                    SubSprache2 = Chr(34) + SubSprache + Chr(34)

                ElseIf CBool(InStr(WebbrowserText, Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SubSprache + Chr(34) + ",")) Then
                    If MessageBox.Show("It look like only Softsubtitle are avalibe." + vbNewLine + "Are you want to use Softsubtitle this time instead?", "No Hardsubtitle", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        SubSprache2 = "null"
                        SoftSubs2.Add(SubSprache)
                    Else
                        Throw New System.Exception("Could not find the sub language")
                    End If


                Else
                    ResoNotFoundString = WebbrowserText
                    DialogTaskString = "Language"
                    Reso.ShowDialog()
                    If UserCloseDialog = True Then
                        Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                    Else
                        If ResoBackString = Nothing Then
                        Else
                            SubSprache2 = ResoBackString
                        End If
                    End If
                End If
            End If


#End Region
            If Grapp_Abord = True Then
                Grapp_RDY = True
                Grapp_Abord = False
                TaskCount = TaskCount - 1
                'MsgBox("grapp_abourd")
                Exit Sub

            End If
#Region "m3u8 suche"
            Dim ii As Integer = 0
            'MsgBox(Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34))
            Dim CR_URI_Master As String = Nothing
            Dim CR_URI_Master_Split1 As String() = WebbrowserText.Split(New String() {My.Resources.hls_Value}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim hls_List As New List(Of String)
            For i As Integer = 0 To CR_URI_Master_Split1.Count - 1
                If InStr(CR_URI_Master_Split1(i), My.Resources.hls_endString) Then
                    Dim s As String() = CR_URI_Master_Split1(i).Split(New String() {My.Resources.hls_endString}, System.StringSplitOptions.RemoveEmptyEntries)
                    hls_List.Add(s(0))
                End If
            Next
            'Dim CR_URI_Master_Split1 As String() = WebbrowserText.Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            For i As Integer = 0 To hls_List.Count - 1
                If InStr(hls_List(i), Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)) Then

                    Dim s() As String = hls_List(i).Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":" + SubSprache2 + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    CR_URI_Master = s(1).Replace("\/", "/")
                    'MsgBox(CR_URI_Master)
                End If

                'Dim CR_URI_Master_Split2 As String() = CR_URI_Master_Split1(i).Split(New [Char]() {Chr(34)})
                'If CBool(InStr(CR_URI_Master_Split2(0), "master.m3u8")) Then 'If CBool(InStr(CR_URI_Master_Split2(0), "master.m3u8")) Then
                '    CR_URI_Master = CR_URI_Master_Split2(0).Replace("\/", "/")
                '    ii = i

                '    Exit For
                'ElseIf CBool(InStr(CR_URI_Master_Split2(0), "index.m3u8")) Then
                '    'MsgBox("Premnium Episode")
                '    Throw New System.Exception("Premnium Episode")
                '    Exit For
                'End If
            Next
            If CBool(InStr(CR_URI_Master, "master.m3u8")) Then
                Me.Invoke(New Action(Function()
                                         Anime_Add.StatusLabel.Text = "Status: m3u8 found, looking for resolution"
                                         Return Nothing
                                     End Function))
            Else
                Throw New System.Exception("Premnium Episode")
            End If
#End Region

#Region "Download softsub file"
            If SoftSubs2.Count > 0 Then
                For i As Integer = 0 To SoftSubs2.Count - 1
                    'EpisodeLabel.Text = SoftSubs2(i)
                    'StatusLabel.Text = "Status: downloading subtitle file"
                    LabelUpdate = "Status: downloading subtitle file"
                    LabelEpisode = SoftSubs2(i)
                    Dim SoftSub As String() = WebbrowserText.Split(New String() {Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SoftSubs2(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                    Dim SoftSub_3 As String = SoftSub_2(0).Replace("\/", "/")
                    Dim client0 As New WebClient
                    client0.Encoding = Encoding.UTF8
                    Dim str0 As String = client0.DownloadString(SoftSub_3)
                    Dim Pfad3 As String = Pfad2.Replace(Chr(34), "")
                    Dim FN As String = Path.ChangeExtension(Path.Combine(Path.GetFileNameWithoutExtension(Pfad3) + " " + SoftSubs2(i) + Path.GetExtension(Pfad3)), "ass")
                    'MsgBox(FN)
                    If i = 0 Then
                        FN = Path.ChangeExtension(Path.GetFileName(Pfad3), "ass")
                        'MsgBox(FN)
                    End If
                    Dim Pfad4 As String = Path.Combine(Path.GetDirectoryName(Pfad3), FN)
                    'MsgBox(Pfad4)
                    File.WriteAllText(Pfad4, str0, Encoding.UTF8)
                    Pause(1)
                Next
            End If
#End Region

#Region "lösche doppel download"

            Dim Pfad5 As String = Pfad2.Replace(Chr(34), "")
            If My.Computer.FileSystem.FileExists(Pfad5) Then 'Pfad = Kompeltter Pfad mit Dateinamen + ENdung
                If MessageBox.Show("The file " + Pfad5 + " already exists." + vbNewLine + "You want to override it?", "File exists!", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Try
                        My.Computer.FileSystem.DeleteFile(Pfad5)
                    Catch ex As Exception
                    End Try
                Else
                    Grapp_RDY = True
                    Exit Sub
                    TaskCount = TaskCount - 1
                End If

            End If
#End Region

            Dim client As New System.Net.WebClient
            client.Encoding = Encoding.UTF8
            'MsgBox(CR_URI_Master)
            Dim str As String = client.DownloadString(CR_URI_Master)
            'MsgBox(str)

            If CBool(InStr(str, "x" + Resu.ToString + ",")) Then
                Resu2 = "x" + Resu.ToString
            Else
                If CBool(InStr(str, ResuSave + ",")) Then
                    Resu2 = Resu2
                Else

                    ResoNotFoundString = str
                    DialogTaskString = "Resolution"
                    Reso.ShowDialog()
                    'MsgBox(ResoBackString)
                    If UserCloseDialog = True Then
                        Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
                    Else
                        Resu2 = ResoBackString
                    End If
                End If
            End If
            'MsgBox(Resu2)
            Dim VLC_URI_1 As String() = str.Split(New String() {Resu2 + ","}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim VLC_URI_2 As String() = VLC_URI_1(1).Split(New [Char]() {Chr(34)})
            Dim VLC_URI_3 As String() = VLC_URI_2(2).Split(New [Char]() {System.Convert.ToChar("#")})
            '            For i As Integer = 0 To 3
            '                Try
            '                    CR_URI_Master = GetPage(CR_URI_Master)
            '                    Exit For
            '                Catch ex As Exception
            '                End Try
            '            Next
            '            MsgBox(CR_URI_Master)
            '            Dim FFMPEG_ResoBack As String = FFMPEG_Reso(CR_URI_Master)
            '            'MsgBox(FFMPEG_ResoBack)
            '            Dim FFMPEG_Back() As String = FFMPEG_ResoBack.Split(New String() {"#1"}, System.StringSplitOptions.RemoveEmptyEntries)
#Region "thumbnail"
            Dim thumbnail As String() = WebbrowserText.Split(New String() {My.Resources.thumbnailString}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim thumbnail2 As String() = thumbnail(1).Split(New String() {Chr(34) + "}"}, System.StringSplitOptions.RemoveEmptyEntries) '(New [Char]() {"-"})
            Dim thumbnail3 As String = thumbnail2(0).Replace("\/", "/")
#End Region
#Region "<li> constructor"
            Dim Subsprache3 As String = HardSubValuesToDisplay(SubSprache2)
            Dim ResoHTMLDisplay As String = Nothing
            If ResoBackString = Nothing Then
                ResoHTMLDisplay = Resu.ToString + "p"
                ResoBackString = Nothing
            Else
                Dim ResoHTML As String() = ResoBackString.Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                If ResoHTML.Count > 1 Then
                    ResoHTMLDisplay = ResoHTML(1) + "p"

                Else
                    ResoHTMLDisplay = ResoHTML(0) + "p"
                End If
            End If
            Me.Invoke(New Action(Function()
                                     ListAdd(CR_FilenName, CR_Anime_Titel, CR_Anime_Staffel + " " + CR_Anime_Folge, ResoHTMLDisplay, Subsprache3, SubValuesToDisplay(), thumbnail3, URL_DL)
                                     Return Nothing
                                 End Function))
            ' liList.Add(My.Resources.htmlvorThumbnail + thumbnail3 + My.Resources.htmlnachTumbnail + CR_Anime_Titel + " <br> " + CR_Anime_Staffel + " " + CR_Anime_Folge + My.Resources.htmlvorAufloesung + ResoHTMLDisplay + My.Resources.htmlvorSoftSubs + vbNewLine + SubValuesToDisplay() + My.Resources.htmlvorHardSubs + Subsprache3 + My.Resources.htmlnachHardSubs + "<!-- " + CR_FilenName + "-->")
#End Region
            'MsgBox(liList(0))
            URL_DL = VLC_URI_3(0).Trim()
            ' URL_DL = Chr(34) + GetPage(CR_URI_Master) + Chr(34) + " -map 0:a " + "-map " + FFMPEG_Back(1)
            'MsgBox(URL_DL)
            Pfad_DL = Pfad2
            AsyncWorkerX.RunAsync(AddressOf DownloadFFMPEG, URL_DL, Pfad_DL, CR_FilenName)
            'GeckoWebBrowser1.LoadHtml(My.Resources.htmlTop + vbNewLine + liList.Last + vbNewLine + My.Resources.ulEnd + My.Resources.htmlEnd)
            Grapp_RDY = True
            Me.Invoke(New Action(Function()

                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Return Nothing
                                 End Function))
            ' ManageWorker(URL_DL, Pfad_DL, CR_FilenName)()
        Catch ex As Exception
            TaskCount = TaskCount - 1
            Me.Invoke(New Action(Function()

                                     Anime_Add.StatusLabel.Text = "Status: idle"
                                     Return Nothing
                                 End Function))
            'StatusLabel.Text = "Status: idle"
            Grapp_RDY = True
            'MsgBox(ex.ToString)
            If CBool(InStr(ex.ToString, "Could not find the sub language")) Then
                MsgBox(Sub_language_NotFound + SubSprache)
            ElseIf CBool(InStr(ex.ToString, "RESOLUTION Not Found")) Then
                MsgBox(Resolution_NotFound)
            ElseIf CBool(InStr(ex.ToString, "Premnium Episode")) Then
                MsgBox(Premium_Stream, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, "System.UnauthorizedAccessException")) Then
                MsgBox(ErrorNoPermisson + vbNewLine + ex.ToString, MsgBoxStyle.Information)
            ElseIf CBool(InStr(ex.ToString, Chr(34) + "UserAbort" + Chr(34))) Then
                MsgBox(ex.ToString, MsgBoxStyle.Information)
            Else
                ' MsgBox(ex.ToString, MsgBoxStyle.Information)

                If MessageBox.Show(Error_unknown, "Error!", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                    Dim CCC As String() = WebbrowserText.Split(New String() {Chr(34) + "country_code" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim CCC1 As String() = CCC(1).Split(New String() {Chr(34) + "});"}, System.StringSplitOptions.RemoveEmptyEntries)
                    'MsgBox(CCC1(0))
                    Dim SaveString As String = "Operating System: " + My.Computer.Info.OSFullName + vbNewLine + vbNewLine + "Crunchyroll URL: " + WebbrowserURL + vbNewLine + vbNewLine + "subtitle language: " + SubSprache + vbNewLine + vbNewLine + "video resolution: " + Resu.ToString + vbNewLine + vbNewLine + "error message: " + ex.ToString + vbNewLine + ex.StackTrace.ToString + vbNewLine + vbNewLine + "softsubs enabled?: " + SoftSubs.ToString + vbNewLine + vbNewLine + "Crunchyroll Downloader Version: " + Application.ProductVersion + vbNewLine + vbNewLine + "detected location from Crunchyroll: " + CCC1(0)
                    'MsgBox(SaveString)
                    File.WriteAllText("Error " + DateTime.Now.ToString("dd.MM.yyyy HH.mm") + ".txt", SaveString)
                    Dim Request As HttpWebRequest = CType(WebRequest.Create("https://docs.google.com/forms/d/e/1FAIpQLSdR1QI19Lh-c-XO_iXNkDwsTUZhCMEu84boQkgW5AOBUxyiyA/formResponse"), HttpWebRequest)
                    Request.Method = "POST"
                    Request.ContentType = "application/x-www-form-urlencoded"
                    Dim Post As String = "entry.240217066=" + My.Computer.Info.OSFullName + "&entry.358200455=" + WebbrowserURL + "&entry.618751432=" + SubSprache + "&entry.924054550=" + Resu.ToString + "&entry.679000538=" + ex.ToString + "&entry.1789515979=" + SoftSubsString + "&entry.683247287=" + Application.ProductVersion + "&entry.377264428=" + CCC1(0) + "&fvv=1&draftResponse=[null,null," + Chr(34) + "-3005021683493723280" + Chr(34) + "] &pageHistory=0&fbzx=-3005021683493723280"
                    Dim byteArray() As Byte = Encoding.UTF8.GetBytes(Post)
                    Request.ContentLength = byteArray.Length
                    Dim DataStream As Stream = Request.GetRequestStream()
                    DataStream.Write(byteArray, 0, byteArray.Length)
                    DataStream.Close()
                    Dim Response As HttpWebResponse = Request.GetResponse()
                    DataStream = Response.GetResponseStream()
                    Dim reader As New StreamReader(DataStream)
                    Dim ServerResponse As String = reader.ReadToEnd()
                    reader.Close()
                    DataStream.Close()
                    Response.Close()
                    Dim Version_Check As String() = ServerResponse.Split(New String() {"<div class=" + Chr(34) + "freebirdFormviewerViewResponseConfirmationMessage" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim Version_Check2 As String() = Version_Check(1).Split(New String() {"</div>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    If Application.ProductVersion = Version_Check2(0) Then
                    Else
                        MsgBox("A newer version is available: v" + Version_Check2(0))
                    End If
                End If
            End If

        End Try
    End Sub



    Private Function DownloadFFMPEG(ByVal DL_URL As String, ByVal DL_Pfad As String, ByVal Filename As String) As String
        Dim proc As New Process
        Control.CheckForIllegalCrossThreadCalls = False
        'Dim input As String = Me.dlgOpen.FileName
        'Dim output As String = Me.dlgSave.FileName

        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"

        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        ' Dim cmd As String = "-i " + Chr(34) + URL_DL + Chr(34) + " -c copy -bsf:a aac_adtstoasc " + Pfad_DL 'start ffmpeg with command strFFCMD string
        '-bsf:a aac_adtstoasc 
        Dim cmd As String = "-i " + DL_URL + " " + ffmpeg_command + " " + DL_Pfad 'start ffmpeg with command strFFCMD string
        MsgBox(cmd)
        '22050
        ' 
        Dim ffmpegOutput As String = Nothing
        Dim ffmpegOutput2 As String = Nothing
        'all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        sr = proc.StandardError 'standard error is used by ffmpeg
        Dim x As Boolean = False
        Dim Grundwert As Integer
        Do
            'If BG.CancellationPending Then 'check if a cancellation request was made
            '    proc.Kill()
            '    Return Nothing
            '    Exit Function
            'End If
            ffmpegOutput = ffmpegOutput + vbNewLine + sr.ReadLine
            ffmpegOutput2 = sr.ReadLine


            Try
                If x = False Then
                    Dim ZeitGesamt As String() = ffmpegOutput.Split(New String() {"Duration: "}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim ZeitGesamt2 As String() = ZeitGesamt(1).Split(New [Char]() {System.Convert.ToChar(".")})
                    Dim ZeitGesamtSplit() As String = ZeitGesamt2(0).Split(New [Char]() {System.Convert.ToChar(":")})
                    Dim ZeitGesamtInteger As Integer = CInt(ZeitGesamtSplit(0)) * 3600 + CInt(ZeitGesamtSplit(1)) * 60 + CInt(ZeitGesamtSplit(2))
                    Grundwert = ZeitGesamtInteger
                    x = True
                End If
                If Me.Visible = False Or AbourtList.Contains(Filename) Then
                    proc.Kill()
                    RaiseEvent UpdateUI(Filename, 200)
                    Return Nothing
                    Exit Function
                End If
                Dim ZeitFertig As String() = sr.ReadLine.Split(New String() {"time="}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeitFertig2 As String() = ZeitFertig(1).Split(New [Char]() {System.Convert.ToChar(".")})

                Dim ZeitFertigSplit() As String = ZeitFertig2(0).Split(New [Char]() {System.Convert.ToChar(":")})
                Dim ZeitFertigInteger As Integer = CInt(ZeitFertigSplit(0)) * 3600 + CInt(ZeitFertigSplit(1)) * 60 + CInt(ZeitFertigSplit(2))
                Dim percent As Integer = (CInt(ZeitFertigInteger / Grundwert * 100))
                RaiseEvent UpdateUI(Filename, percent)
                'AsyncWorkerX.RunAsync(AddressOf Main_Update_Gecko, Filename, percent)
            Catch ex As Exception

            End Try

        Loop Until proc.HasExited And ffmpegOutput2 = Nothing Or ffmpegOutput2 = ""
        'AsyncWorkerX.RunAsync(AddressOf Main_Update_Gecko, Filename, 100)
        RaiseEvent UpdateUI(Filename, 100)
        TaskCount = TaskCount - 1
        'MsgBox(ffmpegOutput)
        Return Nothing
    End Function

    Private Sub Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            Me.Visible = False
            Pause(2)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Main_UpdateUI(sender As String, ByVal int As Integer) Handles Me.UpdateUI
        For i As Integer = 0 To PB_list.Count - 1

            If PB_list(i).Name = sender Then
                If int = 200 Then
                    Dim p As PictureBox = PB_list(i)
                    Dim c As Integer = CInt(ListView1.Items.Item(i).Text)
                    ListView1.Items.Item(i).Text = int
                    p.Image = p.BackgroundImage
                    Dim g As Graphics = Graphics.FromImage(p.Image)
                    Dim ProgressbarPoint As Point = New Point(195, 70)
                    Dim WeißeBox As Point = New Point(750, 93)
                    Dim ProzentText As Point = New Point(773, 95)
                    Dim Weiß As Brush = New SolidBrush(Color.FromArgb(242, 242, 242))
                    g.FillRectangle(Weiß, WeißeBox.X + 1, WeißeBox.Y + 1, 50, 20)
                    g.DrawString("-%", FontLabel2.Font, Brushes.Black, ProzentText)
                    Dim brGradient As Brush = New SolidBrush(Color.FromArgb(125, 0, 0))
                    g.FillRectangle(brGradient, ProgressbarPoint.X + 1, ProgressbarPoint.Y + 1, 600, 19)
                    g.Dispose()
                Else
                    Dim p As PictureBox = PB_list(i)
                    Dim c As Integer = CInt(ListView1.Items.Item(i).Text)
                    ListView1.Items.Item(i).Text = int
                    p.Image = p.BackgroundImage
                    Dim g As Graphics = Graphics.FromImage(p.Image)

                    Dim ProgressbarPoint As Point = New Point(195, 70)
                    Dim WeißeBox As Point = New Point(750, 93)
                    Dim ProzentText As Point = New Point(755, 95)
                    Dim Weiß As Brush = New SolidBrush(Color.FromArgb(242, 242, 242))
                    If int < 10 Then
                        ProzentText = New Point(773, 95)
                    ElseIf int < 100 Then
                        ProzentText = New Point(768, 95)
                    End If
                    g.FillRectangle(Weiß, WeißeBox.X + 1, WeißeBox.Y + 1, 50, 20)
                    g.DrawString(int.ToString + "%", FontLabel2.Font, Brushes.Black, ProzentText)
                    Dim brGradient As Brush = New SolidBrush(Color.FromArgb(247, 140, 37))
                    g.FillRectangle(brGradient, ProgressbarPoint.X + 1, ProgressbarPoint.Y + 1, int * 6, 19)
                    g.Dispose()
                End If
            End If
        Next
    End Sub

    Private Sub pictureBox3_Click(sender As Object, e As EventArgs) Handles pictureBox3.Click
        If TaskCount > 0 Then
            If MessageBox.Show("Are you sure you want close the program and end all active downloads?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub


    Private Sub pictureBox4_Click(sender As Object, e As EventArgs) Handles pictureBox4.Click
        Anime_Add.Show()

    End Sub

    Private Sub pictureBox2_Click(sender As Object, e As EventArgs) Handles pictureBox2.Click
        einstellungen.Show()
    End Sub

    Private Sub pictureBox1_Click(sender As Object, e As EventArgs) Handles pictureBox1.Click
        UserBowser = True
        GeckoFX.Show()
    End Sub

#Region " Move Form "

    ' [ Move Form ]
    '
    ' // By Elektro 

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If

    End Sub
#End Region

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs)
        Startup.ShowDialog()
    End Sub

    Public Function RemoveExtraSpaces(input_text As String) As String

        Dim rsRegEx As System.Text.RegularExpressions.Regex
        rsRegEx = New System.Text.RegularExpressions.Regex("\s+")

        Return rsRegEx.Replace(input_text, " ").Trim()

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            For s As Integer = 0 To ListView1.Items.Count - 1
                Dim r As Rectangle = ListView1.Items.Item(s).Bounds
                PB_list(s).SetBounds(r.X, r.Y, r.Width, r.Height)
                bt_dl(s).SetBounds(755, r.Y + 20, 50, 40)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Function GetPage(url As String) As String
        Try
            Dim ourUri As New Uri(url)
            Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(ourUri), HttpWebRequest)
            myHttpWebRequest.Timeout = 10000
            Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
            Return myHttpWebResponse.ResponseUri.ToString
            myHttpWebResponse.Close()
        Catch e As Exception
            'MsgBox(e.Message.ToString)
            Return url
        End Try
    End Function


    Public Function FFMPEG_Reso(ByVal DL_URL As String) As String
        Dim proc As New Process
        Dim exepath As String = Application.StartupPath + "\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        ' Dim cmd As String = "-i " + Chr(34) + URL_DL + Chr(34) + " -c copy -bsf:a aac_adtstoasc " + Pfad_DL 'start ffmpeg with command strFFCMD string
        '-bsf:a aac_adtstoasc 
        Dim cmd As String = "-i " + Chr(34) + DL_URL + Chr(34) 'start ffmpeg with command strFFCMD string
        'MsgBox(cmd)
        '22050
        ' 
        Dim ffmpegOutput As String = Nothing
        Dim ffmpegOutput2 As String = Nothing
        'all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        sr = proc.StandardError 'standard error is used by ffmpeg
        Dim ZeitAnzeige As String = Nothing
        Dim StreamNR As String = Nothing
        Dim x As Boolean = False
        Do

            ffmpegOutput = ffmpegOutput + vbNewLine + sr.ReadLine
            ffmpegOutput2 = sr.ReadLine
            Try
                If x = False Then
                    If InStr(ffmpegOutput, "Duration: ") Then
                        x = True
                        Dim ZeitGesamt As String() = ffmpegOutput.Split(New String() {"Duration: "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim ZeitGesamt2 As String() = ZeitGesamt(1).Split(New [Char]() {System.Convert.ToChar(".")})
                        Dim ZeitGesamtSplit() As String = ZeitGesamt2(0).Split(New [Char]() {System.Convert.ToChar(":")})

                        For i As Integer = 0 To ZeitGesamtSplit.Count - 1
                            If ZeitGesamtSplit(i) = "00" Then

                            Else
                                If ZeitAnzeige = Nothing Then
                                    ZeitAnzeige = ZeitGesamtSplit(i)
                                Else
                                    ZeitAnzeige = ZeitAnzeige + ":" + ZeitGesamtSplit(i)
                                End If
                            End If
                        Next
                    End If
                End If

            Catch ex As Exception

            End Try
            Pause(1)
        Loop Until proc.HasExited And ffmpegOutput2 = Nothing Or InStr(ffmpegOutput, "At least one output file must be specified") 'And ffmpegOutput2 = Nothing Or ffmpegOutput2 = ""
        If InStr(ffmpegOutput, "Server returned 401 Unauthorized") Then

        End If
        Dim Zeilen() As String = ffmpegOutput.Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 0 To Zeilen.Count - 1
            If InStr(Zeilen(i), "x" + Resu.ToString + " [") Then
                Dim ZeileReso() As String = Zeilen(i).Split(New String() {": Video:"}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeileReso2() As String = ZeileReso(0).Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)
                StreamNR = ZeileReso2(1)
            End If
        Next
        If StreamNR = Nothing Then
            MsgBox(cmd + vbNewLine + ffmpegOutput)
            ResoNotFoundString = ffmpegOutput
            DialogTaskString = "Resolution"
            Reso.ShowDialog()
            'MsgBox(ResoBackString)
            If UserCloseDialog = True Then
                Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
            Else
                For i As Integer = 0 To Zeilen.Count - 1
                    If InStr(Zeilen(i), ResoBackString) Then
                        Dim ZeileReso() As String = Zeilen(i).Split(New String() {": Video:"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim ZeileReso2() As String = ZeileReso(0).Split(New String() {"Stream #"}, System.StringSplitOptions.RemoveEmptyEntries)
                        StreamNR = ZeileReso2(1)
                    End If
                Next
            End If
        End If
        Return ZeitAnzeige + "#1" + StreamNR
    End Function
    'If CBool(InStr(str, "x" + Resu.ToString + ",")) Then
    '    Resu2 = "x" + Resu.ToString
    'Else
    '    If CBool(InStr(str, ResuSave + ",")) Then
    '        Resu2 = Resu2
    '    Else

    '        ResoNotFoundString = str
    '        DialogTaskString = "Resolution"
    '        Reso.ShowDialog()
    '        'MsgBox(ResoBackString)
    '        If UserCloseDialog = True Then
    '            Throw New System.Exception(Chr(34) + "UserAbort" + Chr(34))
    '        Else
    '            Resu2 = ResoBackString
    '        End If
    '    End If
    'End If
    ''MsgBox(Resu2)
    'Dim VLC_URI_1 As String() = str.Split(New String() {Resu2 + ","}, System.StringSplitOptions.RemoveEmptyEntries)
    'Dim VLC_URI_2 As String() = VLC_URI_1(1).Split(New [Char]() {Chr(34)})
    'Dim VLC_URI_3 As String() = VLC_URI_2(2).Split(New [Char]() {System.Convert.ToChar("#")})
End Class