using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensys.Reports.Model
{
    public class ReportFactory

    {

        protected static Queue reportQueue = new Queue();

        protected static int iMaxCount = 4;

        protected static ReportDocument CreateReport(Type reportClass)

        {

            object report = Activator.CreateInstance(reportClass);

            reportQueue.Enqueue(report);

            return (ReportDocument)report;

        }

        public static ReportDocument GetReport(Type reportClass)

        {

            if (reportQueue.Count > iMaxCount)

            {

                ((ReportDocument)reportQueue.Dequeue()).Close();

                ((ReportDocument)reportQueue.Dequeue()).Dispose();

                GC.Collect();

            }

            return CreateReport(reportClass);

        }

    }
}