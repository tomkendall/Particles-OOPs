Public Class Particles
    Public XCoord As Double
    Public YCoord As Double
    Public Velocity As Double
    Public Bearing As Double
    Public Size As Double
    Public Mass As Integer
    Public Sub New()
        Form1.NumberofParticles += 1

        Velocity = 1 'CInt(Math.Ceiling(Rnd() * 5))

        Bearing = Rnd() * (2 * Math.PI)

        YCoord = CInt(Math.Ceiling(Rnd() * (Form1.WindowWidth - Size)))
        XCoord = CInt(Math.Ceiling(Rnd() * (Form1.WindowHeight - Size)))
    End Sub
End Class
