Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class Form1
    Function grabx(str As String)
        If TextBox1.Text.Contains("https") Then
            Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create(str), HttpWebRequest)
            httpWebRequest.Proxy = Nothing
            Using streamReader As StreamReader = New StreamReader(httpWebRequest.GetResponse().GetResponseStream())
                Dim prompt As String = streamReader.ReadToEnd()
                TextBox3.Text = prompt
                regx(TextBox3.Text)
            End Using
        Else
            Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://quiz2020.com/quiz/" + str), HttpWebRequest)
            httpWebRequest.Proxy = Nothing
            Using streamReader As StreamReader = New StreamReader(httpWebRequest.GetResponse().GetResponseStream())
                Dim prompt As String = streamReader.ReadToEnd()
                TextBox3.Text = prompt
                regx(TextBox3.Text)
            End Using
        End If


    End Function
    Dim ix As Integer
    Function regx(str As String)
        Dim r As New Regex("<td value='(.*?)' class=""answer center correct"">")
        Dim matchess As MatchCollection = r.Matches(TextBox3.Text)
        If (matchess.ToString IsNot "") Then
            For Each itemcode As Match In matchess

                ix += 1
                TextBox2.AppendText(ix & "- " & itemcode.Groups(1).Value + vbCrLf)



                Label1.Text = ix

            Next
            ix = 0
        Else
            MsgBox("sd")
        End If

    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox2.Clear()

        grabx(TextBox1.Text)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        End
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        WindowState = FormWindowState.Minimized

    End Sub

    Private Sub Label2_MouseDown(sender As Object, e As MouseEventArgs) Handles Label2.MouseDown
        Dim flag As Boolean = e.Button = MouseButtons.Left
        If flag Then
            ' The following expression was wrapped in a checked-expression
            Me.mouseOffset = New Point(0 - e.X, 0 - e.Y)
            Me.isMouseDown = True
        End If
    End Sub
    Private isMouseDown As Boolean
    Private mouseOffset As Point
    Private Sub Label2_MouseMove(sender As Object, e As MouseEventArgs) Handles Label2.MouseMove
        Dim flag As Boolean = Me.isMouseDown
        If flag Then
            Dim mousePosition As Point = Control.MousePosition
            mousePosition.Offset(Me.mouseOffset.X, Me.mouseOffset.Y)
            Me.Location = mousePosition
        End If
    End Sub

    Private Sub Label2_MouseUp(sender As Object, e As MouseEventArgs) Handles Label2.MouseUp
        Dim flag As Boolean = e.Button = MouseButtons.Left
        If flag Then
            Me.isMouseDown = False
        End If
    End Sub
End Class
