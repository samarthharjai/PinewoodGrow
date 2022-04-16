//Reusable refresh routine for SelectLists
//Parameters: ddl_ID - ID of the Select to refresh
//  uri - /controller/action/... to call to get a new SelcectList as JSON
// showNoDataMsg - Boolean if you want to clear all options and show NO DATA message
//      if no data is returned.  Otherwise it leaves the original values in place.
//  addDefault - Boolean indicating if you want to add a default/prompt 
//      option at the top of the list.
//  defaultText - string value to display as the default/prompt value
// fadeOutIn - Boolean for visual effect after refresh
function refreshCheckbox(ddl_ID, URL, showNoDataMsg, noDataMsg, addDefault, defaultText, fadeOutIn) {
    var theDDL = $("#" + ddl_ID);
    $(function () {
        $.getJSON(URL, function (data) {
            if (data !== null && !jQuery.isEmptyObject(data)) {
                theDDL.empty();
                $.each(data, function (index, item) {
               
                    var div = $('<div>');

                    var row = $('<label>');
                    row.append($('<input>', {
                        type: 'checkbox',
                        value: item.id,
                        name: item.name,
                        checked: item.assigned
                    }));

                    var span = $('<span>');
                    span.append(item.displayText);

                    row.append(span);
                    div.append(row);

                
                    theDDL.append(div);

                });
                theDDL.trigger("chosen:updated");
            } else {
                if (showNoDataMsg) {
                    theDDL.empty();
                    if (noDataMsg == null || jQuery.isEmptyObject(noDataMsg)) {
                        noDataMsg = 'No Matching Data'
                    };
                    theDDL.append($('<option/>', {
                        value: null,
                        text: noDataMsg
                    }));
                    theDDL.trigger("chosen:updated");
                }
            }
        });
    });
    if (fadeOutIn) {
        theDDL.fadeToggle(400, function () {
            theDDL.fadeToggle(400);
        });
    }
    return;
}