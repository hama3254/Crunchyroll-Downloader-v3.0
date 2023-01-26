Option Strict On
Imports MetroFramework
Imports MetroFramework.Components

Public Class ffmpeg_options
    Public Property command As String

    Dim Manager As MetroStyleManager = Main.Manager

#Region "UI stuff"




    Private Sub Btn_ok_MouseEnter(sender As Object, e As EventArgs) Handles btn_ok.MouseEnter
        btn_ok.BackgroundImage = My.Resources.ffmpeg_OK_cL_hover
    End Sub

    Private Sub Btn_ok_MouseLeave(sender As Object, e As EventArgs) Handles btn_ok.MouseLeave
        btn_ok.BackgroundImage = My.Resources.ffmpeg_OK_cL
    End Sub

    Private Sub Btn_cl_MouseEnter(sender As Object, e As EventArgs) Handles btn_cl.MouseEnter
        btn_cl.BackgroundImage = My.Resources.ffmpeg_OK_cL_hover
    End Sub

    Private Sub Btn_cl_MouseLeave(sender As Object, e As EventArgs) Handles btn_cl.MouseLeave
        btn_cl.BackgroundImage = My.Resources.ffmpeg_OK_cL
    End Sub

#End Region

    Private Sub options_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Manager.Owner = Me
        Me.StyleManager = Manager
        TB_cmd.Text = command

    End Sub

    Private Sub btn_cl_Click(sender As Object, e As EventArgs) Handles btn_cl.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
        command = TB_cmd.Text
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class