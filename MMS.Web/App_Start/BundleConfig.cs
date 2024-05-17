using System.Web;
using System.Web.Optimization;

namespace MMS.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {


            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));                      

            bundles.Add(new ScriptBundle("~/bundles/DatePicker").Include(
                       "~/Datepicker/js/jquery-1.10.2.js",
                       "~/Datepicker/js/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap-3.3.6.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/tableSorter").Include(
                       "~/Scripts/jquery.tablesorter.min.js",
                       "~/Scripts/jquery.tablesorter.pager.js"));

            bundles.Add(new StyleBundle("~/Content/MMS/css").Include("~/Scripts/PopUp/Css/jquery-ui.css",
             "~/Scripts/MultiSelect_/css/jquery.multiselect.css",
             "~/Datepicker/css/ui-lightness/jquery-ui-1.8.19.custom.css",
             "~/jquery-ui-timepicker-0.3.3/jquery.ui.timepicker.css",
             "~/Content/css/bootstrap.min.css",
             "~/Content/css/FormStyle.css",
             "~/css/responsive.css",
             "~/css/jquery.mCustomScrollbar.css",
             "~/Scripts/DatePicker/jquery-ui-timepicker-addon.css",
           //  "~/code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css",
             "~/resources/demos/style.css",
             "~/Datepicker/css/ui-lightness/jquery-ui-1.8.19.custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/BuyerMaster").Include(

                        
                            "~/Scripts/EasyAutocomplete-1.3.5/ComboBox.js"));

            bundles.Add(new StyleBundle("~/Content/BuyerMastercss").Include("~/code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css",
                                        "~/resources/demos/style.css",
                                        "~/Scripts/EasyAutocomplete-1.3.5/comboStyles.css"));

            bundles.Add(new ScriptBundle("~/bundles/MultiplIssue").Include(
                      "~/Scripts/EasyAutocomplete-1.3.5/ComboBox.js",
                      "~/Scripts/EasyAutocomplete-1.3.5/AutoComplete%20v1.10.2.js",
                      "~/Scripts/EasyAutocomplete-1.3.5/AutoComplete%20v1.11.4.js",
                      "~/Scripts/AutoCompleteCommon.js",
                       "~/Scripts/EasyAutocomplete-1.3.5/ComboBox.js",
                      "~/Scripts/MultiSelect_/jquery.multiselect.js",
                      "~/Scripts/MultiSelect_/jquery.multiselect.min.js",
                      "~/Scripts/PopUp/Js/jquery-1.9.1.js",
                      "~/Scripts/PopUp/Js/jquery-ui.js",
                      "~/Scripts/DatePicker/jquery-ui-timepicker-addon.js",
                      "~/jquery-ui-timepicker-0.3.3/jquery.ui.timepicker.js",
                      //"https://code.jquery.com/jquery-1.12.4.js",
                      //"https://code.jquery.com/ui/1.12.1/jquery-ui.js",
                      "~/jquery-ui-timepicker-0.3.3/jquery.ui.timepicker.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/Bomcss").Include(
              "~/Content/css/bootstrap.min.css",
              "~/Content/css/FormStyle.css",
              "~/Scripts/EasyAutocomplete-1.3.5/comboStyles.css",
               "~/Scripts/EasyAutocomplete-1.3.5/Autocomplete.css",
              "~/css/responsive.css",
              "~/css/jquery.mCustomScrollbar.css",
              "~/code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css",
              "~/resources/demos/style.css",

              "~/jquery-ui-timepicker-0.3.3/jquery.ui.timepicker.css",
              "~/Datepicker/css/ui-lightness/jquery-ui-1.8.19.custom.css"
              ));
            bundles.Add(new ScriptBundle("~/bundles/Bom").Include(
                "~/Scripts/jquery-{version}.js",
                 "~/Scripts/jquery-ui-{version}.js",
            "~/Scripts/EasyAutocomplete-1.3.5/AutoComplete%20v1.10.2.js",
                      "~/Scripts/EasyAutocomplete-1.3.5/AutoComplete%20v1.11.4.js",
                      "~/Scripts/AutoCompleteCommon.js",
                        "~/Scripts/EasyAutocomplete-1.3.5/ComboBox.js",
               
                   
                      "~/jquery-ui-timepicker-0.3.3/jquery.ui.timepicker.js",
                       "~/Scripts/BomScripts/bom.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/GateEntryScripts").Include(
                  "~/Scripts/jquery-{version}.js",
                 "~/Scripts/jquery-ui-{version}.js",
"~/jquery-ui-timepicker-0.3.3/include/jquery-1.9.0.min.js",

"~/jquery-ui-timepicker-0.3.3/include/ui-1.10.0/jquery.ui.core.min.js",
"~/jquery-ui-timepicker-0.3.3/include/ui-1.10.0/jquery.ui.position.min.js",
"~/jquery-ui-timepicker-0.3.3/include/ui-1.10.0/jquery.ui.tabs.min.js",
"~/jquery-ui-timepicker-0.3.3/include/ui-1.10.0/jquery.ui.widget.min.js",                      
                      "~/jquery-ui-timepicker-0.3.3/jquery.ui.timepicker.js"));

            bundles.Add(new StyleBundle("~/Content/GateEntrycss").Include("~/jquery-ui-timepicker-0.3.3/include/ui-1.10.0/ui-lightness/jquery-ui-1.10.0.custom.min.css"));

          // Use the development version of Modernizr to develop with and learn from. Then, when you're
          // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
          bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css","~/Datepicker/css/ui-lightness/jquery-ui-1.8.19.custom.css"));
        }
    }
}