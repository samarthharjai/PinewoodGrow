/*
 *Adds google maps autocomplete to text input with id = "autocomplete"
 *
 * Outputs components lat lng to hidden fields with id="lat" and id "long"
 *
 * Outputs City value to id="city"
 * Outputs Postal Code to id="postal"
 * Outputs address name to hidden Field AddressName ****IMPORTANT**** dont use the auto complete input for address name use the formated one
 * Outputs PlaceID to Hidden Field *PlaceID*
 */




let autocomplete;

function initAutocomplete() {
    autocomplete = new google.maps.places.Autocomplete(
        document.getElementById('autocomplete'),
        {
            types: ['address'],
            componentRestrictions: { 'country': ['CA'] },
            fields: ['geometry', 'name', 'address_component', 'place_id']
        });
    autocomplete.addListener('place_changed', onPlaceChanged);
}


function onPlaceChanged() {
    var place = autocomplete.getPlace();
    if (!place.geometry) {
        document.getElementById('autocomplete').placeholder = 'Enter a place';
    } else {

     
        //Gets Address Full Name
        document.getElementById('AddressName').value = place.name;
        //Gets Postal Code      let placeID = place.place_id;

       document.getElementById('postal').value = place.address_components.find(a => a.types[0] === 'postal_code').long_name;
        //Gets City
 
       document.getElementById('city').value = place.address_components.find(a => a.types[0] === 'locality').long_name;
        // Gets Place ID
        document.getElementById('PlaceID').value = place.place_id;
        //lat

        document.getElementById('Lat').value = place.geometry.location.lat();
        document.getElementById('Lng').value = place.geometry.location.lng();
        //long
        //
        /*map.setCenter({ lat: place.geometry.location.lat(), lng: place.geometry.location.lng() });*/
    }
}
    