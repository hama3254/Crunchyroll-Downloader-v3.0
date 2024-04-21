Option Strict On
Imports Crunchyroll_Downloader.CRD_Classes
Imports MetroFramework.Components

Public Class LoginForm

    Dim Manager As MetroStyleManager = Main.Manager


    Private Sub Reso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.TopMost = True
        Manager.Owner = Me
        Me.StyleManager = Manager


        Me.StartPosition = 0
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        'MsgBox(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2).ToString)
    End Sub

    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click
        If PW.Text = "Password" Or Mail.Text = "E-Mail" Then
            MsgBox("Invalid Input", MsgBoxStyle.Information)
            Exit Sub
        ElseIf Save.Checked = True Then
            My.Settings.Mail = Mail.Text
            My.Settings.PW = PW.Text
        End If
        Main.PW = PW.Text
        Main.Mail = Mail.Text
        Me.Close()
    End Sub

    Private Sub Submit_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Save.MouseEnter
        Btn_Save.Image = My.Resources.DialogNotFound_Submit_hover
    End Sub

    Private Sub Submit_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Save.MouseLeave
        Btn_Save.Image = My.Resources.DialogNotFound_Submit
    End Sub


    Private Sub MetroLink1_Click(sender As Object, e As EventArgs) Handles IssueLink.Click
        Process.Start("https://github.com/hama3254/Crunchyroll-Downloader-v3.0/issues/938#issuecomment-2067383212")
    End Sub

    Private Sub PW_Click(sender As Object, e As EventArgs) Handles PW.Click
        If PW.Text = "Password" Then
            PW.Text = Nothing
        End If
    End Sub

    Private Sub Mail_Click(sender As Object, e As EventArgs) Handles Mail.Click
        If Mail.Text = "E-Mail" Then
            Mail.Text = Nothing
        End If
    End Sub


End Class