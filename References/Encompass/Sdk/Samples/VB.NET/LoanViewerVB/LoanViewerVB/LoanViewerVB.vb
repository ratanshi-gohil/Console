Imports EllieMae.Encompass.AsmResolver
Imports EllieMae.Encompass.Runtime
Imports EllieMae.Encompass.Client

Module LoanViewerVB

    ' The Entry point for the application
    Public Sub Main()
        Dim EncRuntimeSvcs As New EllieMae.Encompass.Runtime.RuntimeServices()
        EncRuntimeSvcs.Initialize()
        StartApplication()
    End Sub

    Private Sub StartApplication()
        Dim currentLoginForm As LoginForm = Nothing
        currentLoginForm = New LoginForm()
        Dim res As DialogResult = currentLoginForm.ShowDialog()

        If res = DialogResult.Cancel Then
            Return
        End If

        Dim currentMonitor As MonitorWindow = Nothing
        currentMonitor = New MonitorWindow()
        Application.Run(currentMonitor)
        Globals.Session.End()

    End Sub





End Module
