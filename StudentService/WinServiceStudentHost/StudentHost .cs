using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinServiceStudentHost
{
    public partial class StudentHost : ServiceBase
    {
        ServiceHost studentServiceHost = null;

        public StudentHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //Base Address for StudentService
                Uri httpBaseAddress = new Uri("http://localhost:4321/StudentService");
                studentServiceHost = new ServiceHost(typeof(StudentService.StudentService), httpBaseAddress);
                //Add Endpoint to Host
                studentServiceHost.AddServiceEndpoint(typeof(StudentService.IStudentService),new WSHttpBinding(), "");
                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                studentServiceHost.Description.Behaviors.Add(serviceBehavior);
                //Open
                studentServiceHost.Open();
            }
            catch (Exception ex)
            {
                studentServiceHost = null;
            }
        }

        protected override void OnStop()
        {
            if (studentServiceHost != null)
            {
                studentServiceHost.Close();
                studentServiceHost = null;
            }
        }
    }
}
