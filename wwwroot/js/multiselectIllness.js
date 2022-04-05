let DDLforChosenIllness = document.getElementById("selectedIllnessOptions");
let DDLforAvailIllness = document.getElementById("availIllnessOptions");

/*function to switch list items from one ddl to another
use the sender param for the DDL from which the user is multi-selecting
use the receiver param for the DDL that gets the options*/
function switchIllnessOptions(event, senderDDL, receiverDDL) {
    //find all selected option tags - selectedIllnessOptions becomes a nodelist
    let senderID = senderDDL.id;
    let selectedIllnessOptions = document.querySelectorAll(`#${senderID} option:checked`);
    event.preventDefault();

    if (selectedIllnessOptions.length === 0) {
        //alert("Nothing to move.");
    }
    else {
        selectedIllnessOptions.forEach(function (o, idx) {
            senderDDL.remove(o.index);
            receiverDDL.appendChild(o);
        });
    }
}
//create closures so that we can access the event & the 2 parameters
let addIllnessOptions = (event) => switchIllnessOptions(event, DDLforAvailIllness, DDLforChosenIllness);
let removeIllnessOptions = (event) => switchIllnessOptions(event, DDLforChosenIllness, DDLforAvailIllness);
//assign the closures as the event handlers for each button
document.getElementById("btnIllnessLeft").addEventListener("click", addIllnessOptions);
document.getElementById("btnIllnessRight").addEventListener("click", removeIllnessOptions);

document.getElementById("btnIllnessSubmit").addEventListener("click", function () {
    DDLforChosenIllness.childNodes.forEach(opt => opt.selected = "selected");
})