/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2015/8/21
* Description: Automated building by service@jucheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/
using System;
using System.Web.Optimization;

namespace JuCheap.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //重新处理bundle忽略资源的规则
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            #region JS
            
            //控制面板通用js
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/BaseScripts").Include(
                "~/Template/Admin/jucheap/js/jquery-1.10.2.min.js",
                "~/Template/Admin/jucheap/js/jquery-migrate.js",
                "~/Template/Admin/jucheap/js/bootstrap.min.js",
                "~/Template/Admin/jucheap/js/modernizr.min.js",
                "~/Template/Admin/jucheap/js/jquery.nicescroll.js",
                "~/Template/Admin/jucheap/js/slidebars.min.js",
                "~/Template/Admin/jucheap/js/switchery/switchery.min.js",
                "~/Template/Admin/jucheap/js/switchery/switchery-init.js",
                "~/Template/Admin/jucheap/js/sparkline/jquery.sparkline.js",
                "~/Template/Admin/jucheap/js/sparkline/sparkline-init.js",
                "~/Template/Admin/jucheap/js/jquery.validate.min.js",
                "~/Template/Admin/jucheap/js/json2.js",
                "~/Template/Admin/jucheap/js/scripts.js"));

            bundles.Add(new JuCheapScriptBundle("~/Template/Admin/jucheap/JS/Layer/BaseLayer").Include(
                "~/Template/Admin/jucheap/js/layer/layer.js"));

            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/JuCheap").Include(
                "~/Template/Admin/jucheap/js/jucheap.js"));

            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/JuCheapMenu").Include(
                "~/Template/Admin/jucheap/js/jucheap.menu.js"));

            

            //DataTable
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/DataTable").Include(
                "~/Template/Admin/jucheap/js/data-table/js/jquery.dataTables.min.js",
                "~/Template/Admin/jucheap/js/data-table/js/dataTables.tableTools.min.js",
                "~/Template/Admin/jucheap/js/data-table/js/bootstrap-dataTable.js",
                "~/Template/Admin/jucheap/js/data-table/js/dataTables.colVis.min.js",
                "~/Template/Admin/jucheap/js/data-table/js/dataTables.responsive.min.js",
                "~/Template/Admin/jucheap/js/data-table/js/dataTables.scroller.min.js",
                "~/Template/Admin/jucheap/js/jucheap.tables.js"));

            //DataTable Demo Page
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/DataTableDemo").Include(
                "~/Template/Admin/jucheap/js/data-table/js/jquery.dataTables.min.js",
                "~/Template/Admin/jucheap/js/data-table/js/dataTables.tableTools.min.js",
                "~/Template/Admin/jucheap/js/data-table/js/bootstrap-dataTable.js",
                "~/Template/Admin/jucheap/js/data-table/js/dataTables.colVis.min.js",
                "~/Template/Admin/jucheap/js/data-table/js/dataTables.responsive.min.js",
                "~/Template/Admin/jucheap/js/data-table/js/dataTables.scroller.min.js",
                "~/Template/Admin/jucheap/js/data-table-init.js"));

            //tree
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/Tree").Include(
                "~/Template/Admin/jucheap/js/fuelux/js/tree.min.js"));

            //login page
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/Login").Include(
                "~/Template/Admin/jucheap/js/jquery-1.10.2.min.js",
                "~/Template/Admin/jucheap/js/bootstrap.min.js", 
                "~/Template/Admin/jucheap/js/jquery.validate.min.js",
                "~/Template/Admin/jucheap/js/jucheap.login.valid.js"));

            //select2 js
            bundles.Add(new ScriptBundle("~/JS/Admin/jucheap/Select").Include(
                "~/Template/Admin/jucheap/js/select2.js",
                "~/Template/Admin/jucheap/js/select2-init.js"));

            //nesttable js
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/Nestable").Include(
                "~/Template/Admin/jucheap/js/nestable/jquery.nestable.js"));

            //tagsinput js
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/Tags").Include(
                "~/Template/Admin/jucheap/js/tags-input.js",
                "~/Template/Admin/jucheap/js/tags-input-init.js"));

            //fileinput js
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/File").Include(
                "~/Template/Admin/jucheap/js/bootstrap-fileinput-master/js/fileinput.js",
                "~/Template/Admin/jucheap/js/file-input-init.js"));

            //email js
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/Email").Include(
                "~/Template/Admin/jucheap/js/bootstrap-wysihtml5/wysihtml5-0.3.0.js",
                "~/Template/Admin/jucheap/js/bootstrap-wysihtml5/bootstrap-wysihtml5.js",
                "~/Template/Admin/jucheap/js/bootstrap-fileinput-master/js/fileinput.js",
                "~/Template/Admin/jucheap/js/file-input-init.js"));

            //editor js
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/Editor").Include(
                "~/Template/Admin/jucheap/js/bootstrap-wysihtml5/wysihtml5-0.3.0.js",
                "~/Template/Admin/jucheap/js/bootstrap-wysihtml5/bootstrap-wysihtml5.js",
                "~/Template/Admin/jucheap/js/summernote/dist/summernote.min.js"));

            //icheck js
            bundles.Add(new JuCheapScriptBundle("~/JS/Admin/jucheap/FormValidate").Include(
                "~/Template/Admin/jucheap/js/jquery.validate.min.js",
                "~/Template/Admin/jucheap/js/form-validation-init.js",
                "~/Template/Admin/jucheap/js/icheck/skins/icheck.min.js"));

            //Advance demo js
            bundles.Add(new ScriptBundle("~/JS/Admin/jucheap/Advance").Include(
                "~/Template/Admin/jucheap/js/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/Template/Admin/jucheap/js/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js",
                "~/Template/Admin/jucheap/js/bootstrap-daterangepicker/moment.min.js",
                "~/Template/Admin/jucheap/js/bootstrap-daterangepicker/daterangepicker.js",
                "~/Template/Admin/jucheap/js/bootstrap-colorpicker/js/bootstrap-colorpicker.js",
                "~/Template/Admin/jucheap/js/bootstrap-timepicker/js/bootstrap-timepicker.js",
                "~/Template/Admin/jucheap/js/picker-init.js",
                "~/Template/Admin/jucheap/js/icheck/skins/icheck.min.js",
                "~/Template/Admin/jucheap/js/icheck-init.js",
                "~/Template/Admin/jucheap/js/tags-input.js",
                "~/Template/Admin/jucheap/js/tags-input-init.js",
                "~/Template/Admin/jucheap/js/touchspin.js",
                "~/Template/Admin/jucheap/js/spinner-init.js",
                "~/Template/Admin/jucheap/js/dropzone.js",
                "~/Template/Admin/jucheap/js/select2.js",
                "~/Template/Admin/jucheap/js/select2-init.js"));

            //flot chart demo
            bundles.Add(new ScriptBundle("~/JS/Admin/jucheap/Chart").Include(
                "~/Template/admin/jucheap/js/sparkline/jquery.sparkline.js",
                "~/Template/Admin/jucheap/js/sparkline/sparkline-init.js",
                "~/Template/Admin/jucheap/js/flot-chart/jquery.flot.js",
                "~/Template/Admin/jucheap/js/flot-chart/jquery.flot.resize.js",
                "~/Template/Admin/jucheap/js/flot-chart/jquery.flot.tooltip.min.js",
                "~/Template/Admin/jucheap/js/flot-chart/jquery.flot.pie.js",
                "~/Template/Admin/jucheap/js/flot-chart/jquery.flot.selection.js",
                "~/Template/Admin/jucheap/js/flot-chart/jquery.flot.selection.js",
                "~/Template/Admin/jucheap/js/flot-chart/jquery.flot.stack.js",
                "~/Template/Admin/jucheap/js/flot-chart/jquery.flot.crosshair.js",
                "~/Template/Admin/jucheap/js/flot-chart-init.js"));

            //morris chart demo
            bundles.Add(new ScriptBundle("~/JS/Admin/jucheap/ChartMorris").Include(
                "~/Template/admin/jucheap/js/morris-chart/morris.js",
                "~/Template/Admin/jucheap/js/morris-chart/raphael-min.js",
                "~/Template/Admin/jucheap/js/morris-init.js"));

            //chartjs demo
            bundles.Add(new ScriptBundle("~/JS/Admin/jucheap/ChartJs").Include(
                "~/Template/admin/jucheap/js/chart-js/chart.js",
                "~/Template/Admin/jucheap/js/chartJs-init.js"));

            bundles.Add(new JuCheapScriptBundle("~/JS/Front/Web/Home").Include(
                "~/Template/front/web/js/jquery.js",
                "~/Template/front/web/js/bootstrap.min.js",
                "~/Template/front/web/js/jquery.prettyPhoto.js",
                "~/Template/front/web/js/jquery.isotope.min.js",
                "~/Template/front/web/js/main.js",
                "~/Template/front/web/js/wow.min.js"));

            #endregion

            #region CSS

            //Base Styles
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/BaseStyles").Include(
                "~/Template/Admin/jucheap/css/style.css",
                "~/Template/Admin/jucheap/css/style-responsive.css"));

            //Data Table
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/DataTable").Include(
                "~/Template/Admin/jucheap/js/data-table/css/jquery.dataTables.css",
                "~/Template/Admin/jucheap/js/data-table/css/dataTables.tableTools.css",
                "~/Template/Admin/jucheap/js/data-table/css/dataTables.colVis.min.css",
                "~/Template/Admin/jucheap/js/data-table/css/dataTables.responsive.css",
                "~/Template/Admin/jucheap/js/data-table/css/dataTables.scroller.css"));

            //tree
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/TreeStyle").Include(
                "~/Template/Admin/jucheap/js/fuelux/css/tree-style.css"));

            //select2
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/SelectStyle").Include(
                "~/Template/Admin/jucheap/css/select2.css",
                "~/Template/Admin/jucheap/css/select2-bootstrap.css"));

            //Nestable
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/Nestable").Include(
                "~/Template/Admin/jucheap/js/nestable/jquery.nestable.css"));
            //Tagsinput
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/Tags").Include(
                "~/Template/Admin/jucheap/css/tagsinput.css"));

            //FileInput
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/File").Include(
                "~/Template/Admin/jucheap/js/bootstrap-fileinput-master/css/fileinput.css"));

            //Email
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/Email").Include(
                "~/Template/Admin/jucheap/js/bootstrap-wysihtml5/bootstrap-wysihtml5.css",
                "~/Template/Admin/jucheap/js/bootstrap-fileinput-master/css/fileinput.css"));

            //Editor
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/Editor").Include(
                "~/Template/Admin/jucheap/js/summernote/dist/summernote.css",
                "~/Template/Admin/jucheap/js/bootstrap-wysihtml5/bootstrap-wysihtml5.css"));

            //icheck Demo
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/FormValidate").Include(
                "~/Template/admin/jucheap/js/icheck/skins/all.css"));

            //morris chart Demo
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/ChartMorris").Include(
                "~/Template/admin/jucheap/js/morris-chart/morris.css"));

            //Advance Demo
            bundles.Add(new StyleBundle("~/Template/Admin/jucheap/Css/Advance").Include(
                "~/Template/admin/jucheap/js/icheck/skins/all.css",
                "~/Template/admin/jucheap/css/tagsinput.css",
                //"~/Template/admin/jucheap/css/dropzone.css",
                "~/Template/admin/jucheap/css/select2.css",
                "~/Template/admin/jucheap/css/select2-bootstrap.css",
                "~/Template/admin/jucheap/css/bootstrap-touchspin.css",
                "~/Template/admin/jucheap/js/bootstrap-datepicker/css/datepicker.css",
                "~/Template/admin/jucheap/js/bootstrap-timepicker/compiled/timepicker.css",
                "~/Template/admin/jucheap/js/bootstrap-colorpicker/css/colorpicker.css",
                "~/Template/admin/jucheap/js/bootstrap-daterangepicker/daterangepicker-bs3.css",
                "~/Template/admin/jucheap/js/bootstrap-datetimepicker/css/datetimepicker.css"));



            //front web/index
            bundles.Add(new StyleBundle("~/css/front/web/home").Include(
                "~/Template/front/web/css/bootstrap.min.css",
                "~/Template/front/web/css/font-awesome.min.css",
                "~/Template/front/web/css/animate.min.css",
                "~/Template/front/web/css/prettyPhoto.css",
                "~/Template/front/web/css/main.css",
                "~/Template/front/web/css/responsive.css"));

            #endregion

            //BundleTable.EnableOptimizations = true;强制启用压缩
        }
        /// <summary>
        /// 添加bundle需要忽略的表达式的资源
        /// </summary>
        /// <param name="ignoreList"></param>
        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException(nameof(ignoreList));

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}