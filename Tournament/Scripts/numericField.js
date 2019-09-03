var elements = document.getElementsByClassName('number');

for (var i = 0; i < elements.length; i++) {
    elements[i].onkeypress = onlyNumbers;
}

function onlyNumbers(event) {
    // from 48 to 57 is key codes in ascii tables
    if (event.keyCode < 48 || event.keyCode > 57) {
        console.log('not a number');

        // block all that is not between 48 and 57
        return false;
    }
}