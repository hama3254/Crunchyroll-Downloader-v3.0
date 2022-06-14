Option Strict On
Imports MetroFramework.Components

Public Class ErrorDialog

    Dim Manager As MetroStyleManager = Main.Manager


    Private Sub Reso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Manager.Owner = Me
        Me.StyleManager = Manager
        pictureBox3.Image = Main.CloseImg
        ComboBox1.Text = Nothing

        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        ComboBox1.Items.Clear()
        If Main.DialogTaskString = "Language" Then
            'CheckBox1.Visible = False
            StatusLabel.Text = Main.LabelLangNotFoundText
            Dim lang_avalibe As String() = Main.ResoNotFoundString.Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":"}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To lang_avalibe.Count - 1
                Dim langsplit As String() = lang_avalibe(i).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)

                ComboBox1.Items.Add(langsplit(0))
            Next
            SurroundingSub()
        ElseIf Main.DialogTaskString = "Language_CR_Beta" Then
            'CheckBox1.Visible = False
            StatusLabel.Text = Main.LabelLangNotFoundText

            Dim lang_avalibe As String() = Main.ResoNotFoundString.Split(New String() {"hardsub_locale" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            For i As Integer = 1 To lang_avalibe.Count - 1
                If CBool(InStr(lang_avalibe(i), "https://")) Then
                Else
                    Continue For
                End If
                If lang_avalibe(i).Substring(0, 1) = Chr(34) Then
                    ComboBox1.Items.Add("No Hardsubs")
                    Continue For 'Chr(34) +
                End If
                'MsgBox(lang_avalibe(i))
                Dim langsplit As String() = lang_avalibe(i).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries)
                ComboBox1.Items.Add(Main.HardSubValuesToDisplay(langsplit(0)))
            Next
            SurroundingSub()
        ElseIf Main.DialogTaskString = "Resolution" Then
            StatusLabel.Text = Main.LabelResoNotFoundText
            Dim Reso_avaible1 As String() = Main.ResoNotFoundString.Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To Reso_avaible1.Count - 1
                Dim Reso_avaible2 As String() = Reso_avaible1(i).Split(New [Char]() {Chr(44)})
                ComboBox1.Items.Add(Reso_avaible2(0))
            Next
            SurroundingSub()
            Try
                ComboBox1.SelectedIndex = 0
            Catch ex As Exception
            End Try

        ElseIf Main.DialogTaskString = "Funimation_Resolution" Then
            StatusLabel.Text = Main.LabelResoNotFoundText
            'Try
            '    My.Computer.FileSystem.WriteAllText(Application.StartupPath + "\Funimation-Resolution.log", Main.ResoNotFoundString, True)
            'Catch ex As Exception
            'End Try
            Dim ResoList As New List(Of String)
            Dim m3u8_split As String() = Main.ResoNotFoundString.Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To m3u8_split.Count - 1
                If CBool(InStr(m3u8_split(i), "RESOLUTION=")) Then
                    ResoList.Add(m3u8_split(i))
                End If
            Next

            Dim Reso_avaible1 As String() = Main.ResoNotFoundString.Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To ResoList.Count - 1
                Dim Reso_avaible As String() = ResoList.Item(i).Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)
                If CBool(InStr(Reso_avaible(1), ",")) Then
                    Dim Reso_avaible2 As String() = Reso_avaible(1).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                    ComboBox1.Items.Add(Reso_avaible2(0))
                Else
                    ComboBox1.Items.Add(Reso_avaible(1))
                End If
            Next
            SurroundingSub()
            Try
                ComboBox1.SelectedIndex = 0
            Catch ex As Exception
            End Try

        End If

    End Sub

    Private Sub SurroundingSub()
        Dim list As List(Of Object) = New List(Of Object)()

        For Each o As Object In ComboBox1.Items

            If Not list.Contains(o) Then
                list.Add(o)
            End If
        Next

        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(list.ToArray())
    End Sub


    Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ComboBox1.DrawItem

        ComboBox1.BackColor = Color.White
        If e.Index >= 0 Then
            Using st As New StringFormat With {.Alignment = StringAlignment.Center}
                ' e.DrawBackground()
                ' e.DrawFocusRectangle()
                e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds)
                e.Graphics.DrawString(ComboBox1.Items(e.Index).ToString, e.Font, Brushes.Black, e.Bounds, st)

            End Using
        End If

    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        If ComboBox1.SelectedItem.ToString = Nothing Then
        Else
            If Main.DialogTaskString = "Language_CR_Beta" Then
                Main.ResoBackString = DisplayToHardSubValues(ComboBox1.SelectedItem.ToString)
                Main.UserCloseDialog = False
                Me.Close()
            Else
                Main.ResoBackString = ComboBox1.SelectedItem.ToString
                Main.UserCloseDialog = False
                Me.Close()
            End If
        End If
    End Sub

    Public Function DisplayToHardSubValues(ByVal HardSub As String) As String
        Try
            If HardSub = "Deutsch" Then
                Return "de-DE"
            ElseIf HardSub = "English" Then
                Return "en-US"
            ElseIf HardSub = "Português (Brasil)" Then
                Return "pt-BR"
            ElseIf HardSub = "Español (LA)" Then
                Return "es-LA"
            ElseIf HardSub = "Français (France)" Then
                Return "fr-FR"
            ElseIf HardSub = "العربية (Arabic)" Then
                Return "ar-ME"
            ElseIf HardSub = "Русский (Russian)" Then
                Return "ru-RU"
            ElseIf HardSub = "Italiano (Italian)" Then
                Return "it-IT"
            ElseIf HardSub = "Español (España)" Then
                Return "es-ES"
            Else

                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub PictureBox9_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox9.MouseEnter
        PictureBox9.Image = My.Resources.DialogNotFound_Submit_hover
    End Sub

    Private Sub PictureBox9_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox9.MouseLeave
        PictureBox9.Image = My.Resources.DialogNotFound_Submit
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles pictureBox3.Click
        Main.UserCloseDialog = True
        Me.Close()
    End Sub

    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox3.MouseEnter

        pictureBox3.Image = My.Resources.main_del
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox3.MouseLeave

        pictureBox3.Image = Main.CloseImg
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        PictureBox9.Enabled = True
        PictureBox9.Cursor = Cursors.Hand
    End Sub
End Class