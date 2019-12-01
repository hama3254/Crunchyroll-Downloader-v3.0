Imports System.Net
Imports System.Text
Imports System.IO
Imports Microsoft.Win32
Imports System.ComponentModel
Public Class SoftSub
#Region "UI"

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


    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox2.Image = My.Resources.download_subs_hover
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = My.Resources.download_subs
    End Sub
    Private Sub pictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox4.MouseEnter
        pictureBox4.Image = My.Resources.crdSettings_Button_SafeExit_hover
    End Sub

    Private Sub pictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox4.MouseLeave
        pictureBox4.Image = My.Resources.crdSettings_Button_SafeExit
    End Sub
    Private Sub pictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox1.MouseEnter
        pictureBox1.BackColor = SystemColors.Control
    End Sub

    Private Sub pictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox1.MouseLeave
        pictureBox1.BackColor = Color.Transparent
    End Sub

    Private Sub textBox1_Click(sender As Object, e As EventArgs) Handles textBox1.Click
        If textBox1.Text = "URL" Then
            textBox1.Text = Nothing
        End If
    End Sub
#End Region
    Dim LocalSoftSubs As New List(Of String)
    Private Sub pictureBox1_Click(sender As Object, e As EventArgs) Handles pictureBox1.Click
        Me.Close()
    End Sub

    Private Sub pictureBox4_Click(sender As Object, e As EventArgs) Handles pictureBox4.Click
        Main.SoftSubs.Clear()
        If CBdeDE.Checked = True Then
            Main.SoftSubs.Add("deDE")
        End If
        If CBenUS.Checked = True Then
            Main.SoftSubs.Add("enUS")
        End If
        If CBptBR.Checked = True Then
            Main.SoftSubs.Add("ptBR")
        End If
        If CBesLA.Checked = True Then
            Main.SoftSubs.Add("esLA")
        End If
        If CBfrFR.Checked = True Then
            Main.SoftSubs.Add("frFR")
        End If
        If CBarME.Checked = True Then
            Main.SoftSubs.Add("arME")
        End If
        If CBruRU.Checked = True Then
            Main.SoftSubs.Add("ruRU")
        End If
        If CBitIT.Checked = True Then
            Main.SoftSubs.Add("itIT")
        End If
        If CBesES.Checked = True Then
            Main.SoftSubs.Add("esES")
        End If

        Dim SaveString As String = Nothing
        For ii As Integer = 0 To Main.SoftSubs.Count - 1
            If SaveString = Nothing Then
                SaveString = Main.SoftSubs(ii)
            Else
                SaveString = SaveString + "," + Main.SoftSubs(ii)
            End If
        Next
        If SaveString = Nothing Then
            SaveString = "none"
        End If
        Dim rk As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\CRDownloader")
        rk.SetValue("AddedSubs", SaveString, RegistryValueKind.String)
        Me.Close()
    End Sub

    Private Sub SoftSubs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.icon
        Me.Location = New Point(Main.Location.X + Main.Width / 2 - Me.Width / 2, Main.Location.Y + Main.Height / 2 - Me.Height / 2)

        For i As Integer = 0 To Main.SoftSubs.Count - 1
            If Main.SoftSubs(i) = "deDE" Then
                CBdeDE.Checked = True
            ElseIf Main.SoftSubs(i) = "enUS" Then
                CBenUS.Checked = True
            ElseIf Main.SoftSubs(i) = "ptBR" Then
                CBptBR.Checked = True
            ElseIf Main.SoftSubs(i) = "esLA" Then
                CBesLA.Checked = True
            ElseIf Main.SoftSubs(i) = "frFR" Then
                CBfrFR.Checked = True
            ElseIf Main.SoftSubs(i) = "arME" Then
                CBarME.Checked = True
            ElseIf Main.SoftSubs(i) = "ruRU" Then
                CBruRU.Checked = True
            ElseIf Main.SoftSubs(i) = "itIT" Then
                CBitIT.Checked = True
            ElseIf Main.SoftSubs(i) = "esES" Then
                CBesES.Checked = True
            End If
        Next
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Try
            LocalSoftSubs.Clear()

            If CBdeDE.Checked = True Then
                LocalSoftSubs.Add("deDE")
            End If
            If CBenUS.Checked = True Then
                LocalSoftSubs.Add("enUS")
            End If
            If CBptBR.Checked = True Then
                LocalSoftSubs.Add("ptBR")
            End If
            If CBesLA.Checked = True Then
                LocalSoftSubs.Add("esLA")
            End If
            If CBfrFR.Checked = True Then
                LocalSoftSubs.Add("frFR")
            End If
            If CBarME.Checked = True Then
                LocalSoftSubs.Add("arME")
            End If
            If CBruRU.Checked = True Then
                LocalSoftSubs.Add("ruRU")
            End If
            If CBitIT.Checked = True Then
                LocalSoftSubs.Add("itIT")
            End If
            If CBesES.Checked = True Then
                LocalSoftSubs.Add("esES")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        If LocalSoftSubs.Count > 0 Then
            If CBool(InStr(textBox1.Text, "crunchyroll.com")) Then
                GeckoFX.WebBrowser1.Navigate(textBox1.Text)
                GeckoFX.c = False
                Label2.Text = "Status: looking for sub file"
                PictureBox2.Visible = False
            End If
        Else
            'Label2.Text = "Status: Error - nothing selected"
            MsgBox("Error: no language selected", MsgBoxStyle.Information, "CRD Subtitel")
            PictureBox2.Visible = True
        End If
    End Sub
    Public Sub DownloadSubs()

        Try

