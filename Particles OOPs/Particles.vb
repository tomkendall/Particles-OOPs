Public Class Particles
    Public XCoord As Double
    Public YCoord As Double
    Public Velocity As Double
    Public Bearing As Double
    Public Size As Integer
    Public Mass As Double
    Public Sub New()
        Form1.NumberofParticles += 1

        Me.Velocity = 1 'CInt(Math.Ceiling(Rnd() * 5))

        Me.Bearing = Rnd() * (2 * Math.PI)

        Me.Size = 25

        XCoord = CInt(Math.Ceiling(Rnd() * (Form1.WindowWidth - 220)))
        YCoord = CInt(Math.Ceiling(Rnd() * (Form1.WindowHeight - 17)))
        Form1.ParticleArray.Add(Me)
    End Sub
End Class
