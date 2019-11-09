Imports Gecko
Imports Gecko.Cache
Imports Microsoft.Win32
Imports System.Net


Public Class einstellungen
    Private Sub einstellungen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(Main.Location.X + Main.Width / 2 - Me.Width / 2, Main.Location.Y + Main.Height / 2 - Me.Height / 2)
        Me.Icon = My.Resources.icon
        Try
            GB_Resolution.Text = Main.GB_Resolution_Text
            GB_SubLanguage.Text = Main.GB_SubLanguage_Text
            GB_Sub_Path.Text = Main.GB_Sub_Path_Text
            RBAnime.Text = Main.RBAnime_Text
            RBStaffel.Text = Main.RBStaffel_Text
            DL_Count_simultaneous.Text = Main.DL_Count_simultaneousText
        Catch ex As Exception

        End Try

        If Main.Resu = 1080 Then
            A1080p.Checked = True
        ElseIf Main.Resu = 720 Then
            A720p.Checked = True
        ElseIf Main.Resu = 480 Then
            A480p.Checked = True
        ElseIf Main.Resu = 360 Then
            A360p.Checked = True
        End If
        'ComboBox1.Items.Add("English")
        'ComboBox1.Items.Add("Deutsch")
        'ComboBox1.Items.Add("Italiano (Italian)")
        'ComboBox1.Items.Add("Français (France)")
        'ComboBox1.Items.Add("العربية (Arabic)")
        'ComboBox1.Items.Add("Español (LA)")
        'ComboBox1.Items.Add("Español (España)")
        'ComboBox1.Items.Add("Português (Brasil)")
        'ComboBox1.Items.Add("Русский (Russian)")

        ' ComboBox1.SelectedItem = SubFolder_Nothing
        If Check_CB() = False Then
            ComboBox1.Items.Add(Main.CB_SuB_Nothing)
        End If
        If Main.SubSprache = "deDE" Then
            ComboBox1.SelectedItem = "Deutsch"
        ElseIf Main.SubSprache = "enUS" Then
            ComboBox1.SelectedItem = "English"
        ElseIf Main.SubSprache = "ptBR" Then
            ComboBox1.SelectedItem = "Português (Brasil)"
        ElseIf Main.SubSprache = "esLA" Then
            ComboBox1.SelectedItem = "Español (LA)"
        ElseIf Main.SubSprache = "frFR" Then
            ComboBox1.SelectedItem = "Français (France)"
        ElseIf Main.SubSprache = "arME" Then
            ComboBox1.SelectedItem = "العربية (Arabic)"
        ElseIf Main.SubSprache = "ruRU" Then
            ComboBox1.SelectedItem = "Русский (Russian)"
        ElseIf Main.SubSprache = "itIT" Then
            ComboBox1.SelectedItem = "Italiano (Italian)"
        ElseIf Main.SubSprache = "esES" Then
            ComboBox1.SelectedItem = "Español (España)"
        Else
            ComboBox1.SelectedItem = Main.CB_SuB_Nothing
        End If

        'If Main.SoftSubs = False Then
        '    RawVideo.Checked = False
        '    HardSub.Checked = True
        'ElseIf Main.SoftSubs = True Then
        '    RawVideo.Checked = True
        'End If
        If Main.SubFolder = 1 Then
            RBAnime.Checked = True
        ElseIf Main.SubFolder = 2 Then
            RBStaffel.Checked = True
        End If
        NumericUpDown1.Value = Main.MaxDL
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            Firefox_True.Checked = CBool(Integer.Parse(rkg.GetValue("NoUse").ToString))
            'MsgBox(Resu)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles pictureBox4.Click
        Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
        If A1080p.Checked Then
            Main.Resu = 1080
            rk.SetValue("Resu", 1080, RegistryValueKind.String)
        ElseIf A720p.Checked Then
            Main.Resu = 720
            rk.SetValue("Resu", 720, RegistryValueKind.String)
        ElseIf A360p.Checked Then
            Main.Resu = 360
            rk.SetValue("Resu", 360, RegistryValueKind.String)
        ElseIf A480p.Checked Then
            Main.Resu = 480
            rk.SetValue("Resu", 480, RegistryValueKind.String)
        End If
        If ComboBox1.SelectedItem.ToString = "English" Then
            Main.SubSprache = "enUS"
            rk.SetValue("Sub", "enUS", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Deutsch" Then
            Main.SubSprache = "deDE"
            rk.SetValue("Sub", "deDE", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Português (Brasil)" Then
            Main.SubSprache = "ptBR"
            rk.SetValue("Sub", "ptBR", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Español (LA)" Then
            Main.SubSprache = "esLA"
            rk.SetValue("Sub", "esLA", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Français (France)" Then
            Main.SubSprache = "frFR"
            rk.SetValue("Sub", "frFR", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "العربية (Arabic)" Then
            Main.SubSprache = "arME"
            rk.SetValue("Sub", "arME", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Русский (Russian)" Then
            Main.SubSprache = "ruRU"
            rk.SetValue("Sub", "ruRU", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Italiano (Italian)" Then
            Main.SubSprache = "itIT"
            rk.SetValue("Sub", "itIT", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = "Español (España)" Then
            Main.SubSprache = "esES"
            rk.SetValue("Sub", "esES", RegistryValueKind.String)

        ElseIf ComboBox1.SelectedItem.ToString = Main.CB_SuB_Nothing Then
            Main.SubSprache = "None"
            rk.SetValue("Sub", "None", RegistryValueKind.String)

        End If
        'If RawVideo.Checked = True Then
        '    Main.SoftSubs = True
        '    rk.SetValue("RawVideo", 1, RegistryValueKind.String)
        '    'MsgBox("Diese Einstellung benötigt einen Neustart des Programmes!", MsgBoxStyle.OkOnly)
        'Else
        '    Main.SoftSubs = False
        '    rk.SetValue("RawVideo", 0, RegistryValueKind.String)
        'End If
        If RBAnime.Checked = True Then
            Main.SubFolder = 1
            rk.SetValue("SubFolder", 1, RegistryValueKind.String)
        ElseIf RBStaffel.Checked = True Then
            Main.SubFolder = 2
            rk.SetValue("SubFolder", 2, RegistryValueKind.String)
        End If
        rk.SetValue("SL_DL", NumericUpDown1.Value, RegistryValueKind.String)
        Main.MaxDL = NumericUpDown1.Value
        If Firefox_True.Checked = True Then
            rk.SetValue("NoUse", 1, RegistryValueKind.String)
        ElseIf Firefox_True.Checked = False Then
            rk.SetValue("NoUse", 0, RegistryValueKind.String)
        End If
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("https://www.youtube.com/user/hama3254")
    End Sub

    Private Function Check_CB() As Boolean
        Dim C As Boolean = False
        For i As Integer = 0 To ComboBox1.Items.Count - 1
            If ComboBox1.Items.Item(i).ToString = Main.CB_SuB_Nothing Then
                C = True
                Exit For
            End If
        Next
        Return C
    End Function
    Private Sub pictureBox3_Click(sender As Object, e As EventArgs) Handles pictureBox3.Click
        Main.LoginOnly = "US_UnBlock"
        GeckoFX.keks = InputBox("Please insert the cookie below.")
        GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com/")
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

        Main.LoginOnly = "US_UnBlock"
        Dim wb As New WebClient

        Dim Session As String = wb.DownloadString("http://api-manga.crunchyroll.com/cr_start_session?api_ver=1.0&device_type=com.crunchyroll.windows.desktop&access_token=LNDJgOit5yaRIWN&device_id=" + GeräteID())

        If CBool(InStr(Session, "bad_request")) Then
            Session = wb.DownloadString("http://api-manga.crunchyroll.com/cr_start_session?api_ver=1.0&device_type=com.crunchyroll.iphone&access_token=QWjz212GspMHH9h&device_id=" + GeräteID())

        End If
        If CBool(InStr(Session, "bad_request")) Then
            Session = wb.DownloadString("http://api-manga.crunchyroll.com/cr_start_session?api_ver=1.0&device_type=com.crunchyroll.manga.android&access_token=FLpcfZH4CbW4muO&device_id=" + GeräteID())
        End If

        If CBool(InStr(Session, "bad_request")) Then
            MsgBox(Main.CR_Unlock_Error_String, MsgBoxStyle.OkOnly)
            Exit Sub
        ElseIf CBool(InStr(Session, "Unauthenticated request")) Then
            MsgBox(Main.CR_Unlock_Error_String, MsgBoxStyle.OkOnly)
            Exit Sub
        Else
            'MsgBox(Session)
            GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com/")
            Dim SessionID1 As String() = Session.Split(New String() {Chr(34) + "session_id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim SessionID2 As String() = SessionID1(1).Split(New [Char]() {Chr(34)})
            GeckoFX.keks = SessionID2(0)

        End If
    End Sub

    Private Function GeräteID() As String
        Dim rnd As New Random
        Dim possible As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim HWID As String = Nothing

        For i As Integer = 0 To 31
            Dim ZufallsZahl As Integer = rnd.Next(1, 33)
            HWID = HWID + possible(ZufallsZahl)
        Next
        Return HWID
    End Function

    Private Sub pictureBox1_Click(sender As Object, e As EventArgs) Handles pictureBox1.Click
        Me.Close()
    End Sub
#Region "UI"
    Private Sub pictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox1.MouseEnter
        pictureBox1.BackColor = SystemColors.Control
    End Sub

    Private Sub pictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox1.MouseLeave
        pictureBox1.BackColor = Color.Transparent
    End Sub

    Private Sub pictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox3.MouseEnter
        pictureBox3.Image = My.Resources.crdsettings_setowncookie_button_hover
    End Sub

    Private Sub pictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox3.MouseLeave
        pictureBox3.Image = My.Resources.crdsettings_setowncookie_button
    End Sub

    Private Sub pictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox4.MouseEnter
        pictureBox4.Image = My.Resources.crdSettings_Button_SafeExit_hover
    End Sub

    Private Sub pictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox4.MouseLeave
        pictureBox4.Image = My.Resources.crdSettings_Button_SafeExit
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

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox2.Image = My.Resources.crdsettings_setUScookie_button_hover
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = My.Resources.crdsettings_setUScookie_button
    End Sub



    Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ComboBox1.DrawItem
        sender.BackColor = Color.White
        If e.Index >= 0 Then
            Using st As New StringFormat With {.Alignment = StringAlignment.Center}
                ' e.DrawBackground()
                ' e.DrawFocusRectangle()
                e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds)
                e.Graphics.DrawString(sender.Items(e.Index).ToString, e.Font, Brushes.Black, e.Bounds, st)

            End Using
        End If
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        SoftSub.ShowDialog()
    End Sub

    Private Sub PictureBox5_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox5.MouseEnter
        PictureBox5.Image = My.Resources.settings_add_softsubs_hover
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        PictureBox5.Image = My.Resources.settings_add_softsubs
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Startup.ShowDialog()
    End Sub


#End Region
    Private Sub PictureBox6_MouseEnter(sender As Object, e As EventArgs)
        PictureBox6.Image = My.Resources.main_credits_hover
    End Sub

    Private Sub PictureBox6_MouseLeave(sender As Object, e As EventArgs)
        PictureBox6.Image = My.Resources.main_credits_default
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        GeckoFX.WebBrowser1.Navigate("about:config")
    End Sub




#End Region
End Class