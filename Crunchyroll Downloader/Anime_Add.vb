Imports Microsoft.Win32
Imports System.Net
Imports Gecko
Imports System.IO
Public Class Anime_Add
    Public Mass_DL_Cancel As Boolean = False
    Public List_DL_Cancel As Boolean = False

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            If ComboBox2.Text = Main.SubFolder_Nothing Then
                Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
                rk.SetValue("SubFolder_Value", Main.SubFolder_Nothing, RegistryValueKind.String)
            ElseIf ComboBox2.Text = Main.SubFolder_automatic Then
                Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
                rk.SetValue("SubFolder_Value", Main.SubFolder_automatic, RegistryValueKind.String)
            Else
                Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
                rk.SetValue("SubFolder_Value", ComboBox2.Text, RegistryValueKind.String)
            End If
        Catch ex As Exception
            ComboBox2.Text = Main.SubFolder_Nothing
        End Try
    End Sub

    Private Sub Anime_Add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.icon
        Try
            For i As Integer = 0 To Main.ListBoxList.Count - 1
                ListBox1.Items.Add(Main.ListBoxList.Item(i))

            Next
        Catch ex As Exception

        End Try
        Try
            Main.waveOutSetVolume(0, 0)
        Catch ex As Exception

        End Try
        Me.Location = New Point(Main.Location.X + Main.Width / 2 - Me.Width / 2, Main.Location.Y + Main.Height / 2 - Me.Height / 2)
        TextBox4.Text = Main.Pfad

        Dim SubFolder_Value As String
        Try
            Dim rkg As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CRDownloader")
            SubFolder_Value = rkg.GetValue("SubFolder_Value").ToString
            If SubFolder_Value = Main.SubFolder_Nothing Then
                ComboBox2.Items.Add(Main.SubFolder_automatic)
                ComboBox2.Items.Add(Main.SubFolder_Nothing)
            ElseIf SubFolder_Value = Main.SubFolder_automatic Then
                ComboBox2.Items.Add(Main.SubFolder_automatic)
                ComboBox2.Items.Add(Main.SubFolder_Nothing)
            Else

                ComboBox2.Items.Add(Main.SubFolder_automatic)
                ComboBox2.Items.Add(Main.SubFolder_Nothing)
                ComboBox2.Items.Add(SubFolder_Value)
            End If
        Catch ex As Exception
            ComboBox2.Items.Add(Main.SubFolder_automatic)
            ComboBox2.Items.Add(Main.SubFolder_Nothing)
            ComboBox2.SelectedItem = Main.SubFolder_Nothing
            SubFolder_Value = Main.SubFolder_Nothing
        End Try

        Try
            Dim di As New System.IO.DirectoryInfo(Main.Pfad)
            For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                Else
                    ComboBox2.Items.Add(fi.Name)
                End If
            Next
            Dim Result As New List(Of String)
            'Jeder Eintrag in der Combobox durchgehen
            For Each item As String In ComboBox2.Items
                'Wenn der Combobox-Eintrag noch nicht in der Result-List vorhanden ist, Eintrag der Result-List hinzufügen
                If Result.Contains(item) = False Then
                    Result.Add(item)
                End If
            Next
            'In der Result-List sind jetzt alle Einträge einmal vorhanden
            'Combobox leeren
            ComboBox2.Items.Clear()
            'Die Result-List der Combobox hinzufügen
            ComboBox2.Items.AddRange(Result.ToArray)
            ComboBox2.SelectedItem = SubFolder_Value
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox4_DoubleClick(sender As Object, e As EventArgs) Handles TextBox4.DoubleClick
        'MsgBox(DL_Path_String, MsgBoxStyle.OkOnly)
        Dim FolderBrowserDialog1 As New FolderBrowserDialog()

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            ComboBox2.Items.Clear()
            Main.Pfad = FolderBrowserDialog1.SelectedPath
            Dim rk0 As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
            rk0.SetValue("Ordner", Main.Pfad, RegistryValueKind.String)

            ComboBox2.Items.Add(Main.SubFolder_automatic)
            ComboBox2.Items.Add(Main.SubFolder_Nothing)
            ComboBox2.SelectedItem = Main.SubFolder_Nothing
            TextBox4.Text = Main.Pfad
            Try
                Dim di As New System.IO.DirectoryInfo(Main.Pfad)
                For Each fi As System.IO.DirectoryInfo In di.EnumerateDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly)
                    If fi.Attributes.HasFlag(System.IO.FileAttributes.Hidden) Then
                    Else
                        ComboBox2.Items.Add(fi.Name)
                    End If
                Next
                Dim Result As New List(Of String)
                'Jeder Eintrag in der Combobox durchgehen
                For Each item As String In ComboBox2.Items
                    'Wenn der Combobox-Eintrag noch nicht in der Result-List vorhanden ist, Eintrag der Result-List hinzufügen
                    If Result.Contains(item) = False Then
                        Result.Add(item)
                    End If
                Next
                'In der Result-List sind jetzt alle Einträge einmal vorhanden
                'Combobox leeren
                'ComboBox2.Items.Clear()
                'Die Result-List der Combobox hinzufügen
                'ComboBox2.Items.AddRange(Result.ToArray)
            Catch ex As Exception
            End Try
        End If
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
    Private Sub PictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox3.MouseEnter
        pictureBox3.BackColor = SystemColors.Control
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox3.MouseLeave
        pictureBox3.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles pictureBox3.Click
        If ListBox1.Items.Count > 0 Then
            Main.ListBoxList.Clear()
            For i As Integer = 0 To ListBox1.Items.Count - 1
                Main.ListBoxList.Add(ListBox1.Items.Item(i))
            Next
        End If
        Me.Close()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles pictureBox4.Click
        'pictureBox4.Enabled = False
        Main.LoginOnly = "Download Mode!"
        If groupBox1.Visible = True Then
            Try
                If CBool(InStr(textBox1.Text, "crunchyroll.com")) Or CBool(InStr(textBox1.Text, "funimation.com")) Then
                    If StatusLabel.Text = "Status: waiting for episode selection" Then
                        If MessageBox.Show("Are you sure you want cancel the advanced download?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            StatusLabel.Text = "Status: idle"
                        Else
                            Exit Sub
                            pictureBox4.Enabled = True
                        End If
                        'ElseIf LabelUpdate = "Status: looking for video file" Then
                        '    Exit Sub
                        '    pictureBox4.Enabled = True
                    Else
                        If Main.RunningDownloads >= Main.MaxDL Then
                            ListBox1.Items.Add(textBox1.Text)
                            textBox1.ForeColor = Color.FromArgb(9248044)
                            Main.Pause(1)
                            textBox1.ForeColor = Color.Black
                            textBox1.Text = "URL"
                        Else
                            If Main.Grapp_RDY = True Then
                                GeckoFX.WebBrowser1.Navigate(textBox1.Text)
                                StatusLabel.Text = "Status: looking for video file"
                                Main.b = False
                            End If
                        End If
                    End If
                ElseIf CBool(InStr(textBox1.Text, "Test=true")) Then
                    GeckoFX.WebBrowser1.Navigate(textBox1.Text)
                Else 'If CBool(InStr(textBox1.Text, "vrv.co")) Then
                    If MessageBox.Show("This in NOT a Crunchyroll URL, try anyway?", "confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim FileLocation As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
                        Dim CurrentFile As String = Nothing
                        For Each File In FileLocation.GetFiles()
                            If InStr(File.FullName, "gecko-network.txt") Then
                                CurrentFile = File.FullName
                                Exit For
                            End If
                        Next
                        If CurrentFile = Nothing Then
                        Else
                            Dim logFileStream As FileStream = New FileStream(CurrentFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
                            Dim logFileReader As StreamReader = New StreamReader(logFileStream)
                            logFileStream.SetLength(0)
                            logFileReader.Close()
                            logFileStream.Close()
                        End If
                        Main.LogBrowserData = True
                        GeckoPreferences.Default("logging.config.LOG_FILE") = "gecko-network.txt"
                        GeckoPreferences.Default("logging.nsHttp") = 3
                        GeckoFX.WebBrowser1.Navigate(textBox1.Text)
                        StatusLabel.Text = "Status: looking for non CR video file"
                        Main.b = False
                    Else
                        Exit Sub
                        pictureBox4.Enabled = True
                    End If

                    'Else
                    'MsgBox(Main.URL_Invaild, MsgBoxStyle.OkOnly)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString)
                Main.b = True
                MsgBox(Main.URL_Invaild, MsgBoxStyle.OkOnly)
            End Try
        ElseIf groupBox2.Visible = True Then
            If Mass_DL_Cancel = True Then
                Mass_DL_Cancel = False
                GroupBox3.Visible = False
                groupBox2.Visible = False
                Main.Grapp_Abord = True
                Main.b = True
                groupBox1.Visible = True
                pictureBox4.Image = My.Resources.main_button_download_default
            Else
                StatusLabel.Text = "Status: idle"
                pictureBox4.Image = My.Resources.add_mass_running_cancel
                Mass_DL_Cancel = True
                PictureBox1.Enabled = False
                PictureBox1.Visible = False
                Main.MassDL()
                comboBox4.Enabled = False
                comboBox3.Enabled = False
                ComboBox1.Enabled = False
            End If
        ElseIf GroupBox3.Visible = True Then
            GroupBox3.Visible = False
            groupBox2.Visible = False
            groupBox1.Visible = True
            List_DL_Cancel = False
            pictureBox4.Image = My.Resources.main_button_download_default
        End If
        If InStr(My.Computer.Info.OSFullName, "Server") Then
            MsgBox("Windows Server is not supported!", MsgBoxStyle.Critical)
            Me.Close()
        End If
        pictureBox4.Enabled = True
    End Sub



    Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ComboBox1.DrawItem, ComboBox2.DrawItem, comboBox3.DrawItem, comboBox4.DrawItem
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

    Private Sub PictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox4.MouseEnter
        If Mass_DL_Cancel = True Then
            pictureBox4.Image = My.Resources.add_mass_running_cancel_hover
        ElseIf List_DL_Cancel = True Then
            pictureBox4.Image = My.Resources.add_mass_running_cancel_hover

        Else
            pictureBox4.Image = My.Resources.main_button_download_hovert
        End If

    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox4.MouseLeave
        If Mass_DL_Cancel = True Then
            pictureBox4.Image = My.Resources.add_mass_running_cancel
        ElseIf List_DL_Cancel = True Then
            pictureBox4.Image = My.Resources.add_mass_running_cancel
        Else
            pictureBox4.Image = My.Resources.main_button_download_default
        End If

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles textBox1.Click
        If textBox1.Text = "URL" Then
            textBox1.Text = Nothing
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        groupBox1.Visible = True
        groupBox2.Visible = False
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'MsgBox("Test")
        comboBox3.Items.Clear()
        comboBox4.Items.Clear()
        'comboBox3.Items.Add("Test")
        Dim SeasonDropdownAnzahl As String() = Main.WebbrowserText.Split(New String() {"season-dropdown content-menu block"}, System.StringSplitOptions.RemoveEmptyEntries)
        Array.Reverse(SeasonDropdownAnzahl)
        Dim SDV As Integer = 0
        For i As Integer = 0 To SeasonDropdownAnzahl.Count - 1
            If InStr(SeasonDropdownAnzahl(i), Chr(34) + ">" + ComboBox1.SelectedItem.ToString + "</a>") Then
                SDV = i
            End If
        Next
        'MsgBox(SDV)
        Dim Anzahl As String() = SeasonDropdownAnzahl(SDV).Split(New String() {"wrapper container-shadow hover-classes"}, System.StringSplitOptions.RemoveEmptyEntries)
        'MsgBox(Anzahl(0))
        Dim c As Integer = Anzahl.Count - 1
        Array.Reverse(Anzahl)
        For i As Integer = 0 To Anzahl.Count - 2
            Dim URLGrapp As String() = Anzahl(i).Split(New String() {"title=" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim URLGrapp2 As String() = URLGrapp(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            comboBox3.Items.Add(URLGrapp2(0))
            comboBox4.Items.Add(URLGrapp2(0))
        Next
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.add_mass_cancel_hover
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.add_mass_cancel
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ListBox1.Items.Count > 0 Then
            If StatusLabel.Text = "Status: idle" Then
                StatusLabel.Text = "Status: items in queue, click to work off."
            End If
        End If
    End Sub


#Region "Listbox"

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If GroupBox3.Visible = True Then
            If ListBox1.Items.Count = 0 Then
                GroupBox3.Visible = False
                groupBox2.Visible = False
                groupBox1.Visible = True
                List_DL_Cancel = False
                pictureBox4.Image = My.Resources.main_button_download_default
            End If
        End If
        If Main.RunningDownloads < Main.MaxDL Then
            If ListBox1.Items.Count > 0 Then
                If GroupBox3.Visible = True Then
                    If InStr(ListBox1.GetItemText(ListBox1.Items(0)), "funimation.com") Then
                        If Main.Funimation_Grapp_RDY = True Then
                            GeckoFX.WebBrowser1.Navigate(ListBox1.GetItemText(ListBox1.Items(0)))
                            ListBox1.Items.Remove(ListBox1.Items(0))
                            Main.Funimation_Grapp_RDY = False
                            Main.b = False
                        End If

                    Else
                        If Main.Grapp_RDY = True Then
                            GeckoFX.WebBrowser1.Navigate(ListBox1.GetItemText(ListBox1.Items(0)))
                            ListBox1.Items.Remove(ListBox1.Items(0))
                            Main.Grapp_RDY = False
                            Main.b = False
                        End If
                    End If
                End If


            End If
        End If

    End Sub
    Private Sub StatusLabel_Click(sender As Object, e As EventArgs) Handles StatusLabel.Click
        If StatusLabel.Text = "Status: items in queue, click to work off." Then
            groupBox1.Visible = False
            groupBox2.Visible = False
            GroupBox3.Visible = True
            pictureBox4.Image = My.Resources.add_mass_running_cancel
            List_DL_Cancel = True
        End If

    End Sub


    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles textBox2.Click
        If textBox2.Text = "Name of the Anime" Then
            textBox2.Text = Nothing
        End If
    End Sub


    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub




#End Region

End Class