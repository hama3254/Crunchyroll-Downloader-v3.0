Option Strict On

Imports System.ComponentModel
Imports MetroFramework
Imports MetroFramework.Components
Imports MetroFramework.Controls
Imports MetroFramework.Drawing

Public Class CheckBoxComboBox

    Dim Manager As New MetroStyleManager
    Dim Dubs As New List(Of MetroCheckBox)
    Dim Subs As New List(Of MetroCheckBox)
    Private Sub Reso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Manager.Owner = Me
        Me.StyleManager = Manager

        Me.Height = 15
        Animation.Enabled = True

        Me.StyleManager.Style = MetroColorStyle.Orange

        If Me.Text = "CR Dub selection" Then

            'CB_CR_Audio.Items.Clear()

            For i As Integer = 1 To Main.LangValueEnum.Count - 1 ' index 0 = 'null' | last index = jp

                Dim Dub As New MetroCheckBox
                Dub.Text = Main.LangValueEnum(i).Name
                Dub.FontSize = MetroCheckBoxSize.SomethingInBetween
                Dub.TextAlign = ContentAlignment.MiddleCenter
                Dub.SetBounds(2, 30 * Dubs.Count + 1, 316, 25)
                Dub.UseCustomBackColor = True
                Dub.UseCustomForeColor = True
                Dub.ForeColor = Color.Black

                AddHandler Dub.MouseEnter, AddressOf ItemMouseEnter
                AddHandler Dub.MouseLeave, AddressOf ItemMouseLeave

                If Main.LangValueEnum(i).CR_Value = Main.DubSprache.CR_Value Then
                    Dub.Checked = True
                End If

                Dubs.Add(Dub)
                Me.Controls.Add(Dub)
            Next

        ElseIf Me.Text = "CR Sub selection" Then

            For i As Integer = 1 To Main.LangValueEnum.Count - 2 ' index 0 = 'null' | last index = jp

                Dim SubT As New MetroCheckBox
                SubT.Text = Main.LangValueEnum(i).Name
                SubT.Name = Main.LangValueEnum(i).CR_Value
                SubT.FontSize = MetroCheckBoxSize.SomethingInBetween
                SubT.TextAlign = ContentAlignment.MiddleCenter
                SubT.SetBounds(2, 30 * Subs.Count + 1, 316, 25)
                SubT.UseCustomBackColor = True
                SubT.UseCustomForeColor = True
                SubT.ForeColor = Color.Black

                AddHandler SubT.MouseEnter, AddressOf ItemMouseEnter
                AddHandler SubT.MouseLeave, AddressOf ItemMouseLeave
                AddHandler SubT.CheckedChanged, AddressOf ItemCheckedChanged

                If Einstellungen.CR_SoftSubsTemp.Contains(Main.LangValueEnum(i).CR_Value) Then
                    SubT.Checked = True
                End If

                Subs.Add(SubT)
                Me.Controls.Add(SubT)
            Next


        End If




    End Sub

    Private Sub ItemCheckedChanged(sender As Object, e As EventArgs)

        Dim Box As MetroCheckBox = CType(sender, MetroCheckBox)

        If Box.Checked = True Then
            If Einstellungen.CR_SoftSubDefault.Items.Contains(Box.Text) Then
            Else
                Einstellungen.CR_SoftSubDefault.Items.Add(Box.Text)
            End If
        Else
            If Einstellungen.CR_SoftSubDefault.Items.Contains(Box.Text) And Einstellungen.CR_SoftSubDefault.SelectedItem.ToString = Box.Text Then
                Einstellungen.CR_SoftSubDefault.Items.Remove(Box.Text)
                Einstellungen.CR_SoftSubDefault.SelectedIndex = 0
            ElseIf Einstellungen.CR_SoftSubDefault.Items.Contains(Box.Text) Then
                Einstellungen.CR_SoftSubDefault.Items.Remove(Box.Text)
            End If
        End If


    End Sub
    Private Sub ItemMouseEnter(sender As Object, e As EventArgs)
        DirectCast(sender, MetroCheckBox).BackColor = Color.FromArgb(&HFFDEDEDE)
    End Sub

    Private Sub ItemMouseLeave(sender As Object, e As EventArgs)
        DirectCast(sender, MetroCheckBox).BackColor = Me.BackColor
    End Sub

    Private Sub CheckBoxComboBox_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        If Me.Text = "CR Dub selection" Then
            Me.Location = New Point(Einstellungen.Location.X + 116, Einstellungen.Location.Y + 204)
        ElseIf Me.Text = "CR Sub selection" Then
            Me.Location = New Point(Einstellungen.Location.X + 116, Einstellungen.Location.Y + 344)
        End If


    End Sub

    Private Sub CheckBoxComboBox_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub

    Private Sub CheckBoxComboBox_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

#Region "sof subs"


        Einstellungen.CR_SoftSubsTemp.Clear()

        For i As Integer = 0 To Subs.Count - 1
            Dim Box As MetroCheckBox = Subs(i)

            If Box.Checked = True Then
                Einstellungen.CR_SoftSubsTemp.Add(Box.Name)
            End If

        Next



#End Region
    End Sub

    Private Sub Animation_Tick(sender As Object, e As EventArgs) Handles Animation.Tick
        If Me.Text = "CR Dub selection" Then
            If Me.Height < 300 Then
                Me.Height = Me.Height + 30
            Else
                Me.Height = 300
                Animation.Enabled = False
            End If


        ElseIf Me.Text = "CR Sub selection" Then
            If Me.Height < 270 Then
                Me.Height = Me.Height + 30
            Else
                Me.Height = 270
                Animation.Enabled = False
            End If

        End If
    End Sub


End Class