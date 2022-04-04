
function refreshUnitPrice(txt_ID, URL) {

    const theTxt = $("#" + txt_ID);


    $(function () {
        $.getJSON(URL, function (data) {
            if (data !== null && !jQuery.isEmptyObject(data)) {
                console.log(data);
                theTxt.value = data;
                theTxt.trigger("chosen:updated");
           
            } else {
                console.log("in else");
                console.log(data);
                console.log(theTxt);
                theTxt.val(data);
                theTxt.trigger("chosen:updated");
            }
        });
    });



    /*$(function () {
        $.getJSON(URL, function (data) {
            if (data !== null && !jQuery.isEmptyObject(data)) {
                theDDL.empty();
                if (addDefault) {
                    if (defaultText == null || jQuery.isEmptyObject(defaultText)) {
                        defaultText = 'Select'
                    };
                    theDDL.append($('<option/>', {
                        value: "",
                        text: defaultText
                    }));
                }
                $.each(data, function (index, item) {
                    theDDL.append($('<option/>', {
                        value: item.value,
                        text: item.text,
                        selected: item.selected,
                        disabled: item.disabled
                    }));

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
    });*/

    return;
}