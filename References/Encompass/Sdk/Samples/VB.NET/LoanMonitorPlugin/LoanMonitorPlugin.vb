Imports EllieMae.Encompass.Automation
Imports EllieMae.Encompass.ComponentModel

'The primary plugin class for the Loan Monitor App
<Plugin()> _
Public Class LoanMonitorPlugin

    'The current display window
    Private currentMonitor As MonitorWindow = Nothing

    'The public constructor for the plugin. All plugins must have a public, parameterless
    'constructor. In the constructor you should subscribe to the events you wish to
    'handle within Encompass.
    Public Sub New()
        AddHandler EncompassApplication.LoanOpened, New EventHandler(AddressOf Application_LoanOpened)
        AddHandler EncompassApplication.LoanClosing, New EventHandler(AddressOf Application_LoanClosing)
    End Sub

    'Event handler for when a loan is opened
    Private Sub Application_LoanOpened(ByVal sender As Object, ByVal e As EventArgs)
        currentMonitor = New MonitorWindow()
        currentMonitor.Show()
    End Sub

    'Event handler for when a loan is closing
    Private Sub Application_LoanClosing(ByVal sender As Object, ByVal e As EventArgs)

        If Not currentMonitor Is Nothing Then
            currentMonitor.Close()
            currentMonitor = Nothing
        End If

    End Sub

End Class
