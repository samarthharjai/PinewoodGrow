#pragma checksum "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3df494b86a1e4dbd30cea6dca85b6d7b49f73ec1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MapReport_Index), @"mvc.1.0.view", @"/Views/MapReport/Index.cshtml")]
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
#line 1 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\_ViewImports.cshtml"
using PinewoodGrow;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\_ViewImports.cshtml"
using PinewoodGrow.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3df494b86a1e4dbd30cea6dca85b6d7b49f73ec1", @"/Views/MapReport/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d40aafa2a956cb9f4837a45d08862015ed55438c", @"/Views/_ViewImports.cshtml")]
    public class Views_MapReport_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 3 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<h1 id=""test"">Maps</h1>

<div style=""margin: 22px"">
    <div class=""form-inline d-flex justify-content-center md-form form-sm mt-0"">
        <i class=""fas fa-search"" aria-hidden=""true""></i>
        <input class=""form-control form-control-sm ml-3 w-75"" type=""text"" placeholder=""Search""
               aria-label=""Search"" id=""autocomplete"">
    </div>
</div>
<input type=""button"" value=""Hide Purple"" onclick=""filter()""/>
<div id=""map"" style=""height: 900px; width: 100%""></div>

<script>
    let autocomplete;
    var MarkersArr = [];

    function initMap() {
 

        autocomplete = new google.maps.places.Autocomplete(
            document.getElementById('autocomplete'),
            {
                types: ['address'],
                componentRestrictions: { 'country': ['CA'] },
                fields: ['geometry', 'name', 'address_component']
            });
        autocomplete.addListener('place_changed', onPlaceChanged);

        // The location of Grow
        const Grow = { ");
            WriteLiteral(@"lat: 43.1103481, lng: -79.0789613 };
        // The map, centered at Grow
        const map = new window.google.maps.Map(document.getElementById(""map""),
            {
                zoom: 16,
                center: Grow,
                mapId: 'db31debbd2731710'
            });

        const MainMarker = new google.maps.Marker({
            position: Grow,
            map: map,
            title: 'Grow',
            icon: 'https://localhost:44339/images/GrowMapIcon.png'
        });


     

");
#nullable restore
#line 56 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml"
          
            List<PinewoodGrow.ViewModels.MapMarker> mapMarkers = ViewBag.Markers;
            foreach (var mapMarker in mapMarkers)
            {

                

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                var marker = new window.google.maps.Marker({\r\n                        map: map,\r\n                        category: \'");
#nullable restore
#line 64 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml"
                              Write(mapMarker.Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n                        position: { lat: ");
#nullable restore
#line 65 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml"
                                    Write(mapMarker.Lat);

#line default
#line hidden
#nullable disable
            WriteLiteral(", lng: ");
#nullable restore
#line 65 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml"
                                                         Write(mapMarker.Lng);

#line default
#line hidden
#nullable disable
            WriteLiteral(" },\r\n                        title: \'");
#nullable restore
#line 66 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml"
                           Write(mapMarker.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n                        icon: \'");
#nullable restore
#line 67 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml"
                          Write(mapMarker.Color);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                        /*icon: 'https://maps.gstatic.com/mapfiles/place_api/icons/v1/png_71/geocode-71.png',
                        icon_background_color: '#7B9EB0',
                        icon_mask_base_uri: ""https://maps.gstatic.com/mapfiles/place_api/icons/v2/generic_pinlet""*/
                    });
                MarkersArr.push(marker);
                ");
#nullable restore
#line 73 "C:\Users\Hann_\Documents\NC\W22\PROG 1440\PinewoodGrow\Views\MapReport\Index.cshtml"
                       

            }

            


        

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

        

        function onPlaceChanged() {
            var place = autocomplete.getPlace();
            if (!place.geometry) {
                document.getElementById('autocomplete').placeholder = 'Enter a place';
            } else {
   
                //Centers the map around searched locaiton
                console.log(place);
                map.setCenter({ lat: place.geometry.location.lat(), lng: place.geometry.location.lng() });
                console.log(place.address_components);

                //Gets Address Full Name
                console.log(place.name);
                //Gets Postal Code
                console.log(place.address_components.find(a => a.types[0] === 'postal_code').long_name);
                //Gets City
                console.log(place.address_components.find(a => a.types[0] === 'locality').long_name);
            }
        }
    }

    function filter() {
       
        for (var i = 0; i < MarkersArr.length; i++) {
            console.log(M");
            WriteLiteral(@"arkersArr[i].category);
            if (MarkersArr[i].category === ""Mid"") {
                MarkersArr[i].setMap(null);
                console.log(""Hidden"");
                }
            else
                MarkersArr[i].setMap(map);
        
        }
    }
</script>
<script

        src=""https://maps.googleapis.com/maps/api/js?key=AIzaSyBL-MHoHXLeE8E2WJKgnX60Rq03qo9EYxU&libraries=places&callback=initMap"" async defer>
</script>


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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
