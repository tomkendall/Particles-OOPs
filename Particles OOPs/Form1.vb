Public Class Form1

    Public WindowWidth As Integer = Me.Width - 17
    Public WindowHeight As Integer = Me.Height - 220
    Public NumberofParticles As Integer
    Public ParticleArray As New List(Of Particles)(100)
    Public Particle As Particles

    Public BlackPen As New Pen(Color.Black, 1)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Console.WriteLine("hello")

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Invalidate()
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim WindowWidth As Integer = (Me.Width - 17)
        Dim WindowHeight As Integer = (Me.Height - 220)
        e.Graphics.DrawRectangle(BlackPen, 0, 0, WindowWidth, WindowHeight)
        For Each Particle In ParticleArray

        Next
    End Sub

    Private Sub AddParticleButton_Click(sender As Object, e As EventArgs) Handles AddParticleButton.Click
        Particle = New Particles
    End Sub
End Class