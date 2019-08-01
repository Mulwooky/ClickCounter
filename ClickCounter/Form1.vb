Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System

Public Class Form1
    Private WithEvents MouseDetector As MouseDetector
    Dim count As Integer = 0
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim ontop As Boolean = True
    Dim mycount As String = "0"
    Dim mycountright As String = "0"
    Dim distcount As String = "0"
    Dim mouseform As New Mouse()
    Dim congrats As New congrats()
    Dim topper As Boolean
    Dim g = 255
    Dim b = 255
    Dim OldX As Int32 = 0
    Dim intoldX As Int32 = 0
    Dim OldY As Int32 = 0
    Dim intoldY As Int32 = 0
    Dim NewX As Int32 = 0
    Dim intnewX As Int32 = 0
    Dim NewY As Int32 = 0
    Dim intnewY As Int32 = 0
    Dim distance As Int32 = 0
    Dim DPI As Integer = 0
    Dim miles As Double = 0

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MouseDetector = New MouseDetector
        Me.TopMost = ontop
        If (File.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\count.txt")) Then
            mycount = System.IO.File.ReadAllText(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\count.txt")
            If mycount = "" Then
                Label1.Text = 0
            Else
                Label1.Text = mycount
            End If
        Else
            Using fs As FileStream = File.Create(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\count.txt")
                Dim wibble As Byte() = New UTF8Encoding(True).GetBytes("0")
                fs.Write(wibble, 0, wibble.Length)
            End Using
            Label1.Text = "0"
        End If

        If (File.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\rightcount.txt")) Then
            mycountright = System.IO.File.ReadAllText(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\rightcount.txt")
            If mycountright = "" Then
                RightLabel.Text = 0
            Else
                RightLabel.Text = mycountright
            End If
        Else
            Using fs As FileStream = File.Create(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\rightcount.txt")
                Dim wibble As Byte() = New UTF8Encoding(True).GetBytes("0")
                fs.Write(wibble, 0, wibble.Length)
            End Using
            RightLabel.Text = "0"
        End If

        If (File.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\distance.txt")) Then
            distcount = System.IO.File.ReadAllText(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\distance.txt")
            If distcount = "" Then
                Distancetext.Text = "Distance: 0 cm"
            Else
                distance = distcount
                Distancetext.Text = "Distance: " + distcount.ToString() + " cm"
            End If
        Else
            Using fs As FileStream = File.Create(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\distance.txt")
                Dim wibble As Byte() = New UTF8Encoding(True).GetBytes("0")
                fs.Write(wibble, 0, wibble.Length)
            End Using
            Distancetext.Text = "0"
        End If

        Label1.ForeColor = Color.FromArgb(255, 255, 255)
        RightLabel.ForeColor = Color.FromArgb(255, 255, 255)

        Using myGraphics As Graphics = Me.CreateGraphics()
            DPI = myGraphics.DpiX
        End Using

        OldX = MouseXposition()
        OldY = MouseYposition()
        OldX = Math.Abs(OldX)
        OldY = Math.Abs(OldY)
        Timer2.Start()
    End Sub
    Private Sub Form1_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        RemoveHandler MouseDetector.MouseLeftButtonClick, AddressOf MouseDetector_MouseLeftButtonClick
        RemoveHandler MouseDetector.MouseRightButtonClick, AddressOf MouseDetector_MouseRightButtonClick
        MouseDetector.Dispose()
        Label1.BackColor = Color.Transparent
        RightLabel.BackColor = Color.Transparent
        Resetlbl.BackColor = Color.Transparent
    End Sub
    Private Sub Label1_MouseDown1(sender As Object, e As MouseEventArgs)
        drag = True
        mousex = Windows.Forms.Cursor.Position.X - Me.Left
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub
    Private Sub Label1_MouseMove(sender As Object, e As MouseEventArgs)
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub
    Private Sub Label1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        drag = False
    End Sub
    Private Sub MouseDetector_MouseLeftButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MouseDetector.MouseLeftButtonClick
        If IsNumeric(Label1.Text) Then
            Label1.Text = Label1.Text + 1
            If Label1.Text > 1000 Then
                If g > 0.25 Then
                    g = g - 0.25
                    b = b - 0.25
                    If g = CInt(g) Then
                        Label1.ForeColor = Color.FromArgb(255, g, b)
                    End If
                Else
                    Label1.ForeColor = Color.FromArgb(255, 0, 0)
                End If
            End If
        Else
            Distancetext.Text = 1
        End If
    End Sub

    Private Sub MouseDetector_MouseRightButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MouseDetector.MouseRightButtonClick
        If IsNumeric(RightLabel.Text) Then
            RightLabel.Text = RightLabel.Text + 1
            If RightLabel.Text > 1000 Then
                If g > 0.25 Then
                    g = g - 0.25
                    b = b - 0.25
                    If g = CInt(g) Then
                        RightLabel.ForeColor = Color.FromArgb(255, g, b)
                    End If
                Else
                    RightLabel.ForeColor = Color.FromArgb(255, 0, 0)
                End If
            End If
        Else
            RightLabel.Text = 1
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)
        Application.ExitThread()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim w As New StreamWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\count.txt")
        w.WriteLine(Label1.Text.ToString())
        w.Close()
        Dim w2 As New StreamWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\rightcount.txt")
        w2.WriteLine(RightLabel.Text.ToString())
        w2.Close()
        Dim w3 As New StreamWriter(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\distance.txt")
        w3.WriteLine(distance.ToString())
        w3.Close()
    End Sub

    Private Sub Resetlbl_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        mouseform.Dispose()
        congrats.Dispose()
        Timer1.Stop()
        Timer1.Enabled = False
        If Top = True Then
            Me.TopMost = True
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        NewX = MouseXposition()
        NewY = MouseYposition()
        NewX = Math.Abs(NewX)
        NewY = Math.Abs(NewY)
        distance = distance + (2.54 * (Math.Sqrt(((NewX - OldX) ^ 2) + (((NewY - OldY) ^ 2)))) / DPI)
        If distance > 160934 Then
            Distancetext.Text = "Distance: " + (Format(Val(distance / 160934), "0.000")).ToString + " miles"
        ElseIf distance > 100 And distance < 160933 Then
            distancetext.Text = "Distance: " + (distance / 100).ToString + " m"
        Else
            distancetext.Text = "Distance: " + distance.ToString() + " cm"
        End If
        OldX = NewX
        OldY = NewY
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.ExitThread()
    End Sub

    Private Sub Label3_Click_1(sender As Object, e As EventArgs)
        If ontop = True Then
            Me.TopMost = False
            ontop = False
            RightLabel.Text = "Unpinned"
        Else
            Me.TopMost = True
            ontop = True
            RightLabel.Text = "Pinned"
        End If
    End Sub

    Private Sub Resetlbl_Click_1(sender As Object, e As EventArgs) Handles Resetlbl.Click
        Label1.Text = "0"
        RightLabel.Text = "0"
        Distancetext.Text = "Distance: 0 cm"
        distance = 0
        System.IO.File.Delete(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\count.txt")
        Using fs As FileStream = File.Create(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\count.txt")
            Dim wibble As Byte() = New UTF8Encoding(True).GetBytes("0")
            fs.Write(wibble, 0, wibble.Length)
        End Using
        System.IO.File.Delete(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\rightcount.txt")
        Using fs As FileStream = File.Create(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\rightcount.txt")
            Dim wibble As Byte() = New UTF8Encoding(True).GetBytes("0")
            fs.Write(wibble, 0, wibble.Length)
        End Using
        System.IO.File.Delete(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\distance.txt")
        Using fs As FileStream = File.Create(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\distance.txt")
            Dim wibble As Byte() = New UTF8Encoding(True).GetBytes("0")
            fs.Write(wibble, 0, wibble.Length)
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ontop = True Then
            Me.TopMost = False
            ontop = False
            Button1.Text = "Unpinned"
        Else
            Me.TopMost = True
            ontop = True
            Button1.Text = "Pinned"
        End If
    End Sub
End Class