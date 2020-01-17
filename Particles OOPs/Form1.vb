Public Class Form1

    Public WindowWidth As Integer
    Public WindowHeight As Integer
    Public NumberofParticles As Integer
    Public ParticleArray As New List(Of Particles)(100)
    Public Particle As Particles


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WindowWidth = Me.Width - 17
        WindowHeight = Me.Height - 220
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Invalidate()
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        e.Graphics.DrawRectangle(Pens.Black, 0, 0, WindowWidth, WindowHeight)
        For Each Particle In ParticleArray
            If Particle.XCoord < (WindowWidth - Particle.Size) And (Particle.XCoord > 0) And Particle.YCoord < (WindowHeight - Particle.Size) And Particle.YCoord > 0 Then

                Particle.XCoord += Math.Cos(Particle.Bearing) * Particle.Velocity
                Particle.YCoord += Math.Sin(Particle.Bearing) * Particle.Velocity

                e.Graphics.FillEllipse(Brushes.Black, Convert.ToInt32(Particle.XCoord), Convert.ToInt32(Particle.YCoord), Particle.Size, Particle.Size)

                e.Graphics.DrawString(Math.Round((Particle.Bearing * (180 / Math.PI)), 1, MidpointRounding.AwayFromZero).ToString + "°", New Font("Tahoma", 7), Brushes.Red, New Point((Particle.XCoord + (Particle.Size / 2) - 9), (Particle.YCoord + (Particle.Size + 20))))
            End If
        Next
    End Sub

    Private Sub AddParticleButton_Click(sender As Object, e As EventArgs) Handles AddParticleButton.Click
        Particle = New Particles
    End Sub
End Class