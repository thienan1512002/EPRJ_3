#pragma checksum "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a659b03c87fa791fa4bd673445b93654cf691732"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Home_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\_ViewImports.cshtml"
using Clinic_web_app;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\_ViewImports.cshtml"
using Clinic_web_app.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a659b03c87fa791fa4bd673445b93654cf691732", @"/Areas/Admin/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bc7f96fe8c344ee5a198c9e9381e083aa7b7862", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Clinic_web_app.Models.EcomerceOrder>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("animation__shake"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/img/logo1.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("KiviCareLogo"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("weight", new global::Microsoft.AspNetCore.Html.HtmlString("100"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Admin Index";
    Layout = "~/Areas/Admin/Views/Shared/_AdminLayout.cshtml";
    List<EcomerceMedOrderDetail> firstDayOrder = ViewBag.OrderToday;
    decimal totalToday = 0;
    decimal totalDay2 = 0;
    decimal totalDay3 = 0;
    decimal totalDay4 = 0;
    decimal totalDay5 = 0;
    decimal totalDay6 = 0;
    decimal totalDay7 = 0;
    int pending = 0;
    int decline = 0;
    int completed = 0;
    foreach (var item in Model)
    {
        switch (item.Status)
        {
            case "Pending":
                {
                    pending++;
                    break;
                }
            case "Decline":
                {
                    decline++;
                    break;
                }
            case "Completed":
                {
                    completed++;
                    break;
                }
        }
    }
    double pendingRate = (double)pending / (double)(pending + decline + completed) * 100;
    double declineRate = (double)decline / (double)(pending + decline + completed) * 100;
    double completedRated = (double)completed / (double)(pending + decline + completed) * 100;
    foreach (var item in firstDayOrder)
    {
        totalToday += Convert.ToDecimal(item.Total);
    }
    List<EcomerceMedOrderDetail> secondDayOrder = ViewBag.OrderDay2;
    foreach (var item in secondDayOrder)
    {
        totalDay2 += Convert.ToDecimal(item.Total);
    }
    List<EcomerceMedOrderDetail> orderDay3 = ViewBag.OrderDay3;
    foreach (var item in orderDay3)
    {
        totalDay3 += Convert.ToDecimal(item.Total);
    }
    List<EcomerceMedOrderDetail> orderDay4 = ViewBag.OrderDay4;
    foreach (var item in orderDay4)
    {
        totalDay4 += Convert.ToDecimal(item.Total);
    }
    List<EcomerceMedOrderDetail> orderDay5 = ViewBag.OrderDay5;
    foreach (var item in orderDay5)
    {
        totalDay5 += Convert.ToDecimal(item.Total);
    }
    List<EcomerceMedOrderDetail> orderDay6 = ViewBag.OrderDay6;
    foreach (var item in orderDay6)
    {
        totalDay6 += Convert.ToDecimal(item.Total);
    }
    List<EcomerceMedOrderDetail> orderDay7 = ViewBag.OrderDay7;
    foreach (var item in orderDay7)
    {
        totalDay7 += Convert.ToDecimal(item.Total);
    }
    decimal grandTotal = totalToday + totalDay2 + totalDay3 + totalDay4 + totalDay5 + totalDay6 + totalDay7;
    List<CustomerAccount> Register = ViewBag.Customer;
    List<StaffAccount> Staff = ViewBag.Staff;

