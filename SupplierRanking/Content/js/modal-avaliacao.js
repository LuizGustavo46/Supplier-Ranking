//$(document).ready(function () {
//    'use strict';

    var modal_avaliacao = document.getElementById('myModal2');

    // Get the button that opens the modal
    var btn = document.getElementsByClassName("myBtn");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close-avaliacao")[0];


    // When the user clicks on the button, open the modal
    function linkaCaixaContexto(id) {
        console.log(id);
        modal_avaliacao.style.display = "block";
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal_avaliacao.style.display = "none";
    }


    //window.onclick = function (event) {
    //    if (event.target == modal) {
    //        modal_avaliacao.style.display = "none";
    //    }
    //}
//});

