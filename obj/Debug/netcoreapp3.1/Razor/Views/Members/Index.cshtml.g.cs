#pragma checksum "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cd84a976be7b00753e5441825864dfd98e30f3ec"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Members_Index), @"mvc.1.0.view", @"/Views/Members/Index.cshtml")]
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
#line 1 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\_ViewImports.cshtml"
using PinewoodGrow;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\_ViewImports.cshtml"
using PinewoodGrow.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd84a976be7b00753e5441825864dfd98e30f3ec", @"/Views/Members/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"508f55dd4c0b38a666d6e99b674b2dae790b2d4f", @"/Views/_ViewImports.cshtml")]
    public class Views_Members_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<PinewoodGrow.Models.Member>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-info right float-right"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-primary ml-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
  
	ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\n<h1 style=\"text-align: center\">Member List</h1>\n\n<style>\n\ttable {\n\t\tmargin: 0 auto;\n\t}\n</style>\n\n<p>\n\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cd84a976be7b00753e5441825864dfd98e30f3ec6890", async() => {
                WriteLiteral("Create Member");
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
            WriteLiteral("\n</p>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cd84a976be7b00753e5441825864dfd98e30f3ec8139", async() => {
                WriteLiteral("\n\t<input type=\"hidden\" name=\"sortDirection\"");
                BeginWriteAttribute("value", " value=\"", 365, "\"", 399, 1);
#nullable restore
#line 19 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
WriteAttributeValue("", 373, ViewData["sortDirection"], 373, 26, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\n    <input type=\"hidden\" name=\"sortField\"");
                BeginWriteAttribute("value", " value=\"", 445, "\"", 475, 1);
#nullable restore
#line 20 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
WriteAttributeValue("", 453, ViewData["sortField"], 453, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
    <div class=""form-horizontal"">
        <br />
        <button class=""btn btn-outline-secondary"" type=""button"" data-toggle=""collapse"" id=""filterToggle"" data-target=""#collapseFilter"" aria-expanded=""false"" aria-controls=""collapseFilter"">
            Filter/Search
        </button>
        <div");
                BeginWriteAttribute("class", " class=\"", 774, "\"", 815, 2);
                WriteAttributeValue("", 782, "collapse", 782, 8, true);
#nullable restore
#line 26 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
WriteAttributeValue(" ", 790, ViewData["Filtering"], 791, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" id=""collapseFilter"">
            <div class=""card card-body bg-light"">
                <div class=""row"">
                    <div class=""form-group col-md-4"">
                        <label class=""control-label"">Filter by Dietary Restriction:</label>
                        ");
#nullable restore
#line 31 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
                   Write(Html.DropDownList("DietaryID", null, "None", htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    </div>\n                    <div class=\"form-group col-md-6\">\n                        <label class=\"control-label\">Search Patient Name:</label>\n                        ");
#nullable restore
#line 35 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
                   Write(Html.TextBox("SearchString", null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    </div>\n\n                </div>\n                <div class=\"row\">\n                    <div class=\"form-group col-md-4\">\n                        <label class=\"control-label\">Filter by Living Situation:</label>\n                        ");
#nullable restore
#line 42 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
                   Write(Html.DropDownList("SituationID", null, "None", htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    </div>\n                    <div class=\"form-group col-md-4 align-self-end\">\n                        <input type=\"submit\" name=\"actionButton\" value=\"Filter\" class=\"btn btn-outline-primary\" />\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cd84a976be7b00753e5441825864dfd98e30f3ec12251", async() => {
                    WriteLiteral("Clear");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
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
                </div>
            </div>
        </div>
    </div>
	<table class=""table table-striped"">
		<thead>
			<tr>
				<th>
					<input type=""submit"" name=""actionButton"" value=""Member"" class=""btn btn-link"" />
				</th>
				<th>
					<input type=""submit"" name=""actionButton"" value=""Age"" class=""btn btn-link"" />
				</th>
				<th>
					<input type=""submit"" disabled=""disabled"" value=""Telephone"" class=""btn btn-link"" />
				</th>
				<th>
					<input type=""submit"" disabled=""disabled"" value=""Email"" class=""btn btn-link"" />
				</th>
				<th>
					<input type=""submit"" name=""actionButton"" value=""Family Size"" class=""btn btn-link"" />
				</th>
				<th>
					<input type=""submit"" name=""actionButton"" value=""Income"" class=""btn btn-link"" />
				</th>
	            <th>
		            <input type=""submit"" disabled=""disabled"" value=""Gender"" class=""btn btn-link"" />
	            </th>
				<th></th>
			</tr>
		</thead>
		<tbody>
");
#nullable restore
#line 80 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
             foreach (var item in Model)
			{

#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr>\n                <td>\n                    ");
#nullable restore
#line 84 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.FullName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 87 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Age));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 90 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.TelephoneFormatted));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 93 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 96 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.FamilySize));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 99 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Income));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </td>\n                \n                <td>\n                    ");
#nullable restore
#line 103 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Gender.Name));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </td>\n                <td>\n                    <div class=\"btn-group\">\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cd84a976be7b00753e5441825864dfd98e30f3ec17644", async() => {
                    WriteLiteral("Edit");
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
#line 107 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
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
                WriteLiteral("\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cd84a976be7b00753e5441825864dfd98e30f3ec20054", async() => {
                    WriteLiteral("Details");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 108 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
                                                  WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
                WriteLiteral("                    </div>\n\n                </td>\n            </tr>\n");
#nullable restore
#line 114 "C:\Users\Samarth\Documents\Developer\Niagara College\Community Sponsored Project\PinewoodGrow-v.1.00\Views\Members\Index.cshtml"
			}

#line default
#line hidden
#nullable disable
                WriteLiteral("\t\t</tbody>\n\t</table>\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n\t<script type=\"text/javascript\">\n        $(function () {\n            $(\'[data-toggle=\"popover\"]\').popover();\n        });\n\t</script>\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<PinewoodGrow.Models.Member>> Html { get; private set; }
    }
}
#pragma warning restore 1591