#Region "Name + Pfad"
            Dim NameTextBox As Boolean = False
            Dim Pfad2 As String
            Dim CR_FilenName As String
            Dim Bug_Deutsch As String = "-"
            Dim CR_Anime_Titel As String
            Dim CR_Anime_Staffel As String
            Dim CR_Anime_Folge As String
            'Dim CR_Name_by_Titel As String() = GeckoFX.WebBrowser1.Document.Body.OuterHtml.Split(New String() {"<title>"}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim CR_Name_by_Titel_2_Patch As String =CR_Name_by_Titel(1).Split(New String() {"</title>"}, System.StringSplitOptions.RemoveEmptyEntries)
            If CBool(InStr(GeckoFX.WebBrowser1.DocumentTitle, "Anschauen auf Crunchyroll")) Then
                Bug_Deutsch = ":"
                'Throw New System.Exception("Test")
            Else
            End If
            Dim CR_Name_by_Titel_2 As String() = Main.WebbrowserTitle.Split(New String() {Bug_Deutsch}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim CR_Name_by_Script As String() = Main.WebbrowserText.Split(New String() {Chr(34) + "name" + Chr(34) + ": " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim CR_Name_by_Script2 As String() = CR_Name_by_Script(1).Split(New [Char]() {Chr(34)})
            CR_FilenName = CR_Name_by_Titel_2(0).Trim() '+ " " + CR_Name_by_Script2(0).Trim

            Dim CR_FilenName_Backup As String = Nothing

            Dim SubfolderValue As String = Nothing
            If CBool(InStr(GeckoFX.WebBrowser1.Document.Body.OuterHtml, "<h4>")) Then ' Film statt Serie
                Dim CR_Name_1 As String() = Main.WebbrowserText.Split(New String() {"<h4>"}, System.StringSplitOptions.RemoveEmptyEntries)
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

                CR_FilenName_Backup = Main.RemoveExtraSpaces(CR_FilenName)
                If Anime_Add.ComboBox2.Text = Main.SubFolder_automatic Then
                    If Main.SubFolder = 2 Then
                        SubfolderValue = CR_Anime_Titel + "\" + CR_Anime_Staffel + "\"
                    ElseIf Main.SubFolder = 1 Then
                        SubfolderValue = CR_Anime_Titel + "\"
                    End If
                ElseIf Anime_Add.ComboBox2.Text = Main.SubFolder_Nothing Then
                Else
                    SubfolderValue = Anime_Add.ComboBox2.Text + "\"
                End If
            End If
            CR_FilenName = System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
            CR_FilenName = Main.RemoveExtraSpaces(CR_FilenName)

            Pfad2 = Main.Pfad + "\" + CR_FilenName + ".ass"

#End Region
#Region "Subs"
            Dim SoftSubs2 As New List(Of String)
            If LocalSoftSubs.Count > 0 Then
                For i As Integer = 0 To LocalSoftSubs.Count - 1
                    If CBool(InStr(Main.WebbrowserText, Chr(34) + "language" + Chr(34) + ":" + Chr(34) + LocalSoftSubs(i) + Chr(34) + ",")) Then
                        SoftSubs2.Add(LocalSoftSubs(i))
                    Else
                        MsgBox("Softsubtitle for " + LocalSoftSubs(i) + " is not avalible.", MsgBoxStyle.Information)
                    End If
                Next

            End If

#End Region


#Region "Download softsub file"
            If SoftSubs2.Count > 0 Then
                For i As Integer = 0 To SoftSubs2.Count - 1
                    Label2.Text = "Status: downloading subtitle file - " + SoftSubs2(i)
                    Dim SoftSub As String() = Main.WebbrowserText.Split(New String() {Chr(34) + "language" + Chr(34) + ":" + Chr(34) + SoftSubs2(i) + Chr(34) + "," + Chr(34) + "url" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim SoftSub_2 As String() = SoftSub(1).Split(New [Char]() {Chr(34)})
                    Dim SoftSub_3 As String = SoftSub_2(0).Replace("\/", "/")
                    Dim client0 As New Net.WebClient
                    client0.Encoding = Encoding.UTF8
                    Dim str0 As String = client0.DownloadString(SoftSub_3)
                    If File.Exists(Pfad2) Then
                        Pfad2 = Main.Pfad + "\" + CR_FilenName + " " + SoftSubs2(i) + ".ass"
                    End If
                    'MsgBox(Pfad2 + vbNewLine + Main.Pfad)
                    File.WriteAllText(Pfad2, str0, Encoding.UTF8)
                    Main.Pause(1)
                Next
            End If
#End Region

        Catch ex As Exception

        End Try
        Label2.Text = "Status: idle"
        PictureBox2.Visible = True
    End Sub





End Class