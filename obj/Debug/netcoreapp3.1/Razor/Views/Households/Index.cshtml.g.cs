#pragma checksum "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "46d6748a722c3c7bef7102a4b27f5587d3bf42d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Households_Index), @"mvc.1.0.view", @"/Views/Households/Index.cshtml")]
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
#line 1 "C:\Users\Owner\Desktop\PinewoodGrow\Views\_ViewImports.cshtml"
using PinewoodGrow;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Owner\Desktop\PinewoodGrow\Views\_ViewImports.cshtml"
using PinewoodGrow.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46d6748a722c3c7bef7102a4b27f5587d3bf42d1", @"/Views/Households/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d40aafa2a956cb9f4837a45d08862015ed55438c", @"/Views/_ViewImports.cshtml")]
    public class Views_Households_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<PinewoodGrow.Models.Household>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-info right float-right"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-primary ml-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
   ViewData["Title"] = "Index"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n<h1 style=\"text-align: center\">Household List</h1>\r\n\r\n<p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "46d6748a722c3c7bef7102a4b27f5587d3bf42d15377", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </p>\r\n\r\n<style>\r\n    table {\r\n        margin: 0 auto;\r\n    }\r\n</style>\r\n\r\n<br />\r\n<table class=\"table table-striped\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.HouseIncome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 28 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.LICO));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                Member\r\n            </th>\r\n            <th>\r\n                Family Size\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 40 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 44 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 47 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.HouseIncome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 50 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.LICO));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n");
#nullable restore
#line 53 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                  
                    int memberCount = item.Members.Count;
                    if (memberCount > 0)
                    {
                        string firstMember = item.Members.FirstOrDefault().FullName;
                        if (memberCount > 1)
                        {
                            string memberList = "";
                            var c = item.Members.ToList();
                            for (int i = 1; i < memberCount; i++)
                            {
                                memberList += c[i].FullName + " <br />";
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <a");
            BeginWriteAttribute("class", " class=\"", 1903, "\"", 1911, 0);
            EndWriteAttribute();
            WriteLiteral(" role=\"button\" data-toggle=\"collapse\"");
            BeginWriteAttribute("href", " href=\"", 1949, "\"", 1982, 2);
            WriteAttributeValue("", 1956, "#collapseAthlete", 1956, 16, true);
#nullable restore
#line 66 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
WriteAttributeValue("", 1972, item.ID, 1972, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("\r\n                               aria-expanded=\"false\"");
            BeginWriteAttribute("aria-controls", " aria-controls=\"", 2037, "\"", 2078, 2);
            WriteAttributeValue("", 2053, "collapseAthlete", 2053, 15, true);
#nullable restore
#line 67 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
WriteAttributeValue("", 2068, item.ID, 2068, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                ");
#nullable restore
#line 68 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                           Write(firstMember);

#line default
#line hidden
#nullable disable
            WriteLiteral("... <span class=\"badge badge-info\">");
#nullable restore
#line 68 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                                                                          Write(memberCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                            </a>\r\n                            <div class=\"collapse\"");
            BeginWriteAttribute("id", " id=\"", 2265, "\"", 2295, 2);
            WriteAttributeValue("", 2270, "collapseAthlete", 2270, 15, true);
#nullable restore
#line 70 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
WriteAttributeValue("", 2285, item.ID, 2285, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                ");
#nullable restore
#line 71 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                           Write(Html.Raw(memberList));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n");
#nullable restore
#line 73 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                        }
                        else
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                       Write(firstMember);

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                                        
                        }
                    }
                

#line default
#line hidden
#nullable disable
            WriteLiteral("            </td>\r\n");
            WriteLiteral("            <td>\r\n                ");
#nullable restore
#line 85 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Members.Count));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                <div class=\"btn-group\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "46d6748a722c3c7bef7102a4b27f5587d3bf42d113602", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 89 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                                           WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "46d6748a722c3c7bef7102a4b27f5587d3bf42d115843", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 90 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
                                              WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("               \r\n                </div>\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 94 "C:\Users\Owner\Desktop\PinewoodGrow\Views\Households\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<PinewoodGrow.Models.Household>> Html { get; private set; }
    }
}
#pragma warning restore 1591
