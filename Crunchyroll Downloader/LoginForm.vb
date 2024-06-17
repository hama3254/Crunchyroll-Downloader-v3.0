Option Strict On
Imports Crunchyroll_Downloader.CRD_Classes
Imports MetroFramework.Components
Imports System.Net.WebUtility

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

        If My.Settings.Mail = "na" Then
        Else
            Mail.Text = My.Settings.Mail
            Save.Checked = True
        End If
        If My.Settings.PW = "na" Then
        Else
            PW.Text = My.Settings.PW
            'PW.UseSystemPasswordChar = False
            PW.PasswordChar = Nothing
        End If
        DisplayTB.TextAlign = HorizontalAlignment.Center
    End Sub



    'Private Sub Submit_MouseEnter(sender As Object, e As EventArgs)
    '    Btn_Save.Image = My.Resources.DialogNotFound_Submit_hover
    'End Sub

    'Private Sub Submit_MouseLeave(sender As Object, e As EventArgs)
    '    Btn_Save.Image = My.Resources.DialogNotFound_Submit
    'End Sub


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

    Private Sub btn_ok_Click(sender As Object, e As EventArgs) Handles btn_ok.Click
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

    Private Sub UserTest_Click(sender As Object, e As EventArgs) Handles UserTest.Click

        Dim Loc_CR_Cookies As String = "" ' do we need that? does not seem like it.

        Dim Auth As String = " -H " + Chr(34) + "Authorization: " + Main.CrBetaBasic + Chr(34)

        Dim Post As String = " -d " + Chr(34) + "username=" + UrlEncode(Mail.Text) + "&password=" + UrlEncode(PW.Text) + "&grant_type=password&scope=offline_access" + Chr(34) + " -X POST -H " + Chr(34) + "Content-Type: application/x-www-form-urlencoded; charset=utf-8" + Chr(34)

        Dim v1Token As String = CurlPost("https://beta-api.crunchyroll.com/auth/v1/token", Loc_CR_Cookies, Auth, Post, "add_main_4494")
        'MsgBox(v1Token)
        If CBool(InStr(v1Token, "HTTP Status: 401")) = True Then
            MsgBox("CR reported :" + vbNewLine + v1Token, MsgBoxStyle.Exclamation, "CR-Error 401")
            Exit Sub
        End If

        If CBool(InStr(v1Token, "HTTP Status: 400")) = True Then
            MsgBox("CR reported :" + vbNewLine + v1Token, MsgBoxStyle.Exclamation, "CR-Error 400")
            Exit Sub
        End If

        Dim Token() As String = v1Token.Split(New String() {Chr(34) + "access_token" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim Token2() As String = Token(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

        Dim CRBetaBearer As String = "Bearer "
        CRBetaBearer = CRBetaBearer + Token2(0)

        Dim Auth2 As String = " -H " + Chr(34) + "Authorization: " + CRBetaBearer + Chr(34)


        Dim AccoutID() As String = v1Token.Split(New String() {Chr(34) + "account_id" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim AccoutID2() As String = AccoutID(1).Split(New String() {Chr(34) + "," + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

        Dim SubsV3 As String = "https://www.crunchyroll.com/subs/v3/subscriptions/" + AccoutID2(0)
        Try
            Dim Test As String = CurlAuthNew(SubsV3, Loc_CR_Cookies, Auth2)
            Error_msg.ShowErrorDia(Test, "Login Ok - Premium Ok", "None")
        Catch ex As Exception
            Error_msg.ShowErrorDia(ex.ToString, "Login Ok - Premium check failed", "None")
        End Try

    End Sub
End Class