#line default
#line hidden
#nullable disable
            WriteLiteral("<!-- Preloader -->\r\n<div class=\"preloader flex-column justify-content-center align-items-center\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "a659b03c87fa791fa4bd673445b93654cf6917327580", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
</div>
<!-- Content Wrapper. Contains page content -->
<div class=""content-wrapper"">
    <!-- Content Header (Page header) -->
    <div class=""content-header"">
        <div class=""container-fluid"">
            <div class=""row mb-2"">
                <div class=""col-sm-6"">
                    <h1 class=""m-0"">Dashboard</h1>
                </div><!-- /.col -->
                <div class=""col-sm-6"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item""><a href=""#"">Home</a></li>
                        <li class=""breadcrumb-item active"">Dashboard v1</li>
                    </ol>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <!-- Main content -->
    <section class=""content"">
        <div class=""container-fluid"">
            <!-- Small boxes (Stat box) -->
            <div class=""row"">
                <div class=""col-lg-3 c");
            WriteLiteral("ol-6\">\r\n                    <!-- small box -->\r\n                    <div class=\"small-box bg-info\">\r\n                        <div class=\"inner\">\r\n                            <h3>");
#nullable restore
#line 110 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                           Write(grandTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>

                            <p>Total income in 7 days</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-bag""></i>
                        </div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class=""col-lg-3 col-6"">
                    <!-- small box -->
                    <div class=""small-box bg-success"">
                        <div class=""inner"">
                            <h3>");
#nullable restore
#line 125 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                           Write(pendingRate);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<sup style=""font-size: 20px"">%</sup></h3>

                            <p>Pending Order Rate</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-stats-bars""></i>
                        </div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class=""col-lg-3 col-6"">
                    <!-- small box -->
                    <div class=""small-box bg-warning"">
                        <div class=""inner"">
                            <h3>");
#nullable restore
#line 140 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                           Write(Register.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>

                            <p>User Registrations</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-person-add""></i>
                        </div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class=""col-lg-3 col-6"">
                    <!-- small box -->
                    <div class=""small-box bg-danger"">
                        <div class=""inner"">
                            <h3>");
#nullable restore
#line 155 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                           Write(Staff.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>

                            <p>Staff Account availables</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-pie-graph""></i>
                        </div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <!-- ./col -->
            </div>
            <!-- /.row -->
            <!-- Main row -->
            <div class=""row"">
                <!-- Left col -->
                <section class=""col-lg-7 connectedSortable"">
                    <!-- Custom tabs (Charts with tabs)-->
                    <div class=""card"">
                        <div class=""card-header"">
                            <h3 class=""card-title"">
                                <i class=""fas fa-chart-pie mr-1""></i>
                                Sales
                            </h3>
                         ");
            WriteLiteral(@"   <div class=""card-tools"">
                                <ul class=""nav nav-pills ml-auto"">
                                    <li class=""nav-item"">
                                        <a class=""nav-link active"" href=""#revenue-chart"" data-toggle=""tab"">Incomes In 7 Days</a>
                                    </li>
                                </ul>
                            </div>
                        </div><!-- /.card-header -->
                        <script src=""https://canvasjs.com/assets/script/canvasjs.min.js""></script>
                        <div class=""card-body"">
                            <div class=""tab-content p-0"">
                                <div id=""chartContainer"" style=""height: 370px; width: 100%;""></div>
                            </div>
                        </div><!-- /.card-body -->
                    </div>
                    <!--chart income script-->
                    <script>
                        window.onload = function () {

      ");
            WriteLiteral(@"          var chart = new CanvasJS.Chart(""chartContainer"", {
                                exportEnabled: true,
                                animationEnabled: true,
                                theme: ""light2"",
                                title: {
                                    text: ""Income in 7 Days""
                                },
                                data: [{
                                    type: ""line"",
                                    indexLabel :""{y} $"",
                                    indexLabelFontSize: 16,
                                    dataPoints: [
                                        { x: 1, y: ");
#nullable restore
#line 210 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                              Write(totalToday);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ,label:\"Today\" },\r\n                                        { x: 2, y: ");
#nullable restore
#line 211 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                              Write(totalDay2);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ,label :\"Day 2\"},\r\n                                        { x: 3, y: ");
#nullable restore
#line 212 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                              Write(totalDay3);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ,label :\"Day 3\"},\r\n                                        { x: 4, y: ");
#nullable restore
#line 213 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                              Write(totalDay4);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ,label :\"Day 4\"},\r\n                                        { x: 5, y: ");
#nullable restore
#line 214 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                              Write(totalDay5);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ,label :\"Day 5\"},\r\n                                        { x: 6, y: ");
#nullable restore
#line 215 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                              Write(totalDay6);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ,label :\"Day 6\"},\r\n                                        { x: 7, y: ");
#nullable restore
#line 216 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                              Write(totalDay7);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" ,label :""Day 7""}
                                    ]
                                }]
                            });
                            chart.render();

                            var chartOrder = new CanvasJS.Chart(""chartOrder"", {
                                theme: ""light2"", // ""light1"", ""light2"", ""dark1"", ""dark2""
                                exportEnabled: true,
                                animationEnabled: true,

                                data: [{
                                    type: ""pie"",
                                    startAngle: 25,
                                    toolTipContent: ""<b>{label}</b>: {y}%"",
                                    showInLegend: ""true"",
                                    legendText: ""{label}"",
                                    indexLabelFontSize: 16,
                                    indexLabel: ""{label} - {y}%"",
                                    dataPoints: [
                                        { y: ");
#nullable restore
#line 236 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                        Write(pendingRate);

#line default
#line hidden
#nullable disable
            WriteLiteral(", label: \"Pending\" },\r\n                                        { y: ");
#nullable restore
#line 237 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                        Write(declineRate);

#line default
#line hidden
#nullable disable
            WriteLiteral(", label: \"Decline\" },\r\n                                        { y: ");
#nullable restore
#line 238 "D:\Workspace\GitHub\EPRJ_3\Source Code\admin\clinic_management_API\Clinic_web_app\Areas\Admin\Views\Home\Index.cshtml"
                                        Write(completedRated);

#line default
#line hidden
#nullable disable
            WriteLiteral(@", label: ""Completed"" }

                                    ]
                                }]
                            });
    chartOrder.render();
                        }

                    </script>
                    <!-- /.card -->
                    <!-- DIRECT CHAT -->
                    <div class=""card direct-chat direct-chat-primary"">
                        <div class=""card-header"">
                            <h3 class=""card-title"">Direct Chat</h3>


                        </div>
                        <!-- /.card-header -->
                        <div class=""card-body"">

                        </div>
                        <!-- /.card-body -->
                        <div class=""card-footer"">

                        </div>
                        <!-- /.card-footer-->
                    </div>
                    <!--/.direct-chat -->
                    <!-- TO DO List -->
                    <div class=""card"">
                        <div class=""");
            WriteLiteral(@"card-header"">
                            <h3 class=""card-title"">
                                <i class=""ion ion-clipboard mr-1""></i>
                                To Do List
                            </h3>


                        </div>
                        <!-- /.card-header -->
                        <div class=""card-body"">

                        </div>
                        <!-- /.card-body -->
                        <div class=""card-footer clearfix"">

                        </div>
                    </div>
                    <!-- /.card -->
                </section>
                <!-- /.Left col -->
                <!-- right col (We are only adding the ID to make the widgets sortable)-->
                <section class=""col-lg-5 connectedSortable"">

                    <!-- Map card -->
                    <div class=""card bg-gradient-primary"">
                        <div class=""card-header border-0"">
                            <h3 class=""card-title"">
");
            WriteLiteral(@"                                <i class=""fas fa-map-marker-alt mr-1""></i>
                                Orders Status Rate
                            </h3>

                        </div>
                        <div class=""card-body"">
                            <div id=""chartOrder"" style=""height: 370px; width: 100%;""></div>
                        </div>
                        <!-- /.card-body-->

                    </div>

                    <!-- /.card -->
                    <!-- solid sales graph -->
                    <div class=""card bg-gradient-info"">
                        <div class=""card-header border-0"">
                            <h3 class=""card-title"">
                                <i class=""fas fa-th mr-1""></i>
                                Sales Graph
                            </h3>

                            <div class=""card-tools"">
                                <button type=""button"" class=""btn bg-info btn-sm"" data-card-widget=""collapse"">
          ");
            WriteLiteral(@"                          <i class=""fas fa-minus""></i>
                                </button>
                                <button type=""button"" class=""btn bg-info btn-sm"" data-card-widget=""remove"">
                                    <i class=""fas fa-times""></i>
                                </button>
                            </div>
                        </div>
                        <div class=""card-body"">
                            <canvas class=""chart"" id=""line-chart"" style=""min-height: 250px; height: 250px; max-height: 250px; max-width: 100%;""></canvas>
                        </div>
                        <!-- /.card-body -->
                        <div class=""card-footer bg-transparent"">
                            <div class=""row"">
                                <div class=""col-4 text-center"">
                                    <input type=""text"" class=""knob"" data-readonly=""true"" value=""20"" data-width=""60"" data-height=""60""
                                           da");
            WriteLiteral(@"ta-fgColor=""#39CCCC"">

                                    <div class=""text-white"">Mail-Orders</div>
                                </div>
                                <!-- ./col -->
                                <div class=""col-4 text-center"">
                                    <input type=""text"" class=""knob"" data-readonly=""true"" value=""50"" data-width=""60"" data-height=""60""
                                           data-fgColor=""#39CCCC"">

                                    <div class=""text-white"">Online</div>
                                </div>
                                <!-- ./col -->
                                <div class=""col-4 text-center"">
                                    <input type=""text"" class=""knob"" data-readonly=""true"" value=""30"" data-width=""60"" data-height=""60""
                                           data-fgColor=""#39CCCC"">

                                    <div class=""text-white"">In-Store</div>
                                </div>
                ");
            WriteLiteral(@"                <!-- ./col -->
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.card-footer -->
                    </div>
                    <!-- /.card -->
                    <!-- Calendar -->
                    <div class=""card bg-gradient-success"">
                        <div class=""card-header border-0"">

                            <h3 class=""card-title"">
                                <i class=""far fa-calendar-alt""></i>
                                Calendar
                            </h3>
                            <!-- tools card -->
                            <div class=""card-tools"">
                                <!-- button with a dropdown -->
                                <div class=""btn-group"">
                                    <button type=""button"" class=""btn btn-success btn-sm dropdown-toggle"" data-toggle=""dropdown"" data-offset=""-52"">
                                  ");
            WriteLiteral(@"      <i class=""fas fa-bars""></i>
                                    </button>
                                    <div class=""dropdown-menu"" role=""menu"">
                                        <a href=""#"" class=""dropdown-item"">Add new event</a>
                                        <a href=""#"" class=""dropdown-item"">Clear events</a>
                                        <div class=""dropdown-divider""></div>
                                        <a href=""#"" class=""dropdown-item"">View calendar</a>
                                    </div>
                                </div>
                                <button type=""button"" class=""btn btn-success btn-sm"" data-card-widget=""collapse"">
                                    <i class=""fas fa-minus""></i>
                                </button>
                                <button type=""button"" class=""btn btn-success btn-sm"" data-card-widget=""remove"">
                                    <i class=""fas fa-times""></i>
                      ");
            WriteLiteral(@"          </button>
                            </div>
                            <!-- /. tools -->
                        </div>
                        <!-- /.card-header -->
                        <div class=""card-body pt-0"">
                            <!--The calendar -->
                            <div id=""calendar"" style=""width: 100%""></div>
                        </div>
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->
                </section>
                <!-- right col -->
            </div>
            <!-- /.row (main row) -->
        </div><!-- /.container-fluid -->
    </section>
    <!-- /.content -->
</div>

");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Clinic_web_app.Models.EcomerceOrder>> Html { get; private set; }
    }
}
#pragma warning restore 1591